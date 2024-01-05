using System;

namespace ZAxis {
    public class DepthBuffer {
        public Trigon[] Faces { get; set; }

        public DepthBuffer(Trigon[] faces) {
            this.Faces = faces;
        }

        // float[] Sort(float[] array) {
        //     var length = array.Length;

        //     for (int i = 1; i < length; i++) {
        //         var ky = array[i];
        //         var fl = 0;

        //         for (int j = i - 1; j >= 0 && fl != 1;) {
        //             if (ky < array[j]) {
        //                 array[j + 1] = array[j];

        //                 j--;

        //                 array[j + 1] = ky;
        //             }
        //             else
        //                 fl = 1;
        //         }
        //     }

        //    return array;
        // }

        // Vector3[] Vec3Sort(Vector3 origin, Vector3[] array) {
        //     var length = array.Length;

        //     for (int i = 1; i < length; i++) {
        //         var ky = array[i];
        //         var fl = 0;

        //         for (int j = i - 1; j >= 0 && fl != 1;) {
        //             if (Vector3.Distance(origin, ky) < Vector3.Distance(origin, array[j])) {
        //                 array[j + 1] = array[j];
        //                 j--;

        //                 array[j + 1] = ky;
        //             }
        //             else
        //                 fl = 1;
        //         }
        //     }

        //    return array;
        // }

        Trigon[] TriSort(Vector3 origin, Trigon[] array) {
            var length = array.Length;

            for (int i = 1; i < length; i++) {
                var ky = array[i];
                var fl = 0;

                for (int j = i - 1; j >= 0 && fl != 1;) {
                    // if (Vector3.Distance(origin, ky) < Vector3.Distance(origin, array[j])) {
                    // if (Trigon.DistanceFromPoint(origin, ky) < Trigon.DistanceFromPoint(origin, array[j])) {
                    // if ( Vector3.Distance(origin, ky.GetCentroid()) < Vector3.Distance(origin, array[j].GetCentroid()) ) {
                    // if ( Vector3.Distance(origin, ky.ClosestPoint(origin)) < Vector3.Distance(origin, array[j].ClosestPoint(origin)) ) {
                    // if (  Vector3.Distance(origin, ky.WeightedAveragePoint(origin)) < Vector3.Distance(origin, array[j].WeightedAveragePoint(origin)) ) {
                    if ( ky.GetCentroid().Z < array[j].GetCentroid().Z ) {
                        array[j + 1] = array[j];
                        j--;

                        array[j + 1] = ky;
                    }
                    else
                        fl = 1;
                } 
            }

           return array;
        }

        public void OrderFromNegativeZ(Camera camera) {
            var sortedFaces = TriSort(camera.Position, this.Faces);

            Array.Reverse(sortedFaces);

            foreach (var v in sortedFaces) {
                // Console.WriteLine($"sV: {v} {v.X} {v.Y} {v.Z} {Vector3.Distance(camera.Position, v)}");
                Console.WriteLine($"{ Vector3.Distance(camera.Position, v.GetCentroid())}");
            }

            this.Faces = sortedFaces;
        }
    }
}