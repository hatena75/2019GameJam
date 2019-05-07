using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class RushingEnemy : Enemy
    {
        private asd.Vector2DF moveVelocity;

        public RushingEnemy(asd.Vector2DF pos,Player player)
            : base(pos, player)
        {
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = new asd.Vector2DF();
        }

        protected override void OnUpdate()
        {
            if (player.Position.X != Position.X)
            {
                moveVelocity = (player.Position - Position);

                Position += new asd.Vector2DF(moveVelocity.X, 0.0f).Normal;
            }

            base.OnUpdate();
        }

        public override void OnCollide(CollidableObject obj)
        {
            
                base.OnCollide(obj);
                //asd.Engine.AddObject2D(new BreakObjectEffect(Position));
                //Singleton.Getsingleton();
                Singleton.singleton.score += 10;
                //deathseID = asd.Engine.Sound.Play(deathSound);

                //Dispose();

                
                if (randomNumber >= 1 & randomNumber <= 4)
                {
                    asd.Engine.AddObject2D(new Item.PenetrateShotItem(Position));
                }
                else
                if (randomNumber >= 11 & randomNumber <= 13)
                {
                    asd.Engine.AddObject2D(new Item.ThereeShotItem(Position));
                }
                else
                if (randomNumber >= 21 & randomNumber <= 24)
                {
                    asd.Engine.AddObject2D(new Item.TwoShotItem(Position));
                }
                else
                if (randomNumber >= 31 & randomNumber <= 33)
                {
                    asd.Engine.AddObject2D(new Item.TriShotItem(Position));

                }
                
        }


    }
}
