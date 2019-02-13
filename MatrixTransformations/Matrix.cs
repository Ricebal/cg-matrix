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

        public Matrix() : this(1, 0, 0, 1) { }
        public Matrix(float m11, float m12,
                      float m21, float m22)
        {
            mat[0, 0] = m11; mat[0, 1] = m12;
            mat[1, 0] = m21; mat[1, 1] = m22;
        }

        public Matrix(Vector v) : this(v.x, 0, v.y, 0) { }

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
            throw new NotImplementedException();
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            return new Matrix(
                           m1.mat[0, 0] * m2.mat[0, 0] + m1.mat[0, 1] * m2.mat[1, 0],
                           m1.mat[0, 0] * m2.mat[0, 1] + m1.mat[0, 1] * m2.mat[1, 1],
                           m1.mat[1, 0] * m2.mat[0, 0] + m1.mat[1, 1] * m2.mat[1, 0],
                           m1.mat[1, 0] * m2.mat[0, 1] + m1.mat[1, 1] * m2.mat[1, 1]);
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
