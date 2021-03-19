using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

namespace Galaga
{
    public class PlayerShot : DIKUArcade.Entities.Entity
    {
        private static DIKUArcade.Math.Vec2F extent
        {
            get { return extent; }
            set
            {
                extent = value;
            }
        }
        private static DIKUArcade.Math.Vec2F direction
        {
            get { return direction; }
            set
            {
                direction = value;
            }
        }

        public PlayerShot(DynamicShape shape, DIKUArcade.Graphics.IBaseImage image)
            : base(shape, image)
        {
            extent = new DIKUArcade.Math.Vec2F(0.008f, 0.021f);
            direction = new DIKUArcade.Math.Vec2F(0.0f, 0.1f);
        }

    }
}