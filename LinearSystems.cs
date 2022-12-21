using System;
using System.Linq;

namespace LinearAlgebra
{
    class Program
    {
        public static double[,] A =
        {
            {0.87, -0.1, 0.26},
            {-0.07, 1.63, 0.4},
            {-0.33, -0.17, -0.6}
        };
        public static int n = A.GetLength(0);
        public static double[] b = {2.42, 2.69, -2.43};
        public static double[] x = {1, 1, 1}; // начальное приближение
        public const double EPS = 0.5 * 10e-4;
        public static double[] x_next = new double[n];

        /* Вычисление нормы-1 вектора */
        public static double GetVectorNorm(double[] vector)
        {
            return vector.Sum(Math.Abs);
        }
        
        public static double JacobiMethod(int currentIndex)
        {
            var result = 0d;
            for (int i = 0; i < n; i++)
            {
                if (currentIndex != i)
                {
                    result += A[currentIndex, i] * x[i];
                }
            }
            result -= b[currentIndex];
            return -1 / A[currentIndex, currentIndex] * result;
        }

        public static double GaussSeidelMethod(int currentIndex)
        {
            var result = 0d;
            for (int i = 0; i < n; i++)
            {
                if (currentIndex != i)
                {
                    if(i < currentIndex) 
                        result += A[currentIndex, i] * x_next[i];
                    else if (i > currentIndex)
                        result += A[currentIndex, i] * x[i];
                }
            }
            result -= b[currentIndex];
            return -1 / A[currentIndex, currentIndex] * result;
        }

        public static double DotProduct(double[] a, double[] b)
        {
            var res = 0d;
            for (var i = 0; i < n; i++)
            {
                res += a[i] * b[i];
            }
            return res;
        }

        public static void PrintMatrix(double[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
        
        public static double[] GaussMethod()
        {
            var A1 = new double[n, n + 1];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    A1[i, j] = A[i, j];
                }
            }
            for (int i = 0; i < n; i++)
            {
                A1[i, n] = b[i];
            }
            var B = new double[n, n + 1];
            var C = new double[n, n + 1];
            var solution = new double[n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n + 1; j++)
                {
                    var sum = 0d;
                    if (i >= j)
                    {
                        for (var k = 0; k <= j - 1; k++)
                        {
                            sum += B[i, k] * C[k, j];
                        }
                        B[i, j] = A1[i, j] - sum;
                    }
                    if (i <= n - 1 && j >= i)
                    {
                        sum = 0d;
                        for (var k = 0; k <= i - 1; k++)
                        {
                            sum += B[i, k] * C[k, j];
                        }
                        C[i, j] = 1 / B[i, i] * (A1[i, j] - sum);
                    }
                }
            }
            for (int i = n - 1; i >= 0; i--)
            {
                var sum = 0d;
                for (var k = n - 1; k > i; k--)
                {
                    sum += C[i, k] * solution[k];
                }
                solution[i] = C[i, n] - sum;
            }
            return solution;
        }
        
        
        
        public static void Main()
        {
            var iterations = 0;
            var difference = Math.Abs(GetVectorNorm(x) - GetVectorNorm(x_next));
            while (difference >= EPS)
            {
                for (var i = 0; i < n; i++)
                {
                    x_next[i] = GaussSeidelMethod(i);
                }
                difference = Math.Abs(GetVectorNorm(x) - GetVectorNorm(x_next));
                x_next.CopyTo(x, 0);
                iterations++;
            }

            for (int i = 0; i < n; i++)
            {
                Console.Write(x_next[i] + " ");
            }
            Console.WriteLine(iterations);
            var solution = GaussMethod();
            solution.ToList().ForEach(Console.WriteLine);
        }
    }
}
