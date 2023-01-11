using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public static class AsteroidHelper
    {
        private static readonly Dictionary<int, Color> asteroidColors = new Dictionary<int, Color>()
        {
            {1, Color.blue},
            {2, Color.green},
            {3, Color.red},
            {4, Color.grey}
        };

        public static Color SetColor(int colorIndex)
        {
            return asteroidColors.ContainsKey(colorIndex) ? asteroidColors[colorIndex] : Color.white;
        }
    }
}