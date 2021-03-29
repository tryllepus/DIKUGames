using System;
using NUnit.Framework;
using Galaga.GalagaStates;

namespace GalagaTests
{
    [TestFixture]
    public class TestTransformStringToState
    {
        [Test]
        public void TestTransformStringToState_GAME_MAINMENU()
        {
            Assert.AreSame(StateTransformer.TransformStringToState("MAIN_MENU"),
                    GameStateType.MainMenu);
        }

        [Test]
        public void TestTransformStringToState_GAME_PAUSED()
        {
            Assert.AreSame(StateTransformer.TransformStringToState("GAME_PAUSED"),
                    GameStateType.GamePaused);
        }

        [Test]
        public void TestTransformStringToState_GAME_RUNNING()
        {
            Assert.AreSame(StateTransformer.TransformStringToState("GAME_RUNNING"),
                    GameStateType.GameRunning);
        }

    }
}