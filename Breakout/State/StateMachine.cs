using System;
using DIKUArcade.State;
using DIKUArcade.EventBus;
using Breakout.BreakoutEventBus;


namespace Breakout.State
{
    public class StateMachine : IGameEventProcessor<object>
    {
        public IGameState _activeState { get; private set; }
        public StateMachine()
        {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            _activeState = GameMenu.GetInstance();
        }

        private void SwitchState(StateType stateType)
        {
            switch (stateType)
            {
                case StateType._gameMenu:
                    if (!Object.ReferenceEquals(_activeState, GameMenu.GetInstance()))
                    {
                        _activeState = GameMenu.GetInstance();
                    }
                    break;
                case StateType._gamePaused:
                    if (!Object.ReferenceEquals(_activeState, GamePaused.GetInstance()))
                    {
                        _activeState = GamePaused.GetInstance();
                    }
                    break;
                case StateType._gameRunner:
                    if (!Object.ReferenceEquals(_activeState, GameRunner.GetInstance()))
                    {
                        _activeState = GameRunner.GetInstance();
                    }
                    break;
            }
        }


        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.GameStateEvent && gameEvent.Message == "CHANGE_STATE")
            {
                SwitchState(StateTransformer.TransformStringToState(gameEvent.Parameter1));
            }
            else if (eventType == GameEventType.InputEvent)
            {
                _activeState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            }
        }
    }
}