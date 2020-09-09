using System;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry2D
{
    /// <summary>
    /// Represents an oriented box in 2D space.
    /// </summary>
    /// <remarks>
    /// An oriented box is a box whose faces have normals that are all pairwise orthogonal-i.e., it is an axis aligned box arbitrarily rotated.
    /// A 2D oriented box is defined by a center point, two orthonormal axes that describe the side
    /// directions of the box, and their respective positive half-lengths extents.
    /// </remarks>
    [Serializable]
    public struct OrientedBox : ISerializable, ICloneable
    {
        #region Private Fields
        private Vector2F _center;
        private Vector2F _axis1;
        private Vector2F _axis2;
        private float _extent1;
        private float _extent2;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="OrientedBox"/> class using given center point, axes and extents.
        /// </summary>
        /// <param name="center">The center of the box..</param>
        /// <param name="axes">The axes of the box.</param>
        /// <param name="extents">The extent values of the box..</param>
        public OrientedBox(Vector2F center, Vector2F[] axes, float[] extents)
        {
            Debug.Assert(axes.Length >= 2);
            Debug.Assert(extents.Length >= 2);

            _center = center;

            _axis1 = axes[0];
            _axis2 = axes[1];

            _extent1 = extents[0];
            _extent2 = extents[1];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OrientedBox"/> class using given values from another box instance.
        /// </summary>
        /// <param name="box">A <see cref="OrientedBox"/> instance to take values from.</param>
        public OrientedBox(OrientedBox box)
        {
            _center = box.Center;

            _axis1 = box.Axis1;
            _axis2 = box.Axis2;

            _extent1 = box.Extent1;
            _extent2 = box.Extent2;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OrientedBox"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private OrientedBox(SerializationInfo info, StreamingContext context)
        {
            _center = (Vector2F)info.GetValue("Center", typeof(Vector2F));

            _axis1 = (Vector2F)info.GetValue("Axis1", typeof(Vector2F));
            _axis2 = (Vector2F)info.GetValue("Axis2", typeof(Vector2F));

            _extent1 = info.GetSingle("Extent1");
            _extent2 = info.GetSingle("Extent2");
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the box's center point.
        /// </summary>
        public Vector2F Center
        {
            get { return _center; }
            set { _center = value; }
        }
        /// <summary>
        /// Gets or sets the box's first axis.
        /// </summary>
        public Vector2F Axis1
        {
            get { return _axis1; }
            set { _axis1 = value; }
        }

        /// <summary>
        /// Gets or sets the box's second axis.
        /// </summary>
        public Vector2F Axis2
        {
            get { return _axis2; }
            set { _axis2 = value; }
        }
        /// <summary>
        /// Gets or sets the box's first extent.
        /// </summary>
        public float Extent1
        {
            get { return _extent1; }
            set { _extent1 = value; }
        }
        /// <summary>
        /// Gets or sets the box's second extent.
        /// </summary>
        public float Extent2
        {
            get { return _extent2; }
            set { _extent2 = value; }
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="OrientedBox"/> object.
        /// </summary>
        /// <returns>The <see cref="OrientedBox"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new OrientedBox(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="OrientedBox"/> object.
        /// </summary>
        /// <returns>The <see cref="OrientedBox"/> object this method creates.</returns>
        public OrientedBox Clone()
        {
            return new OrientedBox(this);
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
            info.AddValue("Center", _center, typeof(Vector2F));

            info.AddValue("Axis1", _axis1, typeof(Vector2F));
            info.AddValue("Axis2", _axis2, typeof(Vector2F));

            info.AddValue("Extent1", _extent1, typeof(Vector2F));
            info.AddValue("Extent2", _extent2, typeof(Vector2F));
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Computes the box vertices. 
        /// </summary>
        /// <returns>An array of <see cref="Vector2F"/> containing the box vertices.</returns>
        public Vector2F[] ComputeVertices()
        {
            Vector2F[] vertices = new Vector2F[4];
            Vector2F[] AxisExtents = new Vector2F[2]
                {
                    Axis1*Extent1, Axis2*Extent2
                };


            vertices[0] = Center - AxisExtents[0] - AxisExtents[1];
            vertices[1] = Center + AxisExtents[0] - AxisExtents[1];
            vertices[2] = Center + AxisExtents[0] + AxisExtents[1];
            vertices[3] = Center - AxisExtents[0] + AxisExtents[1];

            return vertices;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns the hashcode for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _center.GetHashCode() ^ _axis1.GetHashCode() ^ _axis2.GetHashCode() ^ _extent1.GetHashCode() ^ _extent2.GetHashCode();
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to
        /// the specified object.
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns>True if <paramref name="obj"/> is a <see cref="Vector2F"/> and has the same values as this instance; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj is OrientedBox)
            {
                OrientedBox b = (OrientedBox)obj;
                return
                    (_center == b.Center) &&
                    (_axis1 == b.Axis1) && (_axis2 == b.Axis2) &&
                    (_extent1 == b.Extent1) && (_extent2 == b.Extent2);
            }
            return false;
        }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            return string.Format("OrientedBox(Center={0}, Axis1={1}, Axis2={2}, Extent1={3}, Extent2={4})",
                Center, Axis1, Axis2, Extent1, Extent2);
        }
        #endregion

        #region Comparison Operators
        /// <summary>
        /// Tests whether two specified boxes are equal.
        /// </summary>
        /// <param name="a">The left-hand box.</param>
        /// <param name="b">The right-hand box.</param>
        /// <returns>True if the two vectors are equal; otherwise, False.</returns>
        public static bool operator ==(OrientedBox a, OrientedBox b)
        {
            if (Object.Equals(a, null))
            {
                return Object.Equals(b, null);
            }

            if (Object.Equals(b, null))
            {
                return Object.Equals(a, null);
            }

            return
                (a.Center == b.Center) &&
                (a.Axis1 == b.Axis1) && (a.Axis2 == b.Axis2) &&
                (a.Extent1 == b.Extent1) && (a.Extent2 == b.Extent2);
        }
        /// <summary>
        /// Tests whether two specified boxes are not equal.
        /// </summary>
        /// <param name="a">The left-hand box.</param>
        /// <param name="b">The right-hand box.</param>
        /// <returns>True if the two boxes are not equal; otherwise, False.</returns>
        public static bool operator !=(OrientedBox a, OrientedBox b)
        {
            if (Object.Equals(a, null))
            {
                return !Object.Equals(b, null);
            }

            if (Object.Equals(b, null))
            {
                return !Object.Equals(a, null);
            }

            return !(
                (a.Center == b.Center) &&
                (a.Axis1 == b.Axis1) && (a.Axis2 == b.Axis2) &&
                (a.Extent1 == b.Extent1) && (a.Extent2 == b.Extent2)
                );
        }

        #endregion
    }
}
