
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
