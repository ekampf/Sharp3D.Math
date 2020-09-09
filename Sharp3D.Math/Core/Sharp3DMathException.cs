using System;
using System.Runtime.Serialization;

namespace Sharp3D.Math.Core
{
    /// <summary>
    /// Base class for all exceptions thrown by Sharp3D.Math.
    /// </summary>
    [Serializable]
    public class Sharp3DMathException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharp3DMathException"/> class.
        /// </summary>
        public Sharp3DMathException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharp3DMathException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public Sharp3DMathException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharp3DMathException"/> class 
        /// with a specified error message and a reference to the inner exception that is 
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. 
        /// If the innerException parameter is not a null reference, the current exception is raised 
        /// in a catch block that handles the inner exception.
        /// </param>
        public Sharp3DMathException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Sharp3DMathException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected Sharp3DMathException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
