using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Galaga;
using Galaga.MovementStrategy;
using Galaga.Squadrons;

namespace GalagaStates
{
    public class GameRunning //: IGameState
    {
        /*
        private static GameRunning instance;
        private IBaseImage playerShotImage;
        private const int EXPLOSION_LENGTH_MS = 500;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private EntityContainer playerShots;
        private MoveZigzagDown zigzagDown;
        private List<List<Image>> typesOfEnemy;
        private List<List<Image>> alternativeEnemyStrides;
        private List<Image> blueBandits;
        private List<Image> redBandits;
        private List<Image> greenBandits;
        private List<ISquadron> bandidosSquadron;
        private Player player;
        private Image backgroundImage;

        public static GameRunning GetInstance()
        {
            return GameRunning.instance ??
                (GameRunning.instance = new GameRunning());
        }


        public GameRunning()
        {
            backgroundImage = new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"));
            player = new Player(new DynamicShape(
                       new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                       new Image(Path.Combine("Assets", "Images", "Player.png")));

            bandidosSquadron = new List<ISquadron>() {new GreenSquadron(),
                                 new RedSquadron(), new BlueSquadron()};

            greenBandits = ImageStride.CreateStrides(3,
                Path.Combine("Assets", "Images", "GreenMonster.png"));

            redBandits = ImageStride.CreateStrides(2,
                Path.Combine("Assets", "Images", "RedMonster.png"));

            blueBandits = ImageStride.CreateStrides(3,
                Path.Combine("Assets", "Images", "BlueMonster.png"));


            typesOfEnemy = new List<List<Image>>() { greenBandits, redBandits, blueBandits };
            zigzagDown = new MoveZigzagDown();
            alternativeEnemyStrides = new List<List<Image>>() { };

            explosionStrides = ImageStride.CreateStrides(8,
                           Path.Combine("Assets", "Images", "Explosion.png"));


            enemyExplosions = new AnimationContainer(60);
            playerShots = new EntityContainer();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            for (int i = 0; i < bandidosSquadron.Count - 1; i++)
            {
                //! is not working
                //bandidosSquadron[i].CreateEnemies(typesOfEnemy[i], alternativeEnemyStrides[i]);
            }
        }


        public void AddExplosion(Vec2F position, Vec2F extent)
        {
            // TODO: add explosion to the AnimationContainer
            enemyExplosions.AddAnimation(
                new StationaryShape(position, extent), EXPLOSION_LENGTH_MS,
                new ImageStride(EXPLOSION_LENGTH_MS / 8, explosionStrides)
            );
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
                    ((DynamicShape)shot.Shape).Direction.Y += 0.02f;//! maybe just '='

                    //TODO: guard against window borders 
                    if (shot.Shape.Position.Y > 1.0f)
                    {
                        // TODO: delete shot
                        shot.DeleteEntity();
                    }
                    else
                    {
                        for (int i = 0; i < bandidosSquadron.Count; i++)
                        {
                            bandidosSquadron[i].Enemies.Iterate(enemy =>
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
        }

        public void UpdateGameLogic()
        {
            IterateShots();
            for (int i = 0; i < bandidosSquadron.Count - 1; i++)
            {
                zigzagDown.MoveEnemies(bandidosSquadron[i].Enemies);
            }
            player.Move();
        }
        public void RenderState()
        {
            backgroundImage.Render(new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f));
            player.Render();
            for (int i = 0; i < bandidosSquadron.Count - 1; i++)
            {
                bandidosSquadron[i].Enemies.RenderEntities();
            }
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
        }



        public void HandleKeyEvent(string keyValue, string keyAction)
        {
            switch (keyAction)
            {
                case "KEY_PRESS":
                    switch (keyValue)
                    {
                        case "KEY_ESCAPE":
                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.GameStateEvent, this,
                                    "CHANGE_STATE", "GAME_PAUSED", ""));
                            //window.CloseWindow();
                            break;

                        case "KEY_LEFT":
                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.PlayerEvent, this,
                                    "PLAYER_LEFT", "KEY_PRESS", ""));
                            break;

                        case "KEY_RIGHT":
                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.PlayerEvent, this,
                                    "PLAYER_RIGHT", "KEY_PRESS", ""));
                            break;

                        case "KEY_DOWN":
                            //player.SetMoveDown(false);
                            GalagaBus.GetBus().RegisterEvent(
                                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                                        GameEventType.PlayerEvent, this,
                                            "KEY_DOWN", "KEY_PRESS", " "));
                            break;

                        case "KEY_UP":
                            //player.SetMoveUp(false);
                            GalagaBus.GetBus().RegisterEvent(
                                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                                        GameEventType.PlayerEvent, this,
                                            "KEY_UP", "KEY_PRESS", " "));
                            break;

                        case "KEY_SPACE":
                            AddNewShot();
                            break;
                    }
                    break;


                case "KEY_RELEASE":
                    switch (keyValue)
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
                            //player.SetMoveUp(false);
                            GalagaBus.GetBus().RegisterEvent(
                                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                                        GameEventType.PlayerEvent, this,
                                            "KEY_UP", "KEY_RELEASE", " "));
                            break;

                        case "KEY_DOWN":
                            //player.SetMoveDown(false);
                            GalagaBus.GetBus().RegisterEvent(
                                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                                        GameEventType.PlayerEvent, this,
                                            "KEY_DOWN", "KEY_RELEASE", " "));
                            break;

                    }
                    break;



            }
        }
        */
    }
}
