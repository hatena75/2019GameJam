using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class BreakObjectEffect : asd.TextureObject2D
    {
        const int TextureSize = 128;

        const int TextureXCount = 4;

        const int TextureYCount = 4;

        protected int count;
        
        public BreakObjectEffect(asd.Vector2DF pos)
            : base()
        {
            Position = pos;

            CenterPosition = new asd.Vector2DF(TextureSize / 2, TextureSize / 2);

            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/break.png");

            Src = new asd.RectF(0, 0, TextureSize, TextureSize);

            AlphaBlend = asd.AlphaBlendMode.Add;
        }

        protected override void OnUpdate()
        {
            int x = count % TextureXCount;
            int y = count / TextureYCount;

            Src = new asd.RectF(x * TextureSize, y * TextureSize, TextureSize, TextureSize);

            if(count == TextureXCount * TextureYCount)
            {
                Dispose();
            }

            ++count;
        }

    }
}
