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
            set { _differentiator = value; }
        }
        /// <summary>
        /// Gets or sets the integrator associated with this object.
        /// </summary>
        public IIntegrator Integrator
        {
            get { return _integrator; }
            set { _integrator = value; }
        }
        #endregion
    }
}
