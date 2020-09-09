using System;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry2D
{
    /// <summary>
    /// Represents a circle in 2D space.
    /// </summary>
    [Serializable]
    public struct Circle : ICloneable, ISerializable
    {
        private Vector2F center;
        private float radius;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class using center and radius values.
        /// </summary>
        /// <param name="center">The circle's center point.</param>
        /// <param name="radius">The circle's radius.</param>
        public Circle(Vector2F center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class using values from another circle instance.
        /// </summary>
        /// <param name="circle">A <see cref="Circle"/> instance to take values from.</param>
        public Circle(Circle circle)
        {
            this.center = circle.center;
            this.radius = circle.radius;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2F"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        private Circle(SerializationInfo info, StreamingContext context)
        {
            this.center = (Vector2F)info.GetValue("Center", typeof(Vector2F));
            this.radius = info.GetSingle("Radius");
        }
        #endregion

        #region Constants
        /// <summary>
        /// Unit sphere.
        /// </summary>
        public static readonly Circle UnitCircle = new Circle(new Vector2F(0.0f, 0.0f), 1.0f);
        #endregion

        #region Public Properties
        /// <summary>
        /// The circle's center.
        /// </summary>
        public Vector2F Center
        {
            get
            {
                return this.center;
            }
            set
            {
                this.center = value;
            }
        }
        /// <summary>
        /// The circle's radius.
        /// </summary>
        public float Radius
        {
            get
            {
                return this.radius;
            }
            set
            {
                this.radius = value;
            }
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
            info.AddValue("Center", this.center, typeof(Vector2F));
            info.AddValue("Radius", this.radius);
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        object ICloneable.Clone()
        {
            return new Circle(this);
        }
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public Circle Clone()
        {
            return new Circle(this);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Get the hashcode for this vector instance.
        /// </summary>
        /// <returns>Returns the hash code for this vector instance.</returns>
        public override int GetHashCode()
        {
            return this.center.GetHashCode() ^ this.radius.GetHashCode();
        }
        /// <summary>
        /// Checks if a given <see cref="Circle"/> equals to self.
        /// </summary>
        /// <param name="o">Object to check if equal to.</param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            if (o is Circle)
            {
                Circle c = (Circle)o;
                return
                    (this.center == c.Center) && (this.radius == c.radius);
            }
            return false;
        }
        /// <summary>
        /// Convert <see cref="Circle"/> to a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Circle(Center={0}, Radius={1})", this.center, this.radius);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Checks if the two given circles are equal.
        /// </summary>
        /// <param name="a">The first of two 2D circles to compare.</param>
        /// <param name="b">The second of two 2D circles to compare.</param>
        /// <returns><b>true</b> if the circles are equal; otherwise, <b>false</b>.</returns>
        public static bool operator ==(Circle a, Circle b)
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
                (a.center == b.center) && (a.radius == b.radius);
        }

        /// <summary>
        /// Checks if the two given circles are not equal.
        /// </summary>
        /// <param name="a">The first of two 2D circles to compare.</param>
        /// <param name="b">The second of two 2D circles to compare.</param>
        /// <returns><b>true</b> if the circles are not equal; otherwise, <b>false</b>.</returns>
        public static bool operator !=(Circle a, Circle b)
        {
            if (Object.Equals(a, null) == true)
            {
                return !Object.Equals(b, null);
            }
            else if (Object.Equals(b, null) == true)
            {
                return !Object.Equals(a, null);
            }
            return !((a.center == b.center) && (a.radius == b.radius));
        }
        #endregion

        /// <summary>
        /// Tests if a ray intersects the sphere.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <returns>Returns True if the ray intersects the sphere. Otherwise, False.</returns>
        public bool TestIntersection(Ray ray)
        {
            float squaredDistance = DistanceMethods.SquaredDistance(this.center, ray);
            return (squaredDistance <= this.radius * this.radius);
        }

        /// <summary>
        /// Find the intersection of a ray and a sphere.
        /// Only works with unit rays (normalized direction)!!!
        /// </summary>
        /// <remarks>
        /// This is the optimized Ray-Sphere intersection algorithms described in Realtime-Rendering.
        /// </remarks>
        /// <param name="ray">The ray to test.</param>
        /// <param name="t">
        /// If intersection accurs, the function outputs the distance from the ray's origin 
        /// to the closest intersection point to this parameter.
        /// </param>
        /// <returns>Returns True if the ray intersects the sphere. Otherwise, False.</returns>
        public bool FindIntersections(Ray ray, ref float t)
        {
            // Only gives correct result for unit rays.
            //Debug.Assert(MathUtils.ApproxEquals(1.0f, ray.Direction.GetLength()), "Ray direction should be normalized!");

            // Calculates a vector from the ray origin to the sphere center.
            Vector2F diff = this.center - ray.Origin;
            // Compute the projection of diff onto the ray direction
            float d = Vector2F.DotProduct(diff, ray.Direction);

            float diffSquared = diff.GetLengthSquared();
            float radiusSquared = this.radius * this.radius;

            // First rejection test : 
            // if d<0 and the ray origin is outside the sphere than the sphere is behind the ray
            if ((d < 0.0f) && (diffSquared > radiusSquared))
            {
                return false;
            }

            // Compute the distance from the sphere center to the projection
            float mSquared = diffSquared - d * d;

            // Second rejection test:
            // if mSquared > radiusSquared than the ray misses the sphere
            if (mSquared > radiusSquared)
            {
                return false;
            }

            float q = (float)System.Math.Sqrt(radiusSquared - mSquared);

            // We are interested only in the first intersection point:
            if (diffSquared > radiusSquared)
            {
                // If the origin is outside the sphere t = d - q is the first intersection point
                t = d - q;
            }
            else
            {
                // If the origin is inside the sphere t = d + q is the first intersection point
                t = d + q;
            }
            return true;
        }
    }
}
