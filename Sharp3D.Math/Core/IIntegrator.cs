using System;

namespace Sharp3D.Math.Core
{
    /// <summary>
    /// Defines an interface for classes that perform integration of a function over an interval.
    /// </summary>
    public interface IIntegrator : ICloneable
    {
        /// <summary>
        /// Integrates a given function within the given integral.
        /// </summary>
        /// <param name="f">The function to integrate.</param>
        /// <param name="a">The lower limit.</param>
        /// <param name="b">The higher limit.</param>
        /// <returns>
        /// The integral of <paramref name="function"/> over the interval from <paramref name="a"/> to <paramref name="b"/>
        /// </returns>
        double Integrate(OneVariableFunction f, double a, double b);
    }
}
