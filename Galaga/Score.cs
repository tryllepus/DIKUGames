using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class Score
    {
        private int score;
        public Text display { get; private set; }
        public Score(Vec2F position, Vec2F extent)
        {
            score = 0;
            display = new Text("Score: " + score.ToString(), position, extent);
            display.SetColor(new Vec3I(255, 255, 255));
            display.SetFontSize(36);
        }
        public void AddPoint()
        {
            this.score += 50;
        }
        public void RenderScore()
        {
            display.SetText(string.Format("score: {0}", score.ToString()));
            display.RenderText();
        }
    }
}