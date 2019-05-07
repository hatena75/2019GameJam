using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd;

namespace STG
{
    public class Enemy : CollidableObject
    {
        static Random rnd = new Random();
        protected int randomNumber = rnd.Next(1, 700);
        public static int enemycount = 0;
        const int Correct_sizeX = GameScene.gridsizeX / 2 - 1; //あたり判定座標修正
        const int Correct_sizeY = GameScene.gridsizeY / 2 - 1;
        const int moveSpeed = 4;

        //移動できるか否かの判定のため座標を取得
        protected int IsAbleToGo2(float x, float y)
        {
            return Stage.stagelist[GameScene.stage_now][(int)y / 32, (int)x / 32];
        }

        int IsAbleToGo3(asd.Vector2DF pos)
        {
            return Stage.stagelist[GameScene.stage_now][(int)(pos.Y / 32), (int)(pos.X / 32)];
        }

        protected void CollideWith(CollidableObject obj)
        {
            if (obj == null)
                return;

            if(obj is Bullet)
            {
                CollidableObject bullet = obj;
                
                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    bullet.OnCollide(this);
                }
            }

            if (obj is PenetrateBullet)
            {
                CollidableObject bullet = obj;

                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    //bullet.OnCollide(this);
                }
            }
        }


        protected int count;

        protected NormalPlayer player; //GameScineでnewされたプレイヤー情報を格納するための変数

        //破壊効果音
        protected asd.SoundSource deathSound;

        //再生中のBGMを扱うためのID
        protected int deathseID;

        public override void OnCollide(CollidableObject obj)
        {
            asd.Engine.AddObject2D(new BreakObjectEffect(Position));
            Singleton.Getsingleton();
            //Singleton.singleton.score += 10;
            deathseID = asd.Engine.Sound.Play(deathSound);
            randomNumber = rnd.Next(1, 700);
           
            Dispose();
        }
        
        protected bool MoveRight()
        {
            if (IsAbleToGo3(Position + new asd.Vector2DF(+moveSpeed + Correct_sizeX, 0 + Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(+moveSpeed + Correct_sizeX, -Correct_sizeY)) != 1)
            {
                Position += new asd.Vector2DF(+moveSpeed, 0);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool MoveLeft()
        {
            if (IsAbleToGo3(Position + new asd.Vector2DF(-moveSpeed - Correct_sizeX, 0 + Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(-moveSpeed - Correct_sizeX, -Correct_sizeY)) != 1)
            {
                Position += new asd.Vector2DF(-moveSpeed, 0);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool MoveUp()
        {
            if (IsAbleToGo3(Position + new asd.Vector2DF(0 - Correct_sizeX, -moveSpeed - Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(+Correct_sizeX, -moveSpeed - Correct_sizeY)) != 1)
            {
                Position += new asd.Vector2DF(0, -moveSpeed);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool MoveDown()
        {
            if (IsAbleToGo3(Position + new asd.Vector2DF(0 - Correct_sizeX, +moveSpeed + Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(+Correct_sizeX, +moveSpeed + Correct_sizeY)) != 1)
            {
                Position += new asd.Vector2DF(0, +moveSpeed);
                return true;
            }
            else
            {
                return false;
            }
        }

        //コンストラクタ
        /*
        protected Enemy(asd.Vector2DF pos,asd.Vector2DF movevector,Player player)
            : base()
        {
            Position = pos;

            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player2.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            count = 0;

            this.player = player;　//GameScineでnewされた情報を格納

        }
        */
        public Enemy(Vector2DF pos, NormalPlayer player)
        {
            Position = pos;
            this.player = player;

            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player2.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            Radius = Texture.Size.X / 2.0f;

            //破壊時の効果音を読み込む
            deathSound = asd.Engine.Sound.CreateSoundSource("Resources/Explode.wav", true);

            enemycount++;
        }

        protected override void OnUpdate()
        {
            foreach (var obj in Layer.Objects)
                CollideWith((obj as CollidableObject));
            /*
            //ジャンプ可能か（地面についている状態か）判定
            if (IsAbleToGo2(Position.X + 5, Position.Y + 32) == 1 || IsAbleToGo2(Position.X + 26, Position.Y + 32) == 1)
                ground = true;
            else
                ground = false;


            //右壁衝突判定
            if (IsAbleToGo2(Position.X + 31, Position.Y + 5) == 1 || IsAbleToGo2(Position.X + 31, Position.Y + 26) == 1)
                Position = new asd.Vector2DF(((int)Position.X / 32) * 32, Position.Y);

            //左壁衝突判定
            if (IsAbleToGo2(Position.X, Position.Y + 5) == 1 || IsAbleToGo2(Position.X, Position.Y + 26) == 1)
                Position = new asd.Vector2DF(((int)Position.X / 32) * 32 + 32, Position.Y);

            //床衝突判定（Y座標がずらされてしまうため、空中にいるときは判定しない）
            if ((IsAbleToGo2(Position.X, Position.Y + 31) == 1 || IsAbleToGo2(Position.X + 31, Position.Y + 31) == 1) && ground == true)
                Position = new asd.Vector2DF(Position.X, ((int)Position.Y / 32) * 32);
            
            //天井衝突判定
            if (IsAbleToGo2(Position.X, Position.Y) == 1 || IsAbleToGo2(Position.X + 31, Position.Y) == 1)
                Position = new asd.Vector2DF(Position.X, ((int)Position.Y / 32) * 32 + 32);
            

            //重力
            if (ground == false)
            {
                Position = Position + new asd.Vector2DF(0, +5);
            }
            */

            

            //破壊音量を下げる。
            asd.Engine.Sound.SetVolume(deathseID, 0.3f);

            if (Wall.wall_dispose == true && enemycount > 0)
            {

                Dispose();
                enemycount--;

            }

            ++count;
        }

        protected void DisposeFromGame()
        {
            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {    
                Dispose();
            }

            
        }
    }
}
