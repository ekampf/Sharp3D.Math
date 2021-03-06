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
    /// Represents 2-Dimentional vector of single-precision floating point numbers.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2F : ISerializable, ICloneable
    {
        #region Private fields
        private float _x;
        private float _y;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2F"/> class with the specified coordinates.
        /// </summary>
        /// <param name="x">The vector's X coordinate.</param>
        /// <param name="y">The vector's Y coordinate.</param>
        public Vector2F(float x, float y)
        {
            _x = x;
            _y = y;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2F"/> class with the specified coordinates.
        /// </summary>
        /// <param name="coordinates">An array containing the coordinate parameters.</param>
        public Vector2F(float[] coordinates)
        {
            Debug.Assert(coordinates != null);
            Debug.Assert(coordinates.Length >= 2);

            _x = coordinates[0];
            _y = coordinates[1];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2F"/> class with the specified coordinates.
        /// </summary>
        /// <param name="coordinates">An array containing the coordinate parameters.</param>
        public Vector2F(FloatArrayList coordinates)
        {
            Debug.Assert(coordinates != null);
            Debug.Assert(coordinates.Count >= 2);

            _x = coordinates[0];
            _y = coordinates[1];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2F"/> class using coordinates from a given <see cref="Vector2F"/> instance.
        /// </summary>
        /// <param name="vector">A <see cref="Vector2F"/> to get the coordinates from.</param>
        public Vector2F(Vector2F vector)
        {
            _x = vector.X;
            _y = vector.Y;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2F"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private Vector2F(SerializationInfo info, StreamingContext context)
        {
            _x = info.GetSingle("X");
            _y = info.GetSingle("Y");
        }
        #endregion

        #region Constants
        /// <summary>
        /// 2-Dimentional single-precision floating point zero vector.
        /// </summary>
        public static readonly Vector2F Zero = new Vector2F(0.0f, 0.0f);
        /// <summary>
        /// 2-Dimentional single-precision floating point X-Axis vector.
        /// </summary>
        public static readonly Vector2F XAxis = new Vector2F(1.0f, 0.0f);
        /// <summary>
        /// 2-Dimentional single-precision floating point Y-Axis vector.
        /// </summary>
        public static readonly Vector2F YAxis = new Vector2F(0.0f, 1.0f);
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the x-coordinate of this vector.
        /// </summary>
        /// <value>The x-coordinate of this vector.</value>
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// Gets or sets the y-coordinate of this vector.
        /// </summary>
        /// <value>The y-coordinate of this vector.</value>
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="Vector2F"/> object.
        /// </summary>
        /// <returns>The <see cref="Vector2F"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Vector2F(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="Vector2F"/> object.
        /// </summary>
        /// <returns>The <see cref="Vector2F"/> object this method creates.</returns>
        public Vector2F Clone()
        {
            return new Vector2F(this);
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> with the data needed to serialize this object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate with data. </param>
        /// <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("X", _x);
            info.AddValue("Y", _y);
        }
        #endregion

        #region Public Static Parse Methods
        /// <summary>
        /// Converts the specified string to its <see cref="Vector2F"/> equivalent.
        /// </summary>
        /// <param name="s">A string representation of a <see cref="Vector2F"/></param>
        /// <returns>A <see cref="Vector2F"/> that represents the vector specified by the <paramref name="s"/> parameter.</returns>
        public static Vector2F Parse(string s)
        {
            Regex r = new Regex(@"\((?<x>.*),(?<y>.*)\)", RegexOptions.None);
            Match m = r.Match(s);
            if (m.Success)
            {
                return new Vector2F(
                    float.Parse(m.Result("${x}")),
                    float.Parse(m.Result("${y}"))
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
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="w">A <see cref="Vector2F"/> instance.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the sum.</returns>
        public static Vector2F Add(Vector2F v, Vector2F w)
        {
            return new Vector2F(v.X + w.X, v.Y + w.Y);
        }
        /// <summary>
        /// Adds a vector and a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the sum.</returns>
        public static Vector2F Add(Vector2F v, float s)
        {
            return new Vector2F(v.X + s, v.Y + s);
        }
        /// <summary>
        /// Adds two vectors and put the result in the third vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance</param>
        /// <param name="w">A <see cref="Vector2F"/> instance to hold the result.</param>
        public static void Add(Vector2F u, Vector2F v, Vector2F w)
        {
            w.X = u.X + v.X;
            w.Y = u.Y + v.Y;
        }
        /// <summary>
        /// Adds a vector and a scalar and put the result into another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance to hold the result.</param>
        public static void Add(Vector2F u, float s, Vector2F v)
        {
            v.X = u.X + s;
            v.Y = u.Y + s;
        }
        /// <summary>
        /// Subtracts a vector from a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="w">A <see cref="Vector2F"/> instance.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the difference.</returns>
        /// <remarks>
        ///	result[i] = v[i] - w[i].
        /// </remarks>
        public static Vector2F Subtract(Vector2F v, Vector2F w)
        {
            return new Vector2F(v.X - w.X, v.Y - w.Y);
        }
        /// <summary>
        /// Subtracts a scalar from a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the difference.</returns>
        /// <remarks>
        /// result[i] = v[i] - s
        /// </remarks>
        public static Vector2F Subtract(Vector2F v, float s)
        {
            return new Vector2F(v.X - s, v.Y - s);
        }
        /// <summary>
        /// Subtracts a vector from a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the difference.</returns>
        /// <remarks>
        /// result[i] = s - v[i]
        /// </remarks>
        public static Vector2F Subtract(float s, Vector2F v)
        {
            return new Vector2F(s - v.X, s - v.Y);
        }
        /// <summary>
        /// Subtracts a vector from a second vector and puts the result into a third vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance</param>
        /// <param name="w">A <see cref="Vector2F"/> instance to hold the result.</param>
        /// <remarks>
        ///	w[i] = v[i] - w[i].
        /// </remarks>
        public static void Subtract(Vector2F u, Vector2F v, Vector2F w)
        {
            w.X = u.X - v.X;
            w.Y = u.Y - v.Y;
        }
        /// <summary>
        /// Subtracts a vector from a scalar and put the result into another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance to hold the result.</param>
        /// <remarks>
        /// v[i] = u[i] - s
        /// </remarks>
        public static void Subtract(Vector2F u, float s, Vector2F v)
        {
            v.X = u.X - s;
            v.Y = u.Y - s;
        }
        /// <summary>
        /// Subtracts a scalar from a vector and put the result into another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance to hold the result.</param>
        /// <remarks>
        /// v[i] = s - u[i]
        /// </remarks>
        public static void Subtract(float s, Vector2F u, Vector2F v)
        {
            v.X = s - u.X;
            v.Y = s - u.Y;
        }
        /// <summary>
        /// Divides a vector by another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <returns>A new <see cref="Vector2F"/> containing the quotient.</returns>
        /// <remarks>
        ///	result[i] = u[i] / v[i].
        /// </remarks>
        public static Vector2F Divide(Vector2F u, Vector2F v)
        {
            return new Vector2F(u.X / v.X, u.Y / v.Y);
        }
        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <returns>A new <see cref="Vector2F"/> containing the quotient.</returns>
        /// <remarks>
        /// result[i] = v[i] / s;
        /// </remarks>
        public static Vector2F Divide(Vector2F v, float s)
        {
            return new Vector2F(v.X / s, v.Y / s);
        }
        /// <summary>
        /// Divides a scalar by a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <returns>A new <see cref="Vector2F"/> containing the quotient.</returns>
        /// <remarks>
        /// result[i] = s / v[i]
        /// </remarks>
        public static Vector2F Divide(float s, Vector2F v)
        {
            return new Vector2F(s / v.X, s / v.Y);
        }
        /// <summary>
        /// Divides a vector by another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="w">A <see cref="Vector2F"/> instance to hold the result.</param>
        /// <remarks>
        /// w[i] = u[i] / v[i]
        /// </remarks>
        public static void Divide(Vector2F u, Vector2F v, Vector2F w)
        {
            w.X = u.X / v.X;
            w.Y = u.Y / v.Y;
        }
        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <param name="v">A <see cref="Vector2F"/> instance to hold the result.</param>
        /// <remarks>
        /// v[i] = u[i] / s
        /// </remarks>
        public static void Divide(Vector2F u, float s, Vector2F v)
        {
            v.X = u.X / s;
            v.Y = u.Y / s;
        }
        /// <summary>
        /// Divides a scalar by a vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <param name="v">A <see cref="Vector2F"/> instance to hold the result.</param>
        /// <remarks>
        /// v[i] = s / u[i]
        /// </remarks>
        public static void Divide(float s, Vector2F u, Vector2F v)
        {
            v.X = s / u.X;
            v.Y = s / u.Y;
        }
        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> containing the result.</returns>
        public static Vector2F Multiply(Vector2F u, float s)
        {
            return new Vector2F(u.X * s, u.Y * s);
        }
        /// <summary>
        /// Multiplies a vector by a scalar and put the result in another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance to hold the result.</param>
        public static void Multiply(Vector2F u, float s, Vector2F v)
        {
            v.X = u.X * s;
            v.Y = u.Y * s;
        }
        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <returns>The dot product value.</returns>
        public static float DotProduct(Vector2F u, Vector2F v)
        {
            return (u.X * v.X) + (u.Y * v.Y);
        }
        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the negated values.</returns>
        public static Vector2F Negate(Vector2F v)
        {
            return new Vector2F(-v.X, -v.Y);
        }
        /// <summary>
        /// Tests whether two vectors are approximately equal using default tolerance value.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
        public static bool ApproxEqual(Vector2F v, Vector2F u)
        {
            return ApproxEqual(v, u, MathFunctions.EpsilonF);
        }
        /// <summary>
        /// Tests whether two vectors are approximately equal given a tolerance value.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="tolerance">The tolerance value used to test approximate equality.</param>
        /// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
        public static bool ApproxEqual(Vector2F v, Vector2F u, float tolerance)
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

        }
        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>The length of the vector. (Sqrt(X*X + Y*Y))</returns>
        public float GetLength()
        {
            return (float)System.Math.Sqrt(_x * _x + _y * _y);
        }
        /// <summary>
        /// Returns the squared length of the vector.
        /// </summary>
        /// <returns>The squared length of the vector. (X*X + Y*Y)</returns>
        public float GetLengthSquared()
        {
            return (_x * _x + _y * _y);
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
        /// <returns>True if <paramref name="obj"/> is a <see cref="Vector2F"/> and has the same values as this instance; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector2F)
            {
                Vector2F v = (Vector2F)obj;
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
        public static bool operator ==(Vector2F u, Vector2F v)
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
        public static bool operator !=(Vector2F u, Vector2F v)
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
        public static bool operator >(Vector2F u, Vector2F v)
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
        public static bool operator <(Vector2F u, Vector2F v)
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
        public static bool operator >=(Vector2F u, Vector2F v)
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
        public static bool operator <=(Vector2F u, Vector2F v)
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
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the negated values.</returns>
        public static Vector2F operator -(Vector2F v)
        {
            return Vector2F.Negate(v);
        }
        #endregion

        #region Binary Operators
        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the sum.</returns>
        public static Vector2F operator +(Vector2F u, Vector2F v)
        {
            return Vector2F.Add(u, v);
        }
        /// <summary>
        /// Adds a vector and a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the sum.</returns>
        public static Vector2F operator +(Vector2F v, float s)
        {
            return Vector2F.Add(v, s);
        }
        /// <summary>
        /// Adds a vector and a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the sum.</returns>
        public static Vector2F operator +(float s, Vector2F v)
        {
            return Vector2F.Add(v, s);
        }
        /// <summary>
        /// Subtracts a vector from a vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector2F"/> instance.</param>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the difference.</returns>
        /// <remarks>
        ///	result[i] = v[i] - w[i].
        /// </remarks>
        public static Vector2F operator -(Vector2F u, Vector2F v)
        {
            return Vector2F.Subtract(u, v);
        }
        /// <summary>
        /// Subtracts a scalar from a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the difference.</returns>
        /// <remarks>
        /// result[i] = v[i] - s
        /// </remarks>
        public static Vector2F operator -(Vector2F v, float s)
        {
            return Vector2F.Subtract(v, s);
        }
        /// <summary>
        /// Subtracts a vector from a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> instance containing the difference.</returns>
        /// <remarks>
        /// result[i] = s - v[i]
        /// </remarks>
        public static Vector2F operator -(float s, Vector2F v)
        {
            return Vector2F.Subtract(s, v);
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> containing the result.</returns>
        public static Vector2F operator *(Vector2F v, float s)
        {
            return Vector2F.Multiply(v, s);
        }
        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector2F"/> containing the result.</returns>
        public static Vector2F operator *(float s, Vector2F v)
        {
            return Vector2F.Multiply(v, s);
        }
        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <returns>A new <see cref="Vector2F"/> containing the quotient.</returns>
        /// <remarks>
        /// result[i] = v[i] / s;
        /// </remarks>
        public static Vector2F operator /(Vector2F v, float s)
        {
            return Vector2F.Divide(v, s);
        }
        /// <summary>
        /// Divides a scalar by a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <returns>A new <see cref="Vector2F"/> containing the quotient.</returns>
        /// <remarks>
        /// result[i] = s / v[i]
        /// </remarks>
        public static Vector2F operator /(float s, Vector2F v)
        {
            return Vector2F.Divide(s, v);
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
                switch (index)
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
                switch (index)
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
        /// Converts the vector to an array of single-precision floating point values.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <returns>An array of single-precision floating point values.</returns>
        public static explicit operator float[](Vector2F v)
        {
            float[] array = new float[2];
            array[0] = v.X;
            array[1] = v.Y;
            return array;
        }
        /// <summary>
        /// Converts the vector to an array of single-precision floating point values.
        /// </summary>
        /// <param name="v">A <see cref="Vector2F"/> instance.</param>
        /// <returns>An array of single-precision floating point values.</returns>
        public static explicit operator FloatArrayList(Vector2F v)
        {
            FloatArrayList array = new FloatArrayList(2);
            array[0] = v.X;
            array[1] = v.Y;
            return array;
        }
        #endregion
    }

}
