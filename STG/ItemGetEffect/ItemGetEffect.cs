using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class ItemGetEffect : asd.TextureObject2D
    {
        protected const int TextureSize = 100;

        protected const int TextureXCount = 4;

        protected const int TextureYCount = 5;

        protected int count;
        
        public ItemGetEffect(asd.Vector2DF pos)
            : base()
        {
            Position = pos;

            CenterPosition = new asd.Vector2DF(TextureSize / 2, TextureSize / 2);

            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/effect.png");

            Src = new asd.RectF(0, 0, TextureSize, TextureSize);

            AlphaBlend = asd.AlphaBlendMode.Add;

            DrawingPriority = -20;
        }

        protected override void OnUpdate()
        {
            int x = (count / 2) % TextureXCount;
            int y = (count / 2) / TextureYCount;

            Src = new asd.RectF(x * TextureSize, y * TextureSize, TextureSize, TextureSize);

            if(count == TextureXCount * TextureYCount * 2)
            {
                Dispose();
            }

            ++count;
        }

    }
}
