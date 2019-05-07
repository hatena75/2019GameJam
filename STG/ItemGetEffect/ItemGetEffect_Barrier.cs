using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class ItemGetEffect_Barrier : ItemGetEffect
    {

        public ItemGetEffect_Barrier(asd.Vector2DF pos) : base(pos)
        {
          
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/effect4.png");

            Src = new asd.RectF(0, 0, TextureSize, TextureSize);
        
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

    }
}
