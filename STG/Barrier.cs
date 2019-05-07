using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Barrier : CollidableObject
    {
        int count;

        public Barrier(asd.Vector2DF position)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Barrier.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            Position = position;

            Radius = Texture.Size.X / 2.0f;

            count = 0;
        }

        protected override void OnUpdate()
        {
            foreach (var item in Layer.Objects)
                CollideWith(item as CollidableObject);

            if (count >= 300)
            {
                Dispose();
            }

            count++;
        }

        private void CollideWith(CollidableObject obj)
        {
            if (obj == null || !obj.IsAlive)
            {
                return;
            }

            if (obj is Enemy)
            {
                if (IsCollide(obj))
                {
                    obj.OnCollide(this);
                }
            }

        }

    }
}
