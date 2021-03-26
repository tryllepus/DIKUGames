using System.Collections.Generic;
using System.IO;
using System;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Galaga.MovementStrategy;
using Galaga.Squadrons;
using DIKUArcade;

namespace Galaga.GalagaStates
{
    public class GameRunning : IGameState
    {

        private static GameRunning instance;
        private EntityContainer<Enemy> enemies;
        private IBaseImage playerShotImage;
        private const int EXPLOSION_LENGTH_MS = 500;
        private AnimationContainer enemyExplosions;
        private MoveDown moveDown;
        private NoMove noMove;
        private Window window;
        private List<Image> explosionStrides;
        private EntityContainer playerShots;
        private MoveZigzagDown zigzagDown;
        private List<List<Image>> typesOfEnemy;
        private List<Image> blueBandits;
        private List<Image> redBandits;
        private List<Image> greenBandits;
        private List<ISquadron> bandidosSquadron;
        private Player player;
        private Score score;
        private Image backgroundImage;
        private int deadEnemies;
        private int roundCount;
        private RedSquadron redB;
        private BlueSquadron blueB;
        private GreenSquadron greenB;
        private int movingNum;
        private GameOver gameOver;
        private Random random;

        public GameRunning()
        {
            this.InitializeGameState();
        }

        public static GameRunning GetInstance()
        {
            return GameRunning.instance ??
                (GameRunning.instance = new GameRunning());
        }


        public void AddExplosion(Vec2F position, Vec2F extent)
        {
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent), EXPLOSION_LENGTH_MS,
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides)
            );
        }

        public void AddNewShot()
        {
            var shot = new DynamicShape(new Vec2F(player.getPos().X,
                 player.getPos().Y),
                    new Vec2F(0.008f, 0.021f));

            playerShots.AddDynamicEntity(shot, playerShotImage);
        }


        private void IterateShots()
        {
            playerShots.Iterate(shot =>
            {

                {
                    shot.Shape.Move();
                    ((DynamicShape)shot.Shape).Direction.Y += 0.02f;//! maybe just '='

                    if (shot.Shape.Position.Y > 1.0f)
                    {
                        shot.DeleteEntity();
                    }
                    else
                    {
                        for (int i = 0; i < bandidosSquadron.Count; i++)
                        {
                            bandidosSquadron[i].Enemies.Iterate(enemy =>
                            {
                                if (CollisionDetection.Aabb((DynamicShape)shot.Shape,
                                     enemy.Shape).Collision)
                                {
                                    AddExplosion(new Vec2F(enemy.Shape.Position.X, enemy.Shape.Position.Y),
                                        new Vec2F(enemy.Shape.Extent.X, enemy.Shape.Extent.Y));
                                    shot.DeleteEntity();
                                    enemy.HitMarker();
                                    enemy.Criticalhealth();
                                    if (enemy.isDead())
                                    {
                                        enemy.DeleteEntity();
                                        score.AddPoint();
                                        deadEnemies++;
                                        if (deadEnemies >= 10)
                                        {
                                            moveDown.MOVEMENT_SPEED += 0.001f;
                                            zigzagDown.MOVEMENT_SPEED += 0.001f;
                                            deadEnemies = 0;
                                        }

                                    }
                                }

                            });
                        }


                    }
                }
            });

        }



        public void GameLoop()
        {
        }

        public void InitializeGameState()
        {
            player = new Player(new DynamicShape(
                       new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                       new Image(Path.Combine("Assets", "Images", "Player.png")));

            greenBandits = ImageStride.CreateStrides(2,
                Path.Combine("Assets", "Images", "GreenMonster.png"));

            redBandits = ImageStride.CreateStrides(2,
                Path.Combine("Assets", "Images", "RedMonster.png"));

            blueBandits = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png"));

            bandidosSquadron = new List<ISquadron>();
            redB = new RedSquadron();
            blueB = new BlueSquadron();
            greenB = new GreenSquadron();

            typesOfEnemy = new List<List<Image>>() { greenBandits, redBandits, blueBandits };

            zigzagDown = new MoveZigzagDown();
            noMove = new NoMove();
            moveDown = new MoveDown();

            explosionStrides = ImageStride.CreateStrides(8,
                           Path.Combine("Assets", "Images", "Explosion.png"));
            score = new Score(new Vec2F(0.7f, 0.7f), new Vec2F(0.3f, 0.3f));
            gameOver = new GameOver(new Vec2F(0.2f, 0.3f), new Vec2F(0.4f, 0.4f));

            enemies = new EntityContainer<Enemy>();

            enemyExplosions = new AnimationContainer(60);
            playerShots = new EntityContainer();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            roundCount = 7;
            deadEnemies = 0;
            random = new Random();
        }

        public void UpdateGameLogic()
        {
            IterateShots();
            player.Move();
            FormationAction();
            NextRound();
        }
        public void RenderState()
        {
            IsGameOver();
            if (gameOver.gameIsOver == false)
            {
                player.Render();
                score.RenderScore();
                enemies.RenderEntities();
                playerShots.RenderEntities();
                enemyExplosions.RenderAnimations();
            }
        }
        private void QuickReactionForce()
        {
            greenB.CreateEnemies(greenBandits, typesOfEnemy[0]);
            redB.CreateEnemies(redBandits, typesOfEnemy[1]);
            blueB.CreateEnemies(blueBandits, typesOfEnemy[2]);

            bandidosSquadron.Add(greenB);
            bandidosSquadron.Add(redB);
            bandidosSquadron.Add(blueB);

            var enemyChooser = random.Next(1, 4);
            if ((bandidosSquadron.Count >= 0))
            {
                switch (enemyChooser)
                {
                    case 1:
                        movingNum = 1;
                        enemies = new EntityContainer<Enemy>(greenB.MaxEnemies);
                        foreach (Enemy enemy in greenB.Enemies)
                        {
                            enemies.AddEntity(enemy);
                        }
                        roundCount++;
                        break;
                    case 2:
                        movingNum = 2;
                        enemies = new EntityContainer<Enemy>(redB.MaxEnemies);
                        foreach (Enemy enemy in redB.Enemies)
                        {
                            enemies.AddEntity(enemy);
                        }
                        roundCount++;
                        break;
                    case 3:
                        movingNum = 3;
                        enemies = new EntityContainer<Enemy>(blueB.MaxEnemies);
                        foreach (Enemy enemy in blueB.Enemies)
                        {
                            enemies.AddEntity(enemy);
                        }
                        roundCount++;
                        break;
                }
            }

        }

        private void FormationAction()
        {
            switch (movingNum)
            {
                case 1:
                    zigzagDown.MoveEnemies(enemies);
                    break;
                case 2:
                    moveDown.MoveEnemies(enemies);
                    break;
                case 3:
                    moveDown.MoveEnemies(enemies);
                    break;
            }
        }
        private void NextRound()
        {
            if ((enemies.CountEntities() == 0))
            {
                QuickReactionForce();
            }
        }
        private void IsGameOver()
        {
            enemies.Iterate(enemy =>
            {
                if (enemy.EnemyWins())
                {
                    gameOver.gameIsOver = true;
                }
            });
        }

        public void KeyPress(string key)
        {
            switch (key)
            {
                case "KEY_ESCAPE":
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this,
                            "CHANGE_STATE", "GAME_PAUSED", ""));
                    break;
                case "KEY_LEFT":
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "KEY_LEFT", "KEY_PRESS", ""));
                    break;

                case "KEY_RIGHT":
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this,
                            "KEY_RIGHT", "KEY_PRESS", ""));
                    break;

                case "KEY_DOWN":
                    GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_DOWN", "KEY_PRESS", " "));
                    break;

                case "KEY_UP":
                    GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_UP", "KEY_PRESS", " "));
                    break;

                case "KEY_SPACE":
                    AddNewShot();
                    break;
            }
        }

        public void KeyRelease(string key)
        {
            switch (key)
            {
                case "KEY_LEFT":
                    GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_LEFT", "KEY_RELEASE", " "));
                    break;

                case "KEY_RIGHT":
                    GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                            "KEY_RIGHT", "KEY_RELEASE", " "));
                    break;

                case "KEY_UP":
                    GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_UP", "KEY_RELEASE", " "));
                    break;

                case "KEY_DOWN":
                    GalagaBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_DOWN", "KEY_RELEASE", " "));
                    break;

            }
        }

        public void HandleKeyEvent(string keyAction, string keyValue)
        {
            switch (keyValue)
            {
                case "KEY_PRESS":
                    KeyPress(keyAction);
                    break;
                
                case "KEY_RELEASE":
                    KeyRelease(keyAction);
                    break;
            }
        }
    }
}
