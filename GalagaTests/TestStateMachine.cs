using System;
using Galaga;
using DIKUArcade.EventBus;
using NUnit.Framework;
using Galaga.GalagaStates;
using System.Collections.Generic;

namespace GalagaTests
{
    [TestFixture]
    public class TestStateMachine
    {
        private StateMachine cooster;
        [SetUp]
        public void InitiateStateMachine()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            cooster = new StateMachine();

            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType>() {
                GameEventType.WindowEvent,
                GameEventType.InputEvent,
                GameEventType.GameStateEvent,
                GameEventType.PlayerEvent
            });
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, cooster);

        }


        
        [Test]
        public void TestInitialState()
        {
            Assert.That(cooster.ActiveState, Is.InstanceOf<MainMenu>());
        }


        [Test]
        public void TestEventGamePaused()
        {
            GalagaBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent,
                    this,
                    "CHANGE_STATE",
                    "GAME_PAUSED", ""));

            GalagaBus.GetBus().ProcessEventsSequentially();
            Assert.That(cooster.ActiveState, Is.InstanceOf<GamePaused>());

        }
    }
}