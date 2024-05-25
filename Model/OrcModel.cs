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
        RunLeft,
        Hit,
        Dead,
        Jump,
    }

    public class OrcModel
    {
        public OrcState OrcState;
        public int Health;
        public bool onGravity;
        public bool isHit;
        public bool canHit;
        public float Gravity;
        public int Strange;
        public float Jump;
        public float JumpHorizontal;
        public IEnumerable<Rectangle> currentBlock;
        public Vector2 LastTile;
        public Vector2 TileOfOrc;
        public Vector2 previousPositionOfOrc;
        public Node NodeOfOrc;
        public List<Node> currentWay;
        public bool mayDelite;
        public Vector2 Position;
        public float Speed;
        public Vector2 Velocity;
        public bool hasJump;
        public bool IsDead;
        public Rectangle Rectangle;

        public OrcModel()
        {
            OrcState = OrcState.Stay;
            Health = 50;
            onGravity = true;
            isHit = false;
            canHit = false;
            Gravity = 2f;
            Strange = 1;
            Jump = 100f;
            JumpHorizontal = 116f;
            mayDelite = false;
            hasJump = true;
            IsDead = false;
            Speed = 3f;
        }

    public void JumpUp()
        {
            Position.Y -= JumpHorizontal;
            Velocity.Y += JumpHorizontal;
        }

        public void MoveLeft() => Velocity.X = -Speed;
        public void MoveRight() => Velocity.X = +Speed;
        public void MoveDown() => Position.Y += Speed / 40;
        public void MoveDownDown() => Position.Y += Speed / 2;
        public void JumpToUp() => Position.Y -= Jump / 2;

        private void JumpLogic()
        {
            Velocity.Y += Gravity;
            if (onGravity == false) Velocity.Y = 0f;
        }

        public void ChangeHealth()
        {
            if (Health < 1)
                IsDead = true;
            if (IsDead)
                Health = 0;
        }

        public void FindTile()
        {
            TileOfOrc = new Vector2((float)Math.Round((double)Rectangle.X / LevelModel.sizeOfElement),
                (float)Math.Round((double)(Rectangle.Bottom) / LevelModel.sizeOfElement));
            if (MapInfo.Graph.IsNewPosition(TileOfOrc)) TileOfOrc = previousPositionOfOrc;
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
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y+LevelModel.sizeOfElement, LevelModel.sizeOfElement, LevelModel.sizeOfElement);
            FindTile();
        }
    }
}
