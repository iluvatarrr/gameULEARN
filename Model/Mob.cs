using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2;
using rpgame2.Model;
using rpgame2.View;
using System.Collections.Generic;

namespace rpgame2
{
    public class Mob
    {
        public Vector2 Position;
        public float Speed = 3f;
        public Vector2 Velocity;
        public bool hasJump = true;
        public int Health = 50;
        public bool IsDead;
        public int Strange = 10;
        public bool isHit = false;
        public Rectangle Rectangle;
        public AnimationController controller;
        public Dictionary<string, Animation> animations;
    }
}
