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

        public Form1()
        {
            InitializeComponent();

            this.Width = 800;
            this.Height = 600;

            x_axis = new AxisX(200);
            y_axis = new AxisY(200);
            z_axis = new AxisZ(200);
            square = new Square(Color.Purple, 100);
            square2 = new Square(Color.Orange, 100);
            square3 = new Square(Color.Green, 100);
            square4 = new Square(Color.Pink, 100);
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
                Console.WriteLine("pageup");
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
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
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
                case 't':
                    theta--;
                    Console.WriteLine(theta);
                    break;
                case 'T':
                    theta++;
                    Console.WriteLine(theta);
                    break;
                case 'P':
                    phi++;
                    Console.WriteLine(theta);
                    break;
                case 'p':
                    phi--;
                    Console.WriteLine(theta);
                    break;
                default:
                    Console.WriteLine(e.KeyChar);
                    break;
            }
            
            this.Refresh();
        } 
    }
}
