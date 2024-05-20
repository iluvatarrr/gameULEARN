using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using rpgame2.Model;
using rpgame2.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rpgame2.Controller
{
    public class OrcController
    {
        public AnimationController controller;
        public Dictionary<string, Animation> animations;
        public OrcModel OrcModel;
        public AnimationView AnimationView;
        public OrcController(OrcModel orcModel, Dictionary<string, Animation> currentAnimations)
        {
            animations = currentAnimations;
            controller = new AnimationController(animations.First().Value);
            OrcModel = orcModel;
            AnimationView = new AnimationView(controller);
        }

        public void Move()
        {
            if (OrcModel.NodeOfOrc == null) return;
            OrcModel.way = aStar.FindWays(OrcModel.NodeOfOrc, PlayerModel.NodeOfPlayer);
            OrcModel.currentWay = OrcModel.way.ToList();
            //foreach (var node in OrcModel.currentWay)
            //     OrcModel.Position = new Vector2(node.Position.X * LevelModel.sizeOfElement, node.Position.Y * LevelModel.sizeOfElement - 2 * LevelModel.sizeOfElement);
            if (OrcModel.currentWay.Count < 2) return;
            var firstNode = OrcModel.NodeOfOrc;
            var secondNode = OrcModel.currentWay[1];

            var difference = new Vector2() { X = firstNode.Position.X - secondNode.Position.X, Y = firstNode.Position.Y - secondNode.Position.Y };
            if (difference.X < 0 && difference.Y == 0)
            {
                OrcModel.MoveRight();
                if (difference.X < -2) OrcModel.JumpUp();
            }
            else if (difference.X > 0 && difference.Y == 0)
            {
                OrcModel.MoveLeft();
                if (difference.X > 2) OrcModel.JumpUp();
            }
            else if (difference.X == 0 && difference.Y < 0) OrcModel.MoveDownDown();
            else if (difference.X == 0 && difference.Y > 0) OrcModel.JumpToUp(); //deli na 7
            else if (difference.X > 0 && difference.Y < 0)
            {
                OrcModel.MoveLeft();
                OrcModel.MoveDown();
            }
            else if (difference.X < 0 && difference.Y < 0)
            {
                OrcModel.MoveRight();
                OrcModel.MoveDown();
            }
            else if (difference.X > 0 && difference.Y > 0)
            {
                OrcModel.MoveLeft();
                OrcModel.JumpUp();
            }
            else if (difference.X < 0 && difference.Y > 0)
            {
                OrcModel.MoveRight();
                OrcModel.JumpUp();
            }
        }

        public void UpdatePositionController()
        {
            if (controller != null) controller.Position = OrcModel.Position;
        }

        public void HitLogic()
        {
            if (OrcModel.OrcState.Equals(OrcState.Hit) && controller.animation.FrameCount - 1 == controller.animation.CurrentFrame)
            {
                OrcModel.isHit = true;
            }
            else
            {
                OrcModel.isHit = false;
            }
        }

        public void StayInMap()
        {
            if (OrcModel.Position.X >= MyGame.Game1.ScreenWidth) OrcModel.Position.X -= 2;
            if (OrcModel.Position.X < 0) OrcModel.Position.X += 2;
        }
        public void SetAnimation()
        {
            if (OrcModel.OrcState.Equals(OrcState.Run)) controller.Play(animations["WalkRight"]);
            else if (OrcModel.OrcState.Equals(OrcState.Hit)) controller.Play(animations["Fight"]);
            else if (OrcModel.OrcState.Equals(OrcState.Dead))
            {
                controller.animation.IsLooping = false;
                controller.Play(animations["death"]);
                if (controller.animation.CurrentFrame == controller.animation.FrameCount - 1 && OrcModel.IsDead) OrcModel.mayDelite = true;
            }
            else controller.Play(animations["None"]);
        }

        public void Update(GameTime gameTime)
        {
            controller.Update(gameTime);
            UpdatePositionController();
            SetAnimation();
            if (!OrcModel.IsDead)
            {
                animations["death"].IsFinished = false;
                Move();
            }
            StayInMap();
            HitLogic();
        }
    }
}
