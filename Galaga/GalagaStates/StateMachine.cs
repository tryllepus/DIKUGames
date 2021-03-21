using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga;

namespace GalagaStates
{
    public class StateMachine //: IGameEventProcessor<object>
    {
        /*
        public IGameState ActiveState { get; private set; }
        public StateMachine()
        {
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
        }
        private void SwitchState(GameStateType stateType)
        {
            switch (stateType)
            {
                case GameStateType.MainMenu:
                    if (!Object.ReferenceEquals(ActiveState, MainMenu.GetInstance()))
                    {
                        ActiveState = MainMenu.GetInstance();
                    }
                    break;
                case GameStateType.GamePaused:
                    if (!Object.ReferenceEquals(ActiveState, GamePaused.GetInstance()))
                    {
                        ActiveState = GamePaused.GetInstance();
                    }

                    break;
                case GameStateType.GameRunning:
                    if (!Object.ReferenceEquals(ActiveState, GameRunning.GetInstance()))
                    {
                        ActiveState = GameRunning.GetInstance();
                    }
                    break;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.GameStateEvent)
            {
                if (gameEvent.Message == "CHANGE_STATE")
                {
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.Parameter1));
                }
            }
            else if (eventType == GameEventType.InputEvent)
            {
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            }
        }
        */
    }
}
