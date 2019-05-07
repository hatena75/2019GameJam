using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG.Item
{
    class TwoShotItem : Item
    {

        //private asd.SoundSource itemGet;

        public TwoShotItem(asd.Vector2DF pos) : base(pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Item4.png");            
            /*
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            Radius = Texture.Size.X / 2.0f;

            Position = pos;

            itemGet = asd.Engine.Sound.CreateSoundSource("Resources/Itemget.wav", true);
            */
        }

        public override void OnCollide(CollidableObject obj)
        {
            base.OnCollide(obj);
            //Singleton.Getsingleton();
            Singleton.singleton.itemhaving = 3;
            Singleton.singleton.itemcount = 0;
            //asd.Engine.Sound.Play(itemGet);

            //Dispose();
        }
        /*
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
        }
        */

        private void CollideWith(CollidableObject obj)
        {
            if (obj == null)
                return;

            if (obj is Bullet)
            {
                CollidableObject bullet = obj;

                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    bullet.OnCollide(this);
                    asd.Engine.AddObject2D(new ItemGetEffect_TwoShot(new asd.Vector2DF(320, 240)));

                }
            }

            if (obj is PenetrateBullet)
            {
                CollidableObject bullet = obj;

                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    //bullet.OnCollide(this);
                    asd.Engine.AddObject2D(new ItemGetEffect_TwoShot(new asd.Vector2DF(320, 240)));

                }
            }
        }

        protected override void OnUpdate()
        {
            foreach (var obj in Layer.Objects)
                CollideWith((obj as CollidableObject));
        }

    }
}
