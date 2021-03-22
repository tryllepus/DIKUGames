using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class GameOver
    {
        public bool gameIsOver;
        public Text display { get; private set; }
        public GameOver(Vec2F position, Vec2F extent)
        {
            display = new Text("Game Over", position, extent);
            display.SetColor(new Vec3I(255, 255, 255));
            display.SetFontSize(76);
            gameIsOver = false;
        }
        public void Render()
        {
            display.RenderText();
        }



    }
}