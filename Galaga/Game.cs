using DIKUArcade;
using DIKUArcade.Timers;

namespace Galaga
{
    public class Game
    {
        private Window window;
        private GameTimer gameTimer;
        public Game()
        {
            window = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(30, 30);
        }
        public void Run()
        {
            while(window.IsRunning())
            {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate())
                {
                    window.PollEvents();
                    // update game logic here...
                }
                if (gameTimer.ShouldRender())
                {
                    window.Clear();
                    // render game entities here...
                    window.SwapBuffers();
                }
                if (gameTimer.ShouldReset())
                {
                    // this update happens once every second
                    window.Title = $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{gameTimer.CapturedFrames})";
                }
            }
        }
    }
}
        
 