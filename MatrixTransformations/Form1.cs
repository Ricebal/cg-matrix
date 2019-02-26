using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MatrixTransformations;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        AxisX x_axis;
        AxisY y_axis;
        AxisZ z_axis;
        Square square;
        Square square2;
        Square square3;
        Square square4;
        Cube cube;

        float rx, ry, rz, tx, ty, tz = 0;
        float r = 10;
        float phi = -10;
        float theta = -100;
        float d = 800;
        float s = 1;
        int phase = 1;
        bool isAnimating = false;

        public Form1()
        {
            InitializeComponent();
            UpdateLabel();
            this.Width = 800;
            this.Height = 600;

            x_axis = new AxisX(3);
            y_axis = new AxisY(3);
            z_axis = new AxisZ(3);
            cube = new Cube(Color.Pink);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            List<Vector> vb;
            base.OnPaint(e);

            vb = new List<Vector>();
            vb = ViewingPipeline(x_axis.vb);
            x_axis.Draw(e.Graphics, vb);

            vb = new List<Vector>();
            vb = ViewingPipeline(y_axis.vb);
            y_axis.Draw(e.Graphics, vb);

            vb = new List<Vector>();
            vb = ViewingPipeline(z_axis.vb);
            z_axis.Draw(e.Graphics, vb);

            Matrix S = Matrix.Scale(s);
            Matrix T = Matrix.Translate(new Vector(tx, ty, tz));
            Matrix R = Matrix.RotateX(rx) * Matrix.RotateY(ry) * Matrix.RotateZ(rz);

            Matrix Total = T * R * S;
            vb = new List<Vector>();
            List<Vector> result = new List<Vector>();

            foreach(Vector v in cube.vertexbuffer) {
                Vector vd = Total * v;
                vb.Add(vd);
            }

            result = ViewingPipeline(vb);
            cube.Draw(e.Graphics, result);
        }

        public List<Vector> ViewingPipeline(List<Vector> vb) {
            List<Vector> res = new List<Vector>();
            Vector vp = new Vector();

            foreach (Vector v in vb) {

                Matrix view = Matrix.View(r, phi, theta);
                vp = view * v;

                Matrix projection = Matrix.Project(d, vp.z);
                vp = projection * vp;
                res.Add(vp);
            }

            return ViewportTransformation(800, 600, res);
        }

        public List<Vector> ViewportTransformation(float width, float height, List<Vector> vb)
        {
            List<Vector> result = new List<Vector>();
            float dx = width / 2;
            float dy = height / 2;
            foreach (Vector v in vb)
            {
                Vector v2 = new Vector(v.x + dx, dy - v.y);
                result.Add(v2);
            }
            return result;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
            else if (e.KeyCode == Keys.PageUp)
            {
                tz = tz + 1;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                tz = tz -1;
            }
            else if ( e.KeyCode == Keys.Left)
            {
                tx = tx + 1;
            }
            else if (e.KeyCode == Keys.Right)
            {
                tx = tx - 1;
            }
            else if (e.KeyCode == Keys.Up)
            {
                ty = ty + 1;
            }
            else if (e.KeyCode == Keys.Down)
            {
                ty = ty - 1;
            }
            this.Refresh();
            UpdateLabel();
        }

       private void timer1_Tick(object sender, EventArgs e)
        {
            if(phase == 1)
            {
                if (s < 1.5)
                {
                    s += 0.01f;
                } else
                {
                    phase = -1;
                }
                theta--;
            }
            else if (phase == -1)
            {
                if(s > 1)
                {
                    s -= 0.01f;
                } else
                {
                    phase = 2;
                    s = 1;
                }
                theta--;
            } else if(phase == 2)
            {
                if(rx < 45)
                {
                    rx++;
                } else
                {
                    phase = -2;
                }
                theta--;
            } else if(phase == -2)
            {
                if(rx > 0)
                {
                    rx--;
                } else
                {
                    phase = 3;
                    rx = 0;
                }
                theta--;
            } else if (phase == 3)
            {
                if (ry < 45)
                {
                    ry++;
                }
                else
                {
                    phase = -3;
                }
            } else if(phase == -3)
            {
                if (ry > 0)
                {
                    ry--;
                }
                else
                {
                    phase = 4;
                    ry = 0;
                }
                phi++;
            } else if(phase == 4)
            {
                if(phi > -10)
                {
                    phi--;
                }

                if(theta < -100)
                {
                    theta++;
                }

                if(phi == -10 && theta == -100)
                {
                    phase = 1;
                }
            }

            this.Refresh();
            UpdateLabel();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'c':
                    rx = ry = rz = tx = ty = tz = 0;
                    s = 1;
                    phi = -10;
                    theta = -100;
                    d = 800;
                    break;
                case 'x':
                    rx = rx + 1;
                    break;
                case 'X':
                    rx = rx - 1;
                    break;
                case 'y':
                    ry = ry + 1;
                    break;
                case 'Y':
                    ry = ry - 1;
                    break;
                case 'z':
                    rz = rz + 1;
                    break;
                case 'Z':
                    rz = rz - 1;
                    break;
                case 's':
                    s = s + 0.1f;
                    break;
                case 'S':
                    s = s -0.1f;
                    break;
                case 'a':
                case 'A':
                    if(isAnimating){
                        this.timer1.Stop();
                    } else {
                        this.timer1.Start();
                    }
                    isAnimating = !isAnimating;
                    break;
                case 't':
                    theta--;
                    break;
                case 'T':
                    theta++;
                    break;
                case 'P':
                    phi++;
                    break;
                case 'p':
                    phi--;
                    break;
                default:
                    break;
            }
            
            this.Refresh();
            UpdateLabel();
        } 

        private void UpdateLabel()
        {
            this.label1.Text =
                "Scale: " + s
                + "\nTranslate: ( " + tx + ", " + ty + ", " + tz + ")"
                + "\nRotateX: " + rx
                + "\nRotateY: " + ry
                + "\nRotateZ: " + rz
                + '\n'
                + "\nr: " + r
                + "\nd: " + d
                + "\nphi: " + phi
                + "\ntheta: " + theta
                + '\n'
                + "\nPhase: " + phase;
        }
    }
}
