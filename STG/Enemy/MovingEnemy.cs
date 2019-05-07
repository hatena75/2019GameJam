using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class MovingEnemy : Enemy
    {
        private asd.Vector2DF moveVelocity;

        public MovingEnemy(asd.Vector2DF pos,NormalPlayer player)
            : base(pos, player)
        {
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            //moveVelocity = new asd.Vector2DF();
            moveVelocity = new asd.Vector2DF(-1.0f, 0.0f);
        }

        protected override void OnUpdate()
        {

            Position += moveVelocity.Normal;


            //右壁衝突判定
            if (IsAbleToGo2(Position.X + 31, Position.Y + 5) == 1 || IsAbleToGo2(Position.X + 31, Position.Y + 26) == 1)
                moveVelocity = -moveVelocity;

            //左壁衝突判定
            if (IsAbleToGo2(Position.X, Position.Y + 5) == 1 || IsAbleToGo2(Position.X, Position.Y + 26) == 1)
                moveVelocity = -moveVelocity;


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
                
                
        }


    }
}
