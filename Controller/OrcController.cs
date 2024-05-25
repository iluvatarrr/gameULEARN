using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using rpgame2.Model;
using rpgame2.View;
using System.Collections.Generic;
using System.Linq;

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
            animations = new Dictionary<string, Animation>();
            foreach (var anim in currentAnimations)
                animations.Add(anim.Key, new Animation(anim.Value));
            controller = new AnimationController(animations.First().Value);
            OrcModel = orcModel;
            AnimationView = new AnimationView(controller);
        }

        public void Move()
        {
            if (OrcModel.NodeOfOrc == null) return;
            OrcModel.currentWay = aStar.FindWays(OrcModel.NodeOfOrc, PlayerModel.NodeOfPlayer);
            if (OrcModel.currentWay.Count < 2) return;
            var firstNode = OrcModel.NodeOfOrc;
            var secondNode = OrcModel.currentWay[1];
            var marginX = secondNode.Position.X * LevelModel.sizeOfElement - RectangleHelper.marginFromTop;
            var difference = new Vector2() { X = firstNode.Position.X - secondNode.Position.X, Y = firstNode.Position.Y - secondNode.Position.Y };
            if (difference.X < 0 && difference.Y == 0)
            {
                XDerection(marginX);
                if (difference.X < -2) OrcModel.JumpUp();
            }
            else if (difference.X > 0 && difference.Y == 0)
            {
                XDerection(marginX);
                if (difference.X > 2) OrcModel.JumpUp();
            }
            else if (difference.X == 0 && difference.Y < 0) OrcModel.MoveDownDown();
            else if (difference.X == 0 && difference.Y > 0) OrcModel.JumpToUp();
            else if (difference.X > 0 && difference.Y < 0) DownDirection(marginX);
            else if (difference.X < 0 && difference.Y < 0) DownDirection(marginX);
            else if (difference.X > 0 && difference.Y > 0) UpDirection(marginX);
            else if (difference.X < 0 && difference.Y > 0) UpDirection(marginX);
        }

        public void XDerection(float marginX)
        {
            if (OrcModel.Position.X > marginX) OrcModel.MoveLeft();
            else if (OrcModel.Position.X < marginX) OrcModel.MoveRight();
        }

        public void DownDirection(float marginX)
        {
            XDerection(marginX);
            OrcModel.MoveDown();
        }

        public void UpDirection(float marginX)
        {
            XDerection(marginX);
            OrcModel.JumpUp();
        }

            public void SetState()
        {
            if (OrcModel.Velocity.X > 0 && !OrcModel.canHit) OrcModel.OrcState = OrcState.Run;
            else if (OrcModel.Velocity.X < 0 && !OrcModel.canHit) OrcModel.OrcState = OrcState.RunLeft;
            else if (OrcModel.IsDead || OrcModel.Health < 1) OrcModel.OrcState = OrcState.Dead;
            else if (OrcModel.canHit) OrcModel.OrcState = OrcState.Hit;
            else if (PlayerModel.NodeOfPlayer.Equals(OrcModel.NodeOfOrc)) OrcModel.OrcState = OrcState.Stay;
        }

        public void UpdatePositionController()
        {
            if (controller != null) controller.Position = OrcModel.Position;
        }

        public void HitLogic()
        {
            if (controller.animation.FrameCount - 1 == controller.animation.CurrentFrame && OrcModel.OrcState.Equals(OrcState.Hit))
                OrcModel.isHit = true;
            else
            {
                OrcModel.isHit = false;
                OrcModel.canHit = false;
            }
        }

        public void StayInMap()
        {
            if (OrcModel.Position.X >= MyGame.Game1.ScreenWidth) OrcModel.Position.X -= 2;
            if (OrcModel.Position.X < 0) OrcModel.Position.X += 2;
        }

        public void SetAnimation()
        {
            if (OrcModel.OrcState.Equals(OrcState.Run)) controller.Play(animations["Walk"]);
            else if (OrcModel.OrcState.Equals(OrcState.RunLeft)) controller.Play(animations["WalkLeft"]);
            else if (OrcModel.OrcState.Equals(OrcState.Stay)) controller.Play(animations["None"]);
            else if (OrcModel.OrcState.Equals(OrcState.Hit)) controller.Play(animations["Fight"]);
            else if (OrcModel.OrcState.Equals(OrcState.Dead))
            {
                controller.animation.IsLooping = false;
                controller.Play(animations["death"]);
                if (controller.animation.CurrentFrame == controller.animation.FrameCount - 1 && OrcModel.IsDead) OrcModel.mayDelite = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            controller.Update(gameTime);
            UpdatePositionController();
            if (!OrcModel.IsDead) Move();
            SetState();
            SetAnimation();
            StayInMap();
            HitLogic();
        }
    }
}
