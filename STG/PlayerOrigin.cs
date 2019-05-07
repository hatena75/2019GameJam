using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd;

namespace STG
{
    public class PlayerOrigin : CollidableObject
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



        public PlayerOrigin(Keys right, Keys left, Keys up, Keys down, Keys attack)
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

            asd.Vector2DF position = Position;

            position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - Texture.Size.X / 2.0f, Texture.Size.X / 2.0f);
            position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - Texture.Size.Y / 2.0f, Texture.Size.Y / 2.0f);

            Position = position;

            Count++;
        }
    }
}
