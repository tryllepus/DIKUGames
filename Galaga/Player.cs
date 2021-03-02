using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class Player
    {
        private float moveRight;
        private float moveLeft;
        private const float MOVEMENT_SPEED = 0.01f;
        private Entity entity;
        private DynamicShape shape;
        public Vec2F Shape { get { return this.shape.Position; } } //! to be checked

        public Player(DynamicShape shape, IBaseImage image)
        {
            entity = new Entity(shape, image);
            this.shape = shape;
            this.moveRight = 0.0f;
            this.moveLeft = 0.0f;
        }
        public void Render()
        {
            // TODO: render the player entity
            //this.entity.RenderEntity(); //! this works 
        }

        public void Move() //! to be checked
        {
            // TODO: move the shape and guard against the window borders
            if (this.shape.Position.X <= 0.0f)
            {
                this.shape.Position.X = 0.0f;
            }
            else if (this.shape.Position.X >= 1.0f - this.shape.Extent.X)
            {
                this.shape.Position.X = 1.0f - this.shape.Extent.X;
            }
            this.shape.Move();
        }
        public void SetMoveLeft(bool val) //!to be cheched
        {
            // TODO: set moveLeft appropriately and call UpdateMovement()
            if (val == true)
            {
                moveLeft += MOVEMENT_SPEED;
                UpdateDirection();
            }
            else
            {
                moveLeft += 0.0f;
            }

        }
        public void SetMoveRight(bool val) //! to be checked
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

        private void UpdateDirection() //! to be checked
        {
            if (moveLeft >= 0.0f)
            {
                this.shape.Direction.X -= moveLeft;
            }
            if (moveRight >= 0.0f)
            {
                this.shape.Direction.X += moveRight;
            }


        }
    }
}


