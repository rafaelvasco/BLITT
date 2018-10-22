using BLITTEngine;
using BLITTEngine.Core.Graphics;
using BLITTEngine.Draw;
using BLITTEngine.Input;
using BLITTEngine.Input.Keyboard;
using BLITTEngine.Resources;

namespace BLITTDemo
{
    public class Demo1 : Scene
    {

        private Sprite sprite1;
        private SpriteSheet sprite_sheet;
        private AnimatedSprite anim_sprite;
        private float direction = 1;
        private Gradient gradient;
        private Font font;

        private float sx = 0.0f;


        public override void Init()
        {
            gradient = new Gradient();

            gradient.SetHorizontal(Color.Gold, Color.GreenYellow);

            sprite1 = new Sprite(Content.GetTexture2D("ship"))
            {
                ColorTint = Color.Red,
                Scale = 4.0f
            };

            sprite_sheet = SpriteSheet.FromGrid(Content.GetTexture2D("spritesheet"), 4, 4);

            anim_sprite = new AnimatedSprite(sprite_sheet)
            {
                Scale = 8.0f
            };

            anim_sprite
                .AddAnimation("idle_left", 12)
                .AddAnimation("idle_right", 4)
                .AddAnimation("idle_up", 8)
                .AddAnimation("idle_down", 0)

                .AddAnimation("walk_left", 12, 13, 14, 15)
                .AddAnimation("walk_right", 4, 5, 6, 7)
                .AddAnimation("walk_up", 8, 9, 10, 11)
                .AddAnimation("walk_down", 0, 1, 2, 3)

                .SetAnimation("idle_right");

            font = Content.GetFont("04b03.font");

        }

        public override void Update(float dt)
        {
            if(Control.KeyDown(Key.A))
            {
                sprite1.X -= 5.0f;
            }

            if(Control.KeyDown(Key.D))
            {
                sprite1.X += 5.0f;
            }


            if(Control.KeyPressed(Key.Left))
            {
                direction = -1;
                sx = 5.0f;
                anim_sprite.SetAnimation("walk_left");
            }
            else if(Control.KeyPressed(Key.Right))
            {
                direction = 1;
                sx = 5.0f;
                anim_sprite.SetAnimation("walk_right");

            }

            if(Control.KeyReleased(Key.Left) || Control.KeyReleased(Key.Right))
            {
                sx = 0.0f;

                if(direction == -1)
                {
                    anim_sprite.SetAnimation("idle_left");
                }
                else if (direction == 1)
                {
                    anim_sprite.SetAnimation("idle_right");
                }
            }



            anim_sprite.X += sx * direction;

            anim_sprite.Update();
        }

        public override void Draw(Canvas canvas)
        {
            canvas.Begin();



            // canvas.FillRect(-400, -300, 100, 100, Color.Red);

            canvas.DrawLine(-350, -250, 350, 250, 4, Color.Blue);

            canvas.DrawLine(-350, 250, 350, -250, 4, Color.Green);

            canvas.DrawLine(-350, -250, 350, -250, 4, Color.Yellow);

            canvas.DrawLine(-350, -250, -350, 250, 4, Color.Fuchsia);

            canvas.DrawLine(350, -250, 350, 250, 4, Color.HotPink);

            canvas.DrawLine(-350, 250, 350, 250, 4, Color.LawnGreen);

            canvas.DrawRect(0, 0, 100, 100, 2, Color.LightSalmon);

            canvas.FillGradient(0, 0, 200, 200, gradient);

            canvas.Draw(sprite1);

            canvas.Draw(anim_sprite);

            canvas.DrawText(@" !#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_abcdefghijklmnopqrstuvwxyz{|}~", -400, -200, font);

            canvas.End();
        }

    }
}