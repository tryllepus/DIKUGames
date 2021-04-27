using System;
namespace Breakout.State
{
    public class StateTransformer
    {
        /// <summary>
        /// transforms a string to a state
        /// </summary>
        /// <param name="state"></param>
        /// takes a list of characters  (string)
        /// <returns></returns>
        /// returns the corresponding state representation of the string
        public static StateType TransformStringToState(string state)
        {
            switch (state)
            {
                case "game_loop":
                    return StateType._gameRunner;
                case "game_paused":
                    return StateType._gamePaused;
                case "game_menu":
                    return StateType._gameMenu;
                default:
                    throw new ArgumentException("Invalid state");
            }
        }
        /// <summary>
        /// transforms a state to a string
        /// </summary>
        /// <param name="state"></param>
        /// takes a case of string of type (StateType)
        /// <returns></returns>
        /// returns the corresponding string representation of the state
        public static string TransformStateToString(StateType state)
        {
            switch (state)
            {
                case StateType._gameRunner:
                    return "game_loop";
                case StateType._gamePaused:
                    return "game_paused";
                case StateType._gameMenu:
                    return "game_menu";
                default:
                    throw new ArgumentException("Invalid state");
            }
        }
    }
}