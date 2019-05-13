using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class GameOverScene : asd.Scene
    {
        int WinPlayer;

        public GameOverScene(int winplayer)
        {
            WinPlayer = winplayer;
        }

        protected override void OnRegistered()
        {
            asd.Layer2D layerpict = new asd.Layer2D();
            layerpict.DrawingPriority = -10;
            AddLayer(layerpict);

            asd.Layer2D layertext = new asd.Layer2D();
            AddLayer(layertext);

            asd.TextureObject2D background = new asd.TextureObject2D();
            background.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Over.png");

            layerpict.AddObject(background);

            
            // フォントを生成する。
            var font = asd.Engine.Graphics.CreateFont("font.aff");
            
            // 文字描画オブジェクトを生成する。
            var obj = new asd.TextObject2D();

            // 描画に使うフォントを設定する。
            obj.Font = font;

            // 描画位置を指定する。
            obj.Position = new asd.Vector2DF(220, 240);

            Singleton.Getsingleton();
            // 描画する文字列を指定する。
            obj.Text = $"Player　{WinPlayer}WIN!";

            // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
            layertext.AddObject(obj);
            


        }

        bool isOverChanging = false;

        protected override void OnUpdated()
        {
            if(asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.ButtonState.Push && isOverChanging == false)
            {
                asd.Engine.ChangeSceneWithTransition(new MenuScene(), new asd.TransitionFade(1.0f, 1.0f));

                isOverChanging = true;

                Singleton.singleton.score = 0;
                Singleton.singleton.itemhaving = 0;
                Singleton.singleton.itemcount = 0;
                Singleton.singleton.bomblimit = 1;
                GameScene.stage_now = 0;
                Wall.wallcount = 0;
            }
        }
        
    }
}
