namespace STG.Item
{
    class Item : CollidableObject
    {

        protected asd.SoundSource itemGet;

        public Item(asd.Vector2DF pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player.png"); //これは仮で、サブクラスでオーバーライドする。

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            Radius = Texture.Size.X / 2.0f;

            Position = pos;

            itemGet = asd.Engine.Sound.CreateSoundSource("Resources/Itemget.wav", true);

            
        }

        public override void OnCollide(CollidableObject obj)
        {
            Singleton.Getsingleton();
            //Singleton.singleton.itemhaving = サブクラスで定義
            //Singleton.singleton.itemcount = 0; サブクラスで定義
            asd.Engine.Sound.Play(itemGet);
            Dispose();
        }

        private void CollideWith(CollidableObject obj)
        {
            
            if (obj == null)
                return;

            if (obj is Bullet)
            {
                CollidableObject bullet = obj;

                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    bullet.OnCollide(this);
                    //asd.Engine.AddObject2D(new ItemGetEffect_PenetrateTriShot(new asd.Vector2DF(320, 240)));

                }
            }

            if (obj is PenetrateBullet)
            {
                CollidableObject bullet = obj;

                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    //bullet.OnCollide(this);
                    //asd.Engine.AddObject2D(new ItemGetEffect(new asd.Vector2DF(320, 240)));

                }
            }
            
        }

        protected override void OnUpdate()
        {
            foreach (var obj in Layer.Objects)
                CollideWith((obj as CollidableObject));
        }

    }
}

