using System;

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Simpson integration algorithm
	/// </summary>
	public sealed class SimpsonIntegral : IIntegrator
	{
		#region Private Fields
		private int _stepsNumber = 100;
		#endregion

		#region Constructors
		/// <summary>
		/// Initialize a new instance of the <see cref="SimpsonIntegral"/> class.
		/// </summary>
		public SimpsonIntegral()
		{
		}
		/// <summary>
		/// Initialize a new instance of the <see cref="SimpsonIntegral"/> class.
		/// </summary>
		/// <param name="stepsNumber">The number of steps to use for the integration.</param>
		public SimpsonIntegral(int stepsNumber)
		{
			_stepsNumber = stepsNumber;
		}
		/// <summary>
		/// Initialize a new instance of the <see cref="SimpsonIntegral"/> class using values from another <see cref="SimpsonIntegral"/> instance.
		/// </summary>
		/// <param name="integrator">A <see cref="SimpsonIntegral"/> instance.</param>
		public SimpsonIntegral(SimpsonIntegral integrator)
		{
			_stepsNumber = integrator._stepsNumber;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets a value indicating the number of steps used for the integration.
		/// </summary>
		public int StepsNumber
		{
			get { return _stepsNumber; }
		}
		#endregion
	
		#region IIntegrator Members
		/// <summary>
		/// Integrates a given function within the given integral.
		/// </summary>
		/// <param name="f">The function to integrate.</param>
		/// <param name="a">The lower limit.</param>
		/// <param name="b">The higher limit.</param>
		/// <returns>
		/// The integral of <paramref name="function"/> over the interval from <paramref name="a"/> to <paramref name="b"/>
		/// </returns>
		public double Integrate(OneVariableFunction f, double a, double b)
		{
			if (a > b)  return -Integrate(f, b, a);

			double sum = 0;
			double stepSize = (b-a)/_stepsNumber;
			double stepSizeDiv3 = stepSize/3;
			for (int i = 0; i < _stepsNumber; i = i+2)
			{
				sum += (f.Function(a + i*stepSize) + 4*f.Function(a+(i+1)*stepSize) + f.Function(a+(i+2)*stepSize))*stepSizeDiv3;
			}

			return sum;
		}

		#endregion

		#region ICloneable Members
		/// <summary>
		/// Creates an exact copy of this <see cref="SimpsonIntegral"/> object.
		/// </summary>
		/// <returns>The <see cref="SimpsonIntegral"/> object this method creates, cast as an object.</returns>
		object ICloneable.Clone()
		{
			return new SimpsonIntegral(this);
		}
		/// <summary>
		/// Creates an exact copy of this <see cref="SimpsonIntegral"/> object.
		/// </summary>
		/// <returns>The <see cref="SimpsonIntegral"/> object this method creates.</returns>
		public SimpsonIntegral Clone()
		{
			return new SimpsonIntegral(this);
		}
		#endregion
	}
}
