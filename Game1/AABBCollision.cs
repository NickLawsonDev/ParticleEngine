using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class AABBCollision
    {
        public bool CheckCollision(Rectangle rec1, Rectangle rec2)
        {
            if (rec1.X < rec2.X + rec2.Width &&
               rec1.X + rec1.Width > rec2.X &&
               rec1.Y < rec2.Y + rec2.Height &&
               rec1.Height < rec1.Y + rec2.Y) return true;
            else
                return false;
        }
    }
}
