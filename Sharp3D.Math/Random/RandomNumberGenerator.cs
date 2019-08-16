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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Sharp3D.Math.Core;

namespace Sharp3D.Math.Random
{
	/// <summary>
	/// Abstract base class for random number generators.
	/// </summary>
	public abstract class RandomNumberGenerator
	{
		/// <summary>
		/// Fills the given array with random numbers.
		/// </summary>
		/// <param name="array">An array to fill.</param>
		/// <remarks>The given array must be initialized before this method is called.</remarks>
		public abstract void Fill(int[] array);
		/// <summary>
		/// Fills the given array with random numbers.
		/// </summary>
		/// <param name="array">An array to fill.</param>
		/// <remarks>The given array must be initialized before this method is called.</remarks>
		public abstract void Fill(IntArrayList array);
		/// <summary>
		/// Fills the given array with random numbers.
		/// </summary>
		/// <param name="array">An array to fill.</param>
		/// <remarks>The given array must be initialized before this method is called.</remarks>
		public abstract void Fill(float[] array);
		/// <summary>
		/// Fills the given array with random numbers.
		/// </summary>
		/// <param name="array">An array to fill.</param>
		/// <remarks>The given array must be initialized before this method is called.</remarks>
		public abstract void Fill(FloatArrayList array);
		/// <summary>
		/// Fills the given array with random numbers.
		/// </summary>
		/// <param name="array">An array to fill.</param>
		/// <remarks>The given array must be initialized before this method is called.</remarks>
		public abstract void Fill(double[] array);
		/// <summary>
		/// Fills the given array with random numbers.
		/// </summary>
		/// <param name="array">An array to fill.</param>
		/// <remarks>The given array must be initialized before this method is called.</remarks>
		public abstract void Fill(DoubleArrayList array);
	}
}