using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;

namespace Galaga
{
    public class Game : IGameEventProcessor<object>
    {
        private Player player;
        private Window window;
        private GameTimer gameTimer;
        private GameEventBus<object> eventBus;
        private EntityContainer<Enemy> enemies;
        private EntityContainer playerShots;        //<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        public Game()
        {
            window = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(30, 30);

            player = new Player(new DynamicShape(
                        new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                        new Image(Path.Combine("Assets", "Images", "Player.png")));

            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.
            InputEvent });

            window.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);

            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++)
            {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f),
                    new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, images)));
            }


            playerShots = new DIKUArcade.Entities.EntityContainer();   //<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));


        }

        public void Run()
        {
            while (window.IsRunning())
            {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate())
                {
                    window.PollEvents();
                    // update game logic here...
                    eventBus.ProcessEvents(); //! WORKS
                    player.Move();
                    IterateShots();
                }

                if (gameTimer.ShouldRender())
                {
                    window.Clear();
                    player.Render(); //! this works
                    enemies.RenderEntities();
                    playerShots.RenderEntities();
                    // render game entities here...
                    window.SwapBuffers();
                }

                if (gameTimer.ShouldReset())
                {
                    // this update happens once every second
                    window.Title = $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{gameTimer.CapturedFrames})";
                }
            }
        }


        public void KeyPress(string key)
        {
            // TODO: switch on key string and set the player's move direction
            switch (key)
            {
                case "KEY_LEFT":
                    player.SetMoveLeft(true);
                    break;
                case "KEY_RIGHT":
                    player.SetMoveRight(true);
                    break;
                case "KEY_SPACE":
                    IterateShots(); //! WORK
                    break;

                default:
                    break;
            }
        }

        public void KeyRelease(string key) //! WORKS
        {
            // TODO: switch on key string and disable the player's move direction
            // TODO: Close window if escape is pressed
            switch (key)
            {
                case "KEY_LEFT":
                    player.SetMoveLeft(false);
                    break;
                case "KEY_RIGHT":
                    player.SetMoveRight(false);
                    break;
                case "KEY_ESCAPE":  //! WORKS
                    window.CloseWindow();
                    break;
                case "KEY_SPACE":
                    AddNewShot(); //!WORKS
                    break;

                default:
                    break;
            }
        }

        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent)
        {
            switch (gameEvent.Parameter1)
            {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                default:
                    break;

            }
        }
        public void AddNewShot() //! WORK
        {
            var shot = new DynamicShape(new Vec2F(player.getPos().X, player.getPos().Y), //+ 0.008f, player.Shape.Y + 0.01f),
                new Vec2F(0.008f, 0.021f));

            playerShots.AddDynamicEntity(shot, playerShotImage);
            //playerShots.AddDynamicEntity(); //AddEntity(shot);

        }

        private void IterateShots()
        {
            playerShots.Iterate(shot =>
            {
                // TODO: move the shot's shape
                {
                    shot.Shape.Move();
                    ((DynamicShape)shot.Shape).Direction.Y += 0.02f;

                    /* TODO: guard against window borders */
                    if (shot.Shape.Position.Y > 1.0f) //! to be tested
                    {
                        // TODO: delete shot
                        shot.DeleteEntity(); //! to be tested
                    }
                    else
                    {
                        enemies.Iterate(enemy =>
                        {
                            // TODO: if collision btw shot and enemy -> delete both
                            if (CollisionDetection.Aabb((DynamicShape)shot.Shape,
                                 enemy.Shape).Collision)
                            {
                                shot.DeleteEntity();
                                enemy.DeleteEntity();
                            }
                        });
                    }
                }

            });
        }

    }
}

