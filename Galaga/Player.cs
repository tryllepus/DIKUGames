using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class Player
    {
        private float moveRight;
        private float moveLeft;
        private float moveUp;
        private float moveDown;
        private const float MOVEMENT_SPEED = 0.01f;
        private Entity entity;
        private DynamicShape shape;
        public Vec2F Shape { get { return this.shape.Position; } }

        public Player(DynamicShape shape, IBaseImage image)
        {
            entity = new Entity(shape, image);
            this.shape = shape;
            this.moveRight = 0.0f;
            this.moveLeft = 0.0f;
            this.moveDown = 0.0f;
            this.moveUp = 0.0f;
        }

        public Vec2F getPos()
        {
            return this.shape.Position;
        }
        public void Render()
        {
            // TODO: render the player entity
            this.entity.RenderEntity();
        }

        public void Move()
        {
            this.shape.Move();
            // TODO: move the shape and guard against the window borders
            if (this.shape.Position.X <= 0.0f)
            {
                this.shape.Position.X = 0.0f;
            }
            else if (this.shape.Position.X >= 1.0f - this.shape.Extent.X)
            {
                this.shape.Position.X = 1.0f - this.shape.Extent.X;
            }

            if (this.shape.Position.Y <= 0.0f)
            {
                this.shape.Position.Y = 0.0f;
            }
            else if (this.shape.Position.Y >= 1.0f - this.shape.Extent.Y)
            {
                this.shape.Position.Y = 1.0f - this.shape.Extent.Y;
            }
        }
        public void SetMoveLeft(bool val)
        {
            // TODO: set moveLeft appropriately and call UpdateMovement()
            if (val == true)
            {
                moveLeft -= MOVEMENT_SPEED;
                UpdateDirection();
            }
            else
            {
                moveLeft -= 0.0f;
            }

        }
        public void SetMoveRight(bool val)
        {
            // TODO:set moveRight appropriately and call UpdateMovement()
            if (val == true)
            {
                moveRight += MOVEMENT_SPEED;
                UpdateDirection();
            }
            else
            {
                moveRight += 0.0f;
            }
        }
        public void SetMoveDown(bool val)
        {
            if (val == true)
            {
                moveDown += MOVEMENT_SPEED;
                UpdateDirection();
            }
            else
            {
                moveDown += 0.0f;
            }
        }
        public void SetMoveUp(bool val)
        {
            if (val == true)
            {
                moveUp += MOVEMENT_SPEED;
                UpdateDirection();
            }
            else
            {
                moveUp += 0.0f;
            }
        }

        private void UpdateDirection()
        {
            this.shape.Position.X = moveLeft + moveRight;

            if (moveDown >= 0.0f)
            {
                this.shape.Position.Y -= moveDown;
            }
            if (moveUp >= 0.0f)
            {
                this.shape.Position.Y += moveUp;
            }
        }

    }
}


