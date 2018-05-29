using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Matrixes
{
    public enum RenderCommandType : byte
    {
        DrawBorder = 0,
        DrawValue = 1
    }

    public interface IRenderCommand
    {
        RenderCommandType RenderType { get; }
    }

    public struct DrawBorderRC : IRenderCommand
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Vertical { get; }
        public DrawBorderRC(int x, int y, bool vertical)
        {
            X = x;
            Y = y;
            Vertical = vertical;
        }

        public RenderCommandType RenderType { get { return RenderCommandType.DrawBorder; } }
    }



    public struct DrawValueRC : IRenderCommand
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Value { get; set; }

        public DrawValueRC(int x, int y, double value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public RenderCommandType RenderType { get { return RenderCommandType.DrawValue; } }
    }
}
