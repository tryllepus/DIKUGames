using NUnit.Framework;
using System;
using System.Collections.Generic;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade;

namespace GalagaTests
{
    [TestFixture]
    public class TestPlayer
    {
        private Galaga.Player player;
        private const float MOVEMENT_SPEED = 0.04f;
        private DynamicShape playerShape;
        private GameEventBus<object> rollerCoaster;

        [SetUp]
        public void init()
        {
            DIKUArcade.Window.CreateOpenGLContext();

            this.player = new Player(new DynamicShape(
                        new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                        new Image(Path.Combine("Assets", "Images", "Player.png")));

            rollerCoaster = GalagaBus.GetBus();

            rollerCoaster.InitializeEventBus(new List<GameEventType>(){
                GameEventType.WindowEvent,
                GameEventType.PlayerEvent,
                GameEventType.GameStateEvent,
                GameEventType.InputEvent,
                GameEventType.TimedEvent,
                GameEventType.ControlEvent
            });

        }

        [TestCase("KEY_LEFT")]
        [TestCase("KEY_RIGHT")]
        public void TestPlayer_All_Movement(string message)
        {
            var newpos = new DynamicShape(new Vec2F(0.55f, 0.1f), new Vec2F(0.1f, 0.1f));

            switch (message)
            {
                case "KEY_LEFT":
                    rollerCoaster.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_LEFT",
                                " ", " " ));
                    break;
                case "KEY_RIGHT":
                    rollerCoaster.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "KEY_RIGHT",
                                " ", " " )); 
                    break;
            }
            rollerCoaster.ProcessEvents();
            Assert.AreNotEqual(player.getPos(), newpos);
            


        }
    }
}