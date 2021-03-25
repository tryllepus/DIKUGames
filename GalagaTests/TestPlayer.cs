using NUnit.Framework;
using System;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using System.IO;
using DIKUArcade.Entities;

namespace GalagaTests
{
    [TestFixture]
    public class TestPlayer
    {
        public Galaga.Player player;
        [SetUp]
        public void SetUp()
        {
            this.player = new Player(new DynamicShape(
                        new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                        new Image(Path.Combine("Assets", "Images", "Player.png")));

        }

        [Test]
        public void TestPlayer_moveLeft()
        {
            // Assert.True();
        }

    }
}