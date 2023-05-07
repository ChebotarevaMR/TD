using UnityEngine;

namespace Location {
    public static class TextureGenerator {
        public static void Update(Texture2D texture){
            int size = 200;
            int width = texture.width;
            int xMin = width/2 - size/2;
            int xMax = width/2 + size/2;
            var height = texture.height;
            int yMin= height/2 - size/2;
            int yMax = height/2 + size/2;

            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    texture.SetPixel(x, y, Color.white);
                }
            }
            
            for (int x = xMin; x < xMax; x++){
                for (int y = yMin; y < yMax; y++){
                    texture.SetPixel(x, y, Color.black);
                }
            }
            
            texture.Apply();
        }
    }
}