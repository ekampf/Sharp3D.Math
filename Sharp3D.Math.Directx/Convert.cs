using System;

namespace Sharp3D.Math.DirectX
{
    /// <summary>
    /// Contains functions to convert DirectX structures to\from Sharp3D.Math structures.
    /// </summary>
    public sealed class Convert
    {
        #region Matrix Conversion
        /// <summary>
        /// Converts a <see cref="Sharp3D.Math.Core.Matrix4F"/> to <see cref="Microsoft.DirectX.Matrix"/>.
        /// </summary>
        /// <param name="value">The matrix value to convert.</param>
        /// <returns>A <see cref="Microsoft.DirectX.Matrix"/> value.</returns>
        public static Microsoft.DirectX.Matrix ToDirectX(Sharp3D.Math.Core.Matrix4F value)
        {
            Microsoft.DirectX.Matrix m = new Microsoft.DirectX.Matrix();
            // Since directx use column-major matrices we transpose our matrix.
            m.M11 = value[0, 0]; m.M12 = value[1, 0]; m.M13 = value[2, 0]; m.M14 = value[3, 0];
            m.M21 = value[0, 1]; m.M22 = value[1, 1]; m.M23 = value[2, 1]; m.M24 = value[3, 1];
            m.M31 = value[0, 2]; m.M32 = value[1, 2]; m.M33 = value[2, 2]; m.M34 = value[3, 2];
            m.M41 = value[0, 3]; m.M42 = value[1, 3]; m.M43 = value[2, 3]; m.M44 = value[3, 3];

            return m;
        }
        /// <summary>
        /// Converts a <see cref="Microsoft.DirectX.Matrix"/> to <see cref="Sharp3D.Math.Core.Matrix4F"/>.
        /// </summary>
        /// <param name="value">The matrix value to convert.</param>
        /// <returns>A <see cref="Sharp3D.Math.Core.Matrix4F"/> value.</returns>
        public static Sharp3D.Math.Core.Matrix4F FromDirectX(Microsoft.DirectX.Matrix value)
        {
            Sharp3D.Math.Core.Matrix4F m = new Sharp3D.Math.Core.Matrix4F();
            // Since directx use column-major matrices we transpose the matrix.
            m[0, 0] = value.M11; m[0, 1] = value.M21; m[0, 2] = value.M31; m[0, 2] = value.M41;
            m[1, 0] = value.M12; m[1, 1] = value.M22; m[1, 2] = value.M32; m[1, 2] = value.M42;
            m[2, 0] = value.M13; m[2, 1] = value.M23; m[2, 2] = value.M33; m[2, 2] = value.M43;
            m[3, 0] = value.M14; m[3, 1] = value.M24; m[3, 2] = value.M34; m[3, 2] = value.M44;

            return m;
        }
        #endregion

        #region Vector2 Conversion
        /// <summary>
        /// Converts a <see cref="Sharp3D.Math.Core.Vector2F"/> to <see cref="Microsoft.DirectX.Vector2"/>.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>A <see cref="Microsoft.DirectX.Vector2"/> value.</returns>
        public static Microsoft.DirectX.Vector2 ToDirectX(Sharp3D.Math.Core.Vector2F value)
        {
            return new Microsoft.DirectX.Vector2(value.X, value.Y);
        }
        /// <summary>
        /// Converts a <see cref="Microsoft.DirectX.Vector2"/> to <see cref="Sharp3D.Math.Core.Vector2F"/>.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>A <see cref="Sharp3D.Math.Core.Vector2F"/> value.</returns>
        public static Sharp3D.Math.Core.Vector2F FromDirectX(Microsoft.DirectX.Vector2 value)
        {
            return new Sharp3D.Math.Core.Vector2F(value.X, value.Y);
        }
        #endregion

        #region Vector3 Conversion
        /// <summary>
        /// Converts a <see cref="Sharp3D.Math.Core.Vector3F"/> to <see cref="Microsoft.DirectX.Vector3"/>.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>A <see cref="Microsoft.DirectX.Vector3"/> value.</returns>
        public static Microsoft.DirectX.Vector3 ToDirectX(Sharp3D.Math.Core.Vector3F value)
        {
            return new Microsoft.DirectX.Vector3(value.X, value.Y, value.Z);
        }
        /// <summary>
        /// Converts a <see cref="Microsoft.DirectX.Vector3"/> to <see cref="Sharp3D.Math.Core.Vector3F"/>.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>A <see cref="Sharp3D.Math.Core.Vector3F"/> value.</returns>
        public static Sharp3D.Math.Core.Vector3F FromDirectX(Microsoft.DirectX.Vector3 value)
        {
            return new Sharp3D.Math.Core.Vector3F(value.X, value.Y, value.Z);
        }
        #endregion

        #region Vector4 Conversion
        /// <summary>
        /// Converts a <see cref="Sharp3D.Math.Core.Vector4F"/> to <see cref="Microsoft.DirectX.Vector4"/>.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>A <see cref="Microsoft.DirectX.Vector4"/> value.</returns>
        public static Microsoft.DirectX.Vector4 ToDirectX(Sharp3D.Math.Core.Vector4F value)
        {
            return new Microsoft.DirectX.Vector4(value.X, value.Y, value.Z, value.W);
        }
        /// <summary>
        /// Converts a <see cref="Microsoft.DirectX.Vector4"/> to <see cref="Sharp3D.Math.Core.Vector4F"/>.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>A <see cref="Sharp3D.Math.Core.Vector4F"/> value.</returns>
        public static Sharp3D.Math.Core.Vector4F FromDirectX(Microsoft.DirectX.Vector4 value)
        {
            return new Sharp3D.Math.Core.Vector4F(value.X, value.Y, value.Z, value.W);
        }
        #endregion

        #region Quaternion Conversion
        /// <summary>
        /// Converts a <see cref="Sharp3D.Math.Core.QuaternionF"/> to <see cref="Microsoft.DirectX.Quaternion"/>.
        /// </summary>
        /// <param name="value">The matrix value to convert.</param>
        /// <returns>A <see cref="Microsoft.DirectX.Quaternion"/> value.</returns>
        public static Microsoft.DirectX.Quaternion ToDirectX(Sharp3D.Math.Core.QuaternionF value)
        {
            return new Microsoft.DirectX.Quaternion(value.X, value.Y, value.Z, value.W);
        }
        /// <summary>
        /// Converts a <see cref="Microsoft.DirectX.Quaternion"/> to <see cref="Sharp3D.Math.Core.QuaternionF"/>.
        /// </summary>
        /// <param name="value">The matrix value to convert.</param>
        /// <returns>A <see cref="Sharp3D.Math.Core.QuaternionF"/> value.</returns>
        public static Sharp3D.Math.Core.QuaternionF FromDirectX(Microsoft.DirectX.Quaternion value)
        {
            return new Sharp3D.Math.Core.QuaternionF(value.X, value.Y, value.Z, value.W);
        }
        #endregion

        #region Private Constructor
        private Convert()
        {
        }
        #endregion
    }
}
