using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Wall : CollidableObject
    {
        public static bool wall_dispose = false;

        public static bool wall_dispose_all = false;

        public static int wallcount = 0;

        public Wall(int x, int y)
        {
            /*
            var geometryObj = new asd.GeometryObject2D();

            asd.Engine.AddObject2D(geometryObj);

            var cube = new asd.RectangleShape();

            cube.DrawingArea = new asd.RectF(x * 32, y * 32, 32, 32);

            geometryObj.Shape = cube;
            */

            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Wall.png");

            Position = new asd.Vector2DF(x*32, y*32);

            //Radius = Texture.Size.X / 2.0f;

            wallcount++;
        }

        protected override void OnUpdate()
        {
            if (wall_dispose == true)
            {
                
                Dispose();
                wallcount--;

                if (wallcount == 0)
                {
                    wall_dispose_all = true;
                }
            }
        }
    }
}
