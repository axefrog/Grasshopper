using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Grasshopper.Math
{
    public static class Calc
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T a, ref T b)
        {
            var t = a;
            a = b;
            b = t;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float lowerBound, float upperBound)
        {
            if(lowerBound > upperBound) Swap(ref lowerBound, ref upperBound);
            return value > upperBound ? upperBound : value < lowerBound ? lowerBound : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InverseLerp(float value, float lowerBound, float upperBound)
        {
            if(lowerBound > upperBound) Swap(ref lowerBound, ref upperBound);
            var range = upperBound - lowerBound;
            value = Clamp((value - lowerBound) / (upperBound - lowerBound), 0.0f, 1.0f);
            return lowerBound + range * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothStep(float value, float lowerBound = 0.0f, float upperBound = 1.0f)
        {
            if(value <= lowerBound) return lowerBound;
            if(value >= upperBound) return upperBound;
            var range = upperBound - lowerBound;
            var x = (value - lowerBound) / range;
            return (x * x * (3 - 2 * x)) * range + lowerBound;
        }
    }
}
