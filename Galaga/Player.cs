using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga
{
    public class Player
    {
        private float moveRight;
        private float moveLeft;
        private const float MOVEMENT_SPEED = 0.01f;
        private Entity entity;
        private DynamicShape shape;
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            this.moveRight = 0.0f;
            this.moveLeft = 0.0f;
        }
        public void Render() {
            // TODO: render the player entity
        }

        public void Move() {
            // TODO: move the shape and guard against the window borders
        }
        public void SetMoveLeft(bool val) {
            // TODO:set moveLeft appropriately and call UpdateMovement()
            
        }
        public void SetMoveRight(bool val)
        {
            // TODO:set moveRight appropriately and call UpdateMovement()
        }
    }
}   
        

