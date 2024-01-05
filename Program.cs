using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;


namespace ZAxis {
    public static class Program {
        public static Form MainForm = new Form();
        public static Camera MainCamera = new Camera(800, 600, 100, new Vector3(0, 0, 0));

        public static void Main(string[] args) {

            var outlineOn = false;
            var vertsOn = false;

            MainForm.Width = 675;
            MainForm.Height = 675;
            MainForm.Text = "ZAxis 3D Renderer Test: Cube";

            var vec = new Vector3(10, 10, 10);

            var cam = new Camera(100, 100, 90F, new Vector3(10, 10, 10)); 

            ZMesh.ZMSHProcess("0:<0,0,0;0,1,0;1,0,0;>|!");

            var a = (float)(10) * 1F;
            var b = (float)(7.5) * 1F;

            var cubePolygons = new Trigon[] {
                new Trigon(
                    new Vector3(-5,-5,a),
                    new Vector3(-25,-5,a),
                    new Vector3(-5,-25,a)
                ),
                new Trigon(
                    new Vector3(-25,-25,a),
                    new Vector3(-5,-25,a),
                    new Vector3(-25,-5,a)
                ),
                new Trigon(
                    new Vector3(-5,-25,a),
                    new Vector3(-5,-25,b),
                    new Vector3(-25,-25,a)
                ),
                new Trigon(
                    new Vector3(-25,-25,b),
                    new Vector3(-25,-25,a),
                    new Vector3(-5,-25,b)
                ),
                new Trigon(
                    new Vector3(-5,-5,a),
                    new Vector3(-5,-25,a),
                    new Vector3(-5,-5,b)
                ),
                new Trigon(
                    new Vector3(-5,-25,b),
                    new Vector3(-5,-25,a),
                    new Vector3(-5,-5,b)
                ),
                new Trigon(
                    new Vector3(-25,-5,a),
                    new Vector3(-25,-25,a),
                    new Vector3(-25,-5,b)
                ),
                new Trigon(
                    new Vector3(-25,-25,b),
                    new Vector3(-25,-25,a),
                    new Vector3(-25,-5,b)
                ),
                new Trigon(
                    new Vector3(-5,-5,a),
                    new Vector3(-5,-5,b),
                    new Vector3(-25,-5,a)
                ),
                new Trigon(
                    new Vector3(-25,-5,b),
                    new Vector3(-25,-5,a),
                    new Vector3(-5,-5,b)
                ),
                new Trigon(
                    new Vector3(-5,-5,b),
                    new Vector3(-25,-5,b),
                    new Vector3(-5,-25,b)
                ),
                new Trigon(
                    new Vector3(-25,-25,b),
                    new Vector3(-25,-5,b),
                    new Vector3(-5,-25,b)
                )
            };

            var cubeUpdPoly = new Trigon[] {
                new Trigon(
                    new Vector3(1,1,1),
                    new Vector3(-1,1,1),
                    new Vector3(1,-1,1)
                ),
                new Trigon(
                    new Vector3(-1,-1,1),
                    new Vector3(1,-1,1),
                    new Vector3(-1,1,1)
                ),
                new Trigon(
                    new Vector3(1,1,1),
                    new Vector3(1,-1,1),
                    new Vector3(1,1,-1)
                ),
                new Trigon(
                    new Vector3(1,-1,-1),
                    new Vector3(1,-1,1),
                    new Vector3(1,1,-1)
                ),
                new Trigon(
                    new Vector3(1,1,1),
                    new Vector3(1,1,-1),
                    new Vector3(-1,1,1)
                ),
                new Trigon(
                    new Vector3(-1,1,-1),
                    new Vector3(-1,1,1),
                    new Vector3(1,1,-1)
                ),
                new Trigon(
                    new Vector3(-1,1,1),
                    new Vector3(-1,-1,1),
                    new Vector3(-1,1,-1)
                ),
                new Trigon(
                    new Vector3(-1,-1,-1),
                    new Vector3(-1,-1,1),
                    new Vector3(-1,1,-1)
                ),
                new Trigon(
                    new Vector3(1,-1,1),
                    new Vector3(1,-1,-1),
                    new Vector3(-1,-1,1)
                ),
                new Trigon(
                    new Vector3(-1,-1,-1),
                    new Vector3(-1,-1,1),
                    new Vector3(1,-1,-1)
                ),
                new Trigon(
                    new Vector3(1,1,-1),
                    new Vector3(-1,1, 1),
                    new Vector3(1,-1,-1)
                ),
                new Trigon(
                    new Vector3(-1,-1,-1),
                    new Vector3(-1,1,-1),
                    new Vector3(1,-1,-1)
                )
            };

            var oo = 20;

            var c = oo; // five
            var d = -oo; // negative five
            var ee = 30; // negative five 

            var pyrPoly = new Trigon[] {
                new Trigon( // Back Bottom Left
                    new Vector3(d, d, d),
                    new Vector3(d, d, c),
                    new Vector3(c, d, d)
                ),
                new Trigon( // Back Top Right
                    new Vector3(c, d, c),
                    new Vector3(d, d, c),
                    new Vector3(c, d, d)
                ),
                new Trigon( // -Z +-X
                    new Vector3(0, ee, 0),
                    new Vector3(c, d, d),
                    new Vector3(d, d, d)
                ),
                new Trigon( // +-Z -X 
                    new Vector3(0, ee, 0),
                    new Vector3(d, d, d),
                    new Vector3(d, d, c)
                ),
                new Trigon( // +-Z +X
                    new Vector3(0, ee, 0),
                    new Vector3(c, d, d),
                    new Vector3(c, d, c)
                ),
                new Trigon( // +Z +-X
                    new Vector3(0, ee, 0),
                    new Vector3(c, d, c),
                    new Vector3(d, d, c)
                )
            };

            var newBuf = new DepthBuffer(cubePolygons);
            newBuf.OrderFromNegativeZ(MainCamera);

            var rotation = new Vector3(0, 0, 0);
            var rotOrder = "Y__";

            var cubeMesh = new Mesh(cubePolygons);

            var isOutlineOn = new Label();

            isOutlineOn.Size = new Size(20, 20);
            isOutlineOn.Location = new Point(110, 30);
            isOutlineOn.Text = "Off";

            var isVertsOn = new Label();

            isVertsOn.Size = new Size(20, 20);
            isVertsOn.Location = new Point(130, 30);
            isVertsOn.Text = "Off";

            void redrawCubeClick(object sender, EventArgs e) {
                MainForm.Invalidate();
            }

            void outlineToggleClick(object sender, EventArgs e) {
                outlineOn = !outlineOn;

                if (outlineOn)
                    isOutlineOn.Text = "On";
                else
                    isOutlineOn.Text = "Off";
            }

            void vertsToggleClick(object sender, EventArgs e) {
                vertsOn = !vertsOn;

                if (vertsOn)
                    isVertsOn.Text = "On";
                else
                    isVertsOn.Text = "Off";
            }

            var redrawCube = new Button();

            redrawCube.Text = "Redraw";
            redrawCube.BackColor = Color.FromArgb(255, 255, 255);

            redrawCube.Click += new EventHandler(redrawCubeClick);

            var outlineToggle = new Button();

            outlineToggle.Size = new Size(55, 22);
            outlineToggle.Location = new Point(0, 25); 
            outlineToggle.Text = "Outline";
            outlineToggle.BackColor = Color.FromArgb(255, 255, 255);

            outlineToggle.Click += new EventHandler(outlineToggleClick);

            var vertsToggle = new Button();

            vertsToggle.Size = new Size(55, 22);
            vertsToggle.Location = new Point(50, 25); 
            vertsToggle.Text = "Vertices";
            vertsToggle.BackColor = Color.FromArgb(255, 255, 255);

            vertsToggle.Click += vertsToggleClick;

            var triDrawDel = new TextBox();

            triDrawDel.Location = new Point(0, 50);

            var triDrawDelLbl = new Label();

            triDrawDelLbl.Location = new Point(100, 55);
            triDrawDelLbl.Text = "Draw delay in ms";

            var moveDef = 5F;

            var moveMin = 0.1F;
            var moveMax = 25F;

            var zDiv = 3;

            var moveDis = new TextBox();

            moveDis.Location = new Point(0, 75);

            var moveDisLbl = new Label();

            moveDisLbl.Size = new Size(150, 25);
            moveDisLbl.Location = new Point(100, 80);
            moveDisLbl.Text = "Camera movement speed";

            void moveLeftClick(object sender, EventArgs e) {
                if (Int32.TryParse(moveDis.Text, out _) == true) {
                    MainCamera.Position.X -= (float)Cmd.Math.ClampFloat(float.Parse(moveDis.Text), moveMin, moveMax);
                }
                else {
                    MainCamera.Position.X -= moveDef;
                }

                MainForm.Invalidate();
            }

            void moveRightClick(object sender, EventArgs e) {
                if (Int32.TryParse(moveDis.Text, out _) == true) {
                    MainCamera.Position.X += (float)Cmd.Math.ClampFloat(float.Parse(moveDis.Text), moveMin, moveMax);
                }
                else {
                    MainCamera.Position.X += moveDef;
                }

                MainForm.Invalidate();
            }

            void moveUpClick(object sender, EventArgs e) {
                if (Int32.TryParse(moveDis.Text, out _) == true) {
                    MainCamera.Position.Y += (float)Cmd.Math.ClampFloat(float.Parse(moveDis.Text), moveMin, moveMax);
                }
                else {
                    MainCamera.Position.Y += moveDef;
                }

                MainForm.Invalidate();
            }

            void moveDownClick(object sender, EventArgs e) {
                if (Int32.TryParse(moveDis.Text, out _) == true) {
                    MainCamera.Position.Y -= (float)Cmd.Math.ClampFloat(float.Parse(moveDis.Text), moveMin, moveMax);
                }
                else {
                    MainCamera.Position.Y -= moveDef;
                }

                MainForm.Invalidate();
            }

            void moveForwardsClick(object sender, EventArgs e) {
                if (Int32.TryParse(moveDis.Text, out _) == true) {
                    MainCamera.Position.Z += (float)Cmd.Math.ClampFloat(float.Parse(moveDis.Text), moveMin, moveMax) / zDiv;
                }
                else {
                    MainCamera.Position.Z += moveDef / zDiv;
                }

                MainForm.Invalidate();
            }

            void moveBackwardsClick(object sender, EventArgs e) {
                if (Int32.TryParse(moveDis.Text, out _) == true) {
                    MainCamera.Position.Z -= (float)Cmd.Math.ClampFloat(float.Parse(moveDis.Text), moveMin, moveMax) / zDiv;
                }
                else {
                    MainCamera.Position.Z -= moveDef / zDiv;
                }

                MainForm.Invalidate();
            }

            float rotID = 15;

            void incAngleClick(object sender, EventArgs e) {
                rotation.X += rotID;
                rotation.Y += rotID;
                rotation.Z += rotID;

                MainForm.Invalidate();
            }

            void decAngleClick(object sender, EventArgs e) {
                rotation.X -= rotID;
                rotation.Y -= rotID;
                rotation.Z -= rotID;

                MainForm.Invalidate();
            }

            var moveLeft = new Button();

            moveLeft.Size = new Size(50, 25);
            moveLeft.Location = new Point(0, 150);
            moveLeft.Text = "<-";

            moveLeft.Click += moveLeftClick;

            var moveRight = new Button();

            moveRight.Size = new Size(50, 25);
            moveRight.Location = new Point(50, 150);
            moveRight.Text = "->";

            moveRight.Click += moveRightClick;

            var moveUp = new Button();

            moveUp.Size = new Size(50, 25);
            moveUp.Location = new Point(25, 125);
            moveUp.Text = "ʌ\n|";

            moveUp.Click += moveUpClick;

            var moveDown = new Button();

            moveDown.Size = new Size(50, 25);
            moveDown.Location = new Point(25, 175);
            moveDown.Text = "|\nv";

            moveDown.Click += moveDownClick;

            var moveForwards = new Button();

            moveForwards.Size = new Size(50, 25);
            moveForwards.Location = new Point(25, 225);
            moveForwards.Text = "ʌ";

            moveForwards.Click += moveForwardsClick;

            var moveBackwards = new Button();

            moveBackwards.Size = new Size(50, 25);
            moveBackwards.Location = new Point(25, 250);
            moveBackwards.Text = "v";

            moveBackwards.Click += moveBackwardsClick;

            var decAngle = new Button();

            decAngle.Size = new Size(25, 25);
            decAngle.Location = new Point(0, 300);
            decAngle.Text = "-";

            decAngle.Click += decAngleClick;

            var incAngle = new Button();

            incAngle.Size = new Size(25, 25);
            incAngle.Location = new Point(25, 300);
            incAngle.Text = "+";

            incAngle.Click += incAngleClick;

            var depthBuffer = new DepthBuffer(pyrPoly);

            void MainPaint(object sender, PaintEventArgs e) {
                var s = 0;
                var i = 0;


                // foreach (Trigon t in cubePolygons) {                    
                //     if (s == 0) {
                //         // Render.R3D.Triangle(t, rotation, rotOrder, MainCamera, e, Color.FromArgb(150, 150, 150), outlineOn, vertsOn);
                //         Render.R3D.AddToBuffer(ref depthBuffer, i, t, )

                //         s = 1;
                //     }
                //     else if (s == 1) {
                //         Render.R3D.Triangle(t, rotation, rotOrder, MainCamera, e, Color.FromArgb(200, 200, 200), outlineOn, vertsOn);

                //         s = 0;
                //     }


                //     i++;
                // }

                    if (Int32.TryParse(triDrawDel.Text, out _) == true) {
                        Console.WriteLine("test");
                        Render.R3D.RenderBuffer(ref depthBuffer, rotation, rotOrder, MainCamera, e, Color.FromArgb(150, 150, 150), outlineOn, vertsOn, Int32.Parse(triDrawDel.Text));
                    }
                    else {
                        Render.R3D.RenderBuffer(ref depthBuffer, rotation, rotOrder, MainCamera, e, Color.FromArgb(150, 150, 150), outlineOn, vertsOn, -1);
                    }
            }

            void rotLoop() {
                while (true) {
                    rotation.X += rotID;
                    rotation.Y += rotID;
                    rotation.Z += rotID;

                    MainForm.Invalidate();

                    Thread.Sleep(250);
                }
            }

            var rL = new Thread(new ThreadStart(rotLoop));

            // rL.Start();

            MainForm.FormClosed += new FormClosedEventHandler(MainFormFormClosed);

            void MainFormFormClosed(object sender, FormClosedEventArgs e){
                rL.Abort();
            }

            MainForm.Paint += MainPaint;

            MainForm.Controls.Add(redrawCube);
            MainForm.Controls.Add(outlineToggle);
            MainForm.Controls.Add(vertsToggle);
            MainForm.Controls.Add(isOutlineOn);
            MainForm.Controls.Add(isVertsOn);
            MainForm.Controls.Add(triDrawDel);
            MainForm.Controls.Add(triDrawDelLbl);

            MainForm.Controls.Add(moveDis);
            MainForm.Controls.Add(moveDisLbl);

            MainForm.Controls.Add(moveLeft);
            MainForm.Controls.Add(moveRight);
            MainForm.Controls.Add(moveUp);
            MainForm.Controls.Add(moveDown);

            MainForm.Controls.Add(moveForwards);
            MainForm.Controls.Add(moveBackwards);

            MainForm.Controls.Add(decAngle);
            MainForm.Controls.Add(incAngle);

            MainForm.ShowDialog();
        }
    }
}