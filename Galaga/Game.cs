using System;
using OpenTK;
using OpenTK.Graphics;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.Collections.Generic;
using DIKUArcade.EventBus;
using DIKUArcade.Physics;
using Galaga;
using Galaga.MovementStrategy;
using Galaga.Squadrons;
using GalagaStates;
using OpenTK.Windowing.Desktop;

namespace Galaga
{
    public class Game : IGameEventProcessor<object>
    {
        private Player player;
        private List<ISquadron> bandidosSquadron;
        private List<Image> greenBandits;
        private List<Image> redBandits;
        private List<Image> blueBandits;
        private List<List<Image>> typesOfEnemy;
        private List<List<Image>> alternativeEnemyStrides;
        private Score score;
        private Text display;
        private Window window;
        private MoveZigzagDown zigzagDown;
        private GameTimer gameTimer;
        private GameEventBus<object> eventBus;
        private EntityContainer<Enemy> enemies;
        private EntityContainer playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;
        private List<Image> enemyStridesRed;
        private StateMachine stateMachine;
        public Game()
        {
            stateMachine = new StateMachine();
            window = new Window("Galaga", 500, 500);
            gameTimer = new GameTimer(30, 30);
            //TODO mute from to here
            player = new Player(new DynamicShape(
                        new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                        new Image(Path.Combine("Assets", "Images", "Player.png")));
            //TODO mute to here

            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                 GameEventType.InputEvent,
                 GameEventType.PlayerEvent,
                 GameEventType.WindowEvent});

            window.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, player);



            //TODO mute from here
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;
            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++)
            {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f),
                    new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, images)));
            }
            zigzagDown = new MoveZigzagDown();



            playerShots = new EntityContainer();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
            enemyExplosions = new AnimationContainer(numEnemies);
            //TODO mute to here

            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));

            enemyStridesRed = ImageStride.CreateStrides(2,
                    Path.Combine("Assets", "Images", "RedMonster.png"));


            score = new Score(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f));
            display = new Text("SCORE POINTS", new Vec2F(0.3f, 0.5f), new Vec2F(0.2f, 0.3f));

            //TODO mute from here

            bandidosSquadron = new List<ISquadron>() {new GreenSquadron(),
                                 new RedSquadron(), new BlueSquadron()};

            greenBandits = ImageStride.CreateStrides(3,
                Path.Combine("Assets", "Images", "GreenMonster.png"));

            redBandits = ImageStride.CreateStrides(2,
                Path.Combine("Assets", "Images", "RedMonster.png"));

            blueBandits = ImageStride.CreateStrides(3,
                Path.Combine("Assets", "Images", "BlueMonster.png"));

            typesOfEnemy = new List<List<Image>>() { greenBandits, redBandits, blueBandits };
            alternativeEnemyStrides = new List<List<Image>>() { };

            //TODO mute to here

            for (int i = 0; i < bandidosSquadron.Count - 1; i++)
            {
                //! is not working
                bandidosSquadron[i].CreateEnemies(typesOfEnemy[i], alternativeEnemyStrides[i]);
            }



        }

        public void Run()
        {
            while (window.IsRunning())
            {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate())
                {

                    window.PollEvents();
                    // update game logic here...
                    //GalagaBus.GetBus().ProcessEvents(); //! when GalagaBus.cs is on, then umute
                    eventBus.ProcessEvents();  //! mute when galaga.cs takes over
                                               //stateMachine.ActiveState.UpdateGameLogic();//! when gamerunning.cs is on, then umute

                    //TODO mute from here
                    IterateShots();
                    player.Move();
                    //TODO mute to here
                    zigzagDown.MoveEnemies(enemies);    //!WORKS FINE
                    for (int i = 0; i < bandidosSquadron.Count; i++)
                    {
                        zigzagDown.MoveEnemies(bandidosSquadron[i].Enemies);
                    }
                }

                if (gameTimer.ShouldRender())
                {
                    window.Clear();
                    //stateMachine.ActiveState.RenderState(); //! when gamerunning.cs is on, then umute
                    //score.RenderScore();  //TODO mute
                    //display.RenderText(); //TODO mute
                    player.Render();
                    enemies.RenderEntities();

                    playerShots.RenderEntities();
                    enemyExplosions.RenderAnimations();
                    for (int i = 0; i < bandidosSquadron.Count; i++)
                    {
                        bandidosSquadron[i].Enemies.RenderEntities();
                    }

                    // render game entities here... 
                    window.SwapBuffers();
                }

                if (gameTimer.ShouldReset())
                {
                    // this update happens once every second
                    window.Title = $"Galaga | (UPS,FPS): ({gameTimer.CapturedUpdates},{gameTimer.CapturedFrames})";
                }
            }
        }

        public void KeyPress(string key)
        {
            // TODO mute from here
            //switch on key string and set the player's move direction
            switch (key)
            {
                case "KEY_LEFT":
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_LEFT", "KEY_PRESS", " "));
                    //player.SetMoveLeft(true);
                    break;
                case "KEY_RIGHT":
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_RIGHT", "KEY_PRESS", " "));
                    //player.SetMoveRight(true);
                    break;
                case "KEY_UP":
                    eventBus.RegisterEvent(
                           GameEventFactory<object>.CreateGameEventForAllProcessors(
                               GameEventType.PlayerEvent, this,
                                   "KEY_UP", "KEY_PRESS", " "));
                    //player.SetMoveUp(true);
                    break;
                case "KEY_DOWN":
                    eventBus.RegisterEvent(
                           GameEventFactory<object>.CreateGameEventForAllProcessors(
                               GameEventType.PlayerEvent, this,
                                   "KEY_DOWN", "KEY_PRESS", " "));
                    //player.SetMoveDown(true);
                    break;
                case "KEY_SPACE":
                    IterateShots();
                    break;

                default:
                    break;
            }
            //TODO mute to here
        }


        public void KeyRelease(string key)
        {
            // TODO mute from here
            // switch on key string and disable the player's move direction
            // TODO: Close window if escape is pressed
            switch (key)
            {
                case "KEY_LEFT":
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_LEFT", "KEY_RELEASE", " "));
                    //player.SetMoveLeft(false);
                    break;
                case "KEY_RIGHT":
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                            "KEY_RIGHT", "KEY_RELEASE", " "));
                    //player.SetMoveRight(false);
                    break;
                case "KEY_UP":
                    //player.SetMoveUp(false);
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_UP", "KEY_RELEASE", " "));
                    break;
                case "KEY_DOWN":
                    //player.SetMoveDown(false);
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_DOWN", "KEY_RELEASE", " "));
                    break;
                case "KEY_ESCAPE":
                    eventBus.RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.PlayerEvent, this,
                                    "KEY_ESCAPE", "KEY_RELEASE", " "));
                    window.CloseWindow();
                    break;

                case "KEY_SPACE":
                    AddNewShot();
                    break;

                default:
                    break;
            }

            //TODO mute to here
        }

        public void ProcessEvent(GameEventType type, GameEvent<object> gameEvent)
        {
            switch (gameEvent.Parameter1)
            {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                default:
                    break;

            }
        }
        public void AddNewShot()
        {
            var shot = new DynamicShape(new Vec2F(player.getPos().X,
                 player.getPos().Y), //+ 0.008f, player.Shape.Y + 0.01f),
                    new Vec2F(0.008f, 0.021f));

            playerShots.AddDynamicEntity(shot, playerShotImage);

        }

        private void IterateShots()
        {
            playerShots.Iterate(shot =>
            {
                // TODO: move the shot's shape
                {
                    shot.Shape.Move();
                    ((DynamicShape)shot.Shape).Direction.Y += 0.02f;

                    // guard against window borders 
                    if (shot.Shape.Position.Y > 1.0f)
                    {
                        // TODO: delete shot
                        shot.DeleteEntity();
                    }
                    else
                    {
                        //TODO below should be adden once ISquardonis on!
                        //for (int i = 0; i < bandidosSquadron.Count; i++)
                        // {
                        //bandidosSquadron[i].
                        enemies.Iterate(enemy =>
                        {
                            // TODO: if collision btw shot and enemy -> delete both
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

                                }
                            }

                        });
                        // }


                    }
                }
            });

        }


        public void AddExplosion(Vec2F position, Vec2F extent)
        {
            // TODO: add explosion to the AnimationContainer
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent), EXPLOSION_LENGTH_MS,
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides)
            );

        }
        public void IncreaseDifficulty()
        {

        }


    }
}

