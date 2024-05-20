using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;

namespace rpgame2.Model
{
    public class MusicValue
    {
        public Vector2 Position;
        public readonly SpriteFont Font;
        public static Song CurrentSong;
        public static Song NextSong;
        public static float Volume = 0.05f;
        public MusicValue(SpriteFont font, Song song)
        {
            Font = font;
            CurrentSong = song;
            Position = new Vector2(850, 250);
            MediaPlayer.Play(CurrentSong);
            MediaPlayer.IsRepeating = true;
        }
        public static void PlusMusic()
        {
            if (Volume < 1) Volume = (float)Math.Round(Volume + 0.05f, 2);
            else return;
        }
        public static void MinusMusic()
        {
            if (Volume >= 0.05f) Volume = (float)Math.Round(Volume - 0.05f, 2);
            else return;
        }
        public void ChangeSong(Song song)
        {
            if (!CurrentSong.Equals(song))
            {
                MediaPlayer.Play(song);
                CurrentSong = song;
            }
            else return;
        }

        public void Update() 
        {
            MediaPlayer.Volume = Volume;
        }
    }
}
