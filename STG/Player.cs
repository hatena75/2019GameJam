using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd;

namespace STG
{
    public class Player : CollidableObject
    {
        protected const int Correct_sizeX = GameScene.gridsizeX / 2 - 1; //あたり判定座標修正
        protected const int Correct_sizeY = GameScene.gridsizeY / 2 - 1;       
        protected Keys right, left, up, down, attack;
        protected bool isReload = false;
        protected int ReloadTriggar = 0;
        protected PlayerWays PlayerWay = PlayerWays.Up;

        //プレイヤーごとの固有値
        private const int moveSpeed = 4;
        private const int ReloadCount = 20;
        private const int BulletSpeed = 5;
        private const int BulletRange = 30;

        public int Count { get; set; } = 0;

        public SoundSource ShotSound { get; }

        public SoundSource BombSound { get; }

        protected enum PlayerWays
        {
            Right, Left, Up, Down
        }

        //移動できるか否かの判定のため座標を取得
        protected int IsAbleToGo3(asd.Vector2DF pos)
        {
            return Stage.stagelist[GameScene.stage_now][(int)(pos.Y/ 32), (int)(pos.X / 32)];
        }



        public override void OnCollide(CollidableObject obj)
        {
            Layer.AddObject(new BreakObjectEffect(Position));
            Dispose();
        }

        protected void CollideWith(CollidableObject obj)
        {
            if (obj == null)
                return;
            
            if (obj is Enemy || obj is Bullet)
            {
                CollidableObject enemyBullet = obj;

                if (IsCollide(enemyBullet))
                {
                    OnCollide(enemyBullet);
                    enemyBullet.OnCollide(this);
                }
            }

        }



        public Player(Keys right, Keys left, Keys up, Keys down, Keys attack)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player2.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f); //中心に判定を設定

            Radius = Texture.Size.X / 3.0f;

            //ショットの効果音を読み込む
            ShotSound = asd.Engine.Sound.CreateSoundSource("Resources/Shot.wav", true);

            //ボム発動の効果音を読み込む
            BombSound = asd.Engine.Sound.CreateSoundSource("Resources/Bomb.wav", true);

            this.right = right;
            this.left = left;
            this.up = up;
            this.down = down;
            this.attack = attack;

        }

        protected override void OnUpdate()
        {
            //敵との当たり判定(STG準拠)
            foreach (var obj in Layer.Objects)
                CollideWith(obj as CollidableObject);
           
            //マップ遷移
            if (Stage.stagelist[GameScene.stage_now][(int)Position.Y / 32 , (int)Position.X / 32] == Stage.n)
            {
                Wall.wall_dispose = true;

            }

            
            //プレイヤー移動(4方向)
            if (asd.Engine.Keyboard.GetKeyState(right) == asd.ButtonState.Hold)
            {
                PlayerWay = PlayerWays.Right;
                if(IsAbleToGo3(Position + new asd.Vector2DF(+moveSpeed +Correct_sizeX, 0+Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(+moveSpeed +Correct_sizeX, -Correct_sizeY)) != 1)
                {
                    Position += new asd.Vector2DF(+moveSpeed, 0);
                }
              
            }
            
            if (asd.Engine.Keyboard.GetKeyState(left) == asd.ButtonState.Hold)
            {
                PlayerWay = PlayerWays.Left;
                if (IsAbleToGo3(Position + new asd.Vector2DF(-moveSpeed -Correct_sizeX, 0+Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(-moveSpeed -Correct_sizeX, -Correct_sizeY)) != 1)
                {
                    Position += new asd.Vector2DF(-moveSpeed, 0);
                }
            }
            
            if (asd.Engine.Keyboard.GetKeyState(up) == asd.ButtonState.Hold)
            {
                PlayerWay = PlayerWays.Up;
                if (IsAbleToGo3(Position + new asd.Vector2DF(0-Correct_sizeX, -moveSpeed-Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(+Correct_sizeX, -moveSpeed-Correct_sizeY)) != 1)
                {
                    Position += new asd.Vector2DF(0, -moveSpeed);
                }
            }
            
            if (asd.Engine.Keyboard.GetKeyState(down) == asd.ButtonState.Hold)
            {
                PlayerWay = PlayerWays.Down;
                if (IsAbleToGo3(Position + new asd.Vector2DF(0-Correct_sizeX, +moveSpeed +Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(+Correct_sizeX, +moveSpeed +Correct_sizeY)) != 1)
                {
                    Position += new asd.Vector2DF(0, +moveSpeed);
                }
            }

            //自機の向きに弾を発射
            if (asd.Engine.Keyboard.GetKeyState(attack) == asd.ButtonState.Push && isReload == false)
            {
                asd.Vector2DF BulletWay;

                BulletWay = PlayerWay switch
                {
                    PlayerWays.Right => new asd.Vector2DF(1, 0),
                    PlayerWays.Left => new asd.Vector2DF(-1, 0),
                    PlayerWays.Up => new asd.Vector2DF(0, -1),
                    PlayerWays.Down => new asd.Vector2DF(0, 1),
                    _ => throw new InvalidOperationException()
                };

                asd.Engine.AddObject2D(new Bullet(Position + BulletWay * 20, BulletWay * BulletSpeed, BulletRange));
                isReload = true;
                ReloadTriggar = Count;
            }

            asd.Vector2DF position = Position;

            position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - Texture.Size.X / 2.0f, Texture.Size.X / 2.0f);
            position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - Texture.Size.Y / 2.0f, Texture.Size.Y / 2.0f);

            Position = position;

            if(ReloadTriggar + ReloadCount == Count)
            {
                isReload = false;
            }

            Count++;
        }
    }
}
