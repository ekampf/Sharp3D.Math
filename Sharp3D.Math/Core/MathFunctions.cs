using System;

namespace Sharp3D.Math.Core
{
	/// <summary>
	/// Provides standard mathematical functions for the library types.
	/// </summary>
	public sealed class MathFunctions
	{
		#region Delegates
		#region Delegates - Boolean Functions
		/// <summary>
		/// Functor that takes no arguments and returns a boolean.
		/// </summary>
		public delegate bool BooleanFunction();
		/// <summary>
		/// Functor that takes one booleans and returns a boolean. 
		/// </summary>
		public delegate bool BooleanUnaryFunction(bool b);
		/// <summary>
		/// Functor that takes two booleans and returns a boolean. 
		/// </summary>
		public delegate bool BooleanBinaryFunction(bool b1, bool b2);
		#endregion

		#region Delegates - Double Functions
		/// <summary>
		/// Functor that takes no arguments and returns a double.
		/// </summary>
		public delegate double DoubleFunction();
		/// <summary>
		/// Functor that takes one double and returns a double. 
		/// </summary>
		public delegate double DoubleUnaryFunction(double x);
		/// <summary>
		/// Functor that takes two doubles and returns a double. 
		/// </summary>
		public delegate double DoubleBinaryFunction(double x, double y);
		/// <summary>
		/// Functor that takes three doubles and returns a double. 
		/// </summary>
		public delegate double DoubleTernaryFunction(double x, double y, double z);
		#endregion

		#region Delegates - Float Functions
		/// <summary>
		/// Functor that takes no arguments and returns a float.
		/// </summary>
		public delegate float FloatFunction();
		/// <summary>
		/// Functor that takes one float and returns a float. 
		/// </summary>
		public delegate float FloatUnaryFunction(float x);
		/// <summary>
		/// Functor that takes two floats and returns a float. 
		/// </summary>
		public delegate float FloatBinaryFunction(float x, float y);
		/// <summary>
		/// Functor that takes three floats and returns a float. 
		/// </summary>
		public delegate float FloatTernaryFunction(float x, float y, float z);
		#endregion

		#region Delegates - Integet Functions
		/// <summary>
		/// Functor that takes no arguments and returns an integer.
		/// </summary>
		public delegate int IntFunction();
		/// <summary>
		/// Functor that takes one integer and returns an integer.
		/// </summary>
		public delegate int IntUnaryFunction(int x);
		/// <summary>
		/// Functor that takes two integers and returns an integer.
		/// </summary>
		public delegate int IntBinaryFunction(int x, int y);
		/// <summary>
		/// Functor that takes three integers and returns an integer.
		/// </summary>
		public delegate int IntTernaryFunction(int x, int y, int z);
		#endregion

		#region Delegates - Object Functions
		/// <summary>
		/// Functor that takes no arguments and returns an object.
		/// </summary>
		public delegate object ObjectFunction();
		/// <summary>
		/// Functor that takes one object and returns an object.
		/// </summary>
		public delegate object ObjectUnaryFunction(object obj);
		/// <summary>
		/// Functor that takes one objects and returns an object.
		/// </summary>
		public delegate object ObjectBinaryFunction(object obj1, object obj2);
		/// <summary>
		/// Functor that takes three objects and returns an object.
		/// </summary>
		public delegate object ObjectTernaryFunction(object obj1, object obj2, object obj3);
		#endregion

		#region Delegates - String Functions
		/// <summary>
		/// Functor that takes no arguments and returns a string.
		/// </summary>
		public delegate string StringFunction();
		/// <summary>
		/// Functor that takes one object and returns a string.
		/// </summary>
		public delegate string StringUnaryFunction(string s);
		/// <summary>
		/// Functor that takes two objects and returns a string.
		/// </summary>
		public delegate string StringBinaryFunction(string s1, string s2);
		/// <summary>
		/// Functor that takes three objects and returns a string.
		/// </summary>
		public delegate string StringTernaryFunction(string s1, string s2, string s3);
		#endregion
		#endregion

		#region Enums
		public enum Sign
		{
			/// <summary>
			/// Negative sign
			/// </summary>
			Negative = -1,
			/// <summary>
			/// Zero
			/// </summary>
			Zero = 0,
			/// <summary>
			/// Positive sign
			/// </summary>
			Positive = 1
		}
		#endregion

		#region Constants
		/// <summary>
		/// The value of PI.
		/// </summary>
		public const double PI = System.Math.PI;
		/// <summary>
		/// The value of (2 * PI).
		/// </summary>
		public const double TwoPI = 2*System.Math.PI;
		/// <summary>
		/// The value of (PI*PI).
		/// </summary>
		public const double SquaredPI = System.Math.PI * System.Math.PI;
		/// <summary>
		/// The value of PI/2.
		/// </summary>
		public const double HalfPI = System.Math.PI / 2.0;

		/// <summary>
		/// Epsilon, a fairly small value for a single precision floating point
		/// </summary>
		public const float EpsilonF = 4.76837158203125E-7f;
		/// <summary>
		/// Epsilon, a fairly small value for a double precision floating point
		/// </summary>
		public const double EpsilonD = 8.8817841970012523233891E-16;
		#endregion

		#region Abs Functions
		/// <summary>
		/// Absolute value function for single-precision floating point numbers.
		/// </summary>
		public static readonly FloatUnaryFunction	FloatAbsFunction	= new FloatUnaryFunction(System.Math.Abs);
		/// <summary>
		/// Absolute value function for double-precision floating point numbers.
		/// </summary>
		public static readonly DoubleUnaryFunction	DoubleAbsFunction	= new DoubleUnaryFunction(System.Math.Abs);
		/// <summary>
		/// Absolute value function for integers.
		/// </summary>
		public static readonly IntUnaryFunction		IntAbsFunction		= new IntUnaryFunction(System.Math.Abs);
		#endregion

		#region Interpolation Functions
		/// <summary>
		/// Linear interpolation function  for double-precision floating point numbers.
		/// </summary>
		public static readonly DoubleTernaryFunction DoubleLinearInterpolationFunction = new DoubleTernaryFunction(LinearInterpolation);
		/// <summary>
		/// Linear interpolation function  for single-precision floating point numbers.
		/// </summary>
		public static readonly FloatTernaryFunction  FloatLinearInterpolationFunction  = new FloatTernaryFunction(LinearInterpolation);
		/// <summary>
		/// Cosine interpolation function  for double-precision floating point numbers.
		/// </summary>
		public static readonly DoubleTernaryFunction DoubleCosineInterpolationFunction = new DoubleTernaryFunction(CosineInterpolation);
		/// <summary>
		/// Cosine interpolation function  for double-precision floating point numbers.
		/// </summary>
		public static readonly FloatTernaryFunction  FloatCosineInterpolationFunction  = new FloatTernaryFunction(CosineInterpolation);
		/// <summary>
		/// Cubic interpolation function  for double-precision floating point numbers.
		/// </summary>
		public static readonly DoubleTernaryFunction DoubleCubicInterpolationFunction = new DoubleTernaryFunction(CubicInterpolation);
		/// <summary>
		/// Cubic interpolation function  for double-precision floating point numbers.
		/// </summary>
		public static readonly FloatTernaryFunction  FloatCubicInterpolationFunction  = new FloatTernaryFunction(CubicInterpolation);
		#endregion

		#region Abs
		/// <summary>
		/// Calculates the absolute value of an integer.
		/// </summary>
		/// <param name="x">An integer.</param>
		/// <returns>The absolute value of <paramref name="x"/>.</returns>
		public static int Abs(int x)
		{
			return System.Math.Abs(x);
		}
		/// <summary>
		/// Calculates the absolute value of a single-precision floating point number.
		/// </summary>
		/// <param name="x">A single-precision floating point number.</param>
		/// <returns>The absolute value of <paramref name="x"/>.</returns>
		public static float Abs(float x)
		{
			return System.Math.Abs(x);
		}
		/// <summary>
		/// Calculates the absolute value of a double-precision floating point number.
		/// </summary>
		/// <param name="x">A double-precision floating point number.</param>
		/// <returns>The absolute value of <paramref name="x"/>.</returns>
		public static double Abs(double x)
		{
			return System.Math.Abs(x);
		}
		/// <summary>
		/// Creates a new <see cref="IntArrayList"/> whose element values are the
		/// result of applying the absolute function on the elements of the
		/// given integers array.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>A new <see cref="IntArrayList"/> whose values are the result of applying the absolute function to each element in <paramref name="array"/></returns>
		public static IntArrayList Abs(IntArrayList array)
		{
			IntArrayList result = new IntArrayList(array.Count);
			for (int i = 0; i < array.Count; i++)
			{
				result[i] = Abs(array[i]);
			}
			return result;
		}
		/// <summary>
		/// Creates a new <see cref="FloatArrayList"/> whose element values are the
		/// result of applying the absolute function on the elements of the
		/// given floats array.
		/// </summary>
		/// <param name="array">An array of single-precision floating point values.</param>
		/// <returns>A new <see cref="FloatArrayList"/> whose values are the result of applying the absolute function to each element in <paramref name="array"/></returns>
		public static FloatArrayList Abs(FloatArrayList array)
		{
			FloatArrayList result = new FloatArrayList(array.Count);
			for (int i = 0; i < array.Count; i++)
			{
				result[i] = Abs(array[i]);
			}
			return result;
		}
		/// <summary>
		/// Creates a new <see cref="DoubleArrayList"/> whose element values are the
		/// result of applying the absolute function on the elements of the
		/// given doubles array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>A new <see cref="DoubleArrayList"/> whose values are the result of applying the absolute function to each element in <paramref name="array"/></returns>
		public static DoubleArrayList Abs(DoubleArrayList array)
		{
			DoubleArrayList result = new DoubleArrayList(array.Count);
			for (int i = 0; i < array.Count; i++)
			{
				result[i] = Abs(array[i]);
			}
			return result;
		}
		/// <summary>
		/// Creates a new <see cref="IntArrayList"/> whose element values are the
		/// result of applying the absolute function on the elements of the
		/// given integers array.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>A new <see cref="IntArrayList"/> whose values are the result of applying the absolute function to each element in <paramref name="array"/></returns>
		public static int[] Abs(int[] array)
		{
			int[] result = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				result[i] = Abs(array[i]);
			}
			return result;
		}		/// <summary>
		/// Creates a new <see cref="FloatArrayList"/> whose element values are the
		/// result of applying the absolute function on the elements of the
		/// given floats array.
		/// </summary>
		/// <param name="array">An array of single-precision floating point values.</param>
		/// <returns>A new <see cref="FloatArrayList"/> whose values are the result of applying the absolute function to each element in <paramref name="array"/></returns>
		public static float[] Abs(float[] array)
		{
			float[] result = new float[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				result[i] = Abs(array[i]);
			}
			return result;
		}
		/// <summary>
		/// Creates a new <see cref="DoubleArrayList"/> whose element values are the
		/// result of applying the absolute function on the elements of the
		/// given doubles array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>A new <see cref="DoubleArrayList"/> whose values are the result of applying the absolute function to each element in <paramref name="array"/></returns>
		public static double[] Abs(double[] array)
		{
			double[] result = new double[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				result[i] = Abs(array[i]);
			}

			return result;
		}
		#endregion

		#region AbsSum
		/// <summary>
		/// Calculates the sum of the absolute value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The sum of the absolute values of the elements in <paramref name="array"/>.</returns>
		/// <remarks>sum = abs(array[0]) + abs(array[1])...</remarks>
		public static int AbsSum(int[] array)
		{
			int sum = 0;
			foreach (int i in array)
				sum += Abs(i);

			return sum;
		}
		/// <summary>
		/// Calculates the sum of the absolute value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The sum of the absolute values of the elements in <paramref name="array"/>.</returns>
		/// <remarks>sum = abs(array[0]) + abs(array[1])...</remarks>
		public static int AbsSum(IntArrayList array)
		{
			int sum = 0;
			foreach (int i in array)
				sum += Abs(i);

			return sum;
		}
		/// <summary>
		/// Calculates the sum of the absolute value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point values.</param>
		/// <returns>The sum of the absolute values of the elements in <paramref name="array"/>.</returns>
		/// <remarks>sum = abs(array[0]) + abs(array[1])...</remarks>
		public static float AbsSum(float[] array)
		{
			float sum = 0;
			foreach (float f in array)
				sum += Abs(f);

			return sum;
		}
		/// <summary>
		/// Calculates the sum of the absolute value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point values.</param>
		/// <returns>The sum of the absolute values of the elements in <paramref name="array"/>.</returns>
		/// <remarks>sum = abs(array[0]) + abs(array[1])...</remarks>
		public static float AbsSum(FloatArrayList array)
		{
			float sum = 0;
			foreach (float f in array)
				sum += Abs(f);

			return sum;
		}
		/// <summary>
		/// Calculates the sum of the absolute value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The sum of the absolute values of the elements in <paramref name="array"/>.</returns>
		/// <remarks>sum = abs(array[0]) + abs(array[1])...</remarks>
		public static double AbsSum(double[] array)
		{
			double sum = 0;
			foreach (double d in array)
				sum += Abs(d);

			return sum;
		}
		/// <summary>
		/// Calculates the sum of the absolute value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The sum of the absolute values of the elements in <paramref name="array"/>.</returns>
		/// <remarks>sum = abs(array[0]) + abs(array[1])...</remarks>
		public static double AbsSum(DoubleArrayList array)
		{
			double sum = 0;
			foreach (double d in array)
				sum += Abs(d);

			return sum;
		}
		#endregion

		#region Sum
		/// <summary>
		/// Calculates the sum of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The sum of the array's elements.</returns>
		/// <remarks>sum = array[0] + array[1]...</remarks>
		public static int Sum(int[] array)
		{
			int sum = 0;
			foreach (int i in array)
				sum += i;

			return sum;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The sum of the array's elements.</returns>
		/// <remarks>sum = array[0] + array[1]...</remarks>
		public static int Sum(IntArrayList array)
		{
			int sum = 0;
			foreach (int i in array)
				sum += i;

			return sum;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point values.</param>
		/// <returns>The sum of the array's elements.</returns>
		/// <remarks>sum = array[0] + array[1]...</remarks>
		public static float Sum(float[] array)
		{
			float sum = 0;
			foreach (float f in array)
				sum += f;

			return sum;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point values.</param>
		/// <returns>The sum of the array's elements.</returns>
		/// <remarks>sum = array[0] + array[1]...</remarks>
		public static float Sum(FloatArrayList array)
		{
			float sum = 0;
			foreach (float f in array)
				sum += f;

			return sum;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The sum of the array's elements.</returns>
		/// <remarks>sum = array[0] + array[1]...</remarks>
		public static double Sum(double[] array)
		{
			double sum = 0;
			foreach (double d in array)
				sum += d;

			return sum;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The sum of the array's elements.</returns>
		/// <remarks>sum = array[0] + array[1]...</remarks>
		public static double Sum(DoubleArrayList array)
		{
			double sum = 0;
			foreach (double d in array)
				sum += d;

			return sum;
		}
		#endregion

		#region SumOfSquares
		/// <summary>
		/// Calculates the sum of a given array's elements square values.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The sum of the array's elements square value.</returns>
		/// <remarks>sum = array[0]^2 + array[1]^2 ...</remarks>
		public static int SumOfSquares(int[] array)
		{
			int SumOfSquares = 0;
			foreach (int i in array)
				SumOfSquares += i*i;

			return SumOfSquares;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements square values.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The sum of the array's elements square value.</returns>
		/// <remarks>sum = array[0]^2 + array[1]^2 ...</remarks>
		public static int SumOfSquares(IntArrayList array)
		{
			int SumOfSquares = 0;
			foreach (int i in array)
				SumOfSquares += i*i;

			return SumOfSquares;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements square values.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The sum of the array's elements square value.</returns>
		/// <remarks>sum = array[0]^2 + array[1]^2 ...</remarks>
		public static float SumOfSquares(float[] array)
		{
			float SumOfSquares = 0;
			foreach (float f in array)
				SumOfSquares += f*f;

			return SumOfSquares;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements square values.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The sum of the array's elements square value.</returns>
		/// <remarks>sum = array[0]^2 + array[1]^2 ...</remarks>
		public static float SumOfSquares(FloatArrayList array)
		{
			float SumOfSquares = 0;
			foreach (float f in array)
				SumOfSquares += f*f;

			return SumOfSquares;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements square values.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The sum of the array's elements square value.</returns>
		/// <remarks>sum = array[0]^2 + array[1]^2 ...</remarks>
		public static double SumOfSquares(double[] array)
		{
			double SumOfSquares = 0;
			foreach (double d in array)
				SumOfSquares += d*d;

			return SumOfSquares;
		}
		/// <summary>
		/// Calculates the sum of a given array's elements square values.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The sum of the array's elements square value.</returns>
		/// <remarks>sum = array[0]^2 + array[1]^2 ...</remarks>
		public static double SumOfSquares(DoubleArrayList array)
		{
			double SumOfSquares = 0;
			foreach (double d in array)
				SumOfSquares += d*d;

			return SumOfSquares;
		}
		#endregion

		#region Sqrt
		/// <summary>
		/// Returns the square root of a specified number.
		/// </summary>
		/// <param name="value">A double-precision floating point number.</param>
		/// <returns>The square root of a specified number.</returns>
		public static double Sqrt(double value)
		{
			return System.Math.Sqrt(value);
		}
		/// <summary>
		/// Returns the square root of a specified number.
		/// </summary>
		/// <param name="value">A single-precision floating point number.</param>
		/// <returns>The square root of a specified number.</returns>
		public static float Sqrt(float value)
		{
			return (float)System.Math.Sqrt(value);
		}
		/// <summary>
		/// Returns the square root of a specified number.
		/// </summary>
		/// <param name="value">A <see cref="ComplexD"/> instance.</param>
		/// <returns>The square root of a specified number.</returns>
		public static ComplexD Sqrt(ComplexD value)
		{
			return ComplexD.Sqrt(value);
		}
		/// <summary>
		/// Returns the square root of a specified number.
		/// </summary>
		/// <param name="value">A <see cref="ComplexF"/> instance.</param>
		/// <returns>The square root of a specified number.</returns>
		public static ComplexF Sqrt(ComplexF value)
		{
			return ComplexF.Sqrt(value);
		}
		#endregion

		#region MinValue
		/// <summary>
		/// Calculates the minimum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static int MinValue(int[] array)
		{
			if (array.Length == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			int value = array[0];
			foreach(int i in array)
			{
				if (i < value)
					value = i;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static int MinValue(IntArrayList array)
		{
			if (array.Count == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			int value = array[0];
			foreach(int i in array)
			{
				if (i < value)
					value = i;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static float MinValue(float[] array)
		{
			if (array.Length == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			float value = array[0];
			foreach(float f in array)
			{
				if (f < value)
					value = f;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static float MinValue(FloatArrayList array)
		{
			if (array.Count == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			float value = array[0];
			foreach(float f in array)
			{
				if (f < value)
					value = f;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static double MinValue(double[] array)
		{
			if (array.Length == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			double value = array[0];
			foreach(double d in array)
			{
				if (d < value)
					value = d;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static double MinValue(DoubleArrayList array)
		{
			if (array.Count == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			double value = array[0];
			foreach(double d in array)
			{
				if (d < value)
					value = d;
			}

			return value;
		}
		#endregion

		#region MaxValue
		/// <summary>
		/// Calculates the maximum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static int MaxValue(int[] array)
		{
			if (array.Length == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			int value = array[0];
			foreach(int i in array)
			{
				if (i > value)
					value = i;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static int MaxValue(IntArrayList array)
		{
			if (array.Count == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			int value = array[0];
			foreach(int i in array)
			{
				if (i > value)
					value = i;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static float MaxValue(float[] array)
		{
			if (array.Length == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			float value = array[0];
			foreach(float f in array)
			{
				if (f > value)
					value = f;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static float MaxValue(FloatArrayList array)
		{
			if (array.Count == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			float value = array[0];
			foreach(float f in array)
			{
				if (f > value)
					value = f;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static double MaxValue(double[] array)
		{
			if (array.Length == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			double value = array[0];
			foreach(double d in array)
			{
				if (d > value)
					value = d;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static double MaxValue(DoubleArrayList array)
		{
			if (array.Count == 0)
				throw new ArgumentException("Array has zero elements.", "array");

			double value = array[0];
			foreach(double d in array)
			{
				if (d > value)
					value = d;
			}

			return value;
		}
		#endregion

		#region MinAbsValue
		/// <summary>
		/// Calculates the minimum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static int MinAbsValue(int[] array)
		{
			//if (array.Length == 0)
			//	throw new InvalidArgumentException();

			int value = array[0];
			foreach(int i in array)
			{
				if (Abs(i) < value)
					value = i;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static int MinAbsValue(IntArrayList array)
		{
			//if (IntArrayList.Count == 0)
			//	throw new InvalidArgumentException();

			int value = array[0];
			foreach(int i in array)
			{
				if (Abs(i) < value)
					value = i;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static float MinAbsValue(float[] array)
		{
			//if (array.Length == 0)
			//	throw new InvalidArgumentException();

			float value = array[0];
			foreach(float f in array)
			{
				if (Abs(f) < value)
					value = f;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static float MinAbsValue(FloatArrayList array)
		{
			//if (IntArrayList.Count == 0)
			//	throw new InvalidArgumentException();

			float value = array[0];
			foreach(float f in array)
			{
				if (Abs(f) < value)
					value = f;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static double MinAbsValue(double[] array)
		{
			//if (array.Length == 0)
			//	throw new InvalidArgumentException();

			double value = array[0];
			foreach(double d in array)
			{
				if (Abs(d) < value)
					value = d;
			}

			return value;
		}
		/// <summary>
		/// Calculates the minimum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The minimum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static double MinAbsValue(DoubleArrayList array)
		{
			//if (IntArrayList.Count == 0)
			//	throw new InvalidArgumentException();

			double value = array[0];
			foreach(double d in array)
			{
				if (Abs(d) < value)
					value = d;
			}

			return value;
		}
		#endregion

		#region MaxAbsValue
		/// <summary>
		/// Calculates the maximum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static int MaxAbsValue(int[] array)
		{
			//if (array.Length == 0)
			//	throw new InvalidArgumentException();

			int value = array[0];
			foreach(int i in array)
			{
				if (Abs(i) > value)
					value = i;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static int MaxAbsValue(IntArrayList array)
		{
			//if (IntArrayList.Count == 0)
			//	throw new InvalidArgumentException();

			int value = array[0];
			foreach(int i in array)
			{
				if (Abs(i) > value)
					value = i;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static float MaxAbsValue(float[] array)
		{
			//if (array.Length == 0)
			//	throw new InvalidArgumentException();

			float value = array[0];
			foreach(float f in array)
			{
				if (Abs(f) > value)
					value = f;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static float MaxAbsValue(FloatArrayList array)
		{
			//if (IntArrayList.Count == 0)
			//	throw new InvalidArgumentException();

			float value = array[0];
			foreach(float f in array)
			{
				if (Abs(f) > value)
					value = f;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static double MaxAbsValue(double[] array)
		{
			//if (array.Length == 0)
			//	throw new InvalidArgumentException();

			double value = array[0];
			foreach(double d in array)
			{
				if (Abs(d) > value)
					value = d;
			}

			return value;
		}
		/// <summary>
		/// Calculates the maximum value of a given array's elements absolute values.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The maximum value.</returns>
		/// <exception cref="ArgumentException">The if the given array's length is zero.</exception>
		public static double MaxAbsValue(DoubleArrayList array)
		{
			//if (IntArrayList.Count == 0)
			//	throw new InvalidArgumentException();

			double value = array[0];
			foreach(double d in array)
			{
				if (Abs(d) > value)
					value = d;
			}

			return value;
		}
		#endregion

		#region Mean
		/// <summary>
		/// Calculates the mean of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The mean of the elements in <paramref name="array"/>.</returns>
		public static float Mean(int[] array)
		{
			return Sum(array) / array.Length;
		}
		/// <summary>
		/// Calculates the mean of a given array's elements.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The mean of the elements in <paramref name="array"/>.</returns>
		public static float Mean(IntArrayList array)
		{
			return Sum(array) / array.Count;
		}
		/// <summary>
		/// Calculates the mean of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point values.</param>
		/// <returns>The mean of the elements in <paramref name="array"/>.</returns>
		public static float Mean(float[] array)
		{
			return Sum(array) / array.Length;
		}
		/// <summary>
		/// Calculates the mean of a given array's elements.
		/// </summary>
		/// <param name="array">An array of single-precision floating point values.</param>
		/// <returns>The mean of the elements in <paramref name="array"/>.</returns>
		public static float Mean(FloatArrayList array)
		{
			return Sum(array) / array.Count;
		}
		/// <summary>
		/// Calculates the mean of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The mean of the elements in <paramref name="array"/>.</returns>
		public static double Mean(double[] array)
		{
			return Sum(array) / array.Length;
		}
		/// <summary>
		/// Calculates the mean of a given array's elements.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The mean of the elements in <paramref name="array"/>.</returns>
		public static double Mean(DoubleArrayList array)
		{
			return Sum(array) / array.Count;
		}
		#endregion

		#region Variance
		/// <summary>
		/// Calculates the variance of the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The variance of the array elements.</returns>
		public static float Variance(int[] array)
		{
			float variance = 0;
			float delta = 0;
			float mean = Mean(array);

			for (int i = 0; i < array.Length; i++)
			{
				delta = array[i] - mean;
				variance += (delta*delta - variance) / (i+1);
			}

			return variance;
		}

		/// <summary>
		/// Calculates the variance of the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The variance of the array elements.</returns>
		public static float Variance(IntArrayList array)
		{
			float variance = 0;
			float delta = 0;
			float mean = Mean(array);

			for (int i = 0; i < array.Count; i++)
			{
				delta = array[i] - mean;
				variance += (delta*delta - variance) / (i+1);
			}

			return variance;
		}
		/// <summary>
		/// Calculates the variance of the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The variance of the array elements.</returns>
		public static float Variance(float[] array)
		{
			float variance = 0;
			float delta = 0;
			float mean = Mean(array);

			for (int i = 0; i < array.Length; i++)
			{
				delta = array[i] - mean;
				variance += (delta*delta - variance) / (i+1);
			}

			return variance;
		}

		/// <summary>
		/// Calculates the variance of the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The variance of the array elements.</returns>
		public static float Variance(FloatArrayList array)
		{
			float variance = 0;
			float delta = 0;
			float mean = Mean(array);

			for (int i = 0; i < array.Count; i++)
			{
				delta = array[i] - mean;
				variance += (delta*delta - variance) / (i+1);
			}

			return variance;
		}
		/// <summary>
		/// Calculates the variance of the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The variance of the array elements.</returns>
		public static double Variance(double[] array)
		{
			double variance = 0;
			double delta = 0;
			double mean = Mean(array);

			for (int i = 0; i < array.Length; i++)
			{
				delta = array[i] - mean;
				variance += (delta*delta - variance) / (i+1);
			}

			return variance;
		}

		/// <summary>
		/// Calculates the variance of the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point values.</param>
		/// <returns>The variance of the array elements.</returns>
		public static double Variance(DoubleArrayList array)
		{
			double variance = 0;
			double delta = 0;
			double mean = Mean(array);

			for (int i = 0; i < array.Count; i++)
			{
				delta = array[i] - mean;
				variance += (delta*delta - variance) / (i+1);
			}

			return variance;
		}

		#endregion

		#region CountPositives
		/// <summary>
		/// Calculates the number of positive values in the given array.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountPositives(int[] array)
		{
			int count = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] > 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of positive values in the given array.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountPositives(IntArrayList array)
		{
			int count = 0;
			for (int i = 0; i < array.Count; i++)
			{
				if (array[i] > 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of positive values in the given array.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountPositives(float[] array)
		{
			int count = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] > 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of positive values in the given array.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		public static int CountPositives(FloatArrayList array)
		{
			int count = 0;
			for (int i = 0; i < array.Count; i++)
			{
				if (array[i] > 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of positive values in the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountPositives(double[] array)
		{
			int count = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] > 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of positive values in the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountPositives(DoubleArrayList array)
		{
			int count = 0;
			for (int i = 0; i < array.Count; i++)
			{
				if (array[i] > 0)
					count++;
			}
			return count;
		}
		#endregion

		#region CountNegatives
		/// <summary>
		/// Calculates the number of negative values in the given array.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountNegatives(int[] array)
		{
			int count = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] < 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of negative values in the given array.
		/// </summary>
		/// <param name="array">An array of integers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountNegatives(IntArrayList array)
		{
			int count = 0;
			for (int i = 0; i < array.Count; i++)
			{
				if (array[i] < 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of negative values in the given array.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountNegatives(float[] array)
		{
			int count = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] < 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of negative values in the given array.
		/// </summary>
		/// <param name="array">An array of single-precision floating point numbers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountNegatives(FloatArrayList array)
		{
			int count = 0;
			for (int i = 0; i < array.Count; i++)
			{
				if (array[i] < 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of negative values in the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountNegatives(double[] array)
		{
			int count = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] < 0)
					count++;
			}
			return count;
		}
		/// <summary>
		/// Calculates the number of negative values in the given array.
		/// </summary>
		/// <param name="array">An array of double-precision floating point numbers.</param>
		/// <returns>The number of positive values in the array</returns>
		public static int CountNegatives(DoubleArrayList array)
		{
			int count = 0;
			for (int i = 0; i < array.Count; i++)
			{
				if (array[i] < 0)
					count++;
			}
			return count;
		}
		#endregion

		#region GetSign
		public static Sign GetSign(int i)
		{
			return (Sign)System.Math.Sign(i);
		}
		public static Sign GetSign(float f)
		{
			return (Sign)System.Math.Sign(f);
		}
		public static Sign GetSign(double d)
		{
			return (Sign)System.Math.Sign(d);
		}
		public static Sign GetSign(float f, float tolerance)
		{
			if( f > tolerance )	
			{					
				return	Sign.Positive;
			}
			if( f < -tolerance )	
			{
				return	Sign.Negative;
			}
			return	Sign.Zero;
		}
		public static Sign GetSign(double d, double tolerance)
		{
			if( d > tolerance )	
			{					
				return	Sign.Positive;
			}
			if( d < -tolerance )	
			{
				return	Sign.Negative;
			}
			return	Sign.Zero;
		}

		#endregion

		#region ApproxEquals
		/// <summary>
		/// Tests whether two single-precision floating point numbers are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="a">A single-precision floating point number.</param>
		/// <param name="b">A single-precision floating point number.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEquals(float a, float b)
		{
			return (System.Math.Abs(a-b) <= EpsilonF);
		}
		/// <summary>
		/// Tests whether two single-precision floating point numbers are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="a">A single-precision floating point number.</param>
		/// <param name="b">A single-precision floating point number.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEquals(float a, float b, float tolerance)
		{
			return (System.Math.Abs(a-b) <= tolerance);
		}
		/// <summary>
		/// Tests whether two double-precision floating point numbers are approximately equal using default tolerance value.
		/// </summary>
		/// <param name="a">A double-precision floating point number.</param>
		/// <param name="b">A double-precision floating point number.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEquals(double a, double b)
		{
			return (System.Math.Abs(a-b) <= EpsilonD);
		}
		/// <summary>
		/// Tests whether two double-precision floating point numbers are approximately equal given a tolerance value.
		/// </summary>
		/// <param name="a">A double-precision floating point number.</param>
		/// <param name="b">A double-precision floating point number.</param>
		/// <param name="tolerance">The tolerance value used to test approximate equality.</param>
		/// <returns>True if the two vectors are approximately equal; otherwise, False.</returns>
		public static bool ApproxEquals(double a, double b, double tolerance)
		{
			return (System.Math.Abs(a-b) <= tolerance);
		}
		#endregion

		#region Swap
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A double-precision floating point number.</param>
		/// <param name="b">A double-precision floating point number.</param>
		public static void Swap(ref double a, ref double b) 
		{
			double c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A single-precision floating point number.</param>
		/// <param name="b">A single-precision floating point number.</param>
		public static void Swap(ref float a, ref float b) 
		{
			float c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="decimal"/> value.</param>
		/// <param name="b">A <see cref="decimal"/> value.</param>
		public static void Swap(ref decimal a, ref decimal b) 
		{
			decimal c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="sbyte"/> value.</param>
		/// <param name="b">A <see cref="sbyte"/> value.</param>
		[CLSCompliant(false)]
		public static void Swap(ref sbyte a, ref sbyte b) 
		{
			sbyte c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="byte"/> value.</param>
		/// <param name="b">A <see cref="byte"/> value.</param>
		public static void Swap(ref byte a, ref byte b) 
		{
			byte c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="char"/> value.</param>
		/// <param name="b">A <see cref="char"/> value.</param>
		public static void Swap(ref char a, ref char b) 
		{
			char c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="short"/> value.</param>
		/// <param name="b">A <see cref="short"/> value.</param>
		public static void Swap(ref short a, ref short b) 
		{
			short c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="ushort"/> value.</param>
		/// <param name="b">A <see cref="ushort"/> value.</param>
		[CLSCompliant(false)]
		public static void Swap(ref ushort a, ref ushort b) 
		{
			ushort c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="int"/> value.</param>
		/// <param name="b">A <see cref="int"/> value.</param>
		public static void Swap(ref int a, ref int b) 
		{
			int c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="uint"/> value.</param>
		/// <param name="b">A <see cref="uint"/> value.</param>
		[CLSCompliant(false)]
		public static void Swap(ref uint a, ref uint b) 
		{
			uint c = a;
			a = b;
			b = c;
		}

		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="long"/> value.</param>
		/// <param name="b">A <see cref="long"/> value.</param>
		public static void Swap(ref long a, ref long b) 
		{
			long c = a;
			a = b;
			b = c;
		}
		/// <summary>
		/// Swaps two values.
		/// </summary>
		/// <param name="a">A <see cref="ulong"/> value.</param>
		/// <param name="b">A <see cref="ulong"/> value.</param>
		[CLSCompliant(false)]
		public static void Swap(ref ulong a, ref ulong b) 
		{
			ulong c = a;
			a = b;
			b = c;
		}

		#endregion

		#region Linear Interpolation
		/// <summary>
		/// Interpolate two values using linear interpolation.
		/// </summary>
		/// <param name="a">A double-precision floating point number representing the first point.</param>
		/// <param name="b">A double-precision floating point number representing the second point.</param>
		/// <param name="x">A double-precision floating point number between 0 and 1 ( [0,1] ).</param>
		/// <returns>The interpolated value.</returns>
		public static double LinearInterpolation(double a, double b, double x)
		{
			return a*(1-x) + b*x;
		}
		/// <summary>
		/// Interpolate two values using linear interpolation.
		/// </summary>
		/// <param name="a">A single-precision floating point number representing the first point.</param>
		/// <param name="b">A single-precision floating point number representing the second point.</param>
		/// <param name="x">A single-precision floating point number between 0 and 1 ( [0,1] ).</param>
		/// <returns>The interpolated value.</returns>
		public static float LinearInterpolation(float a, float b, float x)
		{
			return a*(1-x) + b*x;
		}
		#endregion

		#region Cosine Interpolation
		/// <summary>
		/// Interpolate two values using cosine interpolation.
		/// </summary>
		/// <param name="a">A double-precision floating point number representing the first point.</param>
		/// <param name="b">A double-precision floating point number representing the second point.</param>
		/// <param name="x">A double-precision floating point number between 0 and 1 ( [0,1] ).</param>
		/// <returns></returns>
		public static double CosineInterpolation(double a, double b, double x)
		{
			double ft = (double)(x * PI);
			double f = (1 - System.Math.Cos(ft)) * 0.5;
			return a*(1-f) + b*f;
		}
		/// <summary>
		/// Interpolate two values using cosine interpolation.
		/// </summary>
		/// <param name="a">A single-precision floating point number representing the first point.</param>
		/// <param name="b">A single-precision floating point number representing the second point.</param>
		/// <param name="x">A single-precision floating point number between 0 and 1 ( [0,1] ).</param>
		/// <returns></returns>
		public static float CosineInterpolation(float a, float b, float x)
		{
			float ft = (float)(x * (float)PI);
			float f = (1.0f - (float)System.Math.Cos(ft)) * 0.5f;
			return a*(1-f) + b*f;
		}
		#endregion

		#region Cubic Interpolation
		/// <summary>
		/// Interpolate two values using cubic interpolation.
		/// </summary>
		/// <param name="a">A double-precision floating point number representing the first point.</param>
		/// <param name="b">A double-precision floating point number representing the second point.</param>
		/// <param name="x">A double-precision floating point number between 0 and 1 ( [0,1] ).</param>
		/// <returns></returns>
		public static double CubicInterpolation(double a, double b, double x) 
		{
			double fac1 = 3*System.Math.Pow(1-x, 2) - 2*System.Math.Pow(1-x,3);
			double fac2 = 3*System.Math.Pow(x, 2) - 2*System.Math.Pow(x, 3);

			return a*fac1 + b*fac2; //add the weighted factors
		}
		/// <summary>
		/// Interpolate two values using cubic interpolation.
		/// </summary>
		/// <param name="a">A single-precision floating point number representing the first point.</param>
		/// <param name="b">A single-precision floating point number representing the second point.</param>
		/// <param name="x">A single-precision floating point number between 0 and 1 ( [0,1] ).</param>
		/// <returns></returns>
		public static float CubicInterpolation(float a, float b, float x) 
		{
			float fac1 = 3*(float)System.Math.Pow(1-x, 2) - 2*(float)System.Math.Pow(1-x,3);
			float fac2 = 3*(float)System.Math.Pow(x, 2) - 2*(float)System.Math.Pow(x, 3);

			return a*fac1 + b*fac2; //add the weighted factors
		}
		#endregion

		#region Primes
		/// <summary>
		/// Checks if the given value is a prime number.
		/// </summary>
		/// <param name="value">The number to check.</param>
		/// <returns><c>True</c> if the number is a prime; otherwise, <c>False</c>.</returns>
		public static bool IsPrime(long value)
		{
			int sqrtValue = (int)System.Math.Sqrt(value);

			for (int i = 2; i <= sqrtValue; i++)
			{
				if ((value % i) == 0)
					return false;
			}

			return true;
		}
		#endregion

		#region Private Constructor
		private MathFunctions()
		{
		}
		#endregion
	}
}
