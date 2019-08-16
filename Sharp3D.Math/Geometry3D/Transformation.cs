using System;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry3D
{
	public sealed class Transformation
	{
		public static Matrix4F RotateX(double angle)
		{
			float sin = (float)System.Math.Sin(angle);
			float cos = (float)System.Math.Cos(angle);

			return new Matrix4F(
				1,	0,	0,	 0,
				0,	cos,-sin,0,
				0,	sin, cos,0,
				0,	0,	 0,	 1);
		}
		public static Matrix4F RotateY(double angle)
		{
			float sin = (float)System.Math.Sin(angle);
			float cos = (float)System.Math.Cos(angle);

			return new Matrix4F(
				cos,	0,	sin,	0,
				0,		1,  0,		0,
				-sin,	0,  cos,	0,
				0,		0,	0,		1);
		}
		public static Matrix4F RotateZ(double angle)
		{
			float sin = (float)System.Math.Sin(angle);
			float cos = (float)System.Math.Cos(angle);

			return new Matrix4F(
				cos,	-sin,	0,	0,
				sin,	cos,	0,	0,
				0,		0,		1,	0,
				0,		0,		0,	1);
		}


		#region Private Constructor
		private Transformation()
		{}
		#endregion
	}
}
