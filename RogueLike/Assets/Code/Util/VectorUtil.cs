using UnityEngine;

namespace Assets.Code.Util
{
    public static class VectorUtil
    {
        public const float XToYMultiplier = 0.5f;

        public static Vector3 ToIsometricVector(this Vector3 vector)
        {
            vector.y *= XToYMultiplier;
            return vector;
        }
        public static Vector2 ToVector2(this Vector3 vector) => new(vector.x, vector.y);
        public static Vector2 ToIsometricVector(this Vector2 vector)
        {
            vector.y *= XToYMultiplier;
            return vector;
        }

        public static Vector2 ProjectOn(this Vector2 a, Vector2 b) => b.normalized * a.SizeOn(b);

        public static float SizeOn(this Vector2 a, Vector2 b)
        {
            float dotProduct = Vector2.Dot(a, b);
            float aLength = dotProduct / b.magnitude;
            return aLength;
        }

        public static Vector3Int ToVector3Int(this Vector2Int vector) => new(vector.x, vector.y);
        public static Bounds ToBounds(this BoundsInt bounds) => new(bounds.center, bounds.size);
        public static bool Intersects(this BoundsInt first, BoundsInt second)
        {
            if (second == default)
            {
                return false;
            }

            Bounds firstAsBounds = first.ToBounds();
            Bounds secondAsBounds = second.ToBounds();
            bool intersect = firstAsBounds.Intersects(secondAsBounds);
            return intersect;
        }

        public static float Distance(this BoundsInt from, BoundsInt to)
        {
            Bounds fromAsBounds = from.ToBounds();
            Bounds toAsBounds = to.ToBounds();
            Vector3 closestToCenter = fromAsBounds.ClosestPoint(toAsBounds.center);
            Vector3 closestToClosest = toAsBounds.ClosestPoint(closestToCenter);
            Vector3 difference = closestToClosest - closestToCenter;
            return difference.magnitude;
        }
        public static int Size2D(this BoundsInt sizeOf) => sizeOf.x * sizeOf.y;
    }
}