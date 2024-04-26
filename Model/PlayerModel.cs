using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2.Controller;
using rpgame2.View;
using System.Collections.Generic;

namespace rpgame2.Model
{
    public class PlayerModel
    {
        public AnimationController controller;
        public Dictionary<string, Animation> animations;

        protected Vector2 position;
        public float Speed = 5f;
        public Vector2 Velocity;

        public Input Input = new Input()
        {
            Up = Keys.W,
            Down = Keys.S,
            Left = Keys.A,
            Right = Keys.D,
            Fight = Keys.E,
            Fight2 = Keys.F,
            Fight3 = Keys.R,
            None = Keys.None,
        };
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                if (controller != null)
                {
                    controller.Position = position;
                }
            }
        }
        protected virtual void Move()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Input.Up)) Velocity.Y = -Speed;
            else if (keyboardState.IsKeyDown(Input.Down)) Velocity.Y = Speed;
            else if (keyboardState.IsKeyDown(Input.Left)) Velocity.X = -Speed;
            else if (keyboardState.IsKeyDown(Input.Right)) Velocity.X = Speed;
        }

        protected virtual void SetAnimation()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (Velocity.X > 0) controller.Play(animations["WalkRight"]);
            else if (Velocity.X < 0) controller.Play(animations["WalkLeft"]);
            else if (Velocity.Y > 0) controller.Play(animations["WalkDown"]);
            else if (Velocity.Y < 0) controller.Play(animations["WalkUp"]);
            else if (keyboardState.IsKeyDown(Input.Fight)) controller.Play(animations["Fight"]);
            else if (keyboardState.IsKeyDown(Input.Fight2)) controller.Play(animations["Fight2"]);
            else if (keyboardState.IsKeyDown(Input.Fight3)) controller.Play(animations["Fight3"]);
            else controller.Play(animations["None"]);
        }
        public virtual void Update(GameTime gameTime)
        {
            Move();
            SetAnimation();
            controller.Update(gameTime);
            Position += Velocity;
            Velocity = Vector2.Zero;

        }
    }
}
