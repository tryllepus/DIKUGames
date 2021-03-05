using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class Enemy : Entity
    {
        public Vec2F StartPosition { get; }
        public Enemy(DynamicShape shape, IBaseImage image)
            : base(shape, image)
        {
            this.StartPosition = shape.Position.Copy();
        }


    }
}