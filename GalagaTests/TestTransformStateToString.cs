using System;
using NUnit.Framework;
using Galaga.GalagaStates;



namespace GalagaTests
{
    [TestFixture]
    public class TestTransformStateToString
    {
        [Test]
        public void TestTransformStateToString_GAME_MAINMEMU()
        {
            Assert.AreSame(StateTransformer.TransformStateToString(
                GameStateType.MainMenu), "MAIN_MENU");
        }

        [Test]
        public void TestTransformStateToString_GAME_PAUSED()
        {
            Assert.AreSame(StateTransformer.TransformStateToString(
                GameStateType.GamePaused), "GAME_PAUSED");
        }

        [Test]
        public void TestTransformStateToString_GAME_RUNNING()
        {
            Assert.AreSame(StateTransformer.TransformStateToString(
                GameStateType.GameRunning), "GAME_RUNNING");

        }
    }
}