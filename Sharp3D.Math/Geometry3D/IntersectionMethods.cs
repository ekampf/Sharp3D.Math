using System;
using System.Diagnostics;


using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry3D
{
	public struct IntersectionPair
	{
		private bool _occured;
		private Vector3F _point;

		#region Constructors
		/// <summary>
		/// Initialize a new instance of the <see cref="IntersectionPair"/> class.
		/// </summary>
		/// <param name="intersectionOccured">A boolean value.</param>
		/// <param name="intersectionPoint">A <see cref="Vector3F"/> instance.</param>
		public IntersectionPair(bool intersectionOccured, Vector3F intersectionPoint)
		{
			_occured = intersectionOccured;
			_point = intersectionPoint;
		}

		#endregion

		#region Public Properties
		/// <summary>
		/// Gets a value indicating if an intersection ahs occured.
		/// </summary>
		/// <value>A boolean value.</value>
		public bool IntersectionOccured
		{
			get { return _occured; }
		}

		/// <summary>
		/// Gets the intersection point if intersection has occured.
		/// </summary>
		/// <value>A <see cref="Vector3F"/> instance.</value>
		public Vector3F IntersectionPoint
		{
			get { return _point; }
		}
		#endregion
	}

	public enum IntersectionType
	{
		/// <summary>
		/// Objects do not intersect
		/// </summary>
		None,
		/// <summary>
		/// Objects parially intersect each other.
		/// </summary>
		Partial,
		/// <summary>
		/// An object is fully contained in another object.
		/// </summary>
		Contained
	}

	/// <summary>
	/// Provides method for testing intersections between objects.
	/// </summary>
	public sealed class IntersectionMethods
	{
		#region Ray to plane,AABB,OBB, Triangle and Sphere intersection methods
		/// <summary>
		/// Tests for intersection between a ray and a plane.
		/// </summary>
		/// <param name="ray">A <see cref="Ray"/> instance.</param>
		/// <param name="plane">A <see cref="Plane"/> instance.</param>
		/// <returns>An <see cref="IntersectionPair"/> instance containing the intersection information.</returns>
		public static IntersectionPair Intersects(Ray ray, Plane plane)
		{
			bool intersect = false;
			Vector3F hitPoint = Vector3F.Zero;
			float denominator = Vector3F.DotProduct(plane.Normal, ray.Direction);

			// Check if the ray is parrallel to the plane
			if (MathFunctions.ApproxEquals(denominator, 0.0f))
			{
				// If the line is parallel to the plane it only intersects the plane if it is on the plane.
				intersect = (plane.GetSign(ray.Origin) == MathFunctions.Sign.Zero);
				if (intersect)
					hitPoint = ray.Origin;
			}
			else
			{
				float t = (plane.Constant - Vector3F.DotProduct(plane.Normal, ray.Origin)) / denominator;
				hitPoint = ray.Origin + ray.Direction * t;
				intersect = true;
			}

			return new IntersectionPair(intersect, hitPoint);
		}

		/// <summary>
		/// Tests for intersection between a ray and a an axis aligned box.
		/// </summary>
		/// <param name="ray">A <see cref="Ray"/> instance.</param>
		/// <param name="aabb">A <see cref="AxisAlignedBox"/> instance.</param>
		/// <returns>An <see cref="IntersectionPair"/> instance containing the intersection information.</returns>
		public static IntersectionPair Intersects(Ray ray, AxisAlignedBox aabb)
		{

			throw new NotImplementedException();
		}
		/// <summary>
		/// Tests for intersection between a ray and a an oriented box.
		/// </summary>
		/// <param name="ray">A <see cref="Ray"/> instance.</param>
		/// <param name="obb">A <see cref="OrientedBox"/> instance.</param>
		/// <returns>An <see cref="IntersectionPair"/> instance containing the intersection information.</returns>
		public static IntersectionPair Intersects(Ray ray, OrientedBox obb)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Tests for intersection between a ray and a a sphere.
		/// </summary>
		/// <param name="ray">A <see cref="Ray"/> instance.</param>
		/// <param name="sphere">A <see cref="Sphere"/> instance.</param>
		/// <returns>An <see cref="IntersectionPair"/> instance containing the intersection information.</returns>
		public static IntersectionPair Intersects(Ray ray, Sphere sphere)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Tests for intersection between a Ray and a Triangle.
		/// </summary>
		/// <param name="ray">A <see cref="Ray"/> instance.</param>
		/// <param name="triangle">A <see cref="Triangle"/> instance.</param>
		/// <returns>An <see cref="IntersectionPair"/> instance containing the intersection information.</returns>
		/// <remarks>
		/// For information about the algorithm visit http://www.acm.org/jgt/papers/MollerTrumbore97/ 
		/// </remarks>
		public static IntersectionPair Intersects(Ray ray, Triangle triangle)
		{
			// Find the vectors for the 2 edges sharing trangle.Point0
			Vector3F edge1 = triangle.Point1 - triangle.Point0;
			Vector3F edge2 = triangle.Point2 - triangle.Point0;

			// Begin calculating determinant - also used to calc the U parameter.
			Vector3F pvec = Vector3F.CrossProduct(ray.Direction, edge2);

			float det = Vector3F.DotProduct(edge1, pvec);

			// If determinant is zero the ray lies in plane of the triangle
			if (MathFunctions.ApproxEquals(det, 0.0f))
			{
				return new IntersectionPair(false, Vector3F.Zero);
			}

			float invDet = 1.0f / det;

			// Calculate the distance from triangle.Point0 to the ray origin
			Vector3F tvec = ray.Origin - triangle.Point0;

			// Calculate U parameter and test bounds
			float u = Vector3F.DotProduct(tvec, pvec) * invDet;
			if ((u < 0.0f) || u > 1.0f)
				return new IntersectionPair(false, Vector3F.Zero);

			// Prepare to test the V parameter
			Vector3F qvec  = Vector3F.CrossProduct(tvec, edge1);

			// Calculate V parameter and test bounds
			float v = Vector3F.DotProduct(ray.Direction, qvec) * invDet;
			if ((v < 0.0f) || v > 1.0f)
				return new IntersectionPair(false, Vector3F.Zero);

			// The ray intersects the triangle
			// Calculate the distance from  the ray origin to the intersection point.
			//t = Vector3F.DotProduct(edge2, qvec) * invDet;

			return new IntersectionPair(true, triangle.FromBarycentric(u,v));
		}

		#endregion

		/// <summary>
		/// Tests for intersection between two <see cref="AxisAlignedBox"/> instances.
		/// </summary>
		/// <param name="box1">An <see cref="AxisAlignedBox"/> instance.</param>
		/// <param name="box2">An <see cref="AxisAlignedBox"/> instance.</param>
		/// <returns>An <see cref="IntersectionType"/> value.</returns>
		public static IntersectionType Intersects(AxisAlignedBox box1, AxisAlignedBox box2)
		{
			Vector3F min1 = box1.Min; 
			Vector3F max1 = box1.Max; 
			Vector3F min2 = box2.Min; 
			Vector3F max2 = box2.Max; 

			if ((min2.X > min1.X) && 
				(max2.X < max1.X) && 
				(min2.Y > min1.Y) && 
				(max2.Y < max1.Y) && 
				(min2.Z > min1.Z) && 
				(max2.Z < max1.Z)) 
			{
				// box2 contains box1
				return IntersectionType.Contained; 
			}

			if ((min2.X > max2.X) || 
				(min2.Y > max2.Y) || 
				(min2.Z > max2.Z) || 
				(max2.X < min1.X) || 
				(max2.Y < min1.Y) || 
				(max2.Z < min1.Z)) 
			{

				// The two boxes are not intersecting.
				return IntersectionType.None; 
			}

			// if we got this far, they are partially intersecting
			return IntersectionType.Partial; 
		}
		/// <summary>
		/// Tests for intersection between an <see cref="AxisAlignedBox"/> and an <see cref="OrientedBox"/> .
		/// </summary>
		/// <param name="box1">An <see cref="AxisAlignedBox"/> instance.</param>
		/// <param name="box2">An <see cref="OrientedBox"/> instance.</param>
		/// <returns>An <see cref="IntersectionType"/> value.</returns>
		public static IntersectionType Intersects(AxisAlignedBox box1, OrientedBox box2)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Tests for intersection between two <see cref="OrientedBox"/> instances.
		/// </summary>
		/// <param name="box1">An <see cref="OrientedBox"/> instance.</param>
		/// <param name="box2">An <see cref="OrientedBox"/> instance.</param>
		/// <returns>An <see cref="IntersectionType"/> value.</returns>
		public static IntersectionType Intersects(OrientedBox box1, OrientedBox box2)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Tests for intersection between two <see cref="Sphere"/> instances.
		/// </summary>
		/// <param name="sphere1">A <see cref="Sphere"/> instance.</param>
		/// <param name="sphere2">A <see cref="Sphere"/> instance.</param>
		/// <returns>An <see cref="IntersectionType"/> value.</returns>
		public static IntersectionType Intersects(Sphere sphere1, Sphere sphere2)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Tests for intersection between a <see cref="Sphere"/> and an <see cref="AxisAlignedBox"/> .
		/// </summary>
		/// <param name="sphere">A <see cref="Sphere"/> instance.</param>
		/// <param name="box">An <see cref="AxisAlignedBox"/> instance.</param>
		/// <returns>An <see cref="IntersectionType"/> value.</returns>
		public static IntersectionType Intersects(Sphere sphere, AxisAlignedBox box)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Tests for intersection between a <see cref="Sphere"/> and an <see cref="OrientedBox"/> .
		/// </summary>
		/// <param name="sphere">A <see cref="Sphere"/> instance.</param>
		/// <param name="box">An <see cref="OrientedBox"/> instance.</param>
		/// <returns>An <see cref="IntersectionType"/> value.</returns>
		public static IntersectionType Intersects(Sphere sphere, OrientedBox box)
		{
			throw new NotImplementedException();
		}


		#region Private Constructor
		private IntersectionMethods()
		{
		}
		#endregion
	}
}
