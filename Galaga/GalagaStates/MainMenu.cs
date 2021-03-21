using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Galaga;

namespace GalagaStates
{
    public class MainMenu //: IGameState
    {
        /*
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public static MainMenu GetInstance()
        {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }
        public MainMenu()
        {
            backGroundImage = new Entity
                (new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f),
                    new Image(Path.Combine("Assets", "Images", "TitleImage.png")));

            menuButtons = new[]
            {
                new Text("New Game", new Vec2F(0.3f, 0.5f), new Vec2F(0.2f, 0.3f)),
                new Text("Quit", new Vec2F(0.3f, 0.25f), new Vec2F(0.2f, 0.3f)),
            };
            InitializeGameState();
        }
        public void GameLoop()
        {
        }


        public void InitializeGameState()
        {
            activeMenuButton = 0;
            maxMenuButtons = 1;
        }

        public void RenderState()
        {
            backGroundImage.RenderEntity();
            for (int i = 0; i < menuButtons.Length; i++)
            {
                if (activeMenuButton == i)
                {
                    menuButtons[activeMenuButton].SetColor(new Vec3I(255, 255, 255));
                    menuButtons[i].RenderText();
                }
                else
                {
                    menuButtons[i].SetColor(new Vec3I(255, 0, 0));
                    menuButtons[i].RenderText();
                }
            }

        }
        public void UpdateGameLogic()
        {
        }
        public void HandleKeyEvent(string keyValue, string keyAction)
        {
            if (keyAction == "KEY_PRESS")
            {
                switch (keyValue)
                {
                    case "KEY_UP":
                        if (activeMenuButton > maxMenuButtons)
                        {
                            activeMenuButton -= 1;
                        }
                        else
                        {
                            activeMenuButton %= maxMenuButtons;
                        }
                        break;
                    case "KEY_DOWN":
                        if (activeMenuButton < maxMenuButtons)
                        {
                            activeMenuButton += 1;
                        }
                        else
                        {
                            activeMenuButton %= maxMenuButtons;
                        }
                        break;
                    case "KEY_ENTER":
                        if (activeMenuButton == 0)
                        {
                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.GameStateEvent, this,
                                    "CHANGE_STATE", "GAME_RUNNING", ""));
                        }
                        else if (activeMenuButton == 1)
                        {
                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.WindowEvent, this,
                                    "CLOSE_WINDOW", "KEY_PRESS", ""));
                        }
                        break;

                }
            }
        }
        */
    }
}