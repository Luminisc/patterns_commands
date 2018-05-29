using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Matrixes
{
    public interface IMatrixDecorator: IMatrix
    {

    }

    public class TransposedMatrix : IMatrixDecorator
    {
        IMatrix _matrix;
        public TransposedMatrix(IMatrix matrix)
        {
            _matrix = matrix;
        }
        public IEnumerable<IRenderCommand> RenderSequence
        {
            get
            {
                foreach (var command in _matrix.RenderSequence)
                {
                    switch (command.RenderType)
                    {
                        case RenderCommandType.DrawBorder:
                            yield return command;
                            break;
                        case RenderCommandType.DrawValue:
                            var cell = (DrawValueRC)command;
                            int temp = cell.Y;
                            cell.Y = cell.X;
                            cell.X = temp;
                            yield return cell;
                            break;
                    }
                }
            }
        }

        public Size GetSize()
        {
            var size = _matrix.GetSize();
            var temp = size.Width;
            size.Width = size.Height;
            size.Height = temp;
            return size;
        }

        public double GetValue(int x, int y)
        {
            return _matrix.GetValue(y, x);
        }
    }

    public class UpperTriangleZeroing : IMatrixDecorator
    {
        IMatrix _matrix;
        public UpperTriangleZeroing(IMatrix matrix)
        {
            _matrix = matrix;
        }
        public IEnumerable<IRenderCommand> RenderSequence
        {
            get
            {
                foreach (var command in _matrix.RenderSequence)
                {
                    switch (command.RenderType)
                    {
                        case RenderCommandType.DrawBorder:
                            yield return command;
                            break;
                        case RenderCommandType.DrawValue:
                            var cell = (DrawValueRC)command;
                            if (cell.X > cell.Y) cell.Value = 0;
                            yield return cell;
                            break;
                    }
                }
            }
        }

        public Size GetSize()
        {
            return _matrix.GetSize();
        }

        public double GetValue(int x, int y)
        {
            return x > y ? 0.0 : _matrix.GetValue(x, y);
        }
    }

    public class HorizontalComposer : IMatrix
    {
        IMatrix _matrix1;
        IMatrix _matrix2;
        public HorizontalComposer(IMatrix mat1, IMatrix mat2)
        {
            _matrix1 = mat1;
            _matrix2 = mat2;
        }
        public IEnumerable<IRenderCommand> RenderSequence
        {
            get
            {
                foreach (var command in _matrix1.RenderSequence)
                    yield return command;
                var offset = _matrix1.GetSize().Width;
                foreach (var command in _matrix2.RenderSequence)
                {
                    switch (command.RenderType)
                    {
                        case RenderCommandType.DrawBorder:
                            var border = (DrawBorderRC)command;
                            border.X = border.X + offset;
                            yield return border;
                            break;
                        case RenderCommandType.DrawValue:
                            var cell = (DrawValueRC)command;
                            cell.X = cell.X + offset;
                            yield return cell;
                            break;
                    }
                }
            }
        }

        public Size GetSize()
        {
            var size1 = _matrix1.GetSize();
            var size2 = _matrix2.GetSize();

            if (size1.Height != size2.Height) throw new Exception("Height mismatch of matrixes in horizontal composer");
            return new Size(size1.Width + size2.Width, size1.Height);
        }

        public double GetValue(int x, int y)
        {
            var aSize = _matrix1.GetSize();
            if (x >= aSize.Width)
            {
                return _matrix2.GetValue(x - aSize.Width, y);
            }
            else
            {
                return _matrix1.GetValue(x, y);
            }
        }
    }
}
