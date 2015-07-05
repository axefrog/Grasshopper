using System;
using System.Collections.Generic;
using Grasshopper.Graphics.Materials;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Grasshopper.SharpDX.Graphics.Materials
{
    internal class MaterialResource : Material
    {
        private readonly DeviceManager _deviceManager;
        private readonly ITextureResourceManager _textureManager;
        private readonly ITextureSamplerManager _samplerManager;

        public InputLayout InputLayout { get; private set; }
        public VertexShader VertexShader { get; private set; }
        public PixelShader PixelShader { get; private set; }

        public MaterialResource(DeviceManager deviceManager, ITextureResourceManager textureManager, ITextureSamplerManager samplerManager, string id) : base(id)
        {
            if(deviceManager == null) throw new ArgumentNullException("deviceManager");
            if(textureManager == null) throw new ArgumentNullException("textureManager");
            if(samplerManager == null) throw new ArgumentNullException("samplerManager");

            _deviceManager = deviceManager;
            _textureManager = textureManager;
            _samplerManager = samplerManager;
        }

        protected override void InitializeInternal()
        {
            InitializeVertexShader();
            InitializePixelShader();
        }

        protected override void UninitializeInternal()
        {
            if(InputLayout != null)
            {
                InputLayout.Dispose();
                InputLayout = null;
            }
            if(VertexShader != null)
            {
                VertexShader.Dispose();
                VertexShader = null;
            }
            if(PixelShader != null)
            {
                PixelShader.Dispose();
                PixelShader = null;
            }
        }

        protected override void SetActive()
        {
            _deviceManager.Context.InputAssembler.InputLayout = InputLayout;
            _deviceManager.Context.VertexShader.Set(VertexShader);
            _deviceManager.Context.PixelShader.Set(PixelShader);

            if(Textures != null && Textures.Count > 0)
                _textureManager.Activate(0, Textures);

            if(Samplers != null && Samplers.Count > 0)
                _samplerManager.Activate(0, Samplers);
        }

        private static readonly Dictionary<ShaderInputElementPurpose, string> _semantics = new Dictionary<ShaderInputElementPurpose, string>
        {
            { ShaderInputElementPurpose.Position, "POSITION" },
            { ShaderInputElementPurpose.Color, "COLOR" },
            { ShaderInputElementPurpose.TextureCoordinate, "TEXCOORD" },
            { ShaderInputElementPurpose.Normal, "NORMAL" },
            { ShaderInputElementPurpose.Padding, "PADDING" },
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

        private void InitializeVertexShader()
        {
            ShaderSignature signature;
            using(var bytecode = ShaderBytecode.Compile(VertexShaderSpec.Source, "VSMain", "vs_5_0"))
            {
                signature = ShaderSignature.GetInputSignature(bytecode);
                VertexShader = new VertexShader(_deviceManager.Device, bytecode);
            }
            var elements = InputElementsFromSpec();
            InputLayout = new InputLayout(_deviceManager.Device, signature, elements);
        }

        private void InitializePixelShader()
        {
            using(var bytecode = ShaderBytecode.Compile(PixelShaderSpec.Source, "PSMain", "ps_5_0"))
                PixelShader = new PixelShader(_deviceManager.Device, bytecode);
        }

        private InputElement[] InputElementsFromSpec()
        {
            var list = new List<InputElement>();
            var typeCount = new Dictionary<ShaderInputElementPurpose, int>();
            list.AddRange(InputElementsFromSpec(VertexShaderSpec.PerVertexElements, typeCount, false));
            list.AddRange(InputElementsFromSpec(VertexShaderSpec.PerInstanceElements, typeCount, true));
            return list.ToArray();
        }

        private static List<InputElement> InputElementsFromSpec(IEnumerable<ShaderInputElementSpec> specs, Dictionary<ShaderInputElementPurpose, int> typeCount, bool isInstanceData)
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
                    var element = new InputElement(semantic, i + n, format, offset, slot, classification, stepRate);
                    // Debug.WriteLine("new InputElement(\"{0}\", {1}, {2}, {3}, {4}, {5}, {6})", semantic, i + n, format, offset, slot, classification, stepRate);
                    list.Add(element);
                    offset += elementSize;
                }

                typeCount[spec.Purpose] = n + elementCount;
            }

            return list;
        }
    }
}
