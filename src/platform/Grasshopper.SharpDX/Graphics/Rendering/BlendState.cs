using System;
using Grasshopper.Graphics.Rendering.Blending;
using SharpDX.Direct3D11;

namespace Grasshopper.SharpDX.Graphics.Rendering
{
    class BlendState : ActivatableD3DResource, IBlendState
    {
        public BlendState(DeviceManager deviceManager, string id, IBlendSettings settings) : base(deviceManager, id)
        {
            if(settings == null) throw new ArgumentNullException("settings");
            Settings = settings;
        }

        public IBlendSettings Settings { get; private set; }
        public global::SharpDX.Direct3D11.BlendState State { get; private set; }

        protected override void InitializeInternal()
        {
            var descr = new BlendStateDescription
            {
                AlphaToCoverageEnable = Settings.EnableAlphaToCoverage,
                IndependentBlendEnable = false,
            };

            descr.RenderTarget[0] = new RenderTargetBlendDescription
            {
                IsBlendEnabled = true,
                SourceBlend = GetBlendOption(Settings.FirstRGB),
                DestinationBlend = GetBlendOption(Settings.SecondRGB),
                BlendOperation = GetBlendOperation(Settings.OutputRGB),
                SourceAlphaBlend = GetBlendOption(Settings.FirstAlpha),
                DestinationAlphaBlend = GetBlendOption(Settings.SecondAlpha),
                AlphaBlendOperation = GetBlendOperation(Settings.OutputAlpha),
                RenderTargetWriteMask = (ColorWriteMaskFlags)Settings.OutputColorComponents
            };

            State = new global::SharpDX.Direct3D11.BlendState(DeviceManager.Device, descr);
        }

        private BlendOption GetBlendOption(PreBlend value)
        {
            switch(value)
            {
                case PreBlend.Zero: return BlendOption.Zero;
                case PreBlend.One: return BlendOption.One;
                case PreBlend.SourceColor: return BlendOption.SourceColor;
                case PreBlend.InverseSourceColor: return BlendOption.InverseSourceColor;
                case PreBlend.SourceAlpha: return BlendOption.SourceAlpha;
                case PreBlend.InverseSourceAlpha: return BlendOption.InverseSourceAlpha;
                case PreBlend.DestinationAlpha: return BlendOption.DestinationAlpha;
                case PreBlend.InverseDestinationAlpha: return BlendOption.InverseDestinationAlpha;
                case PreBlend.DestinationColor: return BlendOption.DestinationColor;
                case PreBlend.InverseDestinationColor: return BlendOption.InverseDestinationColor;
                case PreBlend.SourceAlphaSaturate: return BlendOption.SourceAlphaSaturate;
                case PreBlend.BlendFactor: return BlendOption.BlendFactor;
                case PreBlend.InverseBlendFactor: return BlendOption.InverseBlendFactor;
                case PreBlend.SecondarySourceColor: return BlendOption.SecondarySourceColor;
                case PreBlend.InverseSecondarySourceColor: return BlendOption.InverseSecondarySourceColor;
                case PreBlend.SecondarySourceAlpha: return BlendOption.SecondarySourceAlpha;
                case PreBlend.InverseSecondarySourceAlpha: return BlendOption.InverseSecondarySourceAlpha;
            }

            throw new NotSupportedException("The specified blend value is not supported");
        }

        private BlendOperation GetBlendOperation(BlendEquation value)
        {
            switch(value)
            {
                case BlendEquation.AddBothValues: return BlendOperation.Add;
                case BlendEquation.SubtractFirstFromSecond: return BlendOperation.Subtract;
                case BlendEquation.SubtractSecondFromFirst: return BlendOperation.ReverseSubtract;
                case BlendEquation.Minimum: return BlendOperation.Minimum;
                case BlendEquation.Maximum: return BlendOperation.Maximum;
            }

            throw new NotSupportedException("The specified blend operation is not supported");
        }

        protected override void UninitializeInternal()
        {
            if(State != null)
            {
                State.Dispose();
                State = null;
            }
        }

        protected override void SetActive()
        {
            DeviceManager.Context.OutputMerger.SetBlendState(State);
        }
    }
}