using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class GameScene : asd.Scene
    {
        Player player2;
        SpeedPlayer player;

        bool isSceneChanging = false;

        asd.Layer2D gameLayer;

        int count = 0;

        public static int stage_now = 0;

        public const int gridsizeX = 32;
        public const int gridsizeY = 32;
        const int Correct_posX = gridsizeX / 2;
        const int Correct_posY = gridsizeY / 2;


        //BGM
        asd.SoundSource bgm;

        //再生中のBGMを扱うためのID
        int? playingBgmId;

        //乱数を用意する
        static Random rnd = new Random();
        private int randomNumber1 = rnd.Next(0, 640);
        private int randomNumber2 = rnd.Next(0, 480);

        public GameScene()
        {
            stage_now = 0;
        }

        public GameScene(int index)
        {
            stage_now = index - 1;
        }

        protected override void OnRegistered()
        {
            gameLayer = new asd.Layer2D();

            asd.Layer2D backgroundLayer = new asd.Layer2D();

            backgroundLayer.DrawingPriority = -10;

            AddLayer(gameLayer);
            AddLayer(backgroundLayer);

            Background bg = new Background(new asd.Vector2DF(0.0f, 0.0f), "Resources/Bg.png");

            backgroundLayer.AddObject(bg);


            player = new SpeedPlayer(asd.Keys.Right, asd.Keys.Left, asd.Keys.Up, asd.Keys.Down, asd.Keys.Slash);
            player2 = new Player(asd.Keys.D, asd.Keys.A, asd.Keys.W, asd.Keys.S, asd.Keys.T);

            gameLayer.AddObject(player);
            gameLayer.AddObject(player2);


            //BGMを読み込む
            bgm = asd.Engine.Sound.CreateSoundSource("Resources/Bgm.ogg", false);

            //BGMループ
            bgm.IsLoopingMode = true;

            //IDはnull(BGMは流れてない）
            playingBgmId = null;
            
        }

        //ステージ作成用関数
        public static void StageCreate()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (Stage.stagelist[GameScene.stage_now][i, j] == 1)
                    {
                        asd.Engine.AddObject2D(new Wall(j, i));
                    }

                    
                }
            }
        }

        //キャラセット用関数
        public void CharacterCreate()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    
                    if (Stage.stagelist[GameScene.stage_now][i, j] == Stage.p)
                    {
                        player.Position = new asd.Vector2DF(j * gridsizeX +Correct_posX, i * gridsizeY +Correct_posY);
                    }

                    if (Stage.stagelist[GameScene.stage_now][i, j] == Stage.p2)
                    {
                        player2.Position = new asd.Vector2DF(j * gridsizeX + Correct_posX, i * gridsizeY + Correct_posY);
                    }

                    if (Stage.stagelist[GameScene.stage_now][i, j] == 2)
                    {

                        //asd.Engine.AddObject2D(new RushingEnemy(new asd.Vector2DF(j * gridsizeX + Correct_posX, i * gridsizeY + Correct_posY), player));
                    }

                    if (Stage.stagelist[GameScene.stage_now][i, j] == 3)
                    {

                        //asd.Engine.AddObject2D(new MovingEnemy(new asd.Vector2DF(j * gridsizeX + Correct_posX, i * gridsizeY + Correct_posY), player));
                    }

                    
                }
            }
        }

        protected override void OnUpdated()
        {
            if (!isSceneChanging && (!player.IsAlive || !player2.IsAlive) )
            {
                int WinPlayer = 0;

                if (player.IsAlive)
                {
                    WinPlayer = 1;
                }
                else if (player2.IsAlive)
                {
                    WinPlayer = 2;
                }

                asd.Engine.ChangeSceneWithTransition(new GameOverScene(WinPlayer), new asd.TransitionFade(1.0f, 1.0f));

                if (playingBgmId.HasValue)
                {
                    asd.Engine.Sound.FadeOut(playingBgmId.Value, 1.0f);
                    playingBgmId = null;
                }

                isSceneChanging = true;
            }


            

            //asd.Vector2DF moveVelocity = new asd.Vector2DF(1.0f, 0.0f);

            if (count == 10)
            {
                playingBgmId = asd.Engine.Sound.Play(bgm);

                //ステージ描画(初期化)
                StageCreate();
                CharacterCreate();
            }
                

            
            //次のステージへ行く処理
            if (Wall.wall_dispose_all == true)
            {
                GameScene.stage_now++;
                GameScene.StageCreate();
                CharacterCreate();
                Wall.wall_dispose = false;
                Wall.wall_dispose_all = false;
            }

            count++;
        }
    }
}
