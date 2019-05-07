using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd;

namespace STG
{
    public class Player : PlayerOrigin
    {
        //プレイヤーごとの固有値
        private const int moveSpeed = 4;
        private const int ReloadCount = 20;
        private const int BulletSpeed = 5;
        private const int BulletRange = 30;

        public Player(Keys right, Keys left, Keys up, Keys down, Keys attack) : base(right, left, up, down, attack)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player2.png");

        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            //プレイヤー移動(4方向)
            if (asd.Engine.Keyboard.GetKeyState(right) == asd.ButtonState.Hold)
            {
                PlayerWay = PlayerWays.Right;
                if (IsAbleToGo3(Position + new asd.Vector2DF(+moveSpeed + Correct_sizeX, 0 + Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(+moveSpeed + Correct_sizeX, -Correct_sizeY)) != 1)
                {
                    Position += new asd.Vector2DF(+moveSpeed, 0);
                }

            }

            if (asd.Engine.Keyboard.GetKeyState(left) == asd.ButtonState.Hold)
            {
                PlayerWay = PlayerWays.Left;
                if (IsAbleToGo3(Position + new asd.Vector2DF(-moveSpeed - Correct_sizeX, 0 + Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(-moveSpeed - Correct_sizeX, -Correct_sizeY)) != 1)
                {
                    Position += new asd.Vector2DF(-moveSpeed, 0);
                }
            }

            if (asd.Engine.Keyboard.GetKeyState(up) == asd.ButtonState.Hold)
            {
                PlayerWay = PlayerWays.Up;
                if (IsAbleToGo3(Position + new asd.Vector2DF(0 - Correct_sizeX, -moveSpeed - Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(+Correct_sizeX, -moveSpeed - Correct_sizeY)) != 1)
                {
                    Position += new asd.Vector2DF(0, -moveSpeed);
                }
            }

            if (asd.Engine.Keyboard.GetKeyState(down) == asd.ButtonState.Hold)
            {
                PlayerWay = PlayerWays.Down;
                if (IsAbleToGo3(Position + new asd.Vector2DF(0 - Correct_sizeX, +moveSpeed + Correct_sizeY)) != 1 && IsAbleToGo3(Position + new asd.Vector2DF(+Correct_sizeX, +moveSpeed + Correct_sizeY)) != 1)
                {
                    Position += new asd.Vector2DF(0, +moveSpeed);
                }
            }

            //自機の向きに弾を発射
            if (asd.Engine.Keyboard.GetKeyState(attack) == asd.ButtonState.Push && isReload == false)
            {
                asd.Vector2DF BulletWay;

                BulletWay = PlayerWay switch
                {
                    PlayerWays.Right => new asd.Vector2DF(1, 0),
                    PlayerWays.Left => new asd.Vector2DF(-1, 0),
                    PlayerWays.Up => new asd.Vector2DF(0, -1),
                    PlayerWays.Down => new asd.Vector2DF(0, 1),
                    _ => throw new InvalidOperationException()
                };

                asd.Engine.AddObject2D(new Bullet(Position + BulletWay * 20, BulletWay * BulletSpeed, BulletRange));
                isReload = true;
                ReloadTriggar = Count;
            }


            if(ReloadTriggar + ReloadCount == Count)
            {
                isReload = false;
            }
        }
    }
}
