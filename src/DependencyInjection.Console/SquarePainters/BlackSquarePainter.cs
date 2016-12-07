using DependencyInjection.Console.Entities;

namespace DependencyInjection.Console.SquarePainters
{
    internal class BlackSquarePainter : ISquarePainter
    {
        public Square PaintSquare(int width, int height, int i, int j)
        {
            return Square.Black;
        }
    }
}