using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using wraikny.MilleFeuille.Core.Object;
using wraikny.MilleFeuille.Core.UI;
using wraikny.MilleFeuille.Core.UI.Button;
using wraikny.MilleFeuille.Core.Input.Controller;

namespace STG
{
    class CharSelectScene : asd.Scene
    {
        const int playerNum = 2;

        // 文字描画オブジェクトを生成する。
        asd.TextObject2D PlayerSelectText = new asd.TextObject2D();

        int stage_num;
        List<PlayerType> typelist = new List<PlayerType>();
        bool isTitleChanging = false;
        asd.Layer2D uiLayer;

        public enum PlayerType
        {
            Normal, Speed
        }

        public CharSelectScene(int index)
        {
            uiLayer = new asd.Layer2D();
            stage_num = index;
        }

        protected override void OnRegistered()
        {
            base.OnRegistered();

            AddLayer(uiLayer);

            var background = new asd.TextureObject2D();
            background.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Menu.png");
            uiLayer.AddObject(background);

            // フォントを生成する。
            var font = asd.Engine.Graphics.CreateFont("font.aff");

            

            // 描画に使うフォントを設定する。
            PlayerSelectText.Font = font;

            // 描画位置を指定する。
            PlayerSelectText.Position = new asd.Vector2DF(0, 0);

            Singleton.Getsingleton();
            // 描画する文字列を指定する。
            PlayerSelectText.Text = "";

            // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
            uiLayer.AddObject(PlayerSelectText);

            var button1 = CreateButton(PlayerType.Normal, 0.0f, -150.0f, typelist);
            var button2 = CreateButton(PlayerType.Speed, 0.0f, -50.0f, typelist);

            button1
                .Chain(button2, ButtonDirection.Down)
                .Chain(button1, ButtonDirection.Down)
            ;

            uiLayer.AddObject(button1.GetComponent().Owner);
            uiLayer.AddObject(button2.GetComponent().Owner);

            var selecter = new ControllerButtonSelecter(button1);

            var keyboard = new KeyboardController<ControllerSelect>();
            keyboard
                .BindKey(ControllerSelect.Up, asd.Keys.Up)
                .BindKey(ControllerSelect.Down, asd.Keys.Down)
                .BindKey(ControllerSelect.Right, asd.Keys.Right)
                .BindKey(ControllerSelect.Left, asd.Keys.Left)
                .BindKey(ControllerSelect.Select, asd.Keys.Z)
                .BindKey(ControllerSelect.Cancel, asd.Keys.X)
            ;

            selecter.AddController(keyboard);

            uiLayer.AddComponent(selecter, "Selecter");

        }

        private static IControllerButton CreateButton(PlayerType type, float x, float y, List<PlayerType> list)
        {
            var defaultColor = new asd.Color(150, 150, 150);
            var hoverColor = new asd.Color(255, 255, 255);
            //var holdColor =  new asd.Color(50, 50, 50);

            var stage_button = new asd.TextureObject2D();
            stage_button.Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/{type.ToString()}Player.png");

            var size = new asd.Vector2DF(250.0f, 75.0f);
            var buttonArea = new asd.RectF(-size / 2.0f, size);

            var buttonObj =
                new asd.TextureObject2D()
                {
                    Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/{type.ToString()}Player.png")
                    
                    ,
                    Color = defaultColor
                    
                    ,
                    CenterPosition = new asd.Vector2DF(stage_button.Texture.Size.X / 2.0f, stage_button.Texture.Size.Y / 2.0f)

                    ,
                    Position =
                        asd.Engine.WindowSize.To2DF() / 2.0f
                        + (new asd.Vector2DF(x, y))
                
                }
            ;

            var button =
                new ControllerButtonComponent
                    <asd.TextureObject2D>("Button");

            //button.DefaultEvent += owner => { };
            //button.HoverEvent += owner => { };
            //button.HoldEvent += owner => { };
            button.OnEnteredEvent += owner => {
                owner.Color = hoverColor;
            };
            button.OnPushedEvent += owner => {
                //asd.Engine.ChangeSceneWithTransition(new GameScene(type, stage), new asd.TransitionFade(1.0f, 1.0f));
                list.Add(type);
                owner.Color = hoverColor;
            };
            button.OnReleasedEvent += owner => {
                owner.Color = hoverColor;
            };
            button.OnExitedEvent += owner => {
                owner.Color = defaultColor;
            };

            buttonObj.AddComponent(button, button.Name);

            return button;
        }

        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.X) == asd.ButtonState.Push && isTitleChanging == false)
            {
                asd.Engine.ChangeSceneWithTransition(new TitleScene(), new asd.TransitionFade(1.0f, 1.0f));

                isTitleChanging = true;
            }

            if (typelist.Count == playerNum && isTitleChanging == false) //プレイヤー数による
            {
                asd.Engine.ChangeSceneWithTransition(new GameScene(typelist, stage_num), new asd.TransitionFade(1.0f, 1.0f));

                isTitleChanging = true;
            }

            if (typelist.Count != playerNum)
            {
                PlayerSelectText.Text = $"Player　{typelist.Count + 1}SELECTING";
            }
            else
            {
                PlayerSelectText.Text = $"NOW　LOADING...";
            }



        }
    }
}
