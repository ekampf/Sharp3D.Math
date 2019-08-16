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
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Represents a complex single-precision floating point number.
	/// </summary>
	[Serializable]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[StructLayout(LayoutKind.Sequential)]
	public struct ComplexF : ICloneable, ISerializable
	{
		private float _real;
		private float _image;

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ComplexF"/> class using given real and imaginary values.
		/// </summary>
		/// <param name="real">Real value.</param>
		/// <param name="imaginary">Imaginary value.</param>
		public ComplexF(float real, float imaginary)
		{
			_real = real;
			_image = imaginary;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ComplexF"/> class using values from a given complex instance.
		/// </summary>
		/// <param name="c">A complex number to get values from.</param>
		public ComplexF(ComplexF c)
		{
			_real = c.Real;
			_image = c.Imaginary;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ComplexF"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		private ComplexF(SerializationInfo info, StreamingContext context)
		{
			_real	= info.GetSingle("Real");
			_image  = info.GetSingle("Imaginary");
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the real value of the complex number.
		/// </summary>
		/// <value>The real value of this complex number.</value>
		public float Real
		{
			get { return _real; }
			set { _real = value;}
		}
		/// <summary>
		/// Gets or sets the imaginary value of the complex number.
		/// </summary>
		/// <value>The imaginary value of this complex number.</value>
		public float Imaginary
		{
			get { return _image; }
			set { _image = value;}
		}
		#endregion

		#region Constants
		/// <summary>
		///  A single-precision complex number that represents zero.
		/// </summary>
		public static readonly ComplexF Zero = new ComplexF(0,0);
		/// <summary>
		///  A single-precision complex number that represents one.
		/// </summary>
		public static readonly ComplexF One	= new ComplexF(1,0);
		/// <summary>
		///  A single-precision complex number that represents the squere root of (-1).
		/// </summary>
		public static readonly ComplexF I	= new ComplexF(0,1);
		#endregion
	
		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="ComplexF"/> object.
		/// </summary>
		/// <returns>The <see cref="ComplexF"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new ComplexF(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="ComplexF"/> object.
		/// </summary>
		/// <returns>The <see cref="ComplexF"/> object this method creates.</returns>
		public ComplexF Clone()
		{
			return new ComplexF(this);
		}
		#endregion
	
		#region ISerializable Members
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> to populate with data. </param>
		/// <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Real", this.Real);
			info.AddValue("Imaginary", this.Imaginary);
		}
		#endregion

		#region Public Static Parse Methods
		/// <summary>
		/// Converts the specified string to its <see cref="ComplexF"/> equivalent.
		/// </summary>
		/// <param name="s">A string representation of a <see cref="ComplexF"/></param>
		/// <returns>A <see cref="ComplexF"/> that represents the vector specified by the <paramref name="s"/> parameter.</returns>
		public static ComplexF Parse(string s)
		{
			Regex r = new Regex(@"\((?<real>.*),(?<imaginary>.*)\)", RegexOptions.None);
			Match m = r.Match(s);
			if (m.Success)
			{
				return new ComplexF(
					float.Parse(m.Result("${real}")),
					float.Parse(m.Result("${imaginary}"))
					);
			}
			else
			{
				throw new ParseException("Unsuccessful Match.");
			}
		}
		#endregion

		#region Public Static Complex Arithmetics
		/// <summary>
		/// Adds two complex numbers.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		public static ComplexF Add(ComplexF a, ComplexF b)
		{
			return new ComplexF(a.Real + b.Real, a.Imaginary + b.Imaginary);
		}
		/// <summary>
		/// Adds a complex number and a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		public static ComplexF Add(ComplexF a, float s)
		{
			return new ComplexF(a.Real + s, a.Imaginary);
		}
		/// <summary>
		/// Adds two complex numbers and put the result in the third complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Add(ComplexF a, ComplexF b, ComplexF result)
		{
			result.Real = a.Real + b.Real;
			result.Imaginary = a.Imaginary + b.Imaginary;
		}
		/// <summary>
		/// Adds a complex number and a scalar and put the result into another complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Add(ComplexF a, float s, ComplexF result)
		{
			result.Real = a.Real + s;
			result.Imaginary = a.Imaginary;
		}

		/// <summary>
		/// Subtracts a complex from a complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF Subtract(ComplexF a, ComplexF b)
		{
			return new ComplexF(a.Real - b.Real, a.Imaginary - b.Imaginary);
		}
		/// <summary>
		/// Subtracts a scalar from a complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF Subtract(ComplexF a, float s)
		{
			return new ComplexF(a.Real - s, a.Imaginary);
		}
		/// <summary>
		/// Subtracts a complex from a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF Subtract(float s, ComplexF a)
		{
			return new ComplexF(s - a.Real, a.Imaginary);
		}
		/// <summary>
		/// Subtracts a complex from a complex and put the result in the third complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Subtract(ComplexF a, ComplexF b, ComplexF result)
		{
			result.Real = a.Real - b.Real;
			result.Imaginary = a.Imaginary - b.Imaginary;
		}
		/// <summary>
		/// Subtracts a scalar from a complex and put the result into another complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Subtract(ComplexF a, float s, ComplexF result)
		{
			result.Real = a.Real - s;
			result.Imaginary = a.Imaginary;
		}		
		/// <summary>
		/// Subtracts a complex from a scalar and put the result into another complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Subtract(float s, ComplexF a, ComplexF result)
		{
			result.Real = s - a.Real;
			result.Imaginary = a.Imaginary;
		}		
		/// <summary>
		/// Multiplies two complex numbers.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF Multiply(ComplexF a, ComplexF b)
		{
			// (x + yi)(u + vi) = (xu – yv) + (xv + yu)i. 
			float x = a.Real, y = a.Imaginary;
			float u = b.Real, v = b.Imaginary;
			
			return new ComplexF(x*u - y*v, x*v + y*u);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF Multiply(ComplexF a, float s)
		{
			return new ComplexF(a.Real * s, a.Imaginary * s);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF Multiply(float s, ComplexF a)
		{
			return new ComplexF(a.Real * s, a.Imaginary * s);
		}
		/// <summary>
		/// Multiplies two complex numbers and put the result in a third complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Multiply(ComplexF a, ComplexF b, ComplexF result)
		{
			// (x + yi)(u + vi) = (xu – yv) + (xv + yu)i. 
			float x = a.Real, y = a.Imaginary;
			float u = b.Real, v = b.Imaginary;
			
			result.Real = x*u - y*v;
			result.Imaginary = x*v + y*u;
		}
		/// <summary>
		/// Multiplies a complex by a scalar and put the result into another complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Multiply(ComplexF a, float s, ComplexF result)
		{
			result.Real = a.Real * s;
			result.Imaginary = a.Imaginary * s;
		}
		/// <summary>
		/// Multiplies a complex by a scalar and put the result into another complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Multiply(float s, ComplexF a, ComplexF result)
		{
			result.Real = a.Real * s;
			result.Imaginary = a.Imaginary * s;

		}
		/// <summary>
		/// Divides a complex by a complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF Divide(ComplexF a, ComplexF b)
		{
			float	x = a.Real,	y = a.Imaginary;
			float	u = b.Real,	v = b.Imaginary;
			float	modulusSquared = u*u + v*v;

			if( modulusSquared == 0 ) 
			{
				throw new DivideByZeroException();
			}

			float invModulusSquared = 1 / modulusSquared;

			return new ComplexF(
				( x*u + y*v ) * invModulusSquared,
				( y*u - x*v ) * invModulusSquared );
		}
		/// <summary>
		/// Divides a complex by a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF Divide(ComplexF a, float s)
		{
			if( s == 0 ) 
			{
				throw new DivideByZeroException();
			}
			return new ComplexF(
				a.Real / s,
				a.Imaginary / s );
		}
		/// <summary>
		/// Divides a scalar by a complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF Divide(float s, ComplexF a)
		{
			if ((a.Real == 0) || (a.Imaginary == 0))
			{
				throw new DivideByZeroException();
			}
			return new ComplexF(
				s / a.Real,
				s / a.Imaginary);
		}
		/// <summary>
		/// Divides a complex by a complex and put the result in a third complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Divide(ComplexF a, ComplexF b, ComplexF result)
		{
			float	x = a.Real,	y = a.Imaginary;
			float	u = b.Real,	v = b.Imaginary;
			float	modulusSquared = u*u + v*v;

			if( modulusSquared == 0 ) 
			{
				throw new DivideByZeroException();
			}

			float invModulusSquared = 1 / modulusSquared;

			result.Real = ( x*u + y*v ) * invModulusSquared;
			result.Imaginary = ( y*u - x*v ) * invModulusSquared;
		}		
		/// <summary>
		/// Divides a complex by a scalar and put the result into another complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Divide(ComplexF a, float s, ComplexF result)
		{
			if( s == 0 ) 
			{
				throw new DivideByZeroException();
			}
			
			result.Real = a.Real / s;
			result.Imaginary = a.Imaginary / s;
		}
		/// <summary>
		/// Divides a scalar by a complex and put the result into another complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <param name="result">A <see cref="ComplexF"/> instance to hold the result.</param>
		public static void Divide(float s, ComplexF a, ComplexF result)
		{
			if ((a.Real == 0) || (a.Imaginary == 0))
			{
				throw new DivideByZeroException();
			}

			result.Real = s / a.Real;
			result.Imaginary = s / a.Imaginary;
		}
		/// <summary>
		/// Negates a complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the negated values.</returns>
		public static ComplexF Negate(ComplexF a)
		{
			return new ComplexF(-a.Real, -a.Imaginary);
		}
		/// <summary>
		/// Tests whether two complex numbers are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEqual(ComplexF a, ComplexF b)
		{
			return ApproxEqual(a,b, MathFunctions.EpsilonF);
		}
		/// <summary>
		/// Tests whether two complex numbers are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEqual(ComplexF a, ComplexF b, float tolerance)
		{
			return
				(
				(System.Math.Abs(a.Real - b.Real) <= tolerance) &&
				(System.Math.Abs(a.Imaginary - b.Imaginary) <= tolerance)
				);
		}
		#endregion

		#region Public Static Complex Special Functions
		public static ComplexF Sqrt(ComplexF a)
		{
			ComplexF result = ComplexF.Zero;

			if ((a.Real == 0.0f) && (a.Imaginary == 0.0f))
			{
				return result;
			}
			else if (a.Imaginary == 0.0f)
			{
				result.Real = (float)System.Math.Sqrt(a.Real);
				result.Imaginary = 0.0f;
			}
			else
			{
				float modulus = a.GetModulus();

				result.Real		= (float)System.Math.Sqrt(0.5 * (modulus + a.Real));
				result.Imaginary= (float)System.Math.Sqrt(0.5 * (modulus + a.Real));
				if (a.Imaginary < 0.0)
					result.Imaginary = -result.Imaginary;
			}

			return result;
		}
		public static ComplexF Log(ComplexF a)
		{
			ComplexF result = ComplexF.Zero;

			if ((a.Real > 0.0f) && (a.Imaginary == 0.0f))
			{
				result.Real = (float)System.Math.Log(a.Real);
				result.Imaginary = 0.0f;
			}
			else if (a.Real == 0.0f)
			{
				if (a.Imaginary > 0.0f)
				{
					result.Real = (float)System.Math.Log(a.Imaginary);
					result.Imaginary = (float)MathFunctions.HalfPI;
				}
				else
				{
					result.Real = (float)System.Math.Log(-(a.Imaginary));
					result.Imaginary = (float)-MathFunctions.HalfPI;
				}
			}
			else
			{
				result.Real = (float)System.Math.Log(a.GetModulus());
				result.Imaginary = (float)System.Math.Atan2(a.Imaginary, a.Real);
			}

			return result;
		}
		public static ComplexF Exp(ComplexF a)
		{
			ComplexF result = ComplexF.Zero;
			float r = (float)System.Math.Exp(a.Real);
			result.Real		= (float)(r * (float)System.Math.Cos(a.Imaginary));
			result.Imaginary= (float)(r * (float)System.Math.Sin(a.Imaginary));

			return result;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Gets the modulus of the complex number.
		/// </summary>
		/// <returns>
		/// The modulus of the complex number:  sqrt(real*real + imaginary*imaginary).
		/// </returns>
		public float GetModulus()
		{
			return (float)System.Math.Sqrt( _real*_real + _image*_image );
		}
		/// <summary>
		/// Gets the squared modulus of the complex number.
		/// </summary>
		/// <returns>
		/// The squared modulus of the complex number:  (real*real + imaginary*imaginary).
		/// </returns>
		public float GetModulusSquared()
		{
			return (_real*_real + _image*_image);
		}
		/// <summary>
		/// Gets the conjugate of the complex number.
		/// </summary>
		/// <returns>
		/// The conjugate of the complex number.
		/// </returns>
		public ComplexF GetConjugate()
		{
			return new ComplexF(_real, -_image);
		}
		/// <summary>
		/// Scale the complex number to modulus 1.
		/// </summary>
		public void Normalize()
		{
			float modulus = this.GetModulus();
			if(modulus == 0) 
			{
				throw new DivideByZeroException( "Can not normalize a complex number that is zero." );
			}
			_real	= (float)(_real / modulus);
			_image	= (float)(_image / modulus);
		}

		#endregion

		#region Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _real.GetHashCode() ^ _image.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>True if <paramref name="obj"/> is a <see cref="ComplexF"/> and has the same values as this instance; otherwise, False.</returns>
		public override bool Equals(object obj)
		{
			if(obj is ComplexF) 
			{
				ComplexF c = (ComplexF)obj;
				return (this.Real == c.Real) && (this.Imaginary == c.Imaginary);
			}
			return false;
		}

		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format("({0}, {1})", _real, _image);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified complex numbers are equal.
		/// </summary>
		/// <param name="u">The left-hand complex number.</param>
		/// <param name="v">The right-hand complex number.</param>
		/// <returns>True if the two complex numbers are equal; otherwise, False.</returns>
		public static bool operator==(ComplexF u, ComplexF v)
		{
			if (Object.Equals(u, null))
			{
				return Object.Equals(v, null);
			}

			if (Object.Equals(v, null))
			{
				return Object.Equals(u, null);
			}

			return (u.Real == v.Real) && (u.Imaginary == v.Imaginary);
		}
		/// <summary>
		/// Tests whether two specified complex numbers are not equal.
		/// </summary>
		/// <param name="u">The left-hand complex number.</param>
		/// <param name="v">The right-hand complex number.</param>
		/// <returns>True if the two complex numbers are not equal; otherwise, False.</returns>
		public static bool operator!=(ComplexF u, ComplexF v)
		{
			if (Object.Equals(u, null))
			{
				return !Object.Equals(v, null);
			}

			if (Object.Equals(v, null))
			{
				return !Object.Equals(u, null);
			}

			return !((u.Real == v.Real) && (u.Imaginary == v.Imaginary));
		}

		#endregion

		#region Unary Operators
		/// <summary>
		/// Negates the complex number.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the negated values.</returns>
		public static ComplexF operator-(ComplexF a)
		{
			return ComplexF.Negate(a);
		}
		#endregion

		#region Binary Operators
		/// <summary>
		/// Adds two complex numbers.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		public static ComplexF operator+(ComplexF a, ComplexF b)
		{
			return ComplexF.Add(a,b);
		}
		/// <summary>
		/// Adds a complex number and a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		public static ComplexF operator+(ComplexF a, float s)
		{
			return ComplexF.Add(a,s);
		}
		/// <summary>
		/// Adds a complex number and a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the sum.</returns>
		public static ComplexF operator+(float s, ComplexF a)
		{
			return ComplexF.Add(a,s);
		}
		/// <summary>
		/// Subtracts a complex from a complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF operator-(ComplexF a, ComplexF b)
		{
			return ComplexF.Subtract(a,b);
		}
		/// <summary>
		/// Subtracts a scalar from a complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF operator-(ComplexF a, float s)
		{
			return ComplexF.Subtract(a, s);
		}
		/// <summary>
		/// Subtracts a complex from a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the difference.</returns>
		public static ComplexF operator-(float s, ComplexF a)
		{
			return ComplexF.Subtract(s, a);
		}

		/// <summary>
		/// Multiplies two complex numbers.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator*(ComplexF a, ComplexF b)
		{
			return ComplexF.Multiply(a,b);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator*(float s, ComplexF a)
		{
			return ComplexF.Multiply(a,s);
		}
		/// <summary>
		/// Multiplies a complex by a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator*(ComplexF a, float s)
		{
			return ComplexF.Multiply(a,s);
		}
		/// <summary>
		/// Divides a complex by a complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="b">A <see cref="ComplexF"/> instance.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator/(ComplexF a, ComplexF b)
		{
			return ComplexF.Divide(a,b);
		}
		/// <summary>
		/// Divides a complex by a scalar.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator/(ComplexF a, float s)
		{
			return ComplexF.Divide(a,s);
		}
		/// <summary>
		/// Divides a scalar by a complex.
		/// </summary>
		/// <param name="a">A <see cref="ComplexF"/> instance.</param>
		/// <param name="s">A scalar.</param>
		/// <returns>A new <see cref="ComplexF"/> instance containing the result.</returns>
		public static ComplexF operator/(float s, ComplexF a)
		{
			return ComplexF.Divide(s,a);
		}
		#endregion

		#region Conversion Operators
		/// <summary>
		/// Converts from a double-precision real number to a complex number. 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static explicit operator ComplexF(double value)
		{
			return new ComplexF((float)value, 0);
		}
		/// <summary>
		/// Converts from a single-precision real number to a complex number. 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static explicit operator ComplexF(float value)
		{
			return new ComplexF(value, 0);
		}
		#endregion
	}

}
