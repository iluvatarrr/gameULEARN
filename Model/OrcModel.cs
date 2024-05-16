using Microsoft.Xna.Framework;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public float JumpHorizontal = 145f;
        public AnimationController controller;
        public Dictionary<string, Animation> animations;
        public IEnumerable<Node> way;
        public IEnumerable<Rectangle> currentBlock;
        public Vector2 LastTile;
        public Vector2 TileOfOrc;
        public Vector2 previousPositionOfOrc;
        public Node NodeOfOrc;
        public List<Node> currentWay;
        public bool mayDelite = false;

        public void JumpUp()
        {
            Position.Y -= JumpHorizontal;
            Velocity.Y += JumpHorizontal;
        }

        public void Move()
        {
            if (NodeOfOrc == null) return;
            way = BFS.FindWays(NodeOfOrc, PlayerModel.NodeOfPlayer);
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
            else if (difference.X == 0 && difference.Y < 0) Position.Y += Speed / 2;
            else if (difference.X == 0 && difference.Y > 0) Position.Y -= Jump / 7; //deli na 7
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
            if (IsDead)
            {
                OrcState = OrcState.Dead;
                Health = 0;
            }
        }

        public void SetAnimation()
        {
            if (OrcState.Equals(OrcState.Run)) controller.Play(animations["WalkRight"]);
            else if (OrcState.Equals(OrcState.Hit)) controller.Play(animations["Fight"]);
            else if (OrcState.Equals(OrcState.Dead))
            {
                controller.Play(animations["death"]);
                controller.animation.IsLooping = false;
                if (controller.animation.CurrentFrame == controller.animation.FrameCount - 1 && IsDead) mayDelite = true;
            }
            else controller.Play(animations["None"]);
        }

        public void UpdatePositionController()
        {
            if (controller != null) controller.Position = Position;
        }

        public void FindTile()
        {
            //if (TileOfOrc != null)
            //    LastTile = TileOfOrc;
            //TileOfOrc = new Vector2((float)Math.Round((double)Rectangle.Left / LevelModel.sizeOfElement),
            //    (float)Math.Round((double)(Rectangle.Bottom - 5) / LevelModel.sizeOfElement));

            currentBlock = MapInfo.Blocks.Where(platform => Rectangle.OnPlatform(platform));
            if (currentBlock.Count() > 0)
            {
                TileOfOrc = new Vector2((float)Math.Round((double)currentBlock.First().X / LevelModel.sizeOfElement),
                     (float)Math.Round((double)currentBlock.First().Y / LevelModel.sizeOfElement));
            }
            //else
            //{
            //    TileOfOrc = new Vector2((float)Math.Round((double)Rectangle.Left / LevelModel.sizeOfElement),
            //        (float)Math.Round((double)(Rectangle.Bottom - 5) / LevelModel.sizeOfElement));
            //}
            if (/*LastTile != null && */MapInfo.Graph.IsNewPosition(TileOfOrc) /*&& LastTile.Y - TileOfOrc.Y > 2*/) TileOfOrc = previousPositionOfOrc;
            else previousPositionOfOrc = TileOfOrc;
        }

        public void StayInMap()
        {
            if (Position.X >= Game1.Game1.ScreenWidth) Position.X -= 2;
            if (Position.X < 0) Position.X += 2;
        }

        public void Update(GameTime gameTime)
        {
            controller.Update(gameTime);
            if (GameState.CurrentState.Equals(State.Game) && !IsDead)
            {
                animations["death"].IsFinished = false;
                Move();
            }
            StayInMap();
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
