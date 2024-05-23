using Microsoft.Xna.Framework;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rpgame2.Model
{
    public class PlayerModel : Mob
    {
        public float Speed;
        public float Jump;
        public float Gravity;
        public bool onGravity;
        public int Health;
        public int Gems;
        public int Strange;
        public bool isHitж;
        public Vector2 PositionBeforeJump;
        public Vector2 TileOfPlayer;
        public Vector2 previousPositionOfPlayer;
        public static Node NodeOfPlayer;

        public static Vector2 PlayerStartPosition;

        public PlayerModel(Dictionary<string, Animation> currentAnimations)
        {
            Speed = 3f;
            Jump = 90f;
            Gravity = 3f;
            onGravity = true;
            Health = 100;
            Strange = 10;
            isHit = false;
            PositionBeforeJump = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        }

        public void AddGem() => Gems++;

        public void AddKill() => Gems+=10;

        public void ChangeHealth()
        {
            if (Health < 1) IsDead = true;
            if (IsDead) Health = 0;
        }
        public void MoveLeft() => Velocity.X = -Speed;
        public void MoveRight() => Velocity.X = +Speed;
        public void JumpMove()
        {
            if (onGravity == false)
            {
                Position.Y -= Jump;
                Velocity.Y = -Jump / 2;
                onGravity = true;
            }
        }

        private void GravityLogic()
        {
            Velocity.Y += Gravity;
            if (onGravity == false)
            {
                PositionBeforeJump = Position;
                Velocity.Y = 0f;
            }
        }

        private bool InNotMap()
        {
            return (Position.X < -RectangleHelper.marginBlockLeftRight
                    || Position.Y < -RectangleHelper.marginPlayerTop
                    || MyGame.Game1.ScreenWidth < Position.X
                    || MyGame.Game1.ScreenHeight < Position.Y);
        }
        public void FindTile()
        {
            TileOfPlayer = new Vector2((float)Math.Round((double)Rectangle.Left / LevelModel.sizeOfElement),
                (float)Math.Round((double)(Rectangle.Bottom - RectangleHelper.marginFromTop) / LevelModel.sizeOfElement));
            if (MapInfo.Graph.IsNewPosition(TileOfPlayer)) TileOfPlayer = previousPositionOfPlayer;
            else previousPositionOfPlayer = TileOfPlayer;
        }

        public void Update()
        {
            if (InNotMap()) IsDead = true;
            ChangeHealth();
            GravityLogic();
            Position += Velocity;
            Velocity = Vector2.Zero;
            Rectangle = new Rectangle((int)Position.X+ LevelModel.sizeOfElement / 2, (int)Position.Y, LevelModel.sizeOfElement , LevelModel.sizeOfElement);
            if ((Position.Y - PositionBeforeJump.Y) > (LevelModel.sizeOfElement * 2)) IsDead = true;
            FindTile();
        }
    }
}