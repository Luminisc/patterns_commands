using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Patterns.Matrixes
{
    public interface IMatrix
    {
        double GetValue(int x, int y);
        Size GetSize();
        IEnumerable<IRenderCommand> RenderSequence { get; }
    }

    public class FullMatrix : IMatrix
    {
        double[,] _matrix;
        public FullMatrix(double[,] matrix)
        {
            _matrix = matrix;
        }

        public IEnumerable<IRenderCommand> RenderSequence
        {
            get
            {
                var size = GetSize();

                yield return new DrawBorderRC(0, 0, true);
                yield return new DrawBorderRC(0, 0, false);
                yield return new DrawBorderRC(size.Width, 0, true);
                yield return new DrawBorderRC(0, size.Height, false);

                for (int i = 1; i < size.Width; i++)
                    yield return new DrawBorderRC(i, 0, true);

                for (int i = 1; i < size.Height; i++)
                    yield return new DrawBorderRC(0, i, false);

                for (int i = 0; i < size.Width; i++)
                {
                    for (int j = 0; j < size.Height; j++)
                    {
                        yield return new DrawValueRC(i, j, GetValue(i, j));
                    }
                }
            }
        }

        public Size GetSize()
        {
            return new Size(_matrix.GetUpperBound(0) + 1, _matrix.GetUpperBound(1) + 1);
        }

        public double GetValue(int x, int y)
        {
            return _matrix[x, y];
        }
    }

    public class ScatteredMatrix : IMatrix
    {
        List<Tuple<int, int, double>> _matrix;
        int _xsize, _ysize;
        public ScatteredMatrix(List<Tuple<int, int, double>> matrix, int xsize, int ysize)
        { 
            _matrix = matrix;
            _xsize = xsize;
            _ysize = ysize;
        }

        public IEnumerable<IRenderCommand> RenderSequence
        {
            get
            {
                var size = GetSize();

                yield return new DrawBorderRC(0, 0, true);
                yield return new DrawBorderRC(0, 0, false);
                yield return new DrawBorderRC(size.Width, 0, true);
                yield return new DrawBorderRC(0, size.Height, false);

                foreach (var cell in _matrix)
                {
                    yield return new DrawValueRC(cell.Item1, cell.Item2, cell.Item3);
                }
            }
        }

        public Size GetSize()
        {
            return new Size(_xsize, _ysize);
        }

        public double GetValue(int x, int y)
        {
            var index = _matrix.FindIndex(cell => cell.Item1 == x && cell.Item2 == y);
            if (index > -1)
                return _matrix[index].Item3;
            else
                return 0;
        }
    }
}
