using System.IO;
using System;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Breakout.State
{
    public abstract class GameState
    {
        public virtual void GameLoop()
        {
            throw new NotImplementedException();
        }
        public virtual void InitializeGameState()
        {
            throw new NotImplementedException();
        }
        public virtual void UpdateGameLogic()
        {
            throw new NotImplementedException();
        }

    }


}