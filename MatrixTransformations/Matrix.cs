using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    class Matrix
    {
        float[,] mat = new float[4, 4];

        public Matrix() : this(new float[,]{
            {1, 0, 0, 0},
            {0, 1, 0, 0},
            {0, 0, 1, 0},
            {0, 0, 0, 1}
        })
        { }
        public Matrix(float m11, float m12,
                      float m21, float m22) :
                      this(new float[,] {
                             { m11, m12 },
                             { m21, m22 }
                          })
        { }

        public Matrix(Vector v) : this(new float[,]{
            {v.x, 0, 0, 0},
            {v.y, 0, 0, 0},
            {v.z, 0, 0, 0},
            {v.w, 0, 0, 0}
        })
        { }

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
            Matrix translationMatrix = Matrix.Identity();
            translationMatrix.mat[0, 3] = t.x;
            translationMatrix.mat[1, 3] = t.y;
            translationMatrix.mat[2, 3] = t.z;
            translationMatrix.mat[3, 3] = 1;

            return translationMatrix;
        }

        public static Matrix RotateZ(float degrees)
        {
            double rad = degrees * (Math.PI / 180);

            Matrix rotationMatrix = Matrix.Identity();
            rotationMatrix.mat[0, 0] = (float)Math.Cos(rad);
            rotationMatrix.mat[0, 1] = -(float)Math.Sin(rad);
            rotationMatrix.mat[1, 0] = (float)Math.Sin(rad);
            rotationMatrix.mat[1, 1] = (float)Math.Cos(rad);

            return rotationMatrix;
        }

        public static Matrix RotateX(float degrees)
        {
            double rad = degrees * (Math.PI / 180);

            Matrix rotationMatrix = Matrix.Identity();
            rotationMatrix.mat[1, 1] = (float)Math.Cos(rad);
            rotationMatrix.mat[1, 2] = -(float)Math.Sin(rad);
            rotationMatrix.mat[2, 1] = (float)Math.Sin(rad);
            rotationMatrix.mat[2, 2] = (float)Math.Cos(rad);

            return rotationMatrix;
        }

        public static Matrix RotateY(float degrees)
        {
            double rad = degrees * (Math.PI / 180);

            Matrix rotationMatrix = Matrix.Identity();
            rotationMatrix.mat[0, 0] = (float)Math.Cos(rad);
            rotationMatrix.mat[0, 2] = (float)Math.Sin(rad);
            rotationMatrix.mat[2, 0] = -(float)Math.Sin(rad);
            rotationMatrix.mat[2, 2] = (float)Math.Cos(rad);

            return rotationMatrix;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix res = new Matrix();
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

        public static Vector operator *(Matrix m1, Vector v)
        {
            Matrix vm = new Matrix(v);
            Matrix m = m1 * vm;

            return new Vector(m.mat[0, 0], m.mat[1, 0], m.mat[2, 0], 1);
        }

        public static Matrix Identity()
        {
            return new Matrix();
        }

        public static Matrix Scale(float s)
        {
            return s * Identity();
        }

        public static Matrix Project(float distance, float z) {
            Matrix result = new Matrix(new float[,]{
                {-(distance/z), 0, 0, 0},
                {0, -(distance/z), 0, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0}
            });
            return result;
        }

        public static Matrix View(float r, float phi, float theta) {
            float rPhi = phi / 180f * (float)Math.PI;
            float rTheta = theta / 180f * (float)Math.PI;

            Matrix result = new Matrix(new float[,]{
                {-(float)Math.Sin(rTheta), (float)Math.Cos(rTheta), 0, 0},
                {-(float)Math.Cos(rTheta) * (float)Math.Cos(rPhi), -(float)Math.Cos(rPhi) * (float)Math.Sin(rTheta), (float)Math.Sin(rPhi), 0},
                {(float)Math.Cos(rTheta) * (float)Math.Sin(rPhi), (float)Math.Sin(rTheta) * (float)Math.Sin(rPhi), (float)Math.Cos(rPhi), -r},
                {0, 0, 0, 1}
            });

            return result;
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
