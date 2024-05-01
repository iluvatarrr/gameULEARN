using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2.Controller;
using rpgame2.Model;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rpgame2.Model
{
    public class PlayerModel : Mob
    {

        private MouseState pastState;

        public float Speed = 3f;
        public bool hasJump = true;
        public int Health = 100;
        public int Gems;
        public int Strange = 10;
        public bool isHit = false;

        KeyboardState oldKeyboardState;
        KeyboardState keyboardState;

        private Input Input = new Input()
        {
            Up = Keys.W,
            Down = Keys.S,
            Left = Keys.A,
            Right = Keys.D,
            Fight = Keys.E,
            Fight2 = Keys.F,
            Fight3 = Keys.R,
            Jump = Keys.Space,
            None = Keys.None,
        };

        private void DeadInput()
        {
            Input = new Input();
        }

        public void UpdatePositionController()
        {
            if (controller != null) controller.Position = Position;
        }

        public void AddGem() => Gems++;

        public void HitLogic()
        {
            if (keyboardState.IsKeyUp(Input.Fight) && oldKeyboardState.IsKeyDown(Input.Fight)
                || keyboardState.IsKeyDown(Input.Fight2) && oldKeyboardState.IsKeyDown(Input.Fight2)
                || keyboardState.IsKeyDown(Input.Fight3) && oldKeyboardState.IsKeyDown(Input.Fight3))
                isHit = true;
            else isHit = false;
        }

        public void ChangeHealth()
        {
            MouseState MouseState = Mouse.GetState();
            Rectangle MouseRect = new Rectangle(pastState.X, pastState.Y, 1, 1);

            if (MouseRect.Intersects(Rectangle) && (MouseState.LeftButton == ButtonState.Pressed && pastState.LeftButton == ButtonState.Pressed))
                if (controller.timer < 0.000001f) Health -= 10;
            if (Health < 1) IsDead = true;
            if (IsDead) Health = 0;
            pastState = MouseState;
        }

        private void Move()
        {
            if (keyboardState.IsKeyDown(Input.Up)) Velocity.Y = -5*Speed;
            else if (keyboardState.IsKeyDown(Input.Down)) Velocity.Y = Speed;
            else if (keyboardState.IsKeyDown(Input.Left)) Velocity.X = -Speed;
            else if (keyboardState.IsKeyDown(Input.Right)) Velocity.X = Speed;
            JumpLogic();
        }

        private void JumpLogic()
        {
            if (keyboardState.IsKeyDown(Input.Jump) && hasJump == false)
            {
                if (controller.timer < 0.00000001f)
                {
                    Position.Y -= 90f;
                    Velocity.Y = -45f;
                    hasJump = true;
                }
            }
            Velocity.Y += 3f;
            if (hasJump == false) Velocity.Y = 0f;
        }

        protected virtual void SetAnimation()
        {
            if (keyboardState.IsKeyDown(Input.Right)) controller.Play(animations["WalkRight"]);
            else if (keyboardState.IsKeyDown(Input.Left)) controller.Play(animations["WalkLeft"]);
            else if (keyboardState.IsKeyDown(Input.Down)) controller.Play(animations["None"]);
            else if (keyboardState.IsKeyDown(Input.Up)) controller.Play(animations["WalkUp"]);
            else if (keyboardState.IsKeyDown(Input.Fight)) controller.Play(animations["Fight"]);
            else if (keyboardState.IsKeyDown(Input.Fight2)) controller.Play(animations["Fight2"]);
            else if (keyboardState.IsKeyDown(Input.Fight3)) controller.Play(animations["Fight3"]);
            else if (IsDead)
            {
                controller.Play(animations["death"]);
                controller.animation.IsLooping = false;
            }
            else controller.Play(animations["None"]);
        }

        public virtual void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            oldKeyboardState = keyboardState;
            controller.Update(gameTime);
            UpdatePositionController();
            Move();
            HitLogic();
            SetAnimation();
            Position += Velocity;
            Velocity = Vector2.Zero;
            if (IsDead) DeadInput();
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, controller.animation.FrameWidth, controller.animation.FrameHeight);
        }
    }
}