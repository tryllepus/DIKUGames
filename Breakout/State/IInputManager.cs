namespace Breakout.State
{
    public interface IInputManager
    {
        void KeyPress(string key);
        void KeyRelease(string key);
    }
}