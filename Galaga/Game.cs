using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using Galaga.GalagaStates;

namespace Galaga
{
    public class Game : IGameEventProcessor<object>
    {
        private GameEventBus<object> eventBus;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private List<Image> enemyStridesRed;
        private StateMachine stateMachine;
        private GameTimer gameTimer;

        private Player player;
        private Window win;
        public Game()
        {
            win = new Window("Galaga", 500, 500);
            win.RegisterEventBus(GalagaBus.GetBus());
            gameTimer = new GameTimer(30, 30);
            stateMachine = new StateMachine();

            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType>(){
               GameEventType.WindowEvent,
               GameEventType.InputEvent,
               GameEventType.GameStateEvent,
               GameEventType.PlayerEvent
            });

            GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);

            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            enemyStridesRed = ImageStride.CreateStrides(2,
                    Path.Combine("Assets", "Images", "RedMonster.png"));
        }

        public void Run()
        {
            while (win.IsRunning())
            {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate())
                {
                    win.PollEvents();
                    GalagaBus.GetBus().ProcessEvents();
                    stateMachine.ActiveState.UpdateGameLogic();

                }

                if (gameTimer.ShouldRender())
                {
                    win.Clear();
                    stateMachine.ActiveState.RenderState();                                    
                    win.SwapBuffers();
                }
                if (gameTimer.ShouldReset())
                {
                    win.Title = $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{gameTimer.CapturedFrames})";
                }
            }
        }

        public void ProcessEvent(GameEventType evenType, GameEvent<object> gameEvent)
        {
            if (evenType == GameEventType.WindowEvent)
            {
                switch (gameEvent.Message)
                {
                    case "CLOSE_WINDOW":
                        win.CloseWindow();
                        break;
                }
            }
            if (evenType == GameEventType.GameStateEvent)
            {
                stateMachine.ProcessEvent(evenType, gameEvent);
            }
            else if (evenType == GameEventType.InputEvent)
            {   
                switch (gameEvent.Parameter1)
                {
                    case "KEY_PRESS":
                        stateMachine.ActiveState.HandleKeyEvent(
                            gameEvent.Message,
                            gameEvent.Parameter1);

                        break;
                    case "KEY_RELEASE":
                        stateMachine.ActiveState.HandleKeyEvent(
                            gameEvent.Message,
                            gameEvent.Parameter1);
                        break;
                    default:
                        break;
                }
            }

        }
    }
}

