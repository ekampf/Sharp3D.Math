using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry3D
{
    /// <summary>
    /// Represents a ray in 3D space.
    /// </summary>
    /// <remarks>
    /// A ray is R(t) = Origin + t * Direction where Direction isnt necessarily of unit length.
    /// </remarks>
    [Serializable]
    public struct Ray : ICloneable, ISerializable
    {
        #region Private Fields
        private Vector3F _origin;
        private Vector3F _direction;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Ray"/> class using given origin and direction vectors.
        /// </summary>
        /// <param name="origin">Ray's origin point.</param>
        /// <param name="direction">Ray's direction vector.</param>
        public Ray(Vector3F origin, Vector3F direction)
        {
            _origin = origin;
            _direction = direction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ray"/> class using given ray.
        /// </summary>
        /// <param name="ray">A <see cref="Ray"/> instance to assign values from.</param>
        public Ray(Ray ray)
        {
            _origin = ray.Origin;
            _direction = ray.Direction;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Ray"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private Ray(SerializationInfo info, StreamingContext context)
        {
            _origin = (Vector3F)info.GetValue("Origin", typeof(Vector3F));
            _direction = (Vector3F)info.GetValue("Direction", typeof(Vector3F));
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the ray's origin.
        /// </summary>
        public Vector3F Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }
        /// <summary>
        /// Gets or sets the ray's direction vector.
        /// </summary>
        public Vector3F Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="Ray"/> object.
        /// </summary>
        /// <returns>The <see cref="Ray"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Ray(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="Ray"/> object.
        /// </summary>
        /// <returns>The <see cref="Ray"/> object this method creates.</returns>
        public Ray Clone()
        {
            return new Ray(this);
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate with data. </param>
        /// <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Origin", _origin, typeof(Vector3F));
            info.AddValue("Direction", _direction, typeof(Vector3F));
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets a point on the ray.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Vector3F GetPointOnRay(float t)
        {
            return (Origin + Direction * t);
        }

        #endregion

        #region Overrides
        /// <summary>
        /// Get the hashcode for this vector instance.
        /// </summary>
        /// <returns>Returns the hash code for this vector instance.</returns>
        public override int GetHashCode()
        {
            return _origin.GetHashCode() ^ _direction.GetHashCode();
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to
        /// the specified object.
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns>True if <paramref name="obj"/> is a <see cref="Vector3F"/> and has the same values as this instance; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Ray)
            {
                Ray r = (Ray)obj;
                return (_origin == r.Origin) && (_direction == r.Direction);
            }
            return false;
        }
        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            return "Ray[Origin=" + Origin.ToString() + " Direction=" + Direction.ToString() + "]";
        }
        #endregion

        #region Comparison Operators
        /// <summary>
        /// Tests whether two specified rays are equal.
        /// </summary>
        /// <param name="a">The first of two rays to compare.</param>
        /// <param name="b">The second of two rays to compare.</param>
        /// <returns>True if the two rays are equal; otherwise, False.</returns>
        public static bool operator ==(Ray a, Ray b)
        {
            if (Object.Equals(a, null))
            {
                return Object.Equals(b, null);
            }

            if (Object.Equals(b, null))
            {
                return Object.Equals(a, null);
            }

            return (a.Origin == b.Origin) && (a.Direction == b.Direction);
        }
        /// <summary>
        /// Tests whether two specified rays are not equal.
        /// </summary>
        /// <param name="a">The first of two rays to compare.</param>
        /// <param name="b">The second of two rays to compare.</param>
        /// <returns>True if the two rays are not equal; otherwise, False.</returns>
        public static bool operator !=(Ray a, Ray b)
        {
            if (Object.Equals(a, null))
            {
                return !Object.Equals(b, null);
            }

            if (Object.Equals(b, null))
            {
                return !Object.Equals(a, null);
            }

            return !((a.Origin == b.Origin) && (a.Direction == b.Direction));
        }

        #endregion


    }
}
