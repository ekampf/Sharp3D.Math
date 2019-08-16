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
	/// Represents a function of one variable.
	/// </summary>
	public class OneVariableFunction
	{
		MathFunctions.DoubleUnaryFunction _function;
		IDifferentiator _differentiator;
		IIntegrator _integrator;

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="OneVariableFunction"/> class.
		/// </summary>
		/// <param name="f">
		/// A function delegate that takes a double value as a parameter and returns a double value.
		/// </param>
		public OneVariableFunction(MathFunctions.DoubleUnaryFunction f)
		{
			_function = f;
			_differentiator = null;
			_integrator = null;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the function encapsulated by this object.
		/// </summary>
		public MathFunctions.DoubleUnaryFunction Function
		{
			get { return _function; }
		}
		/// <summary>
		/// Gets or sets the differentiator associated with this object.
		/// </summary>
		public IDifferentiator Differentiator
		{
			get { return _differentiator; }
			set { _differentiator = value;}
		}
		/// <summary>
		/// Gets or sets the integrator associated with this object.
		/// </summary>
		public IIntegrator Integrator
		{
			get { return _integrator; }
			set { _integrator = value;}
		}
		#endregion
	}
}
