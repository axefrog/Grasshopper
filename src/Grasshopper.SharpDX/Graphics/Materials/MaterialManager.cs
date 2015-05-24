using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Materials;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Grasshopper.SharpDX.Graphics.Materials
{
	public class MaterialManager : IMaterialManager
	{
		private readonly DeviceManager _deviceManager;
		private readonly Dictionary<string, CompiledMaterial> _materials = new Dictionary<string, CompiledMaterial>();

		public MaterialManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		public bool Exists(string id)
		{
			return _materials.ContainsKey(id);
		}

		public void Add(MaterialSpec spec)
		{
			var material = new CompiledMaterial();
			PrepareVertexShader(spec.VertexShader, material);
			PreparePixelShader(spec.PixelShader, material);
			_materials.Add(spec.Id, material);
		}

		public void SetActive(string id)
		{
			CompiledMaterial material;
			if(!_materials.TryGetValue(id, out material))
				throw new ArgumentException("The specified material id is not registered. Did you forget to add it?");
			_deviceManager.Context.InputAssembler.InputLayout = material.InputLayout;
			_deviceManager.Context.VertexShader.Set(material.VertexShader);
			_deviceManager.Context.PixelShader.Set(material.PixelShader);
		}

		public void Remove(string id)
		{
			CompiledMaterial material;
			if(_materials.TryGetValue(id, out material))
			{
				material.Dispose();
				_materials.Remove(id);
			}
		}

		private void PrepareVertexShader(VertexShaderSpec spec, CompiledMaterial material)
		{
			ShaderSignature signature;
			using(var bytecode = ShaderBytecode.Compile(spec.Source, "VSMain", "vs_5_0"))
			{
				signature = ShaderSignature.GetInputSignature(bytecode);
				material.VertexShader = new VertexShader(_deviceManager.Device, bytecode);
			}
			var elements = InputElementsFromSpec(spec);
			material.InputLayout = new InputLayout(_deviceManager.Device, signature, elements);
		}

		private void PreparePixelShader(ShaderSpec spec, CompiledMaterial material)
		{
			using(var bytecode = ShaderBytecode.Compile(spec.Source, "PSMain", "ps_5_0"))
				material.PixelShader = new PixelShader(_deviceManager.Device, bytecode);
		}

		private static readonly Dictionary<ShaderInputElementPurpose, string> _semantics = new Dictionary<ShaderInputElementPurpose, string>
		{
			{ ShaderInputElementPurpose.Position, "POSITION" },
			{ ShaderInputElementPurpose.Color, "COLOR" },
			{ ShaderInputElementPurpose.TextureCoordinate, "TEXCOORD" },
			{ ShaderInputElementPurpose.Normal, "NORMAL" },
			{ ShaderInputElementPurpose.Custom, "CUSTOM" },
		};

		private static readonly Dictionary<ShaderInputElementFormat, Format> _formats = new Dictionary<ShaderInputElementFormat, Format>
		{
			{ ShaderInputElementFormat.Float, Format.R32_Float },
			{ ShaderInputElementFormat.Float2, Format.R32G32_Float },
			{ ShaderInputElementFormat.Float3, Format.R32G32B32_Float },
			{ ShaderInputElementFormat.Float4, Format.R32G32B32A32_Float },
			{ ShaderInputElementFormat.Int32, Format.R32_SInt },
			{ ShaderInputElementFormat.UInt32, Format.R32_UInt },
			{ ShaderInputElementFormat.Matrix4x4, Format.R32G32B32A32_Float },
		};

		private static readonly Dictionary<ShaderInputElementFormat, int> _elementSizes = new Dictionary<ShaderInputElementFormat, int>
		{
			{ ShaderInputElementFormat.Float, 4 },
			{ ShaderInputElementFormat.Float2, 8 },
			{ ShaderInputElementFormat.Float3, 12 },
			{ ShaderInputElementFormat.Float4, 16 },
			{ ShaderInputElementFormat.Int32, 4 },
			{ ShaderInputElementFormat.UInt32, 4 },
			{ ShaderInputElementFormat.Matrix4x4, 16 },
		};

		private static readonly Dictionary<ShaderInputElementFormat, int> _elementCountsByFormat = new Dictionary<ShaderInputElementFormat, int>
		{
			{ ShaderInputElementFormat.Matrix4x4, 4 },
		};

		private InputElement[] InputElementsFromSpec(VertexShaderSpec spec)
		{
			var list = new List<InputElement>();
			var typeCount = new Dictionary<ShaderInputElementPurpose, int>();
			list.AddRange(InputElementsFromSpec(spec.PerVertexElements, typeCount, false));
			list.AddRange(InputElementsFromSpec(spec.PerInstanceElements, typeCount, false));
			return list.ToArray();
		}

		private List<InputElement> InputElementsFromSpec(IEnumerable<ShaderInputElementSpec> specs, Dictionary<ShaderInputElementPurpose, int> typeCount, bool isInstanceData)
		{
			var list = new List<InputElement>();
			var slot = isInstanceData ? 1 : 0;
			var stepRate = isInstanceData ? 1 : 0;
			var offset = 0;
			var classification = isInstanceData ? InputClassification.PerInstanceData : InputClassification.PerVertexData;

			foreach(var spec in specs)
			{
				var semantic = _semantics[spec.Purpose];
				var format = _formats[spec.Format];
				var elementSize = _elementSizes[spec.Format];
				int elementCount;
				if(!_elementCountsByFormat.TryGetValue(spec.Format, out elementCount))
					elementCount = 1;
				int n;
				typeCount.TryGetValue(spec.Purpose, out n);
				
				for(var i = 0; i < elementCount; i++)
				{
					var element = new InputElement(semantic + n, i, format, offset, slot, classification, stepRate);
					list.Add(element);
					offset += elementSize;
				}

				typeCount[spec.Purpose] = ++n;
			}

			return list;
		}

		public void Dispose()
		{
			foreach(var material in _materials.Values)
				material.Dispose();
			_materials.Clear();
		}
	}
}
