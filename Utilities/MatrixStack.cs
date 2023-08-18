using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.Utilities
{
    public class MatrixStack
    {
        private Matrix Top;
        private readonly List<Matrix> matrixStack;

        public MatrixStack() { 
            matrixStack = new List<Matrix>();
            Matrix newTop = Matrix.Identity;
            Top = newTop;
            matrixStack.Add(newTop);
        }

        /// <summary>
        /// Adds a new Matrix to the Stack, cloning the top matrix.
        /// </summary>
        public void Push()
        {
            matrixStack.Add(Top);
        }

        /// <summary>
        /// Removes the top matrix from the Stack.
        /// </summary>
        public void Pop()
        {
            if (matrixStack.Count > 0)
            {
                Top = matrixStack[matrixStack.Count - 1];
                matrixStack.RemoveAt(matrixStack.Count - 1);
            }
        }

        /// <summary>
        /// Returns the Top Matrix in the MatrixStack.
        /// </summary>
        /// <returns>The Top Matrix in the stack as a Matrix object.</returns>
        public Matrix Peek()
        {
            return this.Top;
        }

        public void Multiply(Matrix matrix)
        {
            Matrix.Multiply(ref Top, ref matrix, out Top);
        }

        public void MultiplyLocal(Matrix matrix)
        {
            Matrix.Multiply(ref matrix, ref Top, out Top);
        }

        public void Rotate(float yaw, float pitch, float roll)
        {
            Matrix tmp;
            Matrix.CreateFromYawPitchRoll(yaw, pitch, roll, out tmp);
            Matrix.Multiply(ref Top, ref tmp, out Top);
        }

        public void RotateYawPitchRollLocal(float yaw, float pitch, float roll)
        {
            Matrix tmp;
            Matrix.CreateFromYawPitchRoll(yaw, pitch, roll, out tmp);
            Matrix.Multiply(ref tmp, ref Top, out Top);
        }
        public void Scale(float X, float Y, float Z)
        {
            Matrix tmp;
            Matrix.CreateScale(X, Y, Z, out tmp);
            Matrix.Multiply(ref Top, ref tmp, out Top);
        }

        public void ScaleLocal(float X, float Y, float Z)
        {
            Matrix tmp;
            Matrix.CreateScale(X, Y, Z, out tmp);
            Matrix.Multiply(ref tmp, ref Top, out Top);
        }

        public void Translate(float X, float Y, float Z)
        {
            Matrix tmp;
            Matrix.CreateTranslation(X, Y, Z, out tmp);
            Matrix.Multiply(ref Top, ref tmp, out Top);
        }

        public void TranslateLocal(float X, float Y, float Z)
        {
            Matrix tmp;
            Matrix.CreateTranslation(X, Y, Z, out tmp);
            Matrix.Multiply(ref tmp, ref Top, out Top);
        }
    }
}
