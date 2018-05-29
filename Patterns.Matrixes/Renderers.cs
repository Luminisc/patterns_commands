using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Matrixes
{
    public interface IRenderer
    {
        void Draw(IMatrix matrix);
    }

    public class FormRenderer : IRenderer
    {
        public void Draw(IMatrix matrix)
        {
            var sequence = matrix.RenderSequence;
            foreach (var command in sequence)
            {
                switch (command.RenderType)
                {
                    case RenderCommandType.DrawBorder:
                        var border = (DrawBorderRC)command;
                        Console.WriteLine("[Form Renderer] Drawing {0} border: X={1} Y={2}", border.Vertical ? "vertical" : "horizontal", border.X, border.Y);
                        break;
                    case RenderCommandType.DrawValue:
                        var cell = (DrawValueRC)command;
                        Console.WriteLine("[Form Renderer] Drawing value \'{0}\': X={1} Y={2}", cell.Value, cell.X, cell.Y);
                        break;
                    default:
                        throw new NotImplementedException("Not yet implemented:");
                }
            }
        }
    }
}
