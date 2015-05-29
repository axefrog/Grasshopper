using System;
using Grasshopper.Graphics.Materials;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Materials
{
	class TextureSampler : IndexActivatableD3DResource, ITextureSampler
	{
		public TextureSampler(DeviceManager deviceManager, string id) : base(deviceManager, id)
		{
		}

		public TextureSamplerSettings Settings { get; internal set; }
		public SamplerState SamplerState { get; private set; }

		protected override void InitializeInternal()
		{
			var descr = new SamplerStateDescription
			{
				AddressU = GetTextureAddressMode(Settings.WrapU),
				AddressV = GetTextureAddressMode(Settings.WrapV),
				AddressW = GetTextureAddressMode(Settings.WrapW),
				Filter = GetFilterType(Settings.Filter)
			};
			SamplerState = new SamplerState(DeviceManager.Device, descr);
		}

		protected override void UninitializeInternal()
		{
			if(SamplerState != null)
			{
				SamplerState.Dispose();
				SamplerState = null;
			}
		}

		private TextureAddressMode GetTextureAddressMode(TextureWrapping wrapping)
		{
			switch(wrapping)
			{
				case TextureWrapping.Wrap: return TextureAddressMode.Wrap;
				case TextureWrapping.Border: return TextureAddressMode.Border;
				case TextureWrapping.Clamp: return TextureAddressMode.Clamp;
				case TextureWrapping.Mirror: return TextureAddressMode.Mirror;
				case TextureWrapping.MirrorOnce: return TextureAddressMode.MirrorOnce;
				default: throw new NotSupportedException();
			}
		}

		private Filter GetFilterType(TextureFiltering filter)
		{
			switch(filter)
			{
				case TextureFiltering.MinMagMipPoint: return Filter.MinMagMipPoint;
				case TextureFiltering.MinMagPointMipLinear: return Filter.MinMagPointMipLinear;
				case TextureFiltering.MinPointMagLinearMipPoint: return Filter.MinPointMagLinearMipPoint;
				case TextureFiltering.MinPointMagMipLinear: return Filter.MinPointMagMipLinear;
				case TextureFiltering.MinLinearMagMipPoint: return Filter.MinLinearMagMipPoint;
				case TextureFiltering.MinLinearMagPointMipLinear: return Filter.MinLinearMagPointMipLinear;
				case TextureFiltering.MinMagLinearMipPoint: return Filter.MinMagLinearMipPoint;
				case TextureFiltering.MinMagMipLinear: return Filter.MinMagMipLinear;
				case TextureFiltering.Anisotropic: return Filter.Anisotropic;
				case TextureFiltering.ComparisonMinMagMipPoint: return Filter.ComparisonMinMagMipPoint;
				case TextureFiltering.ComparisonMinMagPointMipLinear: return Filter.ComparisonMinMagPointMipLinear;
				case TextureFiltering.ComparisonMinPointMagLinearMipPoint: return Filter.ComparisonMinPointMagLinearMipPoint;
				case TextureFiltering.ComparisonMinPointMagMipLinear: return Filter.ComparisonMinPointMagMipLinear;
				case TextureFiltering.ComparisonMinLinearMagMipPoint: return Filter.ComparisonMinLinearMagMipPoint;
				case TextureFiltering.ComparisonMinLinearMagPointMipLinear: return Filter.ComparisonMinLinearMagPointMipLinear;
				case TextureFiltering.ComparisonMinMagLinearMipPoint: return Filter.ComparisonMinMagLinearMipPoint;
				case TextureFiltering.ComparisonMinMagMipLinear: return Filter.ComparisonMinMagMipLinear;
				case TextureFiltering.ComparisonAnisotropic: return Filter.ComparisonAnisotropic;
				case TextureFiltering.MinimumMinMagMipPoint: return Filter.MinimumMinMagMipPoint;
				case TextureFiltering.MinimumMinMagPointMipLinear: return Filter.MinimumMinMagPointMipLinear;
				case TextureFiltering.MinimumMinPointMagLinearMipPoint: return Filter.MinimumMinPointMagLinearMipPoint;
				case TextureFiltering.MinimumMinPointMagMipLinear: return Filter.MinimumMinPointMagMipLinear;
				case TextureFiltering.MinimumMinLinearMagMipPoint: return Filter.MinimumMinLinearMagMipPoint;
				case TextureFiltering.MinimumMinLinearMagPointMipLinear: return Filter.MinimumMinLinearMagPointMipLinear;
				case TextureFiltering.MinimumMinMagLinearMipPoint: return Filter.MinimumMinMagLinearMipPoint;
				case TextureFiltering.MinimumMinMagMipLinear: return Filter.MinimumMinMagMipLinear;
				case TextureFiltering.MinimumAnisotropic: return Filter.MinimumAnisotropic;
				case TextureFiltering.MaximumMinMagMipPoint: return Filter.MaximumMinMagMipPoint;
				case TextureFiltering.MaximumMinMagPointMipLinear: return Filter.MaximumMinMagPointMipLinear;
				case TextureFiltering.MaximumMinPointMagLinearMipPoint: return Filter.MaximumMinPointMagLinearMipPoint;
				case TextureFiltering.MaximumMinPointMagMipLinear: return Filter.MaximumMinPointMagMipLinear;
				case TextureFiltering.MaximumMinLinearMagMipPoint: return Filter.MaximumMinLinearMagMipPoint;
				case TextureFiltering.MaximumMinLinearMagPointMipLinear: return Filter.MaximumMinLinearMagPointMipLinear;
				case TextureFiltering.MaximumMinMagLinearMipPoint: return Filter.MaximumMinMagLinearMipPoint;
				case TextureFiltering.MaximumMinMagMipLinear: return Filter.MaximumMinMagMipLinear;
				case TextureFiltering.MaximumAnisotropic: return Filter.MaximumAnisotropic;
				default: throw new NotSupportedException();
			}
		}

		protected override void ActivateAtIndex(int index)
		{
			DeviceManager.Context.PixelShader.SetSampler(index, SamplerState);
		}
	}
}