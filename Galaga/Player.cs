using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class Player : IGameEventProcessor<object>
    {
        private float moveRight;
        private float moveLeft;
        private float moveUp;
        private float moveDown;
        private const float MOVEMENT_SPEED = 0.03f;
        private Entity entity;
        private DynamicShape shape;
        public Vec2F Shape { get { return this.shape.Position; } }

        public Player(DynamicShape shape, IBaseImage image)
        {
            GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
            entity = new Entity(shape, image);
            this.shape = shape;
            this.moveRight = 0.0f;
            this.moveLeft = 0.0f;
            this.moveDown = 0.0f;
            this.moveUp = 0.0f;
        }

        public Vec2F getPos()
        {
            return new Vec2F(shape.Position.X + (shape.Extent.X / 2),
                 shape.Position.Y);
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
                moveLeft = -MOVEMENT_SPEED;
                UpdateDirection();
            }
            else
            {
                moveLeft = 0.0f;
                UpdateDirection();
            }

        }
        public void SetMoveRight(bool val)
        {
            // TODO:set moveRight appropriately and call UpdateMovement()
            if (val == true)
            {
                moveRight = +MOVEMENT_SPEED;
                UpdateDirection();
            }
            else
            {
                moveRight = 0.0f;
                UpdateDirection();
            }
        }
        public void SetMoveDown(bool val)
        {
            if (val == true)
            {
                moveDown = -MOVEMENT_SPEED;
                UpdateDirection();
            }
            else
            {
                moveDown = 0.0f;
                UpdateDirection();
            }
        }
        public void SetMoveUp(bool val)
        {
            if (val == true)
            {
                moveUp = +MOVEMENT_SPEED;
                UpdateDirection();
            }
            else
            {
                moveUp = 0.0f;
                UpdateDirection();
            }
        }
        private void UpdateDirection()
        {
            this.shape.Direction.X = moveLeft + moveRight;

            if (moveDown >= 0.0f || moveUp >= 0.0f)
            {
                this.shape.Direction.Y = moveDown + moveUp;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.PlayerEvent)
            {
                switch (gameEvent.Parameter1)
                {
                    /* case "CLOSE_WINDOW":
                         window.CloseWindow();
                         break;*/
                    case "KEY_PRESS":
                        switch (gameEvent.Message)
                        {
                            case "KEY_RIGHT":
                                this.SetMoveRight(true);
                                break;
                            case "KEY_LEFT":
                                this.SetMoveLeft(true);
                                break;

                            case "KEY_UP":
                                this.SetMoveUp(true);
                                break;

                            case "KEY_DOWN":
                                this.SetMoveDown(true);
                                break;
                            default:
                                break;
                        }
                        break;

                    case "KEY_RELEASE":
                        switch (gameEvent.Message)
                        {
                            case "KEY_RIGHT":
                                this.SetMoveRight(false);
                                break;
                            case "KEY_LEFT":
                                this.SetMoveLeft(false);
                                break;
                            case "KEY_UP":
                                this.SetMoveUp(false);
                                break;
                            case "KEY_DOWN":
                                this.SetMoveDown(false);
                                break;
                        }
                        break;

                }
            }
        }

    }
}


