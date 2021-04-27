
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;


namespace Breakout.BreakoutEventBus
{
    public static class BreakoutBus
    {
        private static GameEventBus<object> eventBus;
        public static GameEventBus<object> GetBus()
        {
            return BreakoutBus.eventBus ?? (BreakoutBus.eventBus
                                                = new GameEventBus<object>());
        }
    }
}