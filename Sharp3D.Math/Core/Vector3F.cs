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
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Represents 3-Dimentional vector of single-precision floating point numbers.
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3F : ISerializable, ICloneable
	{
		#region Private fields
		private float _x;
		private float _y;
		private float _z;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3F"/> class with the specified coordinates.
		/// </summary>
		/// <param name="x">The vector's X coordinate.</param>
		/// <param name="y">The vector's Y coordinate.</param>
		/// <param name="z">The vector's Z coordinate.</param>
		public Vector3F(float x, float y, float z)
		{
			_x = x;
			_y = y;
			_z = z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3F"/> class with the specified coordinates.
		/// </summary>
		/// <param name="coordinates">An array containing the coordinate parameters.</param>
		public Vector3F(float[] coordinates)
		{
			Debug.Assert(coordinates != null);
			Debug.Assert(coordinates.Length >= 3);

			_x = coordinates[0];
			_y = coordinates[1];
			_z = coordinates[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3F"/> class with the specified coordinates.
		/// </summary>
		/// <param name="coordinates">An array containing the coordinate parameters.</param>
		public Vector3F(FloatArrayList coordinates)
		{
			Debug.Assert(coordinates != null);
			Debug.Assert(coordinates.Count >= 3);

			_x = coordinates[0];
			_y = coordinates[1];
			_z = coordinates[2];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3F"/> class using coordinates from a given <see cref="Vector3F"/> instance.
		/// </summary>
		/// <param name="vector">A <see cref="Vector3F"/> to get the coordinates from.</param>
		public Vector3F(Vector3F vector)
		{
			_x = vector.X;
			_y = vector.Y;
			_z = vector.Z;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector3F"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		private Vector3F(SerializationInfo info, StreamingContext context)
		{
			_x = info.GetSingle("X");
			_y = info.GetSingle("Y");
			_z = info.GetSingle("Z");
		}
		#endregion

		#region Constants
		/// <summary>
		/// 3-Dimentional single-precision floating point zero vector.
		/// </summary>
		public static readonly Vector3F Zero	= new Vector3F(0.0f, 0.0f, 0.0f);
		/// <summary>
		/// 3-Dimentional single-precision floating point X-Axis vector.
		/// </summary>
		public static readonly Vector3F XAxis	= new Vector3F(1.0f, 0.0f, 0.0f);
		/// <summary>
		/// 3-Dimentional single-precision floating point Y-Axis vector.
		/// </summary>
		public static readonly Vector3F YAxis	= new Vector3F(0.0f, 1.0f, 0.0f);
		/// <summary>
		/// 3-Dimentional single-precision floating point Y-Axis vector.
		/// </summary>
		public static readonly Vector3F ZAxis	= new Vector3F(0.0f, 0.0f, 1.0f);
		#endregion

		#region Public properties
		/// <summary>
		/// Gets or sets the x-coordinate of this vector.
		/// </summary>
		/// <value>The x-coordinate of this vector.</value>
		public float X
		{
			get { return _x; }
			set { _x = value;}
		}
		/// <summary>
		/// Gets or sets the y-coordinate of this vector.
		/// </summary>
		/// <value>The y-coordinate of this vector.</value>
		public float Y
		{
			get { return _y; }
			set { _y = value;}
		}
		/// <summary>
		/// Gets or sets the z-coordinate of this vector.
		/// </summary>
		/// <value>The z-coordinate of this vector.</value>
		public float Z
		{
			get { return _z; }
			set { _z = value;}
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Vector3F"/> object.
		/// </summary>
		/// <returns>The <see cref="Vector3F"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Vector3F(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Vector3F"/> object.
		/// </summary>
		/// <returns>The <see cref="Vector3F"/> object this method creates.</returns>
		public Vector3F Clone()
		{
			return new Vector3F(this);
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
			info.AddValue("X", _x);
			info.AddValue("Y", _y);
			info.AddValue("Z", _z);
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="Vector3F"/> equivalent.
		/// </summary>
		/// <param name="s">A string representation of a <see cref="Vector3F"/></param>
		/// <returns>A <see cref="Vector3F"/> that represents the vector specified by the <paramref name="s"/> parameter.</returns>
		public static Vector3F Parse(string s)
		{
			Regex r = new Regex(@"\((?<x>.*),(?<y>.*),(?<z>.*)\)", RegexOptions.None);
			Match m = r.Match(s);
			if (m.Success)
			{
				return new Vector3F(
					float.Parse(m.Result("${x}")),
					float.Parse(m.Result("${y}")),
					float.Parse(m.Result("${z}"))
					);
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		#endregion

		#region Public Static Vector Arithmetics
		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="w">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the sum.</returns>
		public static Vector3F Add(Vector3F v, Vector3F w)
		{
			return new Vector3F(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
		}
		/// <summary>
		/// Adds a vector and a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the sum.</returns>
		public static Vector3F Add(Vector3F v, float s)
		{
			return new Vector3F(v.X + s, v.Y + s, v.Z + s);
		}
		/// <summary>
		/// Adds two vectors and put the result in the third vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance</param>
		/// <param name="w">A <see cref="Vector3F"/> instance to hold the result.</param>
		public static void Add(Vector3F u, Vector3F v, Vector3F w)
		{
			w.X = u.X + v.X;
			w.Y = u.Y + v.Y;
			w.Z = u.Z + v.Z;
		}
		/// <summary>
		/// Adds a vector and a scalar and put the result into another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance to hold the result.</param>
		public static void Add(Vector3F u, float s, Vector3F v)
		{
			v.X = u.X + s;
			v.Y = u.Y + s;
			v.Z = u.Z + s;
		}
		/// <summary>
		/// Subtracts a vector from a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="w">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the difference.</returns>
		/// <remarks>
		///	result[i] = v[i] - w[i].
		/// </remarks>
		public static Vector3F Subtract(Vector3F v, Vector3F w)
		{
			return new Vector3F(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
		}
		/// <summary>
		/// Subtracts a scalar from a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = v[i] - s
		/// </remarks>
		public static Vector3F Subtract(Vector3F v, float s)
		{
			return new Vector3F(v.X - s, v.Y - s, v.Z - s);
		}
		/// <summary>
		/// Subtracts a vector from a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = s - v[i]
		/// </remarks>
		public static Vector3F Subtract(float s, Vector3F v)
		{
			return new Vector3F(s - v.X, s - v.Y, s - v.Z);
		}
		/// <summary>
		/// Subtracts a vector from a second vector and puts the result into a third vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance</param>
		/// <param name="w">A <see cref="Vector3F"/> instance to hold the result.</param>
		/// <remarks>
		///	w[i] = v[i] - w[i].
		/// </remarks>
		public static void Subtract(Vector3F u, Vector3F v, Vector3F w)
		{
			w.X = u.X - v.X;
			w.Y = u.Y - v.Y;
			w.Z = u.Z - v.Z;
		}
		/// <summary>
		/// Subtracts a vector from a scalar and put the result into another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance to hold the result.</param>
		/// <remarks>
		/// v[i] = u[i] - s
		/// </remarks>
		public static void Subtract(Vector3F u, float s, Vector3F v)
		{
			v.X = u.X - s;
			v.Y = u.Y - s;
			v.Z = u.Z - s;
		}
		/// <summary>
		/// Subtracts a scalar from a vector and put the result into another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance to hold the result.</param>
		/// <remarks>
		/// v[i] = s - u[i]
		/// </remarks>
		public static void Subtract(float s, Vector3F u, Vector3F v)
		{
			v.X = s - u.X;
			v.Y = s - u.Y;
			v.Z = s - u.Z;
		}
		/// <summary>
		/// Divides a vector by another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A new <see cref="Vector3F"/> containing the quotient.</returns>
		/// <remarks>
		///	result[i] = u[i] / v[i].
		/// </remarks>
		public static Vector3F Divide(Vector3F u, Vector3F v)
		{
			return new Vector3F(u.X / v.X, u.Y / v.Y, u.Z / v.Z);
		}
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <returns>A new <see cref="Vector3F"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = v[i] / s;
		/// </remarks>
		public static Vector3F Divide(Vector3F v, float s)
		{
			return new Vector3F(v.X / s, v.Y / s, v.Z / s);
		}
		/// <summary>
		/// Divides a scalar by a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <returns>A new <see cref="Vector3F"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = s / v[i]
		/// </remarks>
		public static Vector3F Divide(float s, Vector3F v)
		{
			return new Vector3F(s / v.X, s/ v.Y, s / v.Z);
		}
		/// <summary>
		/// Divides a vector by another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="w">A <see cref="Vector3F"/> instance to hold the result.</param>
		/// <remarks>
		/// w[i] = u[i] / v[i]
		/// </remarks>
		public static void Divide(Vector3F u, Vector3F v, Vector3F w)
		{
			w.X = u.X / v.X;
			w.Y = u.Y / v.Y;
			w.Z = u.Z / v.Z;
		}
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <param name="v">A <see cref="Vector3F"/> instance to hold the result.</param>
		/// <remarks>
		/// v[i] = u[i] / s
		/// </remarks>
		public static void Divide(Vector3F u, float s, Vector3F v)
		{
			v.X = u.X / s;
			v.Y = u.Y / s;
			v.Z = u.Z / s;
		}
		/// <summary>
		/// Divides a scalar by a vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <param name="v">A <see cref="Vector3F"/> instance to hold the result.</param>
		/// <remarks>
		/// v[i] = s / u[i]
		/// </remarks>
		public static void Divide(float s, Vector3F u, Vector3F v)
		{
			v.X = s / u.X;
			v.Y = s / u.Y;
			v.Z = s / u.Z;
		}
		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> containing the result.</returns>
		public static Vector3F Multiply(Vector3F u, float s)
		{
			return new Vector3F(u.X * s, u.Y * s, u.Z * s);
		}
		/// <summary>
		/// Multiplies a vector by a scalar and put the result in another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance to hold the result.</param>
		public static void Multiply(Vector3F u, float s, Vector3F v)
		{
			v.X = u.X * s;
			v.Y = u.Y * s;
			v.Z = u.Z * s;
		}
		/// <summary>
		/// Calculates the dot product of two vectors.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <returns>The dot product value.</returns>
		public static float DotProduct(Vector3F u, Vector3F v)
		{
			return (u.X * v.X) + (u.Y * v.Y) + (u.Z * v.Z);
		}
		/// <summary>
		/// Calculates the cross product of two vectors.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A new <see cref="Vector3F"/> containing the cross product result.</returns>
		public static Vector3F CrossProduct(Vector3F u, Vector3F v)
		{
			return new Vector3F( 
				u.Y*v.Z - u.Z*v.Y, 
				u.Z*v.X - u.X*v.Z, 
				u.X*v.Y - u.Y*v.X );
		}
		/// <summary>
		/// Calculates the cross product of two vectors.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="w">A <see cref="Vector3F"/> instance to hold the cross product result.</param>
		public static void CrossProduct(Vector3F u, Vector3F v, Vector3F w)
		{
			w.X = u.Y*v.Z - u.Z*v.Y;
			w.Y = u.Z*v.X - u.X*v.Z;
			w.Z = u.X*v.Y - u.Y*v.X;
		}
		/// <summary>
		/// Negates a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the negated values.</returns>
		public static Vector3F Negate(Vector3F v)
		{
			return new Vector3F(-v.X, -v.Y, -v.Z);
		}
		/// <summary>
		/// Tests whether two vectors are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEqual(Vector3F v, Vector3F u)
		{
			return ApproxEqual(v,u, MathFunctions.EpsilonF);
		}
		/// <summary>
		/// Tests whether two vectors are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEqual(Vector3F v, Vector3F u, float tolerance)
		{
			return
				(
				(System.Math.Abs(v.X - u.X) <= tolerance) &&
				(System.Math.Abs(v.Y - u.Y) <= tolerance)
				);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Scale the vector so that its length is 1.
		/// </summary>
		public void Normalize()
		{
			float length = GetLength();
			if (length == 0)
			{
				throw new DivideByZeroException("Trying to normalize a vector with length of zero.");
			}

			_x /= length;
			_y /= length;
			_z /= length;
		}
		/// <summary>
		/// Returns the length of the vector.
		/// </summary>
		/// <returns>The length of the vector. (Sqrt(X*X + Y*Y + Z*Z))</returns>
		public float GetLength()
		{
			return (float)System.Math.Sqrt(_x*_x + _y*_y + _z*_z);
		}
		/// <summary>
		/// Returns the squared length of the vector.
		/// </summary>
		/// <returns>The squared length of the vector. (X*X + Y*Y + Z*Z)</returns>
		public float GetLengthSquared()
		{
			return (_x*_x + _y*_y + _z*_z);
		}
		#endregion

		#region Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _x.GetHashCode() ^ _y.GetHashCode() ^ _z.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>True if <paramref name="obj"/> is a <see cref="Vector3F"/> and has the same values as this instance; otherwise, False.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector3F)
			{
				Vector3F v = (Vector3F)obj;
				return (_x == v.X) && (_y == v.Y) && (_z == v.Z);
			}
			return false;
		}

		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format("({0}, {1}, {2})", _x, _y, _z);
		}
		#endregion
		
		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified vectors are equal.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the two vectors are equal; otherwise, False.</returns>
		public static bool operator==(Vector3F u, Vector3F v)
		{
			if (Object.Equals(u, null))
			{
				return Object.Equals(v, null);
			}

			if (Object.Equals(v, null))
			{
				return Object.Equals(u, null);
			}

			return (u.X == v.X) && (u.Y == v.Y) && (u.Z == v.Z);
		}
		/// <summary>
		/// Tests whether two specified vectors are not equal.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the two vectors are not equal; otherwise, False.</returns>
		public static bool operator!=(Vector3F u, Vector3F v)
		{
			if (Object.Equals(u, null))
			{
				return !Object.Equals(v, null);
			}

			if (Object.Equals(v, null))
			{
				return !Object.Equals(u, null);
			}

			return !((u.X == v.X) && (u.Y == v.Y) && (u.Z == v.Z));
		}
		/// <summary>
		/// Tests if a vector's components are greater than another vector's components.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the left-hand vector's components are greater than the right-hand vector's component; otherwise, False.</returns>
		public static bool operator>(Vector3F u, Vector3F v)
		{
			return (
				(u._x > v._x) && 
				(u._y > v._y) && 
				(u._z > v._z));
		}
		/// <summary>
		/// Tests if a vector's components are smaller than another vector's components.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the left-hand vector's components are smaller than the right-hand vector's component; otherwise, False.</returns>
		public static bool operator<(Vector3F u, Vector3F v)
		{
			return (
				(u._x < v._x) && 
				(u._y < v._y) && 
				(u._z < v._z));
		}
		/// <summary>
		/// Tests if a vector's components are greater or equal than another vector's components.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the left-hand vector's components are greater or equal than the right-hand vector's component; otherwise, False.</returns>
		public static bool operator>=(Vector3F u, Vector3F v)
		{
			return (
				(u._x >= v._x) && 
				(u._y >= v._y) && 
				(u._z >= v._z));
		}
		/// <summary>
		/// Tests if a vector's components are smaller or equal than another vector's components.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the left-hand vector's components are smaller or equal than the right-hand vector's component; otherwise, False.</returns>
		public static bool operator<=(Vector3F u, Vector3F v)
		{
			return (
				(u._x <= v._x) && 
				(u._y <= v._y) && 
				(u._z <= v._z));
		}
		#endregion

		#region Unary Operators
		/// <summary>
		/// Negates the values of the vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the negated values.</returns>
		public static Vector3F operator-(Vector3F v)
		{
			return Vector3F.Negate(v);
		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the sum.</returns>
		public static Vector3F operator+(Vector3F u, Vector3F v)
		{
			return Vector3F.Add(u,v);
		}
		/// <summary>
		/// Adds a vector and a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the sum.</returns>
		public static Vector3F operator+(Vector3F v, float s)
		{
			return Vector3F.Add(v,s);
		}
		/// <summary>
		/// Adds a vector and a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the sum.</returns>
		public static Vector3F operator+(float s, Vector3F v)
		{
			return Vector3F.Add(v,s);
		}
		/// <summary>
		/// Subtracts a vector from a vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector3F"/> instance.</param>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the difference.</returns>
		/// <remarks>
		///	result[i] = v[i] - w[i].
		/// </remarks>
		public static Vector3F operator-(Vector3F u, Vector3F v)
		{
			return Vector3F.Subtract(u,v);
		}
		/// <summary>
		/// Subtracts a scalar from a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = v[i] - s
		/// </remarks>
		public static Vector3F operator-(Vector3F v, float s)
		{
			return Vector3F.Subtract(v, s);
		}
		/// <summary>
		/// Subtracts a vector from a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = s - v[i]
		/// </remarks>
		public static Vector3F operator-(float s, Vector3F v)
		{
			return Vector3F.Subtract(s, v);
		}

		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> containing the result.</returns>
		public static Vector3F operator*(Vector3F v, float s)
		{
			return Vector3F.Multiply(v,s);
		}
		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector3F"/> containing the result.</returns>
		public static Vector3F operator*(float s, Vector3F v)
		{
			return Vector3F.Multiply(v,s);
		}
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <returns>A new <see cref="Vector3F"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = v[i] / s;
		/// </remarks>
		public static Vector3F operator/(Vector3F v, float s)
		{
			return Vector3F.Divide(v,s);
		}
		/// <summary>
		/// Divides a scalar by a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <returns>A new <see cref="Vector3F"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = s / v[i]
		/// </remarks>
		public static Vector3F operator/(float s, Vector3F v)
		{
			return Vector3F.Divide(s,v);
		}
		#endregion

		#region Array Indexing Operator
		/// <summary>
		/// Indexer ( [x, y] ).
		/// </summary>
		public float this[int index]
		{
			get	
			{
				switch( index ) 
				{
					case 0:
						return _x;
					case 1:
						return _y;
					case 2:
						return _z;
					default:
						throw new IndexOutOfRangeException();
				}
			}
			set 
			{
				switch( index ) 
				{
					case 0:
						_x = value;
						break;
					case 1:
						_y = value;
						break;
					case 2:
						_z = value;
						break;
					default:
						throw new IndexOutOfRangeException();
				}
			}

		}

		#endregion

		#region Conversion Operators
		/// <summary>
		/// Converts the vector to an array of single-precision floating point values.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <returns>An array of single-precision floating point values.</returns>
		public static explicit operator float[](Vector3F v)
		{
			float[] array = new float[3];
			array[0] = v.X;
			array[1] = v.Y;
			array[2] = v.Z;
			return array;
		}
		/// <summary>
		/// Converts the vector to an array of single-precision floating point values.
		/// </summary>
		/// <param name="v">A <see cref="Vector3F"/> instance.</param>
		/// <returns>An array of single-precision floating point values.</returns>
		public static explicit operator FloatArrayList(Vector3F v)
		{
			FloatArrayList array = new FloatArrayList(3);
			array[0] = v.X;
			array[1] = v.Y;
			array[2] = v.Z;
			return array;
		}
		#endregion
	}

}
