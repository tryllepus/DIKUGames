using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Breakout.BreakoutEventBus;

namespace Breakout.State
{
    public interface IStateFields
    {
        Entity _backgroundImage { get; }
        Text[] _buttonOptions { get; }
        int _activeButtons { get; }
        int _maxButtons { get; }
    }
}