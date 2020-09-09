using System;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;


using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry3D
{
    /// <summary>
    /// Represents a polygon in 3 dimentional space.
    /// </summary>
    [Serializable]
    public class Polygon : ICloneable, ISerializable, IEnumerable
    {
        #region Private fields
        private Vector3FArrayList _points = new Vector3FArrayList();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        public Polygon()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class using an array of coordinates.
        /// </summary>
        /// <param name="points">An <see cref="Vector3FArrayList"/> instance.</param>
        public Polygon(Vector3FArrayList points)
        {
            _points.AddRange(points);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class using an array of coordinates.
        /// </summary>
        /// <param name="points">An array of <see cref="Vector3F"/> coordniates.</param>
        public Polygon(Vector3F[] points)
        {
            _points.AddRange(points);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class using coordinates from another instance.
        /// </summary>
        /// <param name="polygon">A <see cref="Polygon"/> instance.</param>
        public Polygon(Polygon polygon)
        {
            _points = (Vector3FArrayList)polygon._points.Clone();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private Polygon(SerializationInfo info, StreamingContext context)
        {
            _points = (Vector3FArrayList)info.GetValue("Points", typeof(Vector3FArrayList));
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the polygon's list of points.
        /// </summary>
        public Vector3FArrayList Points
        {
            get { return _points; }
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates an exact copy of this <see cref="Polygon"/> object.
        /// </summary>
        /// <returns>The <see cref="Polygon"/> object this method creates, cast as an object.</returns>
        object ICloneable.Clone()
        {
            return new Polygon(this);
        }
        /// <summary>
        /// Creates an exact copy of this <see cref="Polygon"/> object.
        /// </summary>
        /// <returns>The <see cref="Polygon"/> object this method creates.</returns>
        public Polygon Clone()
        {
            return new Polygon(this);
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
            info.AddValue("Points", _points, typeof(Vector3FArrayList));
        }
        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Returns an <see cref="IVector3FEnumerator"/> that can
        /// iterate through the polygon point..
        /// </summary>
        /// <returns>An <see cref="IVector3FEnumerator"/> for the polygon's points.</returns>
        public IVector3FEnumerator GetEnumerator()
        {
            return _points.GetEnumerator();
        }
        /// <summary>
        /// Returns an <see cref="IEnumerator"/> that can
        /// iterate through the polygon point..
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> for the polygon's points.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)_points.GetEnumerator();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Flips the polygon.
        /// </summary>
        public void Flip()
        {
            _points.Reverse();
        }
        #endregion
    }
}
