using System;

namespace Day01ArraysAndLists
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // possibly a jagged array, only the first dimension is allocated
                int[][] twoDimInt = new int[4][];
                // rectangular array
                int[,] twoDimIntRectangular = new int[4, 3];
                int[,,] threeDimIntRect = new int[4, 3, 2]; // 3D: 4 x 3 x 2 size


                // TASK: compute the average value (as floating point) of all values in array
                int[,] data2dInts = { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 18 } };

                int rows = data2dInts.GetLength(0);
                int cols = data2dInts.GetLength(1);
                {// using two loops

                    double sum = 0;
                    for (int row = 0; row < data2dInts.GetLength(0); row++)
                    {
                        for (int col = 0; col < data2dInts.GetLength(1); col++)
                        {
                            sum += data2dInts[row, col];
                        }
                    }
                    double avg = sum / data2dInts.Length;
                    // avg:0.000 will display always 3 decimals
                    // avg:0.### will display 3 decimals only if the number is not even 
                    Console.WriteLine($"The average of all values is {avg:0.###}");
                }
            }
            finally
            {
                Console.WriteLine("Press any key to Finish");
                Console.ReadKey();
            }
        }
    }
}
