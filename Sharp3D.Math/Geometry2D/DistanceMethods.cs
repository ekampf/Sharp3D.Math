using System;
using System.Diagnostics;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry2D
{
    /// <summary>
    /// Provides various distance computation methods.
    /// </summary>
    public sealed class DistanceMethods
    {
        #region Point-Point
        /// <summary>
        /// Calculates the squared distance between two points.
        /// </summary>
        /// <param name="point1">A <see cref="Vector2F"/> instance.</param>
        /// <param name="point2">A <see cref="Vector2F"/> instance.</param>
        /// <returns>The squared distance between the two points.</returns>
        public static float SquaredDistance(Vector2F point1, Vector2F point2)
        {
            Vector2F diff = point1 - point2;
            return diff.GetLengthSquared();
        }

        /// <summary>
        /// Calculates the distance between two points.
        /// </summary>
        /// <param name="point1">A <see cref="Vector2F"/> instance.</param>
        /// <param name="point2">A <see cref="Vector2F"/> instance.</param>
        /// <returns>The distance between the two points.</returns>
        public static float Distance(Vector2F point1, Vector2F point2)
        {
            return (float)System.Math.Sqrt(SquaredDistance(point1, point2));
        }
        #endregion

        #region Point-Ray
        /// <summary>
        /// Calculates the squared distance between a given point and a given ray.
        /// </summary>
        /// <param name="point">A <see cref="Vector2F"/> instance.</param>
        /// <param name="ray">A <see cref="Ray"/> instance.</param>
        /// <returns>The squared distance between the point and the ray.</returns>
        public static float SquaredDistance(Vector2F point, Ray ray)
        {
            Vector2F diff = point - ray.Origin;
            float t = Vector2F.DotProduct(diff, ray.Direction);

            if (t <= 0.0f)
            {
                t = 0.0f;
            }
            else
            {
                t /= ray.Direction.GetLengthSquared();
                diff -= t * ray.Direction;
            }

            return diff.GetLengthSquared();
        }

        /// <summary>
        /// Calculates the distance between a given point and a given ray.
        /// </summary>
        /// <param name="point">A <see cref="Vector2F"/> instance.</param>
        /// <param name="ray">A <see cref="Ray"/> instance.</param>
        /// <returns>The distance between the point and the ray.</returns>
        public static float Distance(Vector2F point, Ray ray)
        {
            return (float)System.Math.Sqrt(SquaredDistance(point, ray));
        }
        #endregion

        #region Point-AABB
        /// <summary>
        /// Calculates the squared distance between a point and a solid axis-aligned box.
        /// </summary>
        /// <param name="point">A <see cref="Vector2F"/> instance.</param>
        /// <param name="aabb">An <see cref="AxisAlignedBox"/> instance.</param>
        /// <returns>The squared distance between a point and a solid axis-aligned box.</returns>
        /// <remarks>
        /// Treating the box as solid means that any point inside the box has
        /// distance zero from the box.
        /// </remarks>
        public static float SquaredDistance(Vector2F point, AxisAlignedBox aabb)
        {
            float sqrDistance = 0.0f;
            float delta;

            if (point.X < aabb.Min.X)
            {
                delta = point.X - aabb.Min.X;
                sqrDistance += delta * delta;
            }
            else if (point.X > aabb.Max.X)
            {
                delta = point.X - aabb.Max.X;
                sqrDistance += delta * delta;
            }

            if (point.Y < aabb.Min.Y)
            {
                delta = point.Y - aabb.Min.Y;
                sqrDistance += delta * delta;
            }
            else if (point.Y > aabb.Max.Y)
            {
                delta = point.Y - aabb.Max.Y;
                sqrDistance += delta * delta;
            }

            return sqrDistance;
        }

        /// <summary>
        /// Calculates the distance between a point and a solid axis-aligned box.
        /// </summary>
        /// <param name="point">A <see cref="Vector2F"/> instance.</param>
        /// <param name="aabb">An <see cref="AxisAlignedBox"/> instance.</param>
        /// <returns>The distance between a point and a solid axis-aligned box.</returns>
        /// <remarks>
        /// Treating the box as solid means that any point inside the box has
        /// distance zero from the box.
        /// </remarks>
        public static float Distance(Vector2F point, AxisAlignedBox aabb)
        {
            return (float)System.Math.Sqrt(SquaredDistance(point, aabb));
        }
        #endregion

        #region Point-OBB
        /// <summary>
        /// Calculates the squared distance between a point and a solid oriented box.
        /// </summary>
        /// <param name="point">A <see cref="Vector2F"/> instance.</param>
        /// <param name="obb">An <see cref="OrientedBox"/> instance.</param>
        /// <param name="closestPoint">The closest point in box coordinates.</param>
        /// <returns>The squared distance between a point and a solid oriented box.</returns>
        /// <remarks>
        /// Treating the oriented box as solid means that any point inside the box has
        /// distance zero from the box.
        /// </remarks>
        public static float SquaredDistancePointSolidOrientedBox(Vector2F point, OrientedBox obb, out Vector2F closestPoint)
        {
            Vector2F diff = point - obb.Center;
            Vector2F closest = new Vector2F(
                Vector2F.DotProduct(diff, obb.Axis1),
                Vector2F.DotProduct(diff, obb.Axis2));

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

            closestPoint = closest;

            return sqrDist;
        }
        /// <summary>
        /// Calculates the squared distance between a point and a solid oriented box.
        /// </summary>
        /// <param name="point">A <see cref="Vector2F"/> instance.</param>
        /// <param name="obb">An <see cref="OrientedBox"/> instance.</param>
        /// <returns>The squared distance between a point and a solid oriented box.</returns>
        /// <remarks>
        /// Treating the oriented box as solid means that any point inside the box has
        /// distance zero from the box.
        /// </remarks>
        public static float SquaredDistance(Vector2F point, OrientedBox obb)
        {
            Vector2F temp;
            return SquaredDistancePointSolidOrientedBox(point, obb, out temp);
        }

        /// <summary>
        /// Calculates the distance between a point and a solid oriented box.
        /// </summary>
        /// <param name="point">A <see cref="Vector2F"/> instance.</param>
        /// <param name="obb">An <see cref="OrientedBox"/> instance.</param>
        /// <returns>The distance between a point and a solid oriented box.</returns>
        /// <remarks>
        /// Treating the oriented box as solid means that any point inside the box has
        /// distance zero from the box.
        /// </remarks>
        public static float Distance(Vector2F point, OrientedBox obb)
        {
            return (float)System.Math.Sqrt(SquaredDistance(point, obb));
        }
        #endregion

        #region Private Constructor
        private DistanceMethods()
        {
        }
        #endregion
    }
}
