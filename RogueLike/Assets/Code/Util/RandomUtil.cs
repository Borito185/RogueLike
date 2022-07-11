using UnityEngine;

namespace Assets.Code.Util
{
    public static class RandomUtil
    {
        public static Vector2Int RandomVector2Int(int min, int max) => 
            RandomVector2Int(min, max, min, max);
        public static Vector2Int RandomVector2Int(int minX, int maxX, int minY, int maxY)
        {
            int x = Random.Range(minX, maxX);
            int y = Random.Range(minY, maxY);
            return new(x, y);
        }
    }
}