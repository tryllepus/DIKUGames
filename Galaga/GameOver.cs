using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class GameOver
    {
        public bool gameIsOver;
        private Text display;
        public GameOver(Vec2F position, Vec2F extent)
        {
            display = new Text("Game Over", position, extent);
            gameIsOver = false;
        }
        public void Render()
        {
            display.RenderText();
        }
    }
}