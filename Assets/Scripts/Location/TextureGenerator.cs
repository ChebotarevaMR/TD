using System.Linq;
using UnityEngine;

namespace Location {
    public static class TextureGenerator
    {
        public static void Update(Texture2D texture, int width, int height, int seed)
        {
            int dx = texture.width / width;
            int dy = texture.height / height;
            var size = Mathf.Min(dx, dy);
            int padX = (texture.width - size * width) / 2;
            int padY = (texture.height - size * height) / 2;

            // map as object:
            // 

            var map = new MapBuilder(width, height)
                .SetSeed(seed)
                .AddColors(Color.green)
                .AttemptBuild();


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var color = map.GetCellColor(x, y);
                    var colors = Enumerable.Repeat(color, size * size).ToArray();
                    texture.SetPixels32(padX + x * size, padY + y * size, size, size, colors);
                }
            }

            var pathColor = new Color32(255, 255, 255, 0);
            foreach (var path in map.Paths)
            {
                foreach (var pt in path)
                {
                    var colors = Enumerable.Repeat(pathColor, size * size).ToArray();
                    texture.SetPixels32(padX + pt.X * size, padY + pt.Y * size, size, size, colors);
                }
            }

            texture.Apply();
        }
    }
}