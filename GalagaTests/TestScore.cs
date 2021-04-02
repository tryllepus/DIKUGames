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
    public class TestScore
    {
       /*
        public Galaga.Player player;
        public Galaga.Enemy enemy;
        public Galaga.Score score;
        public int scorePoint;

        [SetUp]
        public void init()
        {
            player = new Player(new DynamicShape(
                        new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                        new Image(Path.Combine("Assets", "Images", "Player.png")));
            
            score = new Score(new Vec2F(0.7f, 0.7f), new Vec2F(0.3f, 0.3f));

            enemy = this.player = new Enemy(new DynamicShape(
                        new Vec2F(0.25f, 0.9f), new Vec2F(0.1f, 0.1f)),
                        new Image(Path.Combine("Assets", "Images", "BlueMonster")));

            scorePoint = 0;
        }


        [Test]
        public void Test_Score()
        {
            if (enemy.isDead())
            {
                enemy.DeleteEntity();
                score.AddPoint();
            }
            Assert.AreGreater(scorePoint, 0);
        }  
        */  
    }
}