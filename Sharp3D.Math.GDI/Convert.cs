using System;

namespace Sharp3D.Math.GDI
{
	/// <summary>
	/// Contains functions to convert GDI+ structures to\from Sharp3D.Math structures.
	/// </summary>
	public sealed class Convert
	{
		public System.Drawing.PointF ToGDI(Sharp3D.Math.Core.Vector2F value)
		{
			return new System.Drawing.PointF(value.X, value.Y);
		}
		public Sharp3D.Math.Core.Vector2F FromGDI(System.Drawing.PointF value)
		{
			return new Sharp3D.Math.Core.Vector2F(value.X, value.Y);
		}

		#region Private Constructor
		private Convert()
		{
		}
		#endregion
	}
}
