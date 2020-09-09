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
    /// Represents 3-Dimentional vector of double-precision floating point numbers.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3D : ISerializable, ICloneable
    {
        #region Private fields
        private double _x;
        private double _y;
        private double _z;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class with the specified coordinates.
        /// </summary>
        /// <param name="x">The vector's X coordinate.</param>
        /// <param name="y">The vector's Y coordinate.</param>
        /// <param name="z">The vector's Z coordinate.</param>
        public Vector3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class with the specified coordinates.
        /// </summary>
        /// <param name="coordinates">An array containing the coordinate parameters.</param>
        public Vector3D(double[] coordinates)
        {
            Debug.Assert(coordinates != null);
            Debug.Assert(coordinates.Length >= 3);

            _x = coordinates[0];
            _y = coordinates[1];
            _z = coordinates[2];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class with the specified coordinates.
        /// </summary>
        /// <param name="coordinates">An array containing the coordinate parameters.</param>
        public Vector3D(DoubleArrayList coordinates)
        {
            Debug.Assert(coordinates != null);
            Debug.Assert(coordinates.Count >= 3);

            _x = coordinates[0];
            _y = coordinates[1];
            _z = coordinates[2];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class using coordinates from a given <see cref="Vector3D"/> instance.
        /// </summary>
        /// <param name="vector">A <see cref="Vector3D"/> to get the coordinates from.</param>
        public Vector3D(Vector3D vector)
        {
            _x = vector.X;
            _y = vector.Y;
            _z = vector.Z;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private Vector3D(SerializationInfo info, StreamingContext context)
        {
            _x = info.GetSingle("X");
            _y = info.GetSingle("Y");
            _z = info.GetSingle("Z");
        }
        #endregion

        #region Constants
        /// <summary>
        /// 3-Dimentional double-precision floating point zero vector.
        /// </summary>
        public static readonly Vector3D Zero = new Vector3D(0.0f, 0.0f, 0.0f);
        /// <summary>
        /// 3-Dimentional double-precision floating point X-Axis vector.
        /// </summary>
        public static readonly Vector3D XAxis = new Vector3D(1.0f, 0.0f, 0.0f);
        /// <summary>
        /// 3-Dimentional double-precision floating point Y-Axis vector.
        /// </summary>
        public static readonly Vector3D YAxis = new Vector3D(0.0f, 1.0f, 0.0f);
        /// <summary>
        /// 3-Dimentional double-precision floating point Y-Axis vector.
        /// </summary>
        public static readonly Vector3D ZAxis = new Vector3D(0.0f, 0.0f, 1.0f);
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the x-coordinate of this vector.
        /// </summary>
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// Gets or sets the y-coordinate of this vector.
        /// </summary>
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
        /// <summary>
        /// Gets or sets the z-coordinate of this vector.
        /// </summary>
        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="Vector3D"/> object.
        /// </summary>
        /// <returns>The <see cref="Vector3D"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Vector3D(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="Vector3D"/> object.
        /// </summary>
        /// <returns>The <see cref="Vector3D"/> object this method creates.</returns>
        public Vector3D Clone()
        {
            return new Vector3D(this);
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
            info.AddValue("Z", _z);
        }
        #endregion

        #region Public Static Parse Methods
        /// <summary>
        /// Converts the specified string to its <see cref="Vector3D"/> equivalent.
        /// </summary>
        /// <param name="s">A string representation of a <see cref="Vector3D"/></param>
        /// <returns>A <see cref="Vector3D"/> that represents the vector specified by the <paramref name="s"/> parameter.</returns>
        public static Vector3D Parse(string s)
        {
            Regex r = new Regex(@"\((?<x>.*),(?<y>.*),(?<z>.*)\)", RegexOptions.None);
            Match m = r.Match(s);
            if (m.Success)
            {
                return new Vector3D(
                    double.Parse(m.Result("${x}")),
                    double.Parse(m.Result("${y}")),
                    double.Parse(m.Result("${z}"))
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
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="w">A <see cref="Vector3D"/> instance.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
        public static Vector3D Add(Vector3D v, Vector3D w)
        {
            return new Vector3D(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
        }
        /// <summary>
        /// Adds a vector and a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
        public static Vector3D Add(Vector3D v, double s)
        {
            return new Vector3D(v.X + s, v.Y + s, v.Z + s);
        }
        /// <summary>
        /// Adds two vectors and put the result in the third vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance</param>
        /// <param name="w">A <see cref="Vector3D"/> instance to hold the result.</param>
        public static void Add(Vector3D u, Vector3D v, Vector3D w)
        {
            w.X = u.X + v.X;
            w.Y = u.Y + v.Y;
            w.Z = u.Z + v.Z;
        }
        /// <summary>
        /// Adds a vector and a scalar and put the result into another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance to hold the result.</param>
        public static void Add(Vector3D u, double s, Vector3D v)
        {
            v.X = u.X + s;
            v.Y = u.Y + s;
            v.Z = u.Z + s;
        }
        /// <summary>
        /// Subtracts a vector from a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="w">A <see cref="Vector3D"/> instance.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
        /// <remarks>
        ///	result[i] = v[i] - w[i].
        /// </remarks>
        public static Vector3D Subtract(Vector3D v, Vector3D w)
        {
            return new Vector3D(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        }
        /// <summary>
        /// Subtracts a scalar from a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
        /// <remarks>
        /// result[i] = v[i] - s
        /// </remarks>
        public static Vector3D Subtract(Vector3D v, double s)
        {
            return new Vector3D(v.X - s, v.Y - s, v.Z - s);
        }
        /// <summary>
        /// Subtracts a vector from a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
        /// <remarks>
        /// result[i] = s - v[i]
        /// </remarks>
        public static Vector3D Subtract(double s, Vector3D v)
        {
            return new Vector3D(s - v.X, s - v.Y, s - v.Z);
        }
        /// <summary>
        /// Subtracts a vector from a second vector and puts the result into a third vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance</param>
        /// <param name="w">A <see cref="Vector3D"/> instance to hold the result.</param>
        /// <remarks>
        ///	w[i] = v[i] - w[i].
        /// </remarks>
        public static void Subtract(Vector3D u, Vector3D v, Vector3D w)
        {
            w.X = u.X - v.X;
            w.Y = u.Y - v.Y;
            w.Z = u.Z - v.Z;
        }
        /// <summary>
        /// Subtracts a vector from a scalar and put the result into another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance to hold the result.</param>
        /// <remarks>
        /// v[i] = u[i] - s
        /// </remarks>
        public static void Subtract(Vector3D u, double s, Vector3D v)
        {
            v.X = u.X - s;
            v.Y = u.Y - s;
            v.Z = u.Z - s;
        }
        /// <summary>
        /// Subtracts a scalar from a vector and put the result into another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance to hold the result.</param>
        /// <remarks>
        /// v[i] = s - u[i]
        /// </remarks>
        public static void Subtract(double s, Vector3D u, Vector3D v)
        {
            v.X = s - u.X;
            v.Y = s - u.Y;
            v.Z = s - u.Z;
        }
        /// <summary>
        /// Divides a vector by another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
        /// <remarks>
        ///	result[i] = u[i] / v[i].
        /// </remarks>
        public static Vector3D Divide(Vector3D u, Vector3D v)
        {
            return new Vector3D(u.X / v.X, u.Y / v.Y, u.Z / v.Z);
        }
        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
        /// <remarks>
        /// result[i] = v[i] / s;
        /// </remarks>
        public static Vector3D Divide(Vector3D v, double s)
        {
            return new Vector3D(v.X / s, v.Y / s, v.Z / s);
        }
        /// <summary>
        /// Divides a scalar by a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
        /// <remarks>
        /// result[i] = s / v[i]
        /// </remarks>
        public static Vector3D Divide(double s, Vector3D v)
        {
            return new Vector3D(s / v.X, s / v.Y, s / v.Z);
        }
        /// <summary>
        /// Divides a vector by another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="w">A <see cref="Vector3D"/> instance to hold the result.</param>
        /// <remarks>
        /// w[i] = u[i] / v[i]
        /// </remarks>
        public static void Divide(Vector3D u, Vector3D v, Vector3D w)
        {
            w.X = u.X / v.X;
            w.Y = u.Y / v.Y;
            w.Z = u.Z / v.Z;
        }
        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <param name="v">A <see cref="Vector3D"/> instance to hold the result.</param>
        /// <remarks>
        /// v[i] = u[i] / s
        /// </remarks>
        public static void Divide(Vector3D u, double s, Vector3D v)
        {
            v.X = u.X / s;
            v.Y = u.Y / s;
            v.Z = u.Z / s;
        }
        /// <summary>
        /// Divides a scalar by a vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <param name="v">A <see cref="Vector3D"/> instance to hold the result.</param>
        /// <remarks>
        /// v[i] = s / u[i]
        /// </remarks>
        public static void Divide(double s, Vector3D u, Vector3D v)
        {
            v.X = s / u.X;
            v.Y = s / u.Y;
            v.Z = s / u.Z;
        }
        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> containing the result.</returns>
        public static Vector3D Multiply(Vector3D u, double s)
        {
            return new Vector3D(u.X * s, u.Y * s, u.Z * s);
        }
        /// <summary>
        /// Multiplies a vector by a scalar and put the result in another vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance to hold the result.</param>
        public static void Multiply(Vector3D u, double s, Vector3D v)
        {
            v.X = u.X * s;
            v.Y = u.Y * s;
            v.Z = u.Z * s;
        }
        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <returns>The dot product value.</returns>
        public static double DotProduct(Vector3D u, Vector3D v)
        {
            return (u.X * v.X) + (u.Y * v.Y) + (u.Z * v.Z);
        }
        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <returns>A new <see cref="Vector3D"/> containing the cross product result.</returns>
        public static Vector3D CrossProduct(Vector3D u, Vector3D v)
        {
            return new Vector3D(
                u.Y * v.Z - u.Z * v.Y,
                u.Z * v.X - u.X * v.Z,
                u.X * v.Y - u.Y * v.X);
        }
        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="w">A <see cref="Vector3D"/> instance to hold the cross product result.</param>
        public static void CrossProduct(Vector3D u, Vector3D v, Vector3D w)
        {
            w.X = u.Y * v.Z - u.Z * v.Y;
            w.Y = u.Z * v.X - u.X * v.Z;
            w.Z = u.X * v.Y - u.Y * v.X;
        }
        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the negated values.</returns>
        public static Vector3D Negate(Vector3D v)
        {
            return new Vector3D(-v.X, -v.Y, -v.Z);
        }
        /// <summary>
        /// Tests whether two vectors are approximately equal using default tolerance value.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
        public static bool ApproxEqual(Vector3D v, Vector3D u)
        {
            return ApproxEqual(v, u, MathFunctions.EpsilonD);
        }
        /// <summary>
        /// Tests whether two vectors are approximately equal given a tolerance value.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="tolerance">The tolerance value used to test approximate equality.</param>
        /// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
        public static bool ApproxEqual(Vector3D v, Vector3D u, double tolerance)
        {
            return
                (
                (System.Math.Abs(v.X - u.X) <= tolerance) &&
                (System.Math.Abs(v.Y - u.Y) <= tolerance) &&
                (System.Math.Abs(v.Z - u.Z) <= tolerance)
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
            _z /= length;
        }
        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>The length of the vector. (Sqrt(X*X + Y*Y + Z*Z))</returns>
        public double GetLength()
        {
            return System.Math.Sqrt(_x * _x + _y * _y + _z * _z);
        }
        /// <summary>
        /// Returns the squared length of the vector.
        /// </summary>
        /// <returns>The squared length of the vector. (X*X + Y*Y + Z*Z)</returns>
        public double GetLengthSquared()
        {
            return (_x * _x + _y * _y + _z * _z);
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
        /// <returns>True if <paramref name="obj"/> is a <see cref="Vector3D"/> and has the same values as this instance; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector3D)
            {
                Vector3D v = (Vector3D)obj;
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
        public static bool operator ==(Vector3D u, Vector3D v)
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
        public static bool operator !=(Vector3D u, Vector3D v)
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
        public static bool operator >(Vector3D u, Vector3D v)
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
        public static bool operator <(Vector3D u, Vector3D v)
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
        public static bool operator >=(Vector3D u, Vector3D v)
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
        public static bool operator <=(Vector3D u, Vector3D v)
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
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the negated values.</returns>
        public static Vector3D operator -(Vector3D v)
        {
            return Vector3D.Negate(v);
        }
        #endregion

        #region Binary Operators
        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
        public static Vector3D operator +(Vector3D u, Vector3D v)
        {
            return Vector3D.Add(u, v);
        }
        /// <summary>
        /// Adds a vector and a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
        public static Vector3D operator +(Vector3D v, double s)
        {
            return Vector3D.Add(v, s);
        }
        /// <summary>
        /// Adds a vector and a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the sum.</returns>
        public static Vector3D operator +(double s, Vector3D v)
        {
            return Vector3D.Add(v, s);
        }
        /// <summary>
        /// Subtracts a vector from a vector.
        /// </summary>
        /// <param name="u">A <see cref="Vector3D"/> instance.</param>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
        /// <remarks>
        ///	result[i] = v[i] - w[i].
        /// </remarks>
        public static Vector3D operator -(Vector3D u, Vector3D v)
        {
            return Vector3D.Subtract(u, v);
        }
        /// <summary>
        /// Subtracts a scalar from a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
        /// <remarks>
        /// result[i] = v[i] - s
        /// </remarks>
        public static Vector3D operator -(Vector3D v, double s)
        {
            return Vector3D.Subtract(v, s);
        }
        /// <summary>
        /// Subtracts a vector from a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> instance containing the difference.</returns>
        /// <remarks>
        /// result[i] = s - v[i]
        /// </remarks>
        public static Vector3D operator -(double s, Vector3D v)
        {
            return Vector3D.Subtract(s, v);
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> containing the result.</returns>
        public static Vector3D operator *(Vector3D v, double s)
        {
            return Vector3D.Multiply(v, s);
        }
        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar.</param>
        /// <returns>A new <see cref="Vector3D"/> containing the result.</returns>
        public static Vector3D operator *(double s, Vector3D v)
        {
            return Vector3D.Multiply(v, s);
        }
        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
        /// <remarks>
        /// result[i] = v[i] / s;
        /// </remarks>
        public static Vector3D operator /(Vector3D v, double s)
        {
            return Vector3D.Divide(v, s);
        }
        /// <summary>
        /// Divides a scalar by a vector.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <param name="s">A scalar</param>
        /// <returns>A new <see cref="Vector3D"/> containing the quotient.</returns>
        /// <remarks>
        /// result[i] = s / v[i]
        /// </remarks>
        public static Vector3D operator /(double s, Vector3D v)
        {
            return Vector3D.Divide(s, v);
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
                switch (index)
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
                switch (index)
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
        /// Converts the vector to an array of double-precision floating point values.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <returns>An array of double-precision floating point values.</returns>
        public static explicit operator double[](Vector3D v)
        {
            double[] array = new double[3];
            array[0] = v.X;
            array[1] = v.Y;
            array[2] = v.Z;
            return array;
        }
        /// <summary>
        /// Converts the vector to an array of double-precision floating point values.
        /// </summary>
        /// <param name="v">A <see cref="Vector3D"/> instance.</param>
        /// <returns>An array of double-precision floating point values.</returns>
        public static explicit operator DoubleArrayList(Vector3D v)
        {
            DoubleArrayList array = new DoubleArrayList(3);
            array[0] = v.X;
            array[1] = v.Y;
            array[2] = v.Z;
            return array;
        }
        #endregion

    }

}
