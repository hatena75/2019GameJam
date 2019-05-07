using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class Background : asd.TextureObject2D
    {
        public Background(asd.Vector2DF pos, string texturePath)
             : base()
        {
            // 初期位置を設定する。
            Position = pos;

            // texturePath で指定したパスにある画像を読み込んで Texture に変換します。
            Texture = asd.Engine.Graphics.CreateTexture2D(texturePath);
        }
    }
}