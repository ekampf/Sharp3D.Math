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

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Defines an interface for classes that perform differentiation of a function at a point.
	/// </summary>
	public interface IDifferentiator
	{
		/// <summary>
		/// Differentiates the given function at a given point.
		/// </summary>
		/// <param name="f">The function to differentiate.</param>
		/// <param name="x">The point to differentiate at.</param>
		/// <returns>The derivative of function at <paramref name="x"/>.</returns>
		double Differentiate(OneVariableFunction f, double x);
	}
}
