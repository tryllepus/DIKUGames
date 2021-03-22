using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga
{
    public class Score
    {
        private int score;
        private Text display;
        private Enemy enemy;
        public Score(Vec2F position, Vec2F extent)
        {
            score = 0;
            display = new Text(score.ToString(), position, extent);
        }
        public void AddPoint()
        {
            this.score += 50;
        }
        public void RenderScore()
        {
            display.RenderText();
        }
    }
}