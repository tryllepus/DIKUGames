using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using Breakout.State;
using Breakout.BreakoutEventBus;

namespace Breakout
{
    public class Game : IGameEventProcessor<object>
    {
        private GameTimer _timer;
        private Window _window;
        private StateMachine _stateMachine;

        public Game()
        {
            _window = new Window("BREAKOUT", 500, 500);
            _window.RegisterEventBus(BreakoutBus.GetBus());
            _timer = new GameTimer(60, 60); // 60 UPS, 60 FPS(frame per second) limit
            _stateMachine = new StateMachine();

            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType>(){
                GameEventType.WindowEvent,
                GameEventType.InputEvent,
                GameEventType.GameStateEvent,
                GameEventType.PlayerEvent
            });

            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, _stateMachine);
        }

        public void Run()
        {
            while (_window.IsRunning())
            {
                _timer.MeasureTime();
                while (_timer.ShouldUpdate())
                {
                    _window.PollEvents();
                    BreakoutBus.GetBus().ProcessEvents();
                    //_stateMachine._activeState.UpdateGameLogic();

                }
                if (_timer.ShouldRender())
                {
                    _window.Clear();
                    _stateMachine._activeState.RenderState();
                    _window.SwapBuffers();
                }
                if (_timer.ShouldReset())
                {
                    _window.Title = $"BREAKOUT | (UPS,FPS): ({_timer.CapturedUpdates},{_timer.CapturedFrames})";
                }
            }

        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.WindowEvent)
            {
                switch (gameEvent.Message)
                {
                    case "CLOSE_WINDOW":
                        _window.CloseWindow();
                        break;
                }
            }
            if (eventType == GameEventType.GameStateEvent)
            {
                _stateMachine.ProcessEvent(eventType, gameEvent);
            }
            else if (eventType == GameEventType.InputEvent)
            {
                switch (gameEvent.Parameter1)
                {
                    case "KEY_PRESS":
                        _stateMachine._activeState.HandleKeyEvent(
                            gameEvent.Message,
                            gameEvent.Parameter1
                        );
                        break;
                    case "KEY_RELEASE":
                        _stateMachine._activeState.HandleKeyEvent(
                            gameEvent.Message,
                            gameEvent.Parameter1
                        );
                        break;
                    default:
                        break;
                }
            }
        }

    }
}