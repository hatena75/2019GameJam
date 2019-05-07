using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asd;

namespace STG
{
    public class SpeedPlayer : Player
    {
        private const int moveSpeed = 6;
        private const int ReloadCount = 10;
        private const int BulletSpeed = 8;
        private const int BulletRange = 10;

        public SpeedPlayer(Keys right, Keys left, Keys up, Keys down, Keys attack) : base(right, left, up, down, attack)
        {
            
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}
