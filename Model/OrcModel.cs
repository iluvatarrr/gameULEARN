using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using rpgame2.Controller;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;


namespace rpgame2.Model
{
    public enum OrcState
    {
        Stay,
        Run,
        Hit,
        Dead,
        Jump,
    }
    public class OrcModel : Mob
    {
        public OrcState OrcState = OrcState.Stay;
        public Vector2 Position = new Vector2(400, -30);
        public int Health = 50;
        public bool onGravity = true;
        public bool isHit = false;
        public int Strange = 10;
        public static IEnumerable<Node> path;

        public AnimationController controller;
        public Dictionary<string, Animation> animations;

        public void Move()
        {
            path = BFS.FindWays(LevelModel.NodeOfOrc, LevelModel.NodeOfPlayer);
            //if (path.Count < 1) return;
            //Position = path[0].Position;

            //if (path.Count < 2) return;
            var firstPoint = path.First().Previous;
            if (firstPoint == null) firstPoint =  path.First();
            var secondPoint = path.First();
            var difference = new Vector2() { X = firstPoint.Position.X - secondPoint.Position.X, Y = firstPoint.Position.Y - secondPoint.Position.Y };
            if (difference.X < 0 && difference.Y == 0) Position.X += Speed;
            else if (difference.X > 0 && difference.Y == 0) Position.X -= Speed;
            else return;
            //else if (difference.X == 0 && difference.Y < 0) Position.Y += Speed;
            //else if (difference.X == 0 && difference.Y > 0) Position.Y -= Speed;
        }
        private void JumpLogic()
        {
            Velocity.Y += Gravity;
            if (onGravity == false)
            {
                Velocity.Y = 0f;
            }
        }

        public int HitLogic()
        {
            if (controller.animation.FrameCount - 1 == controller.animation.CurrentFrame && controller.timer < 0.000001f)
            {
                isHit = true;
                return Strange;
            }
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

        public void SetAnimation()
        {
            if (OrcState.Equals(OrcState.Run)) controller.Play(animations["WalkRight"]);
            else if (OrcState.Equals(OrcState.Hit)) controller.Play(animations["Fight"]);
            else if (IsDead)
            {
                controller.Play(animations["death"]);
                controller.animation.IsLooping = false;
            }
            else controller.Play(animations["None"]);
        }
        public void UpdatePositionController()
        {
            if (controller != null) controller.Position = Position;
        }

        public void Update(GameTime gameTime)
        {
            controller.Update(gameTime);
            if (GameState.State.Equals(State.Game)) Move();
            JumpLogic();
            Position += Velocity;
            Velocity = Vector2.Zero;
            SetAnimation();
            UpdatePositionController();
            ChangeHealth();
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, controller.animation.FrameWidth, controller.animation.FrameHeight);
        }
    }
}
