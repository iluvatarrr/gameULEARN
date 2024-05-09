using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2.Controller;
using rpgame2.Model;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace rpgame2.Model
{
    public class PlayerModel : Mob
    {
        public float Speed = 3f;
        public float Jump = 90f;
        public float Gravity = 3f;
        public bool onGravity = true;
        public int Health = 100;
        public int Gems;
        public int Strange = 10;
        public bool isHit = false;
        public Vector2 PositionBeforeJump = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        public AnimationController controller;
        public Dictionary<string, Animation> animations;
        KeyboardState oldKeyboardState;
        KeyboardState keyboardState;
        public Vector2 Position = new Vector2(0, 380);
        public Vector2 Gloal = new Vector2(0, 380);

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

        public void AddKill() => Gems+=10;


        public int HelpHitLogic(int damage)
        {
            if (controller.animation.FrameCount - 1 == controller.animation.CurrentFrame && controller.timer < 0.000001f)
            {
                isHit = true;
                return damage;
            }
            else
            {
                isHit = false;
                return 0;
            }
        }

        public int HitLogic()
        {
            if (keyboardState.IsKeyDown(Input.Fight)) return HelpHitLogic(50);
            if (keyboardState.IsKeyDown(Input.Fight2)) return HelpHitLogic(25);
            if (keyboardState.IsKeyDown(Input.Fight3)) return HelpHitLogic(10);
            else
            {
                isHit = false;
                return 0;
            }
        }

        public void ChangeHealth()
        {
            if (Health < 1) IsDead = true;
            if (IsDead) Health = 0;
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
                
            if (keyboardState.IsKeyDown(Input.Jump) && onGravity == false)
                if (controller.timer < 0.000001f)
                {
                    Position.Y -= Jump;
                    Velocity.Y = -Jump/2;
                    onGravity = true;
                }
            Velocity.Y += Gravity;
            if (onGravity == false)
            {
                PositionBeforeJump = Position;
                Velocity.Y = 0f;
            }
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
            keyboardState = Input.GetState();
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
            if ((Position.Y - PositionBeforeJump.Y) > (LevelModel.sizeOfElement * 2)) IsDead = true;
        }
    }
}