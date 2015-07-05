namespace Grasshopper
{
    public class FrameContext
    {
        private float _prevElapsed;

        public FrameContext(GrasshopperApp app)
        {
            App = app;
            NextFrame();
        }

        internal void NextFrame()
        {
            ElapsedSeconds = App.ElapsedSeconds;
            DeltaSeconds = ElapsedSeconds - _prevElapsed;
            _prevElapsed = App.ElapsedSeconds;
            FramesPerSecond = (float)App.TickCounter.TicksPerSecond;
            AverageFrameDuration = (float)App.TickCounter.AverageTickDuration;
            FrameNumber++;
        }

        public GrasshopperApp App { get; private set; }
        public float ElapsedSeconds { get; private set; }
        public float DeltaSeconds { get; private set; }
        public float FramesPerSecond { get; private set; }
        public float AverageFrameDuration { get; private set; }
        public long FrameNumber { get; private set; }
    }
}