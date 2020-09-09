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
