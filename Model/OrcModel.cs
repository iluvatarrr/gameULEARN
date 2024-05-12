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
        public int Health = 50;
        public bool onGravity = true;
        public bool isHit = false;
        public int Strange = 1;
        public float Jump = 100f;
        public float JumpHorizontal = 120f;
        public AnimationController controller;
        public Dictionary<string, Animation> animations;
        public IEnumerable<Node> way;
        public Vector2 TileOfOrc;
        public Vector2 previousPositionOfOrc;
        public Node NodeOfOrc;
        public static List<Node> currentWay;
        public void JumpUp()
        {
            Position.Y -= JumpHorizontal;
            Velocity.Y += JumpHorizontal;
        }

        public void Move()
        {
            if (NodeOfOrc == null) return;
            way = BFS.FindWays(NodeOfOrc, LevelModel.NodeOfPlayer);
            currentWay = way.ToList();
            if (currentWay.Count < 1) return;
            var firstNode = NodeOfOrc;
            var secondNode = currentWay[currentWay.Count - 1];

            var difference = new Vector2() { X = firstNode.Position.X - secondNode.Position.X, Y = firstNode.Position.Y - secondNode.Position.Y };
            if (difference.X < 0 && difference.Y == 0)
            {
                Position.X += Speed;
                if (difference.X < -2) JumpUp();
            }
            else if (difference.X > 0 && difference.Y == 0)
            {
                Position.X -= Speed;
                if (difference.X > 2) JumpUp();
            }
            else if (difference.X == 0 && difference.Y < 0) Position.Y += Speed / 20;
            else if (difference.X == 0 && difference.Y > 0) Position.Y -= Jump;
            else if (difference.X > 0 && difference.Y < 0)
            {
                Position.X -= Speed;
                Position.Y += Speed / 80;
            }
            else if (difference.X < 0 && difference.Y < 0)
            {
                Position.X += Speed;
                Position.Y += Speed / 80;
            }
            else if (difference.X > 0 && difference.Y > 0)
            {
                Position.X -= Speed;
                JumpUp();
            }
            else if (difference.X < 0 && difference.Y > 0)
            {
                Position.X += Speed;
                JumpUp();
            }
        }

        private void JumpLogic()
        {
            Velocity.Y += Gravity;
            if (onGravity == false) Velocity.Y = 0f;
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

        public void FindTile()
        {
            TileOfOrc = new Vector2((float)Math.Round((double)Rectangle.Left / LevelModel.sizeOfElement),
                (float)Math.Round((double)(Rectangle.Bottom - 5) / LevelModel.sizeOfElement));
            if (MapInfo.Graph.IsNewPosition(TileOfOrc)) TileOfOrc = previousPositionOfOrc;
            else previousPositionOfOrc = TileOfOrc;
        }

        public void Update(GameTime gameTime)
        {
            controller.Update(gameTime);
            if (GameState.State.Equals(State.Game) && !IsDead) Move();
            JumpLogic();
            Position += Velocity;
            Velocity = Vector2.Zero;
            SetAnimation();
            UpdatePositionController();
            ChangeHealth();
            Rectangle = new Rectangle((int)Position.X+LevelModel.sizeOfElement/2, (int)Position.Y+LevelModel.sizeOfElement, LevelModel.sizeOfElement, LevelModel.sizeOfElement);
            FindTile();
        }
    }
}
