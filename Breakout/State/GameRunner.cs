using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.BreakoutEventBus;

namespace Breakout.State
{
    public class GameRunner : GameState, IGameState, IInputManager
    {
        private static GameRunner _instance = null;
        public GameRunner()
        {

        }

        public static GameRunner GetInstance()
        {
            return GameRunner._instance ?? (GameRunner._instance = new GameRunner());
        }

        public override void GameLoop()
        {
            base.GameLoop();
        }
        public override void InitializeGameState()
        {
            base.InitializeGameState();
        }
        public override void UpdateGameLogic()
        {
            base.UpdateGameLogic();
        }

        public void RenderState()
        {

        }


        public void KeyPress(string key)
        {
            switch (key)
            {
                case "KEY_ESCAPE":
                    BreakoutBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this,
                            "CHANGE_STATE", "game_paused", ""));
                    break;
                case "KEY_LEFT":
                    BreakoutBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "KEY_LEFT", "KEY_PRESS", ""));
                    break;

                case "KEY_RIGHT":
                    BreakoutBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "KEY_RIGHT", "KEY_PRESS", ""));
                    break;

                case "KEY_DOWN":
                    BreakoutBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_DOWN", "KEY_PRESS", " "));
                    break;

                case "KEY_UP":
                    BreakoutBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_UP", "KEY_PRESS", " "));
                    break;
                    /*
                    case "KEY_SPACE":
                        AddNewShot();
                        break;
                    */
            }
        }

        public void KeyRelease(string key)
        {
            switch (key)
            {
                case "KEY_LEFT":
                    BreakoutBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_LEFT", "KEY_RELEASE", " "));
                    break;

                case "KEY_RIGHT":
                    BreakoutBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                            "KEY_RIGHT", "KEY_RELEASE", " "));
                    break;

                case "KEY_UP":
                    BreakoutBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_UP", "KEY_RELEASE", " "));
                    break;

                case "KEY_DOWN":
                    BreakoutBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_DOWN", "KEY_RELEASE", " "));
                    break;

            }
        }
        public void HandleKeyEvent(string keyAction, string keyValue)
        {
            switch (keyValue)
            {
                case "KEY_PRESS":
                    KeyPress(keyAction);
                    break;

                case "KEY_RELEASE":
                    KeyRelease(keyAction);
                    break;
            }
        }
    }
}