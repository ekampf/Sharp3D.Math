using System;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry3D
{
    /// <summary>
    /// Represents an axis aligned box in 3D space.
    /// </summary>
    /// <remarks>
    /// An axis-aligned box is a box whose faces coincide with the standard basis axes.
    /// </remarks>
    [Serializable]
    public struct AxisAlignedBox : ISerializable, ICloneable
    {
        #region Private Fields
        private Vector3F _min;
        private Vector3F _max;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AxisAlignedBox"/> class using given minimum and maximum points.
        /// </summary>
        /// <param name="min">A <see cref="Vector3F"/> instance representing the minimum point.</param>
        /// <param name="max">A <see cref="Vector3F"/> instance representing the maximum point.</param>
        public AxisAlignedBox(Vector3F min, Vector3F max)
        {
            _min = min;
            _max = max;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AxisAlignedBox"/> class using given values from another box instance.
        /// </summary>
        /// <param name="box">A <see cref="AxisAlignedBox"/> instance to take values from.</param>
        public AxisAlignedBox(AxisAlignedBox box)
        {
            _min = box.Min;
            _max = box.Max;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AxisAlignedBox"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private AxisAlignedBox(SerializationInfo info, StreamingContext context)
        {
            _min = (Vector3F)info.GetValue("Min", typeof(Vector3F));
            _max = (Vector3F)info.GetValue("Max", typeof(Vector3F));
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the minimum point which is the box's minimum X and Y coordinates.
        /// </summary>
        public Vector3F Min
        {
            get { return _min; }
            set { _min = value; }
        }
        /// <summary>
        /// Gets or sets the maximum point which is the box's maximum X and Y coordinates.
        /// </summary>
        public Vector3F Max
        {
            get { return _max; }
            set { _max = value; }
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
            info.AddValue("Max", _max, typeof(Vector3F));
            info.AddValue("Min", _min, typeof(Vector3F));
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="AxisAlignedBox"/> object.
        /// </summary>
        /// <returns>The <see cref="AxisAlignedBox"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new AxisAlignedBox(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="AxisAlignedBox"/> object.
        /// </summary>
        /// <returns>The <see cref="AxisAlignedBox"/> object this method creates.</returns>
        public AxisAlignedBox Clone()
        {
            return new AxisAlignedBox(this);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Computes the box vertices. 
        /// </summary>
        /// <returns>An array of <see cref="Vector3F"/> containing the box vertices.</returns>
        public Vector3F[] ComputeVertices()
        {
            Vector3F[] vertices = new Vector3F[8];

            vertices[0] = _min;
            vertices[1] = new Vector3F(_max.X, _min.Y, _min.Z);
            vertices[2] = new Vector3F(_max.X, _max.Y, _min.Z);
            vertices[4] = new Vector3F(_min.X, _max.Y, _min.Z);

            vertices[5] = new Vector3F(_min.X, _min.Y, _max.Z);
            vertices[6] = new Vector3F(_max.X, _min.Y, _max.Z);
            vertices[7] = _max;
            vertices[8] = new Vector3F(_min.X, _max.Y, _max.Z);

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
            return _min.GetHashCode() ^ _max.GetHashCode();
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to
        /// the specified object.
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns>True if <paramref name="obj"/> is a <see cref="Vector3F"/> and has the same values as this instance; otherwise, False.</returns>
        public override bool Equals(object obj)
        {
            if (obj is AxisAlignedBox)
            {
                AxisAlignedBox box = (AxisAlignedBox)obj;
                return (_min == box.Min) && (_max == box.Max);
            }
            return false;
        }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            return string.Format("AxisAlignedBox(Min={0}, Max={1})", _min, _max);
        }
        #endregion

        #region Comparison Operators
        /// <summary>
        /// Checks if the two given boxes are equal.
        /// </summary>
        /// <param name="a">The first of two boxes to compare.</param>
        /// <param name="b">The second of two boxes to compare.</param>
        /// <returns><b>true</b> if the boxes are equal; otherwise, <b>false</b>.</returns>
        public static bool operator ==(AxisAlignedBox a, AxisAlignedBox b)
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
                (a.Min == b.Min) && (a.Max == b.Max);
        }

        /// <summary>
        /// Checks if the two given boxes are not equal.
        /// </summary>
        /// <param name="a">The first of two boxes to compare.</param>
        /// <param name="b">The second of two boxes to compare.</param>
        /// <returns><b>true</b> if the vectors are not equal; otherwise, <b>false</b>.</returns>
        public static bool operator !=(AxisAlignedBox a, AxisAlignedBox b)
        {
            if (Object.Equals(a, null) == true)
            {
                return !Object.Equals(b, null);
            }
            else if (Object.Equals(b, null) == true)
            {
                return !Object.Equals(a, null);
            }
            return !((a.Min == b.Min) && (a.Max == b.Max));
        }
        #endregion
    }
}
