using System;
using System.Diagnostics;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry3D
{
    /// <summary>
    /// Provides various distance computation methods.
    /// </summary>
    public sealed class DistanceMethods
    {
        #region Point-OBB
        /// <summary>
        /// Calculates the squared distance between a point and a solid oriented box.
        /// </summary>
        /// <param name="point">A <see cref="Vector3F"/> instance.</param>
        /// <param name="obb">An <see cref="OrientedBox"/> instance.</param>
        /// <param name="closestPoint">The closest point in box coordinates.</param>
        /// <returns>The squared distance between a point and a solid oriented box.</returns>
        /// <remarks>
        /// Treating the oriented box as solid means that any point inside the box has
        /// distance zero from the box.
        /// </remarks>
        public static float SquaredDistancePointSolidOrientedBox(Vector3F point, OrientedBox obb, out Vector3F closestPoint)
        {
            Vector3F diff = point - obb.Center;
            Vector3F closest = new Vector3F(
                Vector3F.DotProduct(diff, obb.Axis1),
                Vector3F.DotProduct(diff, obb.Axis2),
                Vector3F.DotProduct(diff, obb.Axis3));

            float sqrDist = 0.0f;
            float delta = 0.0f;

            if (closest.X < -obb.Extent1)
            {
                delta = closest.X + obb.Extent1;
                sqrDist += delta * delta;
                closest.X = -obb.Extent1;
            }
            else if (closest.X > obb.Extent1)
            {
                delta = closest.X - obb.Extent1;
                sqrDist += delta * delta;
                closest.X = obb.Extent1;
            }

            if (closest.Y < -obb.Extent2)
            {
                delta = closest.Y + obb.Extent2;
                sqrDist += delta * delta;
                closest.Y = -obb.Extent2;
            }
            else if (closest.Y > obb.Extent2)
            {
                delta = closest.Y - obb.Extent2;
                sqrDist += delta * delta;
                closest.Y = obb.Extent2;
            }

            if (closest.Z < -obb.Extent3)
            {
                delta = closest.Z + obb.Extent3;
                sqrDist += delta * delta;
                closest.Z = -obb.Extent3;
            }
            else if (closest.Z > obb.Extent3)
            {
                delta = closest.Z - obb.Extent3;
                sqrDist += delta * delta;
                closest.Z = obb.Extent3;
            }

            closestPoint = closest;

            return sqrDist;
        }
        /// <summary>
        /// Calculates the squared distance between a point and a solid oriented box.
        /// </summary>
        /// <param name="point">A <see cref="Vector3F"/> instance.</param>
        /// <param name="obb">An <see cref="OrientedBox"/> instance.</param>
        /// <returns>The squared distance between a point and a solid oriented box.</returns>
        /// <remarks>
        /// Treating the oriented box as solid means that any point inside the box has
        /// distance zero from the box.
        /// </remarks>
        public static float SquaredDistance(Vector3F point, OrientedBox obb)
        {
            Vector3F temp;
            return SquaredDistancePointSolidOrientedBox(point, obb, out temp);
        }

        /// <summary>
        /// Calculates the distance between a point and a solid oriented box.
        /// </summary>
        /// <param name="point">A <see cref="Vector3F"/> instance.</param>
        /// <param name="obb">An <see cref="OrientedBox"/> instance.</param>
        /// <returns>The distance between a point and a solid oriented box.</returns>
        /// <remarks>
        /// Treating the oriented box as solid means that any point inside the box has
        /// distance zero from the box.
        /// </remarks>
        public static float Distance(Vector3F point, OrientedBox obb)
        {
            return (float)System.Math.Sqrt(SquaredDistance(point, obb));
        }
        #endregion

        #region Point-Plane
        /// <summary>
        /// Calculates the distance between a point and a plane.
        /// </summary>
        /// <param name="point">A <see cref="Vector3F"/> instance.</param>
        /// <param name="plane">A <see cref="Plane"/> instance.</param>
        /// <returns>The distance between a point and a plane.</returns>
        /// <remarks>
        /// <p>
        ///  A positive return value means teh point is on the positive side of the plane.
        ///  A negative return value means teh point is on the negative side of the plane.
        ///  A zero return value means the point is on the plane.
        /// </p>
        /// <p>
        ///  The absolute value of the return value is the true distance only when the plane normal is
        ///  a unit length vector. 
        /// </p>
        /// </remarks>
        public static float Distance(Vector3F point, Plane plane)
        {
            return Vector3F.DotProduct(plane.Normal, point) + plane.Constant;
        }
        #endregion

        #region Private Constructor
        private DistanceMethods()
        {
        }
        #endregion
    }
}
