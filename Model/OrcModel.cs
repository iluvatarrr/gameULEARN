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
        public void MoveLeft() => Velocity.X = -Speed;
        public void MoveRight() => Velocity.X = +Speed;
        public void MoveDown() => Position.Y += Speed / 80;
        public void MoveDownDown() => Position.Y += Speed / 2;
        public void JumpToUp() => Position.Y -= Jump / 7;

        private void JumpLogic()
        {
            Velocity.Y += Gravity;
            if (onGravity == false) Velocity.Y = 0f;
        }

        public void ChangeHealth()
        {
            if (Health < 1)
            {
                OrcState = OrcState.Dead;
                IsDead = true;
            }
            if (IsDead)
            {
                OrcState = OrcState.Dead;
                Health = 0;
            }
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
            if (Position.X >= MyGame.Game1.ScreenWidth) Position.X -= 2;
            if (Position.X < 0) Position.X += 2;
        }

        public void Update()
        {
            StayInMap();
            JumpLogic();
            Position += Velocity;
            Velocity = Vector2.Zero;
            ChangeHealth();
            Rectangle = new Rectangle((int)Position.X+LevelModel.sizeOfElement/2, (int)Position.Y+LevelModel.sizeOfElement, LevelModel.sizeOfElement, LevelModel.sizeOfElement);
            FindTile();
        }
    }
}
