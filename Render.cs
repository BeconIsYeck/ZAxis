using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace ZAxis {
    public static class Render {
        public static class R3D {
            public static void AddToBuffer(ref DepthBuffer buffer, int i, Trigon tri, Vector3 rotation, string rotOrder, Camera camera) {
                buffer.Faces[i] = tri;
            }

            public static void RenderBuffer(ref DepthBuffer buffer, Vector3 rotation, string rotOrder, Camera camera, PaintEventArgs e, Color color, bool outline, bool verts, int indPolDelMS) {
                var g = false;

                foreach (var tri in buffer.Faces) {
                    var projectedPoints = new PointF[36];

                    Console.WriteLine($"{tri.ClosestPoint(camera.Position)}");

                    if (g) {
                        color = Color.FromArgb(200, 200, 200);
                    }
                    else {
                        color = Color.FromArgb(150, 150, 150);
                    }

                    g = !g;

                    var p0 = tri.Point0;
                    var p1 = tri.Point1;
                    var p2 = tri.Point2;

                    var rX = (float)(rotation.X * (Math.PI / 180));
                    var rY = (float)(rotation.Y * (Math.PI / 180));
                    var rZ = (float)(rotation.Z * (Math.PI / 180));



                    // float rX = (float)(10 * (Math.PI / 180));
                    // float rY = (float)(10 * (Math.PI / 180));
                    // float rZ = (float)(10 * (Math.PI / 180));

                    // var rot = (float)rX;

                    // var rotOrder = "Z__";

                    var (p02d, d0) = camera.Project(p0, rX, rY, rZ, rotOrder);
                    var (p12d, d1) = camera.Project(p1, rX, rY, rZ, rotOrder);
                    var (p22d, d2) = camera.Project(p2, rX, rY, rZ, rotOrder);

                    p02d.X = (p02d.X + 1) / 2 * camera.Width;
                    p02d.Y = (1 - p02d.Y) / 2 * camera.Height;

                    float scaleFactor0 = 1 / (1 + p0.Z / 100);

                    p12d.X = (p12d.X + 1) / 2 * camera.Width;
                    p12d.Y = (1 - p12d.Y) / 2 * camera.Height;

                    float scaleFactor1 = 1 / (1 + p1.Z / 100);

                    p22d.X = (p22d.X + 1) / 2 * camera.Width;
                    p22d.Y = (1 - p22d.Y) / 2 * camera.Height;

                    float scaleFactor2 = 1 / (1 + p2.Z / 100);

                    var size = 12;
                    var pThick = 3F;

                    var color0 = Color.FromArgb(0, 0, 0);
                    var color1 = Color.FromArgb(0, 0, 0);
                    var color2 = Color.FromArgb(0, 0, 0);

                    using (var pen = new Pen(color)) {
                        e.Graphics.FillPolygon(pen.Brush, new PointF[] { p02d, p12d, p22d });
                    }

                    if (outline) {                
                        using (var pen = new Pen(color0, pThick)) {
                            e.Graphics.DrawLine(pen, p02d, p12d);
                        }

                        using (var pen = new Pen(color1, pThick)) {   
                            e.Graphics.DrawLine(pen, p12d, p22d);
                        }

                        using (var pen = new Pen(color2, pThick)) {   
                            e.Graphics.DrawLine(pen, p02d, p22d);
                        }

                    }

                    var C0 = Color.FromArgb(
                        (int)Cmd.Math.ClampFloat(0 - size * scaleFactor0, 0, 255),
                        (int)Cmd.Math.ClampFloat(0 - size * scaleFactor0, 0, 255),
                        (int)Cmd.Math.ClampFloat(0 - size * scaleFactor0, 0, 255)
                    );
                    var C1 = Color.FromArgb(
                        (int)Cmd.Math.ClampFloat(0 - size * scaleFactor1, 0, 255),
                        (int)Cmd.Math.ClampFloat(0 - size * scaleFactor1, 0, 255),
                        (int)Cmd.Math.ClampFloat(0 - size * scaleFactor1, 0, 255)
                    );
                    var C2 = Color.FromArgb(
                        (int)Cmd.Math.ClampFloat(0 - size * scaleFactor2, 0, 255),
                        (int)Cmd.Math.ClampFloat(0 - size * scaleFactor2, 0, 255),
                        (int)Cmd.Math.ClampFloat(0 - size * scaleFactor2, 0, 255)
                    );

                    if (verts) {
                        // e.Graphics.FillEllipse(new SolidBrush(C0), p02d.X - (size * scaleFactor0) / 2, p02d.Y - (size * scaleFactor0) / 2, size * scaleFactor0, size * scaleFactor0);
                        // e.Graphics.FillEllipse(new SolidBrush(C1), p12d.X - (size * scaleFactor1) / 2, p12d.Y - (size * scaleFactor1) / 2, size * scaleFactor1, size * scaleFactor1);
                        // e.Graphics.FillEllipse(new SolidBrush(C2), p22d.X - (size * scaleFactor2) / 2, p22d.Y - (size * scaleFactor2) / 2, size * scaleFactor2, size * scaleFactor2);

                        e.Graphics.FillEllipse(new SolidBrush(C0), p02d.X - (size / 2), p02d.Y - (size / 2), size, size);
                        e.Graphics.FillEllipse(new SolidBrush(C1), p12d.X - (size / 2), p12d.Y - (size / 2), size, size);
                        e.Graphics.FillEllipse(new SolidBrush(C2), p22d.X - (size / 2), p22d.Y - (size / 2), size, size);

                    } 

                    if (indPolDelMS != -1)
                        Thread.Sleep(indPolDelMS);
                }
            }

            public static void Triangle(Trigon tri, Vector3 rotation, string rotOrder, Camera camera, PaintEventArgs e, Color color, bool outline, bool verts) {
                var p0 = tri.Point0;
                var p1 = tri.Point1;
                var p2 = tri.Point2;

                var rX = (float)(rotation.X * (Math.PI / 180));
                var rY = (float)(rotation.Y * (Math.PI / 180));
                var rZ = (float)(rotation.Z * (Math.PI / 180));

                // float rX = (float)(10 * (Math.PI / 180));
                // float rY = (float)(10 * (Math.PI / 180));
                // float rZ = (float)(10 * (Math.PI / 180));

                // var rot = (float)rX;

                // var rotOrder = "Z__";

                var (p02d, d0) = camera.Project(p0, rX, rY, rZ, rotOrder);
                var (p12d, d1) = camera.Project(p1, rX, rY, rZ, rotOrder);
                var (p22d, d2) = camera.Project(p2, rX, rY, rZ, rotOrder);

                p02d.X = (p02d.X + 1) / 2 * camera.Width;
                p02d.Y = (1 - p02d.Y) / 2 * camera.Height;

                float scaleFactor0 = 1 / (1 + p0.Z / 100);

                p12d.X = (p12d.X + 1) / 2 * camera.Width;
                p12d.Y = (1 - p12d.Y) / 2 * camera.Height;

                float scaleFactor1 = 1 / (1 + p1.Z / 100);

                p22d.X = (p22d.X + 1) / 2 * camera.Width;
                p22d.Y = (1 - p22d.Y) / 2 * camera.Height;

                float scaleFactor2 = 1 / (1 + p2.Z / 100);

                var size = 12;
                var pThick = 3F;

                var color0 = Color.FromArgb(0, 0, 0);
                var color1 = Color.FromArgb(0, 0, 0);
                var color2 = Color.FromArgb(0, 0, 0);

                using (var pen = new Pen(color)) {
                    e.Graphics.FillPolygon(pen.Brush, new PointF[] { p02d, p12d, p22d });
                }

                if (outline) {                
                    using (var pen = new Pen(color0, pThick)) {
                        e.Graphics.DrawLine(pen, p02d, p12d);
                    }

                    using (var pen = new Pen(color1, pThick)) {   
                        e.Graphics.DrawLine(pen, p12d, p22d);
                    }

                    using (var pen = new Pen(color2, pThick)) {   
                        e.Graphics.DrawLine(pen, p02d, p22d);
                    }

                }

                var C0 = Color.FromArgb(
                    (int)Cmd.Math.ClampFloat(0 - size * scaleFactor0, 0, 255),
                    (int)Cmd.Math.ClampFloat(0 - size * scaleFactor0, 0, 255),
                    (int)Cmd.Math.ClampFloat(0 - size * scaleFactor0, 0, 255)
                );
                var C1 = Color.FromArgb(
                    (int)Cmd.Math.ClampFloat(0 - size * scaleFactor1, 0, 255),
                    (int)Cmd.Math.ClampFloat(0 - size * scaleFactor1, 0, 255),
                    (int)Cmd.Math.ClampFloat(0 - size * scaleFactor1, 0, 255)
                );
                var C2 = Color.FromArgb(
                    (int)Cmd.Math.ClampFloat(0 - size * scaleFactor2, 0, 255),
                    (int)Cmd.Math.ClampFloat(0 - size * scaleFactor2, 0, 255),
                    (int)Cmd.Math.ClampFloat(0 - size * scaleFactor2, 0, 255)
                );

                if (verts) {
                    // e.Graphics.FillEllipse(new SolidBrush(C0), p02d.X - (size * scaleFactor0) / 2, p02d.Y - (size * scaleFactor0) / 2, size * scaleFactor0, size * scaleFactor0);
                    // e.Graphics.FillEllipse(new SolidBrush(C1), p12d.X - (size * scaleFactor1) / 2, p12d.Y - (size * scaleFactor1) / 2, size * scaleFactor1, size * scaleFactor1);
                    // e.Graphics.FillEllipse(new SolidBrush(C2), p22d.X - (size * scaleFactor2) / 2, p22d.Y - (size * scaleFactor2) / 2, size * scaleFactor2, size * scaleFactor2);

                    e.Graphics.FillEllipse(new SolidBrush(C0), p02d.X - (size / 2), p02d.Y - (size / 2), size, size);
                    e.Graphics.FillEllipse(new SolidBrush(C1), p12d.X - (size / 2), p12d.Y - (size / 2), size, size);
                    e.Graphics.FillEllipse(new SolidBrush(C2), p22d.X - (size / 2), p22d.Y - (size / 2), size, size);

                }
            }
        }
    }
}