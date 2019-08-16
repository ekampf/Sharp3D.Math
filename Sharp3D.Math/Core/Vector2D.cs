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
	/// Represents 2-Dimentional vector of double-precision floating point numbers.
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector2D : ISerializable, ICloneable
	{
		#region Private fields
		private double _x;
		private double _y;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2D"/> class with the specified coordinates.
		/// </summary>
		/// <param name="x">The vector's X coordinate.</param>
		/// <param name="y">The vector's Y coordinate.</param>
		public Vector2D(double x, double y)
		{
			_x = x;
			_y = y;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2D"/> class with the specified coordinates.
		/// </summary>
		/// <param name="coordinates">An array containing the coordinate parameters.</param>
		public Vector2D(double[] coordinates)
		{
			Debug.Assert(coordinates != null);
			Debug.Assert(coordinates.Length >= 2);

			_x = coordinates[0];
			_y = coordinates[1];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2D"/> class with the specified coordinates.
		/// </summary>
		/// <param name="coordinates">An array containing the coordinate parameters.</param>
		public Vector2D(DoubleArrayList coordinates)
		{
			Debug.Assert(coordinates != null);
			Debug.Assert(coordinates.Count >= 2);

			_x = coordinates[0];
			_y = coordinates[1];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2D"/> class using coordinates from a given <see cref="Vector2D"/> instance.
		/// </summary>
		/// <param name="vector">A <see cref="Vector2D"/> to get the coordinates from.</param>
		public Vector2D(Vector2D vector)
		{
			_x = vector.X;
			_y = vector.Y;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2D"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		private Vector2D(SerializationInfo info, StreamingContext context)
		{
			_x = info.GetSingle("X");
			_y = info.GetSingle("Y");
		}
		#endregion

		#region Constants
		/// <summary>
		/// 2-Dimentional double-precision floating point zero vector.
		/// </summary>
		public static readonly Vector2D Zero	= new Vector2D(0.0f, 0.0f);
		/// <summary>
		/// 2-Dimentional double-precision floating point X-Axis vector.
		/// </summary>
		public static readonly Vector2D XAxis	= new Vector2D(1.0f, 0.0f);
		/// <summary>
		/// 2-Dimentional double-precision floating point Y-Axis vector.
		/// </summary>
		public static readonly Vector2D YAxis	= new Vector2D(0.0f, 1.0f);
		#endregion

		#region Public properties
		/// <summery>
		/// Gets or sets the x-coordinate of this vector.
		/// </summery>
		/// <value>The x-coordinate of this vector.</value>
		public double X
		{
			get { return _x; }
			set { _x = value;}
		}
		/// <summery>
		/// Gets or sets the y-coordinate of this vector.
		/// </summery>
		/// <value>The y-coordinate of this vector.</value>
		public double Y
		{
			get { return _y; }
			set { _y = value;}
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Vector2D"/> object.
		/// </summary>
		/// <returns>The <see cref="Vector2D"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Vector2D(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Vector2D"/> object.
		/// </summary>
		/// <returns>The <see cref="Vector2D"/> object this method creates.</returns>
		public Vector2D Clone()
		{
			return new Vector2D(this);
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
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="Vector2D"/> equivalent.
		/// </summary>
		/// <param name="s">A string representation of a <see cref="Vector2D"/></param>
		/// <returns>A <see cref="Vector2D"/> that represents the vector specified by the <paramref name="s"/> parameter.</returns>
		public static Vector2D Parse(string s)
		{
			Regex r = new Regex(@"\((?<x>.*),(?<y>.*)\)", RegexOptions.None);
			Match m = r.Match(s);
			if (m.Success)
			{
				return new Vector2D(
					double.Parse(m.Result("${x}")),
					double.Parse(m.Result("${y}"))
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
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="w">A <see cref="Vector2D"/> instance.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the sum.</returns>
		public static Vector2D Add(Vector2D v, Vector2D w)
		{
			return new Vector2D(v.X + w.X, v.Y + w.Y);
		}
		/// <summary>
		/// Adds a vector and a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the sum.</returns>
		public static Vector2D Add(Vector2D v, double s)
		{
			return new Vector2D(v.X + s, v.Y + s);
		}
		/// <summary>
		/// Adds two vectors and put the result in the third vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance</param>
		/// <param name="w">A <see cref="Vector2D"/> instance to hold the result.</param>
		public static void Add(Vector2D u, Vector2D v, Vector2D w)
		{
			w.X = u.X + v.X;
			w.Y = u.Y + v.Y;
		}
		/// <summary>
		/// Adds a vector and a scalar and put the result into another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance to hold the result.</param>
		public static void Add(Vector2D u, double s, Vector2D v)
		{
			v.X = u.X + s;
			v.Y = u.Y + s;
		}
		/// <summary>
		/// Subtracts a vector from a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="w">A <see cref="Vector2D"/> instance.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the difference.</returns>
		/// <remarks>
		///	result[i] = v[i] - w[i].
		/// </remarks>
		public static Vector2D Subtract(Vector2D v, Vector2D w)
		{
			return new Vector2D(v.X - w.X, v.Y - w.Y);
		}
		/// <summary>
		/// Subtracts a scalar from a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = v[i] - s
		/// </remarks>
		public static Vector2D Subtract(Vector2D v, double s)
		{
			return new Vector2D(v.X - s, v.Y - s);
		}
		/// <summary>
		/// Subtracts a vector from a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = s - v[i]
		/// </remarks>
		public static Vector2D Subtract(double s, Vector2D v)
		{
			return new Vector2D(s - v.X, s - v.Y);
		}
		/// <summary>
		/// Subtracts a vector from a second vector and puts the result into a third vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance</param>
		/// <param name="w">A <see cref="Vector2D"/> instance to hold the result.</param>
		/// <remarks>
		///	w[i] = v[i] - w[i].
		/// </remarks>
		public static void Subtract(Vector2D u, Vector2D v, Vector2D w)
		{
			w.X = u.X - v.X;
			w.Y = u.Y - v.Y;
		}
		/// <summary>
		/// Subtracts a vector from a scalar and put the result into another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance to hold the result.</param>
		/// <remarks>
		/// v[i] = u[i] - s
		/// </remarks>
		public static void Subtract(Vector2D u, double s, Vector2D v)
		{
			v.X = u.X - s;
			v.Y = u.Y - s;
		}
		/// <summary>
		/// Subtracts a scalar from a vector and put the result into another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance to hold the result.</param>
		/// <remarks>
		/// v[i] = s - u[i]
		/// </remarks>
		public static void Subtract(double s, Vector2D u, Vector2D v)
		{
			v.X = s - u.X;
			v.Y = s - u.Y;
		}
		/// <summary>
		/// Divides a vector by another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <returns>A new <see cref="Vector2D"/> containing the quotient.</returns>
		/// <remarks>
		///	result[i] = u[i] / v[i].
		/// </remarks>
		public static Vector2D Divide(Vector2D u, Vector2D v)
		{
			return new Vector2D(u.X / v.X, u.Y / v.Y);
		}
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <returns>A new <see cref="Vector2D"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = v[i] / s;
		/// </remarks>
		public static Vector2D Divide(Vector2D v, double s)
		{
			return new Vector2D(v.X / s, v.Y / s);
		}
		/// <summary>
		/// Divides a scalar by a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <returns>A new <see cref="Vector2D"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = s / v[i]
		/// </remarks>
		public static Vector2D Divide(double s, Vector2D v)
		{
			return new Vector2D(s / v.X, s/ v.Y);
		}
		/// <summary>
		/// Divides a vector by another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="w">A <see cref="Vector2D"/> instance to hold the result.</param>
		/// <remarks>
		/// w[i] = u[i] / v[i]
		/// </remarks>
		public static void Divide(Vector2D u, Vector2D v, Vector2D w)
		{
			w.X = u.X / v.X;
			w.Y = u.Y / v.Y;
		}
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <param name="v">A <see cref="Vector2D"/> instance to hold the result.</param>
		/// <remarks>
		/// v[i] = u[i] / s
		/// </remarks>
		public static void Divide(Vector2D u, double s, Vector2D v)
		{
			v.X = u.X / s;
			v.Y = u.Y / s;
		}
		/// <summary>
		/// Divides a scalar by a vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <param name="v">A <see cref="Vector2D"/> instance to hold the result.</param>
		/// <remarks>
		/// v[i] = s / u[i]
		/// </remarks>
		public static void Divide(double s, Vector2D u, Vector2D v)
		{
			v.X = s / u.X;
			v.Y = s / u.Y;
		}
		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> containing the result.</returns>
		public static Vector2D Multiply(Vector2D u, double s)
		{
			return new Vector2D(u.X * s, u.Y * s);
		}
		/// <summary>
		/// Multiplies a vector by a scalar and put the result in another vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance to hold the result.</param>
		public static void Multiply(Vector2D u, double s, Vector2D v)
		{
			v.X = u.X * s;
			v.Y = u.Y * s;
		}
		/// <summary>
		/// Calculates the dot product of two vectors.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <returns>The dot product value.</returns>
		public static double DotProduct(Vector2D u, Vector2D v)
		{
			return (u.X * v.X) + (u.Y * v.Y);
		}
		/// <summary>
		/// Negates a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the negated values.</returns>
		public static Vector2D Negate(Vector2D v)
		{
			return new Vector2D(-v.X, -v.Y);
		}
		/// <summary>
		/// Tests whether two vectors are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEqual(Vector2D v, Vector2D u)
		{
			return ApproxEqual(v,u, MathFunctions.EpsilonD);
		}
		/// <summary>
		/// Tests whether two vectors are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEqual(Vector2D v, Vector2D u, double tolerance)
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
			double length = GetLength();
			if (length == 0)
			{
				throw new DivideByZeroException("Trying to normalize a vector with length of zero.");
			}

			_x /= length;
			_y /= length;

		}
		/// <summary>
		/// Returns the length of the vector.
		/// </summary>
		/// <returns>The length of the vector. (Sqrt(X*X + Y*Y))</returns>
		public double GetLength()
		{
			return System.Math.Sqrt(_x*_x + _y*_y);
		}
		/// <summary>
		/// Returns the squared length of the vector.
		/// </summary>
		/// <returns>The squared length of the vector. (X*X + Y*Y)</returns>
		public double GetLengthSquared()
		{
			return (_x*_x + _y*_y);
		}
		#endregion

		#region Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _x.GetHashCode() ^ _y.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>True if <paramref name="obj"/> is a <see cref="Vector2D"/> and has the same values as this instance; otherwise, False.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Vector2D)
			{
				Vector2D v = (Vector2D)obj;
				return (_x == v.X) && (_y == v.Y);
			}
			return false;
		}

		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format("({0}, {1})", _x, _y);
		}
		#endregion
		
		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified vectors are equal.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the two vectors are equal; otherwise, False.</returns>
		public static bool operator==(Vector2D u, Vector2D v)
		{
			if (Object.Equals(u, null))
			{
				return Object.Equals(v, null);
			}

			if (Object.Equals(v, null))
			{
				return Object.Equals(u, null);
			}

			return (u.X == v.X) && (u.Y == v.Y);
		}
		/// <summary>
		/// Tests whether two specified vectors are not equal.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the two vectors are not equal; otherwise, False.</returns>
		public static bool operator!=(Vector2D u, Vector2D v)
		{
			if (Object.Equals(u, null))
			{
				return !Object.Equals(v, null);
			}

			if (Object.Equals(v, null))
			{
				return !Object.Equals(u, null);
			}

			return !((u.X == v.X) && (u.Y == v.Y));
		}

		/// <summary>
		/// Tests if a vector's components are greater than another vector's components.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the left-hand vector's components are greater than the right-hand vector's component; otherwise, False.</returns>
		public static bool operator>(Vector2D u, Vector2D v)
		{
			return (
				(u._x > v._x) && 
				(u._y > v._y));
		}
		/// <summary>
		/// Tests if a vector's components are smaller than another vector's components.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the left-hand vector's components are smaller than the right-hand vector's component; otherwise, False.</returns>
		public static bool operator<(Vector2D u, Vector2D v)
		{
			return (
				(u._x < v._x) && 
				(u._y < v._y));
		}
		/// <summary>
		/// Tests if a vector's components are greater or equal than another vector's components.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the left-hand vector's components are greater or equal than the right-hand vector's component; otherwise, False.</returns>
		public static bool operator>=(Vector2D u, Vector2D v)
		{
			return (
				(u._x >= v._x) && 
				(u._y >= v._y));
		}
		/// <summary>
		/// Tests if a vector's components are smaller or equal than another vector's components.
		/// </summary>
		/// <param name="u">The left-hand vector.</param>
		/// <param name="v">The right-hand vector.</param>
		/// <returns>True if the left-hand vector's components are smaller or equal than the right-hand vector's component; otherwise, False.</returns>
		public static bool operator<=(Vector2D u, Vector2D v)
		{
			return (
				(u._x <= v._x) && 
				(u._y <= v._y));
		}
		#endregion

		#region Unary Operators
		/// <summary>
		/// Negates the values of the vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the negated values.</returns>
		public static Vector2D operator-(Vector2D v)
		{
			return Vector2D.Negate(v);
		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the sum.</returns>
		public static Vector2D operator+(Vector2D u, Vector2D v)
		{
			return Vector2D.Add(u,v);
		}
		/// <summary>
		/// Adds a vector and a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the sum.</returns>
		public static Vector2D operator+(Vector2D v, double s)
		{
			return Vector2D.Add(v,s);
		}
		/// <summary>
		/// Adds a vector and a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the sum.</returns>
		public static Vector2D operator+(double s, Vector2D v)
		{
			return Vector2D.Add(v,s);
		}
		/// <summary>
		/// Subtracts a vector from a vector.
		/// </summary>
		/// <param name="u">A <see cref="Vector2D"/> instance.</param>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the difference.</returns>
		/// <remarks>
		///	result[i] = v[i] - w[i].
		/// </remarks>
		public static Vector2D operator-(Vector2D u, Vector2D v)
		{
			return Vector2D.Subtract(u,v);
		}
		/// <summary>
		/// Subtracts a scalar from a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = v[i] - s
		/// </remarks>
		public static Vector2D operator-(Vector2D v, double s)
		{
			return Vector2D.Subtract(v, s);
		}
		/// <summary>
		/// Subtracts a vector from a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> instance containing the difference.</returns>
		/// <remarks>
		/// result[i] = s - v[i]
		/// </remarks>
		public static Vector2D operator-(double s, Vector2D v)
		{
			return Vector2D.Subtract(s, v);
		}

		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> containing the result.</returns>
		public static Vector2D operator*(Vector2D v, double s)
		{
			return Vector2D.Multiply(v,s);
		}
		/// <summary>
		/// Multiplies a vector by a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="Vector2D"/> containing the result.</returns>
		public static Vector2D operator*(double s, Vector2D v)
		{
			return Vector2D.Multiply(v,s);
		}
		/// <summary>
		/// Divides a vector by a scalar.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <returns>A new <see cref="Vector2D"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = v[i] / s;
		/// </remarks>
		public static Vector2D operator/(Vector2D v, double s)
		{
			return Vector2D.Divide(v,s);
		}
		/// <summary>
		/// Divides a scalar by a vector.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <param name="s">A scalar</param>
		/// <returns>A new <see cref="Vector2D"/> containing the quotient.</returns>
		/// <remarks>
		/// result[i] = s / v[i]
		/// </remarks>
		public static Vector2D operator/(double s, Vector2D v)
		{
			return Vector2D.Divide(s,v);
		}
		#endregion

		#region Array Indexing Operator
		/// <summary>
		/// Indexer ( [x, y] ).
		/// </summary>
		public double this[int index]
		{
			get	
			{
				switch( index ) 
				{
					case 0:
						return _x;
					case 1:
						return _y;
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
					default:
						throw new IndexOutOfRangeException();
				}
			}

		}

		#endregion

		#region Conversion Operators
		/// <summary>
		/// Converts the vector to an array of double-precision floating point values.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <returns>An array of double-precision floating point values.</returns>
		public static explicit operator double[](Vector2D v)
		{
			double[] array = new double[2];
			array[0] = v.X;
			array[1] = v.Y;
			return array;
		}
		/// <summary>
		/// Converts the vector to an array of double-precision floating point values.
		/// </summary>
		/// <param name="v">A <see cref="Vector2D"/> instance.</param>
		/// <returns>An array of double-precision floating point values.</returns>
		public static explicit operator DoubleArrayList(Vector2D v)
		{
			DoubleArrayList array = new DoubleArrayList(2);
			array[0] = v.X;
			array[1] = v.Y;
			return array;
		}
		#endregion

	}

}
