using Assets.Code.Util;
using System;
using UnityEngine;

namespace Assets.Code.DungeonGeneration
{
    [Serializable]
    public struct Room
    {
        public Vector2 OriginalCenter;
        public Vector2 Center;
        public int Width;
        public int Height;
        public int Size => Width * Height;

        public BoundsInt ToBoundsInt()
        {
            var centerInt = Vector2Int.RoundToInt(Center);
            int halfWidth = (int)Mathf.Floor(Width * 0.5f);
            int halfHeight = (int)Mathf.Floor(Height * 0.5f);
            return new(centerInt.x - halfWidth, centerInt.y - halfHeight, 0, Width, Height, 0);
        }

        public Room MoveAwayFromCenter()
        {
            Center += OriginalCenter;
            return this;
        }

        public static Room Random(int min, int max)
        {
            float dir = 2 * Mathf.PI * UnityEngine.Random.value;
            float x = Mathf.Cos(dir);
            float y = Mathf.Sin(dir);
            return new()
            {
                Center = new(x, y),
                Width = UnityEngine.Random.Range(min, max),
                Height = UnityEngine.Random.Range(min, max),
                OriginalCenter = new(x, y)
            };
        }

        public bool Intersects(Room with)
        {
            Bounds firstAsBounds = ToBoundsInt().ToBounds();
            Bounds secondAsBounds = with.ToBoundsInt().ToBounds();
            bool intersect = firstAsBounds.Intersects(secondAsBounds);
            return intersect;
        }
    }
}