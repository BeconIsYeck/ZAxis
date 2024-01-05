using System;


namespace ZAxis {
    public class Mesh {
        public Trigon[] Polygons { get; set; }

        public Mesh(Trigon[] polygons) {
            this.Polygons = polygons;
        }
    }
}