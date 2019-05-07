using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class TitleScene : asd.Scene
    {
        protected override void OnRegistered()
        {
            asd.Layer2D layer = new asd.Layer2D();

            AddLayer(layer);

            asd.TextureObject2D background = new asd.TextureObject2D();
            background.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Title2.png");

            layer.AddObject(background);
        }

        bool isTitleChanging = false;

        protected override void OnUpdated()
        {
            if(asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.ButtonState.Push && isTitleChanging == false)
            {
                asd.Engine.ChangeSceneWithTransition(new MenuScene(), new asd.TransitionFade(1.0f, 1.0f));

                isTitleChanging = true;
            }
        }
    }
}
