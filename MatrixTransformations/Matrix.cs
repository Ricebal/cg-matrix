using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    class Matrix
    {
        float[,] mat = new float[2, 2];

        public Matrix() : this(new float[,]{
            {1, 0, 0},
            {0, 1, 0},
            {0, 0, 1}
        })
        { }
        public Matrix(float m11, float m12,
                      float m21, float m22) :
                      this(new float[,] {
                             { m11, m12 },
                             { m21, m22 }
                          })
        { }

        public Matrix(Vector v) : this(v.x, 0, v.y, 0) { }

        public Matrix(float[,] mat)
        {
            this.mat = mat;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            Matrix result = new Matrix();
            for (int i = 0; i < m1.mat.GetLength(0); i++)
            {
                for (int j = 0; j < m1.mat.GetLength(1); j++)
                {
                    result.mat[i, j] = m1.mat[i, j] + m2.mat[i, j];
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            Matrix result = new Matrix();
            for (int i = 0; i < m1.mat.GetLength(0); i++)
            {
                for (int j = 0; j < m1.mat.GetLength(1); j++)
                {
                    result.mat[i, j] = m1.mat[i, j] - m2.mat[i, j];
                }
            }
            return result;
        }
        public static Matrix operator *(Matrix m1, float f)
        {
            Matrix result = new Matrix();
            for (int i = 0; i < m1.mat.GetLength(0); i++)
            {
                for (int j = 0; j < m1.mat.GetLength(1); j++)
                {
                    result.mat[i, j] = m1.mat[i, j] * f;
                }
            }
            return result;
        }

        public static Matrix operator *(float f, Matrix m1)
        {
            Matrix result = new Matrix();
            for (int i = 0; i < m1.mat.GetLength(0); i++)
            {
                for (int j = 0; j < m1.mat.GetLength(1); j++)
                {
                    result.mat[i, j] = m1.mat[i, j] * f;
                }
            }
            return result;
        }

        public static Matrix Translate(Vector t)
        {
            Matrix result = new Matrix();



            return result;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix res = new Matrix();
            if (m1.mat.GetLength(0) == m2.mat.GetLength(1))
            {

                for (int i = 0; i < m1.mat.GetLength(0); i++)
                {
                    for (int j = 0; j < m1.mat.GetLength(1); j++)
                    {
                        res.mat[i, j] = 0;
                        for (int k = 0; k < m1.mat.GetLength(0); k++)
                            res.mat[i, j] += m1.mat[i, k] * m2.mat[k, j];
                    }
                }

                return res;
            }
            else
            {
                Console.WriteLine("poop");
                return res;
            }
        }

        public static Vector operator *(Matrix m1, Vector v)
        {
            Matrix vm = new Matrix(v);
            Matrix m = m1 * vm;

            return new Vector(m.mat[0, 0], m.mat[1, 0]);
        }

        public static Matrix Identity()
        {
            return new Matrix();
        }

        public static Matrix Scale(float s)
        {
            return s * Identity();
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < this.mat.GetLength(0); i++)
            {
                for (int j = 0; j < this.mat.GetLength(1); j++)
                {
                    result += this.mat[i, j] + " ";
                }
                result += '\n';
            }
            return result;
        }
    }
}
