using System;


namespace ZAxis {
    public class Trigon {
        public Vector3 Point0 { get; set; }
        public Vector3 Point1 { get; set; } 
        public Vector3 Point2 { get; set; }

        public Trigon(Vector3 p0, Vector3 p1, Vector3 p2) {
            this.Point0 = p0;
            this.Point1 = p1;
            this.Point2 = p2;
        }

        public Vector3 PointI(int i) {
            switch (i) {
                case 0: {
                    return this.Point0;
                }

                case 1: {
                    return this.Point1;
                }

                case 2: {
                    return this.Point2;
                }

                default: {
                    return new Vector3(-0, -0, -0);
                }
            }
        }

        public Vector3 AddPoints() =>
            this.Point0 + this.Point1 + this.Point2;
        
        public static float DistanceFromPoint(Vector3 point, Trigon trigon) =>
            Vector3.Distance(point, trigon.Point0) + Vector3.Distance(point, trigon.Point1) + Vector3.Distance(point, trigon.Point2);

        // public static float DistanceFromPoint(Vector3 point, Trigon trigon) =>
        //     Vector3.Distance(point, trigon.Point0 + trigon.Point1 + trigon.Point2);
        
        public Vector3 GetCentroid() =>
            new Vector3(
                (this.Point0.X + this.Point1.X + this.Point2.X) / 3,
                (this.Point0.Y + this.Point1.Y + this.Point2.Y) / 3,
                (this.Point0.Z + this.Point1.Z + this.Point2.Z) / 3
            );

        public Vector3 ClosestPoint(Vector3 point) {
            var closestPoint = new Vector3(99999, 99999, 99999);

            for (var i = 0; i < 3; i++) {
                var nPoint = this.PointI(i);

                if (Vector3.Distance(point, closestPoint) > Vector3.Distance(point, nPoint)) {
                    closestPoint = nPoint;
                }
            }

            return closestPoint;
        }

        public Vector3 WeightedAveragePoint(Vector3 cameraPosition) {
            float totalWeight = 0;
            Vector3 weightedSum = new Vector3(0, 0, 0);

            for (var i = 0; i < 3; i++) {
                var point = this.PointI(i);
                var distance = Vector3.Distance(cameraPosition, point);
                var weight = 1 / distance;

                weightedSum += new Vector3(point.X * weight, point.Y * weight, point.Z * weight);
                totalWeight += weight;
            }

            return new Vector3(weightedSum.X / totalWeight, weightedSum.Y / totalWeight, weightedSum.Z / totalWeight);
        }
    }
}