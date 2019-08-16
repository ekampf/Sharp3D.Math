#region Sharp3D.Math, Copyright(C) 2003-2004 Eran Kampf, Licensed under LGPL.
//	Sharp3D.Math math library
//	Copyright (C) 2003-2004  
//	Eran Kampf
//	tentacle@zahav.net.il
//	http://tentacle.flipcode.com
//
//	This library is free software; you can redistribute it and/or
//	modify it under the terms of the GNU Lesser General Public
//	License as published by the Free Software Foundation; either
//	version 2.1 of the License, or (at your option) any later version.
//
//	This library is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//	Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
#endregion
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Represents a 4-dimentional single-precision floating point matrix class.
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix4F : ISerializable, ICloneable
	{
		#region Private Fields
		private float _m11, _m12, _m13, _m14;
		private float _m21, _m22, _m23, _m24;
		private float _m31, _m32, _m33, _m34;
		private float _m41, _m42, _m43, _m44;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix4F"/> structure with the specified values.
		/// </summary>
		public Matrix4F(
			float m11, float m12, float m13, float m14,
			float m21, float m22, float m23, float m24,
			float m31, float m32, float m33, float m34,
			float m41, float m42, float m43, float m44
			)
		{
			_m11 = m11; _m12 = m12; _m13 = m13; _m14 = m14;
			_m21 = m21; _m22 = m22; _m23 = m23; _m24 = m24;
			_m31 = m31; _m32 = m32; _m33 = m33; _m34 = m34;
			_m41 = m41; _m42 = m42; _m43 = m43; _m44 = m44;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix4F"/> structure with the specified values.
		/// </summary>
		/// <param name="elements">An array containing the matrix values in row-major order.</param>
		public Matrix4F(float[] elements)
		{
			Debug.Assert(elements != null);
			Debug.Assert(elements.Length >= 16);

			_m11 = elements[0]; _m12 = elements[1]; _m13 = elements[2]; _m14 = elements[3];
			_m21 = elements[4]; _m22 = elements[5]; _m23 = elements[6]; _m24 = elements[7];
			_m31 = elements[8]; _m32 = elements[9]; _m33 = elements[10]; _m34 = elements[11];
			_m41 = elements[12]; _m42 = elements[13]; _m43 = elements[14]; _m44 = elements[15];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix4F"/> structure with the specified values.
		/// </summary>
		/// <param name="column1">A <see cref="Vector2D"/> instance holding values for the first column.</param>
		/// <param name="column2">A <see cref="Vector2D"/> instance holding values for the second column.</param>
		/// <param name="column3">A <see cref="Vector2D"/> instance holding values for the third column.</param>
		/// <param name="column4">A <see cref="Vector2D"/> instance holding values for the fourth column.</param>
		public Matrix4F(Vector4F column1, Vector4F column2, Vector4F column3, Vector4F column4)
		{
			_m11 = column1.X; _m12 = column2.X; _m13 = column3.X; _m14 = column4.X;
			_m21 = column1.Y; _m22 = column2.Y; _m23 = column3.Y; _m24 = column4.Y;
			_m31 = column1.Z; _m32 = column2.Z; _m33 = column3.Z; _m34 = column4.Z;
			_m41 = column1.W; _m42 = column2.W; _m43 = column3.W; _m44 = column4.W;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix4F"/> class using a given matrix.
		/// </summary>
		public Matrix4F(Matrix4F m)
		{
			_m11 = m.M11; _m12 = m.M12; _m13 = m.M13; _m14 = m.M14;
			_m21 = m.M21; _m22 = m.M22; _m23 = m.M23; _m24 = m.M24;
			_m31 = m.M31; _m32 = m.M32; _m33 = m.M33; _m34 = m.M34;
			_m41 = m.M41; _m42 = m.M42; _m43 = m.M43; _m44 = m.M44;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix4F"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		private Matrix4F(SerializationInfo info, StreamingContext context)
		{
			// Get the first row
			_m11 = info.GetSingle("M11");
			_m12 = info.GetSingle("M12");
			_m13 = info.GetSingle("M13");
			_m14 = info.GetSingle("M14");

			// Get the second row
			_m21 = info.GetSingle("M21");
			_m22 = info.GetSingle("M22");
			_m23 = info.GetSingle("M23");
			_m24 = info.GetSingle("M24");

			// Get the third row
			_m31 = info.GetSingle("M31");
			_m32 = info.GetSingle("M32");
			_m33 = info.GetSingle("M33");
			_m34 = info.GetSingle("M34");
		
			// Get the fourth row
			_m41 = info.GetSingle("M41");
			_m42 = info.GetSingle("M42");
			_m43 = info.GetSingle("M43");
			_m44 = info.GetSingle("M44");
		}
		#endregion

		#region Constants
		/// <summary>
		/// 4-dimentional single-precision floating point zero matrix.
		/// </summary>
		public static readonly Matrix4F Zero = new Matrix4F(0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
		/// <summary>
		/// 4-dimentional single-precision floating point identity matrix.
		/// </summary>
		public static readonly Matrix4F Identity = new Matrix4F(
			1,0,0,0,
			0,1,0,0,
			0,0,1,0,
			0,0,0,1
			);
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Matrix4F"/> object.
		/// </summary>
		/// <returns>The <see cref="Matrix4F"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Matrix4F(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Matrix4F"/> object.
		/// </summary>
		/// <returns>The <see cref="Matrix4F"/> object this method creates.</returns>
		public Matrix4F Clone()
		{
			return new Matrix4F(this);
		}
		#endregion

		#region ISerializable Members
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> with the data needed to serialize this object.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> to populate with data. </param>
		/// <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			// First row
			info.AddValue("M11", _m11);
			info.AddValue("M12", _m12);
			info.AddValue("M13", _m13);
			info.AddValue("M14", _m14);

			// Second row
			info.AddValue("M21", _m21);
			info.AddValue("M22", _m22);
			info.AddValue("M23", _m23);
			info.AddValue("M24", _m24);

			// Third row
			info.AddValue("M31", _m31);
			info.AddValue("M32", _m32);
			info.AddValue("M33", _m33);
			info.AddValue("M34", _m34);

			// Fourth row
			info.AddValue("M41", _m41);
			info.AddValue("M42", _m42);
			info.AddValue("M43", _m43);
			info.AddValue("M44", _m44);
		}
		#endregion

		#region Public Static Vector Arithmetics
		/// <summary>
		/// Adds two matrices.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="b">A <see cref="Matrix4F"/> instance.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the sum.</returns>
		public static Matrix4F Add(Matrix4F a, Matrix4F b)
		{
			return new Matrix4F(
				a.M11 + b.M11, a.M12 + b.M12, a.M13 + b.M13, a.M14 + b.M14,
				a.M21 + b.M21, a.M22 + b.M22, a.M23 + b.M23, a.M24 + b.M24,
				a.M31 + b.M31, a.M32 + b.M32, a.M33 + b.M33, a.M34 + b.M34,
				a.M41 + b.M41, a.M42 + b.M42, a.M43 + b.M43, a.M44 + b.M44
				);
		}
		/// <summary>
		/// Adds a matrix and a scalar.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the sum.</returns>
		public static Matrix4F Add(Matrix4F a, float s)
		{
			return new Matrix4F(
				a.M11 + s, a.M12 + s, a.M13 + s, a.M14 + s,
				a.M21 + s, a.M22 + s, a.M23 + s, a.M24 + s,
				a.M31 + s, a.M32 + s, a.M33 + s, a.M34 + s,
				a.M41 + s, a.M42 + s, a.M43 + s, a.M44 + s
				);
		}
		/// <summary>
		/// Adds two matrices and put the result in a third matrix.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="b">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="result">A <see cref="Matrix4F"/> instance to hold the result.</param>
		public static void Add(Matrix4F a, Matrix4F b, Matrix4F result)
		{
			result.M11 = a.M11 + b.M11;
			result.M12 = a.M12 + b.M12;
			result.M13 = a.M13 + b.M13;
			result.M14 = a.M14 + b.M14;

			result.M21 = a.M21 + b.M21;
			result.M22 = a.M22 + b.M22;
			result.M23 = a.M23 + b.M23;
			result.M24 = a.M24 + b.M24;

			result.M31 = a.M31 + b.M31;
			result.M32 = a.M32 + b.M32;
			result.M33 = a.M33 + b.M33;
			result.M34 = a.M34 + b.M34;
		
			result.M41 = a.M41 + b.M41;
			result.M42 = a.M42 + b.M42;
			result.M43 = a.M43 + b.M43;
			result.M44 = a.M44 + b.M44;
		}
		/// <summary>
		/// Adds a matrix and a scalar and put the result in a third matrix.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="result">A <see cref="Matrix4F"/> instance to hold the result.</param>
		public static void Add(Matrix4F a, float s, Matrix4F result)
		{
			result.M11 = a.M11 + s;
			result.M12 = a.M12 + s;
			result.M13 = a.M13 + s;
			result.M14 = a.M14 + s;

			result.M21 = a.M21 + s;
			result.M22 = a.M22 + s;
			result.M23 = a.M23 + s;
			result.M24 = a.M24 + s;

			result.M31 = a.M31 + s;
			result.M32 = a.M32 + s;
			result.M33 = a.M33 + s;
			result.M34 = a.M34 + s;
		
			result.M41 = a.M41 + s;
			result.M42 = a.M42 + s;
			result.M43 = a.M43 + s;
			result.M44 = a.M44 + s;
		}
		/// <summary>
		/// Subtracts a matrix from a matrix.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="b">A <see cref="Matrix4F"/> instance.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the difference.</returns>
		public static Matrix4F Subtract(Matrix4F a, Matrix4F b)
		{
			return new Matrix4F(
				a.M11 - b.M11, a.M12 - b.M12, a.M13 - b.M13, a.M14 - b.M14,
				a.M21 - b.M21, a.M22 - b.M22, a.M23 - b.M23, a.M24 - b.M24,
				a.M31 - b.M31, a.M32 - b.M32, a.M33 - b.M33, a.M34 - b.M34,
				a.M41 - b.M41, a.M42 - b.M42, a.M43 - b.M43, a.M44 - b.M44
				);
		}
		/// <summary>
		/// Subtracts a scalar from a matrix.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the difference.</returns>
		public static Matrix4F Subtract(Matrix4F a, float s)
		{
			return new Matrix4F(
				a.M11 - s, a.M12 - s, a.M13 - s, a.M14 - s,
				a.M21 - s, a.M22 - s, a.M23 - s, a.M24 - s,
				a.M31 - s, a.M32 - s, a.M33 - s, a.M34 - s,
				a.M41 - s, a.M42 - s, a.M43 - s, a.M44 - s
				);
		}
		/// <summary>
		/// Subtracts a matrix from a matrix and put the result in a third matrix.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="b">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="result">A <see cref="Matrix4F"/> instance to hold the result.</param>
		public static void Subtract(Matrix4F a, Matrix4F b, Matrix4F result)
		{
			result.M11 = a.M11 - b.M11;
			result.M12 = a.M12 - b.M12;
			result.M13 = a.M13 - b.M13;
			result.M14 = a.M14 - b.M14;

			result.M21 = a.M21 - b.M21;
			result.M22 = a.M22 - b.M22;
			result.M23 = a.M23 - b.M23;
			result.M24 = a.M24 - b.M24;

			result.M31 = a.M31 - b.M31;
			result.M32 = a.M32 - b.M32;
			result.M33 = a.M33 - b.M33;
			result.M34 = a.M34 - b.M34;
		
			result.M41 = a.M41 - b.M41;
			result.M42 = a.M42 - b.M42;
			result.M43 = a.M43 - b.M43;
			result.M44 = a.M44 - b.M44;
		}
		/// <summary>
		/// Subtracts a scalar from a matrix and put the result in a third matrix.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="result">A <see cref="Matrix4F"/> instance to hold the result.</param>
		public static void Subtract(Matrix4F a, float s, Matrix4F result)
		{
			result.M11 = a.M11 - s;
			result.M12 = a.M12 - s;
			result.M13 = a.M13 - s;
			result.M14 = a.M14 - s;

			result.M21 = a.M21 - s;
			result.M22 = a.M22 - s;
			result.M23 = a.M23 - s;
			result.M24 = a.M24 - s;

			result.M31 = a.M31 - s;
			result.M32 = a.M32 - s;
			result.M33 = a.M33 - s;
			result.M34 = a.M34 - s;
		
			result.M41 = a.M41 - s;
			result.M42 = a.M42 - s;
			result.M43 = a.M43 - s;
			result.M44 = a.M44 - s;
		}
		/// <summary>
		/// Multiplies two matrices.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="b">A <see cref="Matrix4F"/> instance.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the result.</returns>
		public static Matrix4F Multiply(Matrix4F a, Matrix4F b)
		{
			return new Matrix4F(
				a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
				a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
				a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
				a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,

				a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
				a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
				a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
				a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,

				a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
				a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42, 
				a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
				a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,

				a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
				a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42, 
				a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
				a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44
				);
		}
		/// <summary>
		/// Multiplies two matrices and put the result in a third matrix.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="b">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="result">A <see cref="Matrix4F"/> instance to hold the result.</param>
		public static void Multiply(Matrix4F a, Matrix4F b, Matrix4F result)
		{
			result.M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41;
			result.M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42;
			result.M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43;
			result.M14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44;

			result.M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41;
			result.M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42;
			result.M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43;
			result.M24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44;

			result.M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41;
			result.M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42; 
			result.M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43;
			result.M34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44;

			result.M41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41;
			result.M42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42; 
			result.M43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43;
			result.M44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44;
		}		
		/// <summary>
		/// Transposes a matrix.
		/// </summary>
		/// <param name="m">A <see cref="Matrix4F"/> instance.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the transposed matrix.</returns>
		public static Matrix4F Transpose(Matrix4F m)
		{
			Matrix4F t = new Matrix4F(m);
			t.Transpose();
			return t;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the value of the [1,1] matrix element.
		/// </summary>
		public float M11
		{
			get { return _m11; }
			set { _m11 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [1,2] matrix element.
		/// </summary>
		public float M12
		{
			get { return _m12; }
			set { _m12 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [1,3] matrix element.
		/// </summary>
		public float M13
		{
			get { return _m13; }
			set { _m13 = value;}
		}

		/// <summary>
		/// Gets or sets the value of the [1,4] matrix element.
		/// </summary>
		public float M14
		{
			get { return _m14; }
			set { _m14 = value;}
		}


		/// <summary>
		/// Gets or sets the value of the [2,1] matrix element.
		/// </summary>
		public float M21
		{
			get { return _m21; }
			set { _m21 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [2,2] matrix element.
		/// </summary>
		public float M22
		{
			get { return _m22; }
			set { _m22 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [2,3] matrix element.
		/// </summary>
		public float M23
		{
			get { return _m23; }
			set { _m23 = value;}
		}

		/// <summary>
		/// Gets or sets the value of the [2,4] matrix element.
		/// </summary>
		public float M24
		{
			get { return _m24; }
			set { _m24 = value;}
		}


		/// <summary>
		/// Gets or sets the value of the [3,1] matrix element.
		/// </summary>
		public float M31
		{
			get { return _m31; }
			set { _m31 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [3,2] matrix element.
		/// </summary>
		public float M32
		{
			get { return _m32; }
			set { _m32 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [3,3] matrix element.
		/// </summary>
		public float M33
		{
			get { return _m33; }
			set { _m33 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [3,4] matrix element.
		/// </summary>
		public float M34
		{
			get { return _m34; }
			set { _m34 = value;}
		}


		/// <summary>
		/// Gets or sets the value of the [4,1] matrix element.
		/// </summary>
		public float M41
		{
			get { return _m41; }
			set { _m41 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [4,2] matrix element.
		/// </summary>
		public float M42
		{
			get { return _m42; }
			set { _m42 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [4,3] matrix element.
		/// </summary>
		public float M43
		{
			get { return _m43; }
			set { _m43 = value;}
		}
		/// <summary>
		/// Gets or sets the value of the [4,4] matrix element.
		/// </summary>
		public float M44
		{
			get { return _m44; }
			set { _m44 = value;}
		}
		#endregion

		#region Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return 
				_m11.GetHashCode() ^ _m12.GetHashCode() ^ _m13.GetHashCode() ^ _m14.GetHashCode() ^
				_m21.GetHashCode() ^ _m22.GetHashCode() ^ _m23.GetHashCode() ^ _m24.GetHashCode() ^
				_m31.GetHashCode() ^ _m32.GetHashCode() ^ _m33.GetHashCode() ^ _m34.GetHashCode() ^
				_m41.GetHashCode() ^ _m42.GetHashCode() ^ _m43.GetHashCode() ^ _m44.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>True if <paramref name="obj"/> is a <see cref="Vector2D"/> and has the same values as this instance; otherwise, False.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix4F)
			{
				Matrix4F m = (Matrix4F)obj;
				return 
					(_m11 == m.M11) && (_m12 == m.M12) && (_m13 == m.M13) && (_m14 == m.M14) &&
					(_m21 == m.M21) && (_m22 == m.M22) && (_m23 == m.M23) && (_m24 == m.M24) &&
					(_m31 == m.M31) && (_m32 == m.M32) && (_m33 == m.M33) && (_m34 == m.M34) &&
					(_m41 == m.M41) && (_m42 == m.M42) && (_m43 == m.M43) && (_m44 == m.M44);
			}
			return false;
		}

		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			StringBuilder s = new StringBuilder();
			s.Append(String.Format( "|{0}, {1}, {3}, {4}|\n", _m11, _m12, _m13, _m14));
			s.Append(String.Format( "|{0}, {1}, {3}, {4}|\n", _m21, _m22, _m23, _m24));
			s.Append(String.Format( "|{0}, {1}, {3}, {4}|\n", _m31, _m32, _m33, _m34));
			s.Append(String.Format( "|{0}, {1}, {3}, {4}|\n", _m41, _m42, _m43, _m44));

			return s.ToString();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Calculates the determinant value of the matrix.
		/// </summary>
		/// <returns>The determinant value of the matrix.</returns>
		public float Determinant()
		{
			float det = 0.0f;
			for (int col = 0; col < 4; col++)
			{
				if ((col % 2) == 0)
					det += this[0, col] * Minor(0, col).Determinant();
				else
					det -= this[0, col] * Minor(0, col).Determinant();
			}
			return det;
		}
		/// <summary>
		/// Calculates the adjoint of the matrix.
		/// </summary>
		/// <returns>A <see cref="Matrix4F"/> instance containing the adjoint of the matrix.</returns>
		public Matrix4F Adjoint() 
		{
			Matrix4F result = new Matrix4F();
			for (int row = 0; row < 4; row++)
			{
				for (int col = 0; col < 4; col++) 
				{
					if (((col+row) % 2) == 0)
						result[row, col] = Minor(col, row).Determinant();
					else
						result[row, col] = -Minor(col, row).Determinant();
				}
			}

			return result;
		}
		/// <summary>
		/// Build a 3x3 matrix from from the current matrix without the given row and column.
		/// </summary>
		/// <param name="row">The row to remove.</param>
		/// <param name="column">The column to remove.</param>
		/// <returns>A <see cref="Matrix3D"/> instance containing the result Minor.</returns>
		public Matrix3F Minor(int row, int column) 
		{
			int r = 0;
			Matrix3F result = new Matrix3F();
			for (int iRow = 0; iRow < 4; iRow++) 
			{
				int c = 0;
				if (iRow != row) 
				{
					for (int iCol = 0; iCol < 4; iCol++) 
					{
						if (iCol != column) 
						{
							result[r,c] = this[iRow, iCol];
							c++;
						}
					}
					r++;
				}
			}
			return result;
		}
		/// <summary>
		/// Calculates the trace the matrix which is the sum of its diagonal elements.
		/// </summary>
		/// <returns>Returns the trace value of the matrix.</returns>
		public float Trace()
		{
			return _m11 + _m22 + _m33 + _m44;
		}
		/// <summary>
		/// Transposes this matrix.
		/// </summary>
		public void Transpose()
		{
			MathFunctions.Swap(ref _m12, ref _m21);
			MathFunctions.Swap(ref _m13, ref _m31);
			MathFunctions.Swap(ref _m14, ref _m41);
			MathFunctions.Swap(ref _m23, ref _m32);
			MathFunctions.Swap(ref _m24, ref _m42);
			MathFunctions.Swap(ref _m34, ref _m43);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified matrices are equal.
		/// </summary>
		/// <param name="a">The left-hand matrix.</param>
		/// <param name="b">The right-hand matrix.</param>
		/// <returns>True if the two matrices are equal; otherwise, False.</returns>
		public static bool operator==(Matrix4F a, Matrix4F b)
		{
			if(Object.Equals(a, null) == true) 
			{
				return Object.Equals(b, null);
			}
			
			if(Object.Equals(b, null) == true) 
			{
				return Object.Equals(a, null);
			}

			return 
				(a.M11 == b.M11) && (a.M12 == b.M12) && (a.M13 == b.M13) && (a.M14 == b.M14) &&
				(a.M21 == b.M21) && (a.M22 == b.M22) && (a.M23 == b.M23) && (a.M24 == b.M24) &&
				(a.M31 == b.M31) && (a.M32 == b.M32) && (a.M33 == b.M33) && (a.M34 == b.M34) &&
				(a.M41 == b.M41) && (a.M42 == b.M42) && (a.M43 == b.M43) && (a.M44 == b.M44);

		}
		/// <summary>
		/// Tests whether two specified matrices are not equal.
		/// </summary>
		/// <param name="a">The left-hand matrix.</param>
		/// <param name="b">The right-hand matrix.</param>
		/// <returns>True if the two matrices are not equal; otherwise, False.</returns>
		public static bool operator!=(Matrix4F a, Matrix4F b)
		{
			if(Object.Equals(a, null) == true) 
			{
				return !Object.Equals(b, null);
			}
			
			if(Object.Equals(b, null) == true) 
			{
				return !Object.Equals(a, null);
			}

			return !(
				(a.M11 == b.M11) && (a.M12 == b.M12) && (a.M13 == b.M13) && (a.M14 == b.M14) &&
				(a.M21 == b.M21) && (a.M22 == b.M22) && (a.M23 == b.M23) && (a.M24 == b.M24) &&
				(a.M31 == b.M31) && (a.M32 == b.M32) && (a.M33 == b.M33) && (a.M34 == b.M34) &&
				(a.M41 == b.M41) && (a.M42 == b.M42) && (a.M43 == b.M43) && (a.M44 == b.M44)
				);

		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two matrices.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="b">A <see cref="Matrix4F"/> instance.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the sum.</returns>
		public static Matrix4F operator+(Matrix4F a, Matrix4F b)
		{
			return Matrix4F.Add(a,b);
		}
		/// <summary>
		/// Adds a matrix and a scalar.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the sum.</returns>
		public static Matrix4F operator+(Matrix4F a, float s)
		{
			return Matrix4F.Add(a,s);
		}
		/// <summary>
		/// Adds a matrix and a scalar.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the sum.</returns>
		public static Matrix4F operator+(float s, Matrix4F a)
		{
			return Matrix4F.Add(a,s);
		}
		/// <summary>
		/// Subtracts a matrix from a matrix.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="b">A <see cref="Matrix4F"/> instance.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the difference.</returns>
		public static Matrix4F operator-(Matrix4F a, Matrix4F b)
		{
			return Matrix4F.Subtract(a,b);
		}
		/// <summary>
		/// Subtracts a scalar from a matrix.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="s">A <see cref="Matrix4F"/> instance.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the difference.</returns>
		public static Matrix4F operator-(Matrix4F a, float s)
		{
			return Matrix4F.Subtract(a,s);
		}
		/// <summary>
		/// Multiplies two matrices.
		/// </summary>
		/// <param name="a">A <see cref="Matrix4F"/> instance.</param>
		/// <param name="b">A <see cref="Matrix4F"/> instance.</param>
		/// <returns>A new <see cref="Matrix4F"/> instance containing the result.</returns>
		public static Matrix4F operator*(Matrix4F a, Matrix4F b)
		{
			return Matrix4F.Multiply(a,b);
		}
		#endregion

		#region Indexing Operators
		/// <summary>
		/// Indexer allowing to access the matrix elements by an index
		/// where index = 2*row + column.
		/// </summary>
		public unsafe float this [int index] 
		{			
			get 
			{
				if (index < 0 || index >= 16)
					throw new IndexOutOfRangeException("Invalid matrix index!");

				fixed(float *f = &_m11) 
				{
					return *(f+index);
				}
			}
			set 
			{			
				if (index < 0 || index >= 16)
					throw new IndexOutOfRangeException("Invalid matrix index!");

				fixed(float *f = &_m11) 
				{
					*(f+index) = value;
				}
			}			
		}
		/// <summary>
		/// Indexer allowing to access the matrix elements by row and column.
		/// </summary>
		public float this[int row, int column]
		{
			get 
			{
				return this[ row*2 + column ];
			}
			set 
			{				
				this[ row*2 + column ] = value;
			}			
		}
		#endregion
	}
}
