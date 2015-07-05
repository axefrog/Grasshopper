using System.Diagnostics;

namespace Grasshopper.Core
{
    public class TickCounter
    {
        private readonly int _maxSamples;
        int tickIndex;
        long tickSum;
        readonly long[] tickList;
        readonly Stopwatch clock;
        long frameCount;

        public TickCounter(int maxSamples = 100)
        {
            _maxSamples = maxSamples;
            tickList = new long[maxSamples];
            clock = Stopwatch.StartNew();
        }

        double CalcAverageTick(long newtick)
        {
            tickSum -= tickList[tickIndex];  /* subtract value falling off */
            tickSum += newtick;              /* add new value */
            tickList[tickIndex] = newtick;   /* save new value so it can be subtracted later */
            if(++tickIndex == _maxSamples)   /* inc buffer index */
                tickIndex = 0;

            if(frameCount < _maxSamples)
                return (double)tickSum / frameCount;
            return (double)tickSum / _maxSamples;
        }

        internal void Tick()
        {
            frameCount++;
            var averageTick = CalcAverageTick(clock.ElapsedTicks) / Stopwatch.Frequency;
            TicksPerSecond = 1d / averageTick;
            AverageTickDuration = averageTick * 1000d;
            clock.Restart();
        }

        public double TicksPerSecond { get; private set; }
        public double AverageTickDuration { get; private set; }
    }
}