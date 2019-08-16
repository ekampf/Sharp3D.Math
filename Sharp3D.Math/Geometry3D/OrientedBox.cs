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
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Geometry3D
{
	/// <summary>
	/// Represents an oriented box in 3D space.
	/// </summary>
	/// <remarks>
	/// An oriented box is a box whose faces have normals that are all pairwise orthogonal-i.e., it is an axis aligned box arbitrarily rotated.
	/// A 3D oriented box is defined by a center point, two orthonormal axes that describe the side
	/// directions of the box, and their respective positive half-lengths extents.
	/// </remarks>
	[Serializable]
	public struct OrientedBox : ISerializable, ICloneable
	{
		#region Private Fields
		private Vector3F _center;
		private Vector3F _axis1;
		private Vector3F _axis2;
		private Vector3F _axis3;
		private float _extent1;
		private float _extent2;
		private float _extent3;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="OrientedBox"/> class using given center point, axes and extents.
		/// </summary>
		/// <param name="center">The center of the box..</param>
		/// <param name="axes">The axes of the box.</param>
		/// <param name="extents">The extent values of the box..</param>
		public OrientedBox(Vector3F center, Vector3F[] axes, float[] extents)
		{
			Debug.Assert(axes.Length >= 3);
			Debug.Assert(extents.Length >= 3);

			_center = center;
			
			_axis1 = axes[0];
			_axis2 = axes[1];
			_axis3 = axes[2];

			_extent1 = extents[0];
			_extent2 = extents[1];
			_extent3 = extents[2];
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
			_axis3 = box.Axis3;

			_extent1 = box.Extent1;
			_extent2 = box.Extent2;
			_extent3 = box.Extent3;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="OrientedBox"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		private OrientedBox(SerializationInfo info, StreamingContext context)
		{
			_center = (Vector3F)info.GetValue("Center", typeof(Vector3F));

			_axis1 = (Vector3F)info.GetValue("Axis1", typeof(Vector3F));
			_axis2 = (Vector3F)info.GetValue("Axis2", typeof(Vector3F));
			_axis3 = (Vector3F)info.GetValue("Axis3", typeof(Vector3F));

			_extent1 = info.GetSingle("Extent1");
			_extent2 = info.GetSingle("Extent2");
			_extent3 = info.GetSingle("Extent3");
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the box's center point.
		/// </summary>
		public Vector3F Center
		{
			get { return _center; }
			set { _center = value;}
		}
		/// <summary>
		/// Gets or sets the box's first axis.
		/// </summary>
		public Vector3F Axis1
		{
			get { return _axis1; }
			set { _axis1 = value;}
		}

		/// <summary>
		/// Gets or sets the box's second axis.
		/// </summary>
		public Vector3F Axis2
		{
			get { return _axis2; }
			set { _axis2 = value;}
		}
		/// <summary>
		/// Gets or sets the box's third axis.
		/// </summary>
		public Vector3F Axis3
		{
			get { return _axis3; }
			set { _axis3 = value;}
		}
		/// <summary>
		/// Gets or sets the box's first extent.
		/// </summary>
		public float Extent1
		{
			get { return _extent1; }
			set { _extent1 = value;}
		}
		/// <summary>
		/// Gets or sets the box's second extent.
		/// </summary>
		public float Extent2
		{
			get { return _extent2; }
			set { _extent2 = value;}
		}
		/// <summary>
		/// Gets or sets the box's third extent.
		/// </summary>
		public float Extent3
		{
			get { return _extent3; }
			set { _extent3 = value;}
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
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Center", _center, typeof(Vector3F));

			info.AddValue("Axis1", _axis1, typeof(Vector3F));
			info.AddValue("Axis2", _axis2, typeof(Vector3F));
			info.AddValue("Axis3", _axis3, typeof(Vector3F));

			info.AddValue("Extent1", _extent1, typeof(Vector3F));
			info.AddValue("Extent2", _extent2, typeof(Vector3F));
			info.AddValue("Extent3", _extent3, typeof(Vector3F));
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Computes the box vertices. 
		/// </summary>
		/// <returns>An array of <see cref="Vector3F"/> containing the box vertices.</returns>
		public Vector3F[] ComputeVertices()
		{
			Vector3F[] vertices = new Vector3F[8];
			Vector3F[] AxisExtents = new Vector3F[3]
				{
					Axis1*Extent1, Axis2*Extent2, Axis3*Extent3
				};
			
			vertices[0] = Center - AxisExtents[0] - AxisExtents[1] - AxisExtents[2];
			vertices[1] = Center - AxisExtents[0] + AxisExtents[1] - AxisExtents[2];
			vertices[2] = Center + AxisExtents[0] + AxisExtents[1] - AxisExtents[2];
			vertices[3] = Center - AxisExtents[0] + AxisExtents[1] - AxisExtents[2];

			vertices[0] = Center - AxisExtents[0] - AxisExtents[1] + AxisExtents[2];
			vertices[1] = Center - AxisExtents[0] + AxisExtents[1] + AxisExtents[2];
			vertices[2] = Center + AxisExtents[0] + AxisExtents[1] + AxisExtents[2];
			vertices[3] = Center - AxisExtents[0] + AxisExtents[1] + AxisExtents[2];

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
			return _center.GetHashCode() ^ _axis1.GetHashCode() ^ _axis2.GetHashCode() ^ _axis3.GetHashCode() ^ _extent1.GetHashCode() ^ _extent2.GetHashCode() ^ _extent3.GetHashCode();
		}
		/// <summary>
		/// Returns a value indicating whether this instance is equal to
		/// the specified object.
		/// </summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>True if <paramref name="obj"/> is a <see cref="Vector3F"/> and has the same values as this instance; otherwise, False.</returns>
		public override bool Equals(object obj)
		{
			if (obj is OrientedBox)
			{
				OrientedBox b = (OrientedBox)obj;
				return 
					(_center == b.Center) && 
					(_axis1 == b.Axis1) && (_axis2 == b.Axis2) && (_axis3 == b.Axis3) &&
					(_extent1 == b.Extent1) && (_extent2 == b.Extent2) && (_extent3 == b.Extent3);
			}
			return false;
		}

		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string representation of this object.</returns>
		public override string ToString()
		{
			return string.Format( "OrientedBox(Center={0}, Axis1={1}, Axis2={2}, Axis3={3}, Extent1={4}, Extent2={5}, Extent3={6})", 
				Center, Axis1, Axis2, Axis3, Extent1, Extent2, Extent3);
		}
		#endregion

		#region Comparison Operators
		/// <summary>
		/// Tests whether two specified boxes are equal.
		/// </summary>
		/// <param name="a">The left-hand box.</param>
		/// <param name="b">The right-hand box.</param>
		/// <returns>True if the two vectors are equal; otherwise, False.</returns>
		public static bool operator==(OrientedBox a, OrientedBox b)
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
				(a.Axis1 == b.Axis1) && (a.Axis2 == b.Axis2) && (a.Axis3 == b.Axis3) && 
				(a.Extent1 == b.Extent1) && (a.Extent2 == b.Extent2) && (a.Extent3 == b.Extent3);
		}
		/// <summary>
		/// Tests whether two specified boxes are not equal.
		/// </summary>
		/// <param name="a">The left-hand box.</param>
		/// <param name="b">The right-hand box.</param>
		/// <returns>True if the two boxes are not equal; otherwise, False.</returns>
		public static bool operator!=(OrientedBox a, OrientedBox b)
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
				(a.Axis1 == b.Axis1) && (a.Axis2 == b.Axis2) && (a.Axis3 == b.Axis3) && 
				(a.Extent1 == b.Extent1) && (a.Extent2 == b.Extent2) && (a.Extent3 == b.Extent3)
				);
		}

		#endregion
	}
}
