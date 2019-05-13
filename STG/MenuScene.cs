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
    class MenuScene : asd.Scene
    {
        bool isTitleChanging = false;
        asd.Layer2D uiLayer;

        

        public MenuScene()
        {
            uiLayer = new asd.Layer2D();
        }

        protected override void OnRegistered()
        {
            base.OnRegistered();

            AddLayer(uiLayer);

            var background = new asd.TextureObject2D();
            background.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Menu.png");
            uiLayer.AddObject(background);

            var button1 = CreateButton(1, 0.0f, -150.0f);
            var button2 = CreateButton(2, 0.0f, -50.0f);
            var button3 = CreateButton(3, 0.0f, 50.0f);
            var button4 = CreateButton(4, 0.0f, 150.0f);

            button1
                .Chain(button2, ButtonDirection.Down)
                .Chain(button3, ButtonDirection.Down)
                .Chain(button4, ButtonDirection.Down)
                .Chain(button1, ButtonDirection.Down)
            ;

            uiLayer.AddObject(button1.GetComponent().Owner);
            uiLayer.AddObject(button2.GetComponent().Owner);
            uiLayer.AddObject(button3.GetComponent().Owner);
            uiLayer.AddObject(button4.GetComponent().Owner);

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

        private static IControllerButton CreateButton(int index, float x, float y)
        {
            var defaultColor = new asd.Color(150, 150, 150);
            var hoverColor = new asd.Color(255, 255, 255);
            //var holdColor =  new asd.Color(50, 50, 50);

            var stage_button = new asd.TextureObject2D();
            stage_button.Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/stage{index}.png");

            var size = new asd.Vector2DF(250.0f, 75.0f);
            var buttonArea = new asd.RectF(-size / 2.0f, size);

            var buttonObj =
                new asd.TextureObject2D()
                {
                    Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/stage{index}.png")
                    
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
                Console.WriteLine("Button{0}: OnEntered", index);
                owner.Color = hoverColor;
            };
            button.OnPushedEvent += owner => {
                Console.WriteLine("Button{0}: OnPushed", index);
                asd.Engine.ChangeSceneWithTransition(new CharSelectScene(index), new asd.TransitionFade(1.0f, 1.0f));
                owner.Color = hoverColor;
            };
            button.OnReleasedEvent += owner => {
                Console.WriteLine("Button{0}: OnReleased", index);
                owner.Color = hoverColor;
            };
            button.OnExitedEvent += owner => {
                Console.WriteLine("Button{0}: OnExited", index);
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

            
        }
    }
}
