using System;
using System.Windows.Forms;
using System.Drawing;


namespace ZAxis {
    public class Camera {
        public int Width { get; set; }
        public int Height { get; set; }
        public float FOV { get; set; }
        public Vector3 Position { get; set; } = new Vector3(0, 0, 0);
        public Vector3 Orientation { get; set; } = new Vector3(0, 0, 0);
        public float Near { get; set; } = 0.1F;
        public float Far { get; set; } = 1000;

        public Camera(int width, int height, float fov) {
            this.Width = width;
            this.Height = height;
            this.FOV = fov;
        }

        public Camera(int width, int height, float fov, Vector3 position) : this(width, height, fov) {
            this.Position = position;
        }

        public Camera(int width, int height, float fov, Vector3 position, Vector3 orientation) : this(width, height, fov, position) {
            this.Orientation = orientation;
        }

        float[,] applyRotationMatrix(float[,] vector1x3, float[,] rotationMatrix3x3) {
            float temp = 0;

            var outMat = new float[1, 3] {
                {
                    0F,
                    0F,
                    0F
                }
            };

            for (var i = 0; i < 3; i++) {
                for (var j = 0; j < 3; j++) {
                    var curV = vector1x3[0, j];

                    var curRot = rotationMatrix3x3[j, i];

                    temp += curV * curRot;
                }

                outMat[0, i] = temp;

                temp = 0;
            }

            return outMat;
        }

        public static float[,] addMatrices(float[,] A, float[,] B) {
            var aR = A.GetLength(0);
            var aC = A.GetLength(1);

            var bR = B.GetLength(0);
            var bC = B.GetLength(1);

            if (aR != bR || aC != bC)
                throw new Exception("Matrices A and B are not the same size, cannot perform addition.");

            var outMat = new float[1, 3] {
                {
                    0F,
                    0F,
                    0
                }
            };

            for (var i = 0; i < 3; i++) {
                outMat[0, i] = A[0, i] + B[0, i];
            }

            return outMat;
        }

        public Vector3 RotAndProj(Vector3 point, float angleX, float angleY, float angleZ, string rotOrder) {
             var aspRat = (float)this.Width / (float)this.Height;

            var fovRad = this.FOV * (Math.PI / 180);

            var fovVertRad = (
                2 * Math.Atan(
                    Math.Tan(fovRad / 2) * aspRat
                )
            );
            
            var fovVert = fovVertRad * 180 / Math.PI;

            var projMatrix = new float[4, 4] {
                { 1 / (aspRat * (float)Math.Tan(fovVert / 2)), 0, 0, 0 },
                { 0, 1 / (float)Math.Tan(fovVert / 2), 0, 0 },
                { 0, 0, this.Far / (this.Far - this.Near), -1 },
                { 0, 0, 1, 0 }
            };

            var rotMatrixX = new float[3, 3] {
                { 1,            0,                        0            },
                { 0, (float)Math.Cos(angleX), -(float)Math.Sin(angleX) },
                { 0, (float)Math.Sin(angleX),  (float)Math.Cos(angleX) }
            };

            var rotMatrixY = new float[3, 3] {
                {  (float)Math.Cos(angleY), 0, (float)Math.Sin(angleY) },
                {             0,            1,            0            },
                { -(float)Math.Sin(angleY), 0, (float)Math.Cos(angleY) }
            };

            var rotMatrixZ = new float[3, 3] {
                { (float)Math.Cos(angleZ), (float)-Math.Sin(angleZ), 0 },
                { (float)Math.Sin(angleZ),  (float)Math.Cos(angleZ), 0 },
                {            0,                        0,            1 }
            };

            var vecMat3 = new float[1, 3] {
                { 
                    point.X,// this.Position.X, // Apply camera position later
                    point.Y, //- this.Position.Y,
                    point.Z// - this.Position.Z
                }
            };

            // var vecMat4 = new float[4] {
            //     (point.X - this.Position.X) / w,
            //     (point.Y - this.Position.Y) / w,
            //     (point.Z - this.Position.Z) / w,
            //     w
            // };

            var outMat = new float[1, 3] {
                {
                    0F,
                    0F,
                    0F
                }
            };

            // outMat = vecMat3;

            var cI = 0;

            foreach (var c in rotOrder) {
                if (cI == 0) {
                    if (c == 'X') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixX);;

                        outMat = addMatrices(outMat, o);

                        outMat = o;
                    }
                    else if (c == 'Y') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixY);

                        outMat = addMatrices(outMat, o);
                        
                        outMat = o;
                    }
                    else if (c == 'Z') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixZ);

                        outMat = addMatrices(outMat, o);

                        outMat = o;
                    }
                }
                else if (cI == 1) {
                    if (c == 'X') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixX);;

                        outMat = addMatrices(outMat, o);
                    }
                    else if (c == 'Y') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixY);

                        outMat = addMatrices(outMat, o);
                    }
                    else if (c == 'Z') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixZ);

                        outMat = addMatrices(outMat, o);
                    }
                }
                else if (cI == 2) {
                    if (c == 'X') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixX);;

                        outMat = addMatrices(outMat, o);
                    }
                    else if (c == 'Y') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixY);

                        outMat = addMatrices(outMat, o);
                    }
                    else if (c == 'Z') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixZ);

                        outMat = addMatrices(outMat, o);
                    }
                }
            }

            return new Vector3(
                outMat[0, 0],
                outMat[0, 1],
                outMat[0, 2]
            );
        }

        public (PointF, float) Project(Vector3 point, float angleX, float angleY, float angleZ, string rotOrder) {
            var aspRat = (float)this.Width / (float)this.Height;

            var fovRad = this.FOV * (Math.PI / 180);

            var fovVertRad = (
                2 * Math.Atan(
                    Math.Tan(fovRad / 2) * aspRat
                )
            );
            
            var fovVert = fovVertRad * 180 / Math.PI;

            var projMatrix = new float[4, 4] {
                { 1 / (aspRat * (float)Math.Tan(fovVert / 2)), 0, 0, 0 },
                { 0, 1 / (float)Math.Tan(fovVert / 2), 0, 0 },
                { 0, 0, this.Far / (this.Far - this.Near), -1 },
                { 0, 0, 1, 0 }
            };

            var rotMatrixX = new float[3, 3] {
                { 1,            0,                        0            },
                { 0, (float)Math.Cos(angleX), -(float)Math.Sin(angleX) },
                { 0, (float)Math.Sin(angleX),  (float)Math.Cos(angleX) }
            };

            var rotMatrixY = new float[3, 3] {
                {  (float)Math.Cos(angleY), 0, (float)Math.Sin(angleY) },
                {             0,            1,            0            },
                { -(float)Math.Sin(angleY), 0, (float)Math.Cos(angleY) }
            };

            var rotMatrixZ = new float[3, 3] {
                { (float)Math.Cos(angleZ), (float)-Math.Sin(angleZ), 0 },
                { (float)Math.Sin(angleZ),  (float)Math.Cos(angleZ), 0 },
                {            0,                        0,            1 }
            };

            var vecMat3 = new float[1, 3] {
                { 
                    point.X,// this.Position.X, // Apply camera position later
                    point.Y, //- this.Position.Y,
                    point.Z// - this.Position.Z
                }
            };

            // var vecMat4 = new float[4] {
            //     (point.X - this.Position.X) / w,
            //     (point.Y - this.Position.Y) / w,
            //     (point.Z - this.Position.Z) / w,
            //     w
            // };

            var outMat = new float[1, 3] {
                {
                    0F,
                    0F,
                    0F
                }
            };

            // outMat = vecMat3;

            var cI = 0;

            foreach (var c in rotOrder) {
                if (cI == 0) {
                    if (c == 'X') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixX);;

                        outMat = addMatrices(outMat, o);

                        outMat = o;
                    }
                    else if (c == 'Y') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixY);

                        outMat = addMatrices(outMat, o);
                        
                        outMat = o;
                    }
                    else if (c == 'Z') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixZ);

                        outMat = addMatrices(outMat, o);

                        outMat = o;
                    }
                }
                else if (cI == 1) {
                    if (c == 'X') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixX);;

                        outMat = addMatrices(outMat, o);
                    }
                    else if (c == 'Y') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixY);

                        outMat = addMatrices(outMat, o);
                    }
                    else if (c == 'Z') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixZ);

                        outMat = addMatrices(outMat, o);
                    }
                }
                else if (cI == 2) {
                    if (c == 'X') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixX);;

                        outMat = addMatrices(outMat, o);
                    }
                    else if (c == 'Y') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixY);

                        outMat = addMatrices(outMat, o);
                    }
                    else if (c == 'Z') {
                        var o = applyRotationMatrix(vecMat3, rotMatrixZ);

                        outMat = addMatrices(outMat, o);
                    }
                }
            }

            var w = 1;

            var vecMat4 = new float[4] {
                outMat[0, 0] - this.Position.X / w,
                outMat[0, 1] - this.Position.Y / w,
                outMat[0, 2] - this.Position.Z / w,
                w
            };

            var result = new float[4];

            for (var i = 0; i < 4; i++) {
                result[i] = 0;

                for (var j = 0; j < 4; j++) {
                    result[i] += projMatrix[i, j] * vecMat4[j];
                }
            }

            if (result[3] != 0) {
                result[0] /= result[3];
                result[1] /= result[3];
                result[2] /= result[3];
            }

            // result[0] = (result[0] + 1) / 2 * this.Width;
            // result[1] = (result[1] + 1) / 2 * this.Height;

            var proj2d = new PointF(result[0], result[1]);

            return (new PointF(result[0], result[1]), Vector3.Distance(this.Position, new Vector3(outMat[0, 0], outMat[0, 1], outMat[0, 2])));
        }   
    }
}