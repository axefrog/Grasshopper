using System.Collections.Generic;
using System.Linq;
using Grasshopper.Graphics.Materials;
using Grasshopper.Platform;

namespace Grasshopper.SharpDX.Graphics.Materials
{
	class TextureSamplerManager : IndexActivatablePlatformResourceManager<ITextureSampler>, ITextureSamplerManager
	{
		private readonly DeviceManager _deviceManager;
		private readonly Dictionary<string, TextureSampler> _samplers = new Dictionary<string, TextureSampler>();

		public TextureSamplerManager(DeviceManager deviceManager)
		{
			_deviceManager = deviceManager;
		}

		protected override void Activate(int firstIndex, IEnumerable<ITextureSampler> resources)
		{
			_deviceManager.Context.PixelShader.SetSamplers(firstIndex,
				resources.Select(sampler => ((TextureSampler)sampler).SamplerState).ToArray());
		}

		public ITextureSampler Create(string id, TextureSamplerSettings settings)
		{
			return CreateAndAdd(id, resource =>
			{
				var sampler = (TextureSampler)resource;
				sampler.Settings = settings;
				sampler.Initialize();
			});
		}

		protected override ITextureSampler CreateResource(string id)
		{
			return new TextureSampler(_deviceManager, id);
		}
	}
}
