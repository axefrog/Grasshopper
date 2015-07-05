using System.Numerics;

namespace Grasshopper.Math
{
    public class Transformation
    {
        public Transformation()
        {
            Scale = 1.0f;
            Rotation = Quaternion.Identity;
        }

        public float Scale { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}