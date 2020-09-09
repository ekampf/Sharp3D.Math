using System;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry3D
{
    /// <summary>
    /// Represents a sphere in 3D space.
    /// </summary>
    [Serializable]
    public struct Sphere : ISerializable, ICloneable
    {
        #region Private Fields
        private Vector3F _center;
        private float _radius;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Sphere"/> class using center and radius values.
        /// </summary>
        /// <param name="center">The sphere center point.</param>
        /// <param name="radius">The sphere radius.</param>
        public Sphere(Vector3F center, float radius)
        {
            _center = center;
            _radius = radius;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sphere"/> class using values from another sphere instance.
        /// </summary>
        /// <param name="sphere">A <see cref="Sphere"/> instance to take values from.</param>
        public Sphere(Sphere sphere)
        {
            _center = sphere.Center;
            _radius = sphere.Radius;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3F"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private Sphere(SerializationInfo info, StreamingContext context)
        {
            _center = (Vector3F)info.GetValue("Center", typeof(Vector3F));
            _radius = info.GetSingle("Radius");
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the sphere's center.
        /// </summary>
        public Vector3F Center
        {
            get { return _center; }
            set { _center = value; }
        }
        /// <summary>
        /// Gets or sets the sphere's radius.
        /// </summary>
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
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
            info.AddValue("Center", _center, typeof(Vector3F));
            info.AddValue("Radius", _radius);
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="Sphere"/> object.
        /// </summary>
        /// <returns>The <see cref="Sphere"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Sphere(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="Sphere"/> object.
        /// </summary>
        /// <returns>The <see cref="Sphere"/> object this method creates.</returns>
        public Sphere Clone()
        {
            return new Sphere(this);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _center.GetHashCode() ^ _radius.GetHashCode();
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to
        /// the specified object.
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns>True if <paramref name="obj"/> is a <see cref="Vector2D"/> and has the same values as this instance; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Sphere)
            {
                Sphere s = (Sphere)obj;
                return
                    (_center == s.Center) && (_radius == s.Radius);
            }
            return false;
        }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            return string.Format("Sphere(Center={0}, Radius={1})", _center, _radius);
        }
        #endregion

        #region Comparison Operators
        /// <summary>
        /// Tests whether two specified spheres are equal.
        /// </summary>
        /// <param name="a">The left-hand sphere.</param>
        /// <param name="b">The right-hand sphere.</param>
        /// <returns>True if the two spheres are equal; otherwise, False.</returns>
        public static bool operator ==(Sphere a, Sphere b)
        {
            if (Object.Equals(a, null) == true)
            {
                return Object.Equals(b, null);
            }

            if (Object.Equals(b, null) == true)
            {
                return Object.Equals(a, null);
            }

            return
                (a.Center == b.Center) && (a.Radius == b.Radius);
        }
        /// <summary>
        /// Tests whether two specified spheres are not equal.
        /// </summary>
        /// <param name="a">The left-hand sphere.</param>
        /// <param name="b">The right-hand sphere.</param>
        /// <returns>True if the two spheres are not equal; otherwise, False.</returns>
        public static bool operator !=(Sphere a, Sphere b)
        {
            if (Object.Equals(a, null) == true)
            {
                return !Object.Equals(b, null);
            }
            else if (Object.Equals(b, null) == true)
            {
                return !Object.Equals(a, null);
            }
            return !((a.Center == b.Center) && (a.Radius == b.Radius));
        }
        #endregion
    }
}
