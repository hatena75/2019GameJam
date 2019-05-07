using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Bullet : CollidableObject
    {
        private asd.Vector2DF velp;
        int count = 0;
        int RangeCount = 0;

        public override void OnCollide(CollidableObject obj)
        {
            Dispose();
        }
        int IsAbleToGo3(asd.Vector2DF pos)
        {
            return Stage.stagelist[GameScene.stage_now][(int)(pos.Y / 32), (int)(pos.X / 32)];
        }


        public Bullet(asd.Vector2DF position, asd.Vector2DF movevelocityplayer, int rangecount)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/PlayerBullet.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            DrawingPriority = -10;

            Position = position;

            Radius = Texture.Size.X / 2.0f;

            velp = movevelocityplayer;

            RangeCount = rangecount;
        }

        protected override void OnUpdate()
        {
            Position += velp;

            if (IsAbleToGo3(Position) == 1 || RangeCount == count)
            {
                Dispose();
            }

            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {
                Dispose();
            }

            count++;
        }
    }
}
