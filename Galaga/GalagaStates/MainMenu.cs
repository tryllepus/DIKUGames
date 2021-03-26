using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Galaga.GalagaStates
{
    public class MainMenu : IGameState
    {

        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
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
            activeMenuButton = 0;
            maxMenuButtons = menuButtons.Length;
            
        }

        public static MainMenu GetInstance()
        {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }
        
        public void GameLoop()
        {
            throw new System.NotImplementedException();
        }
        


        public void InitializeGameState()
        {
            throw new System.NotImplementedException();
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
                        activeMenuButton = activeMenuButton == 0 ?
                            maxMenuButtons - 1:
                            activeMenuButton - 1;
                        break;
                    case "KEY_DOWN":
                        activeMenuButton = activeMenuButton == maxMenuButtons - 1?
                            0:
                            activeMenuButton + 1;
                        break;
                    case "KEY_ENTER":
                        switch(activeMenuButton)
                        {
                            case 0:
                                GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.GameStateEvent, this,
                                    "CHANGE_STATE", "GAME_RUNNING", ""));
                                break;
                            case 1:
                                GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.WindowEvent, this,
                                    "CLOSE_WINDOW", "KEY_PRESS", ""));
                                break;
                        }
                        break;
                }
            }
        }
    }
}