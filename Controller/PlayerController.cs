using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2.Controller;
using rpgame2.Model;
using rpgame2.View;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace rpgame2.Controller
{
    public class PlayerController
    {
        KeyboardState keyboardState;
        public AnimationController controller;
        public Dictionary<string, Animation> animations;
        public PlayerModel PlayerModel;
        public AnimationView AnimationView;

        public PlayerController(PlayerModel playerModel, Dictionary<string, Animation> currentAnimations)
        {
            PlayerModel = playerModel;
            animations = currentAnimations;
            controller = new AnimationController(animations.First().Value);
            AnimationView = new AnimationView(controller);

        }

        private Input Input = new Input()
        {
            //Up = Keys.W,
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
            if (controller != null) controller.Position = PlayerModel.Position;
        }

        protected virtual void SetAnimation()
        {
            if (keyboardState.IsKeyDown(Input.Right)) controller.Play(animations["WalkRight"]);
            else if (keyboardState.IsKeyDown(Input.Left)) controller.Play(animations["WalkLeft"]);

            //else if (keyboardState.IsKeyDown(Input.Up)) controller.Play(animations["WalkUp"]);
            else if (keyboardState.IsKeyDown(Input.Fight)) controller.Play(animations["Fight"]);
            else if (keyboardState.IsKeyDown(Input.Fight2)) controller.Play(animations["Fight2"]);
            else if (keyboardState.IsKeyDown(Input.Fight3)) controller.Play(animations["Fight3"]);
            else if (PlayerModel.IsDead)
            {
                controller.Play(animations["death"]);
                controller.animation.IsLooping = false;
            }
            else controller.Play(animations["None"]);
        }
        public int HelpHitLogic(int damage)
        {
            if (controller.animation.FrameCount - 1 == controller.animation.CurrentFrame && controller.timer < 0.000001f)
            {
                PlayerModel.isHit = true;
                return damage;
            }
            else
            {
                PlayerModel.isHit = false;
                return 0;
            }
        }
        private void Move()
        {
            //if (keyboardState.IsKeyDown(Input.Up)) PlayerModel.Velocity.Y = -5 * PlayerModel.Speed;
            if (keyboardState.IsKeyDown(Input.Left)) PlayerModel.MoveLeft();
            else if (keyboardState.IsKeyDown(Input.Right)) PlayerModel.MoveRight();
            JumpLogic();
        }
        public void JumpMove()
        {
            if (controller.timer < 0.000001f)
                PlayerModel.JumpMove();
        }

        private void JumpLogic()
        {
            if (keyboardState.IsKeyDown(Input.Jump)) JumpMove();
        }
        public int HitLogic()
        {
            if (keyboardState.IsKeyDown(Input.Fight)) return HelpHitLogic(50);
            if (keyboardState.IsKeyDown(Input.Fight2)) return HelpHitLogic(25);
            if (keyboardState.IsKeyDown(Input.Fight3)) return HelpHitLogic(10);
            else
            {
                PlayerModel.isHit = false;
                return 0;
            }
        }
        public void Update(GameTime gameTime)
        {
            keyboardState = Input.GetState();
            controller.Update(gameTime);
            UpdatePositionController();
            SetAnimation();
            if (PlayerModel.IsDead) DeadInput();
            Move();
            PlayerModel.Strange = HitLogic();
        }
    }
}
