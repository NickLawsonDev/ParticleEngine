using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Animation
    {
        private SpriteBatch spriteBatch;

        public enum AnimationTypes { SpriteSheet }

        public int FramesPerRow { get; set; }
        public int NumberOfRows { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public string Name { get; set; }

        private Texture2D image;

        public Animation(string name, int frameWidth, int frameHeight, int framesPerRow, int numberOfRows)
        {
            Name = name;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FramesPerRow = framesPerRow;
            NumberOfRows = numberOfRows;
        }
    }
}
