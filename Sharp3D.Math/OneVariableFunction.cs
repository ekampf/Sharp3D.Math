using System;

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Represents a function of one variable.
	/// </summary>
	public class OneVariableFunction
	{
		MathFunctions.DoubleUnaryFunction _function;

		public OneVariableFunction(MathFunctions.DoubleUnaryFunction function)
		{
			_function = function;
		}
	}
}
