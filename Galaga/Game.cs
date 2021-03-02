using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.EventBus;

namespace Galaga
{
    public class Game : IGameEventProcessor<object>
    {
        private Player player;
        private Window window;
        private GameTimer gameTimer;
        private GameEventBus<object> eventBus;
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
                    eventBus.ProcessEvents(); //! to be ckecked
                    player.Move();
                }

                if (gameTimer.ShouldRender())
                {
                    window.Clear();
                    player.Render(); //! this works
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


        public void KeyPress(string key) //! to be checked
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

                default:
                    break;
            }
        }

        public void KeyRelease(string key) //! to be checked
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
    }
}

