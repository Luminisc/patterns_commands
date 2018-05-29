using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Matrixes
{
    class Program
    {
        static double[,] GenerateNumbers()
        {
            var ret = new double[3, 3];
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ret[j, i] = rnd.Next(60000);
                }
            }
            return ret;
        }

        static void Main(string[] args)
        {
            IRenderer renderer = new FormRenderer();
            IMatrix matrix, matrix2;

            double[,] _matrix = GenerateNumbers();
            matrix = new FullMatrix(_matrix);

            var _matrix2 = new List<Tuple<int, int, double>>() {
                new Tuple<int, int, double>(0, 0, 1),
                new Tuple<int, int, double>(0, 2, 2),
                new Tuple<int, int, double>(1, 0, 3)
            };
            matrix2 = new ScatteredMatrix(_matrix2, 3, 3);
            matrix2 = new TransposedMatrix(matrix2);

            IMatrix composed = new HorizontalComposer(matrix, matrix2);
            renderer.Draw(composed);

            Console.ReadLine();
        }
    }
}
