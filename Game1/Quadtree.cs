using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Quadtree
    {
        private int _MaxObjects = 50;
        private int _MaxLevels = 5;
        private int _Level;
        private List<Object> _Objects;
        public Rectangle _Bounds;
        public Quadtree[] _Nodes { get; private set; }

        public Quadtree(int level, Rectangle bounds)
        {
            _Level = level;
            _Bounds = bounds;
            _Objects = new List<object>();
            _Nodes = new Quadtree[4];
        }

        public void CreateTest()
        {
            int subWidth = (int)(_Bounds.Width / 2);
            int subHeight = (int)(_Bounds.Height / 2);
            int x = (int)_Bounds.X;
            int y = (int)_Bounds.Y;

            _Nodes[0] = new Quadtree(_Level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            _Nodes[1] = new Quadtree(_Level + 1, new Rectangle(x, y, subWidth, subHeight));
            _Nodes[2] = new Quadtree(_Level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            _Nodes[3] = new Quadtree(_Level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        public void Clear()
        {
            _Objects.Clear();

            for (int i = 0; i < _Nodes.Length; i++)
            {
                if (_Nodes[i] != null)
                {
                    _Nodes[i].Clear();
                    _Nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            int subWidth = (int)(_Bounds.Width / 2);
            int subHeight = (int)(_Bounds.Height / 2);
            int x = (int)_Bounds.X;
            int y = (int)_Bounds.Y;

            _Nodes[0] = new Quadtree(_Level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            _Nodes[1] = new Quadtree(_Level + 1, new Rectangle(x, y, subWidth, subHeight));
            _Nodes[2] = new Quadtree(_Level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            _Nodes[3] = new Quadtree(_Level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        /*
        * Determine which node the object belongs to. -1 means
        * object cannot completely fit within a child node and is part
        * of the parent node
        */
        private int GetIndex(Rectangle rect)
        {
            int index = -1;
            double verticalMidpoint = _Bounds.X + (_Bounds.Width / 2);
            double horizontalMidpoint = _Bounds.Y + (_Bounds.Height / 2);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (rect.Y < horizontalMidpoint && rect.Y + rect.Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (rect.Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
            if (rect.X < verticalMidpoint && rect.X + rect.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // Object can completely fit within the right quadrants
            else if (rect.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        /*
        * Insert the object into the quadtree. If the node
        * exceeds the capacity, it will split and add all
        * objects to their corresponding nodes.
        */
        public void Insert(Rectangle rect)
        {
            if (_Nodes[0] != null)
            {
                int index = GetIndex(rect);

                if (index != -1)
                {
                    _Nodes[index].Insert(rect);

                    return;
                }
            }

            _Objects.Add(rect);

            if (_Objects.Count() > _MaxObjects && _Level < _MaxLevels)
            {
                if (_Nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < _Objects.Count())
                {
                    int index = GetIndex((Rectangle) _Objects[i]);
                    if (index != -1)
                    {
                        _Nodes[index].Insert((Rectangle)_Objects[i]);
                        _Objects.Remove(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        /*
        * Return all objects that could collide with the given object
        */
        public List<Object> Retrieve(List<Object> returnObjects, Rectangle rect)
        {
            int index = GetIndex(rect);
            if (index != -1 && _Nodes[0] != null)
            {
                _Nodes[index].Retrieve(returnObjects, rect);
            }

            returnObjects.AddRange(returnObjects);

            return returnObjects;
        }
    }
}