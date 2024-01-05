using System;


namespace ZAxis {
    public class Vector3 {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x, float y, float z) {
            this.X = (float)x;
            this.Y = (float)y;
            this.Z = (float)z;
        }

        public static float Distance(Vector3 p0, Vector3 p1) {
            return (float)Math.Sqrt( Math.Pow(p0.X - p1.X, 2) + Math.Pow(p0.Y - p1.Y, 2) + Math.Pow(p0.Z - p1.Z, 2) );
        }

        public static Vector3 operator + (Vector3 vec0, Vector3 vec1) {
            return new Vector3(
                (vec0.X + vec1.X),
                (vec0.Y + vec1.Y),
                (vec0.Z + vec1.Z)
            );
        }

        public static Vector3 operator - (Vector3 vec0, Vector3 vec1) {
            return new Vector3(
                (vec0.X - vec1.X),
                (vec0.Y - vec1.Y),
                (vec0.Z - vec1.Z)
            );
        }

          public static Vector3 operator * (Vector3 vec0, Vector3 vec1) {
            return new Vector3(
                (vec0.X * vec1.X),
                (vec0.Y * vec1.Y),
                (vec0.Z * vec1.Z)
            );
        }

          public static Vector3 operator / (Vector3 vec0, Vector3 vec1) {
            return new Vector3(
                (vec0.X / vec1.X),
                (vec0.Y / vec1.Y),
                (vec0.Z / vec1.Z)
            );
        }
    }
}