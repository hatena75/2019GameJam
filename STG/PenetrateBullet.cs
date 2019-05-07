using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class PenetrateBullet : CollidableObject
    {
        private asd.Vector2DF velp;

        public override void OnCollide(CollidableObject obj)
        {
            //Dispose();
        }


        public PenetrateBullet(asd.Vector2DF position, asd.Vector2DF movevelocityplayer)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/PenetratePlayerBullet.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            DrawingPriority = -10;

            Position = position;

            Radius = Texture.Size.X / 2.0f;

            velp = movevelocityplayer;
        }

        protected override void OnUpdate()
        {
            Position += velp;
            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {
                Dispose();
            }
        }
    }
}
