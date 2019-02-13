﻿using System;
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
        Square square;
        Square square2;

        public Form1()
        {
            InitializeComponent();

            Vector v1 = new Vector();
            Console.WriteLine(v1);
            Vector v2 = new Vector(1, 2);
            Console.WriteLine(v2);
            Vector v3 = new Vector(2, 6);
            Console.WriteLine(v3);
            Vector v4 = v2 + v3;
            Console.WriteLine(v4); // 3, 8

            Matrix m1 = new Matrix();
            Console.WriteLine(m1); // 1, 0, 0, 1
            Matrix m2 = new Matrix(
                2, 4,
                -1, 3);
            Console.WriteLine(m2);
            // Console.WriteLine(m1 + m2); // 3, 4, -1, 4
            // Console.WriteLine(m1 - m2); // -1, -4, 1, -2
            Console.WriteLine(m2 * m2); // 0, 20, -5, 5

            Console.WriteLine(m2 * v3); // 28, 16

            this.Width = 800;
            this.Height = 600;

            x_axis = new AxisX(200);
            y_axis = new AxisY(200);
            square = new Square(Color.Purple, 100);
            square2 = new Square(Color.Orange, 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            List<Vector> vb;
            base.OnPaint(e);

            x_axis.vb = ViewportTransformation(800, 600, x_axis.vb);
            x_axis.Draw(e.Graphics, x_axis.vb);

            y_axis.vb = ViewportTransformation(800, 600, y_axis.vb);
            y_axis.Draw(e.Graphics, y_axis.vb);

            square.vb = ViewportTransformation(800, 600, square.vb);
            square.Draw(e.Graphics, square.vb);

            Matrix s = Matrix.Scale(1.5f);
            vb = new List<Vector>();
            foreach (Vector v in square2.vb)
            {
                Vector v2 = s * v;
                vb.Add(v2);
                Console.WriteLine(v2);
            }

            vb = ViewportTransformation(800, 600, vb);
            square2.Draw(e.Graphics, vb);
        }

        public static List<Vector> ViewportTransformation(float width, float height, List<Vector> vb)
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
        }
    }
}
