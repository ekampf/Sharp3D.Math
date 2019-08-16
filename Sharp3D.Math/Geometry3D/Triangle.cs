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
	[Serializable]
	public struct Triangle : ICloneable, ISerializable
	{
		private Vector3F _p0, _p1, _p2;

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Triangle"/> class.
		/// </summary>
		/// <param name="p0">A <see cref="Vector3F"/> instance.</param>
		/// <param name="p1">A <see cref="Vector3F"/> instance.</param>
		/// <param name="p2">A <see cref="Vector3F"/> instance.</param>
		public Triangle(Vector3F p0, Vector3F p1, Vector3F p2)
		{
			_p0 = p0;
			_p1 = p1;
			_p2 = p2;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Triangle"/> class using a given Triangle.
		/// </summary>
		/// <param name="t">A <see cref="Triangle"/> instance.</param>
		public Triangle(Triangle t)
		{
			_p0 = t._p0;
			_p1 = t._p1;
			_p2 = t._p2;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Triangle"/> class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		private Triangle(SerializationInfo info, StreamingContext context)
		{
			_p0 = (Vector3F)info.GetValue("P0", typeof(Vector3F));
			_p1 = (Vector3F)info.GetValue("P1", typeof(Vector3F));
			_p2 = (Vector3F)info.GetValue("P2", typeof(Vector3F));
		}

		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="Triangle"/> object.
		/// </summary>
		/// <returns>The <see cref="Triangle"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new Triangle(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="Triangle"/> object.
		/// </summary>
		/// <returns>The <see cref="Triangle"/> object this method creates.</returns>
		public Triangle Clone()
		{
			return new Triangle(this);
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
			info.AddValue("P0", _p0, typeof(Vector3F));
			info.AddValue("P1", _p1, typeof(Vector3F));
			info.AddValue("P2", _p2, typeof(Vector3F));
		}
		#endregion

		public Vector3F this[int index]
		{
			get
			{
				switch(index)
				{
					case 0 : return _p0;
					case 1 : return _p1;
					case 2 : return _p2;
					default:
						throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch(index)
				{
					case 0 : _p0 = value; break;
					case 1 : _p1 = value; break;
					case 2 : _p2 = value; break;
					default:
						throw new IndexOutOfRangeException();
				}
			}
		}


		public Vector3F Point0
		{
			get { return _p0; }
			set { _p0 = value;}
		}
		public Vector3F Point1
		{
			get { return _p1; }
			set { _p1 = value;}
		}
		public Vector3F Point2
		{
			get { return _p2; }
			set { _p2 = value;}
		}
		

		public Vector3F FromBarycentric(float u, float v)
		{
			return ((1-u-v)*_p0)+(u*_p1)+(v*_p2);
		}
	}
}
