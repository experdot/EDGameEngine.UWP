using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FarseerPhysics.Portable.Common
{
    internal static class Vector2Extensions
    {
        public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount)
        {
            return new Vector2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4,
                                      float amount, out Vector2 result)
        {
            result = new Vector2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        public static void Transform(ref Vector2 position, ref Matrix4x4 matrix, out Vector2 result)
        {
            result = new Vector2((position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
                                 (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42);
        }

        public static void Transform(Vector2[] sourceArray, ref Matrix4x4 matrix, Vector2[] destinationArray)
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                Transform(ref sourceArray[i], ref matrix, out destinationArray[i]);
            }
        }
    }
}
