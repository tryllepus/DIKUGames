using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.BreakoutEventBus;

namespace Breakout.State
{
    public class GamePaused : GameState, IGameState, IStateFields
    {
        private static GamePaused _instance = null;

        public Entity _backgroundImage { get; private set; }
        public Text[] _buttonOptions { get; private set; }
        public int _activeButtons { get; private set; }
        public int _maxButtons { get; private set; }
        public GamePaused()
        {
            _backgroundImage = new Entity
                (new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f),
                    new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            _buttonOptions = new[] {
                new Text("Continue", new Vec2F(0.4f, 0.5f), new Vec2F(0.3f, 0.2f)),
                new Text("Main Menu", new Vec2F(0.4f, 0.25f), new Vec2F(0.3f, 0.2f))};

            _activeButtons = 0;
            _maxButtons = _buttonOptions.Length;
        }

        public static GamePaused GetInstance()
        {
            return GamePaused._instance ?? (GamePaused._instance = new GamePaused());
        }

        public void RenderState()
        {
            _backgroundImage.RenderEntity();
            for (int i = 0; i < _buttonOptions.Length; i++)
            {
                if (_activeButtons == i)
                {
                    _buttonOptions[_activeButtons].SetColor(new Vec3I(255, 255, 255));
                    _buttonOptions[i].RenderText();
                }
                else
                {
                    _buttonOptions[i].SetColor(new Vec3I(255, 0, 0));
                    _buttonOptions[i].RenderText();
                }
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction)
        {
            if (keyAction == "KEY_PRESS")
            {
                switch (keyValue)
                {
                    case "KEY_UP":
                        _activeButtons = _activeButtons == 0 ?
                            _maxButtons - 1 : _activeButtons - 1;
                        break;
                    case "KEY_DOWN":
                        _activeButtons = _activeButtons == _maxButtons - 1 ?
                            0 : _activeButtons + 1;
                        break;
                    case "KEY_ENTER":
                        switch (_activeButtons)
                        {
                            case 0:
                                BreakoutBus.GetBus().RegisterEvent(
                                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                                        GameEventType.GameStateEvent, this,
                                            "CHANGE_STATE", "game_loop", ""));
                                break;
                            case 1:
                                BreakoutBus.GetBus().RegisterEvent(
                                     GameEventFactory<object>.CreateGameEventForAllProcessors(
                                         GameEventType.GameStateEvent, this,
                                             "CHANGE_STATE", "game_menu", ""));
                                break;
                        }
                        break;
                }
            }
        }
    }
}