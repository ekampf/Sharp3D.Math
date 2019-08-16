#region Sharp3D.Math, Copyright(C) 2003-2004 Eran Kampf, Licensed under LGPL.
//	Sharp3D.Math math library
//	Copyright (C) 2003-2004  
//	Eran Kampf
//	tentacle@zahav.net.il
//	http://tentacle.flipcode.com
//
//	This library is free software; you can redistribute it and/or
//	modify it under the terms of the GNU Lesser General Public
//	License as published by the Free Software Foundation; either
//	version 2.1 of the License, or (at your option) any later version.
//
//	This library is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//	Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
#endregion
using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry3D
{
	/// <summary>
	/// An enumeration representing the sides of the plane.
	/// </summary>
	public enum PlaneSide
	{
		/// <summary>
		/// Represents the plane itself.
		/// </summary>
		None,
		/// <summary>
		/// Represents the positive halfspace of the plane (the side where the normal points to).
		/// </summary>
		Positive,
		/// <summary>
		/// Represents the negative halfspace of the plane
		/// </summary>
		Negative
	}

	/// <summary>
	/// Represents a plane in 3D space.
	/// </summary>
	/// <remarks>
	/// The plane is described by a normal and a constant (N,D) which 
	/// denotes that the plane is consisting of points Q that
	/// satisfies (N dot Q)+D = 0.
	/// </remarks>
	[Serializable]
	public struct Plane : ISerializable, ICloneable
	{
		#region Private Fields
		private Vector3F _normal;
		private float _const;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Plane"/> class using given normal and constant values.
		/// </summary>
		/// <param name="normal">The plane's normal vector.</param>
		/// <param name="constant">The plane's constant value.</param>
		public Plane(Vector3F normal, float constant)
		{
			_normal	= normal;
			_const	= constant;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Plane"/> class using given normal and a point.
		/// </summary>
		/// <param name="normal">The plane's normal vector.</param>
		/// <param name="point">A point on the plane in 3D space.</param>
		public Plane(Vector3F normal, Vector3F point)
		{
			_normal = normal;
			_const	= Vector3F.DotProduct(normal, point);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Plane"/> class using 3 given points.
		/// </summary>
		/// <param name="p0">A point on the plane in 3D space.</param>
		/// <param name="p1">A point on the plane in 3D space.</param>
		/// <param name="p2">A point on the plane in 3D space.</param>
		public Plane(Vector3F p0, Vector3F p1, Vector3F p2)
		{
			_normal = Vector3F.CrossProduct(p2-p1, p0-p1);
			_normal.Normalize();
			_const	= Vector3F.DotProduct(_normal, p0);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Plane"/> class using given a plane to assign values from.
		/// </summary>
		/// <param name="p">A 3D plane to assign values from.</param>
		public Plane(Plane p)
		{
			_normal	= p.Normal;
			_const	= p.Constant;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Plane"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		private Plane(SerializationInfo info, StreamingContext context)
		{
			_normal = (Vector3F)info.GetValue("Normal", typeof(Vector3F));
			_const	= info.GetSingle("Constant");
		}
		#endregion

		#region Constants
		/// <summary>
		/// Plane on the X axis.
		/// </summary>
		public static readonly Plane XPlane = new Plane(Vector3F.XAxis, Vector3F.Zero);
		/// <summary>
		/// Plane on the Y axis.
		/// </summary>
		public static readonly Plane YPlane = new Plane(Vector3F.YAxis, Vector3F.Zero);
		/// <summary>
		/// Plane on the Z axis.
		/// </summary>
		public static readonly Plane ZPlane = new Plane(Vector3F.ZAxis, Vector3F.Zero);
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the plane's normal vector.
		/// </summary>
		public Vector3F Normal
		{
			get { return _normal; }
			set { _normal = value;}
		}
		/// <summary>
		/// Gets or sets the plane's constant value.
		/// </summary>
		public float Constant
		{
			get { return _const; }
			set { _const = value;}
		}
		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Plane"/> object.
		/// </summary>
		/// <returns>The <see cref="Plane"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Plane(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Plane"/> object.
		/// </summary>
		/// <returns>The <see cref="Plane"/> object this method creates.</returns>
		public Plane Clone()
		{
			return new Plane(this);
		}
		#endregion
	
		#region ISerializable Members
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> to populate with data. </param>
		/// <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Normal", _normal, typeof(Vector3F));
			info.AddValue("Constant", _const);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Flip the plane.
		/// </summary>
		public void Flip()
		{
			_normal = -_normal;
		}
		/// <summary>
		/// Creates a new flipped plane (-normal, constant).
		/// </summary>
		/// <returns>A new <see cref="Plane"/> instance.</returns>
		public Plane GetFlipped()
		{
			return new Plane(-_normal, _const);
		}
		/// <summary>
		/// Returns the points's position relative to the plane itself (i.e Front/Back/On)
		/// </summary>
		/// <param name="p">A point in 3D space.</param>
		/// <returns>A <see cref="MathFunctions.Sign"/>.</returns>
		public MathFunctions.Sign GetSign(Vector3F p) 
		{
			return MathFunctions.GetSign( GetDistanceToPlane( p ) );
		}
		/// <summary>
		/// Get the shortest distance from a 3D vector to the plane.
		/// </summary>
		/// <param name="p">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A float representing the shortest distance from the given vector to the plane.</returns>
		public float GetDistanceToPlane(Vector3F p) 
		{
			return Vector3F.DotProduct( p, this.Normal ) - this.Constant; 
		}
		/// <summary>
		/// Determine whether the line connecting P0 to P1 intersects the plane.
		/// </summary>
		/// <param name="p0">A <see cref="Vector3F"/> instance.</param>
		/// <param name="p1">A <see cref="Vector3F"/> instance.</param>
		/// <returns>True if the line intersects the plane; otherwise, False.</returns>
		public bool IsIntersection(Vector3F p0, Vector3F p1) 
		{
			int sign0 = (int) MathFunctions.GetSign( GetDistanceToPlane( p0 ) );
			int sign1 = (int) MathFunctions.GetSign( GetDistanceToPlane( p1 ) );
			
			return ( sign0 < 0 && sign1 > 0 ) || ( sign0 > 0 && sign1 <= 0 );
		}

		/// <summary>
		/// Determine the exact location where a given line intersects the plane
		/// </summary>
		/// <param name="p0">A <see cref="Vector3F"/> instance.</param>
		/// <param name="p1">A <see cref="Vector3F"/> instance.</param>
		/// <returns>A <see cref="Vector3F"/> instance containing the intersection location.</returns>
		public Vector3F GetIntersection(Vector3F p0, Vector3F p1) 
		{
			Debug.Assert( IsIntersection( p0, p1 ) == true );

			Vector3F Direction = p1 - p0;
			float denominator = Vector3F.DotProduct( Normal, Direction );
			if( denominator == 0 ) 
			{
				throw new DivideByZeroException( "Can not get the intersection of a plane with a line when they are parallel to each other." );
			}
			float t = ( Constant - Vector3F.DotProduct( Normal, p0 ) ) / denominator;
			return	p0 + Direction * t;
		}
		#endregion

		#region Overrides
		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return _normal.GetHashCode() ^ _const.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>True if <paramref name="obj"/> is a <see cref="Vector2D"/> and has the same values as this instance; otherwise, False.</returns>
		public override bool Equals(object obj)
		{
			if(obj is Plane) 
			{
				Plane p = (Plane)obj;
				return (_normal == p.Normal) && (_const == p.Constant);
			}
			return false;
		}

		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format("Plane[n={0}, c={1}]", _normal.ToString(), _const.ToString());
		}
		#endregion
	}
}
