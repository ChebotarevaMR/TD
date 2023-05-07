using UnityEngine;
using UnityEngine.UI;

namespace Location {
    public class Location : MonoBehaviour {
        [SerializeField] private Image generationField;

        public void GenerateField(){
            Debug.Log($"Generate field begin");
            var texture = generationField.sprite.texture;

            texture.SetPixel(0, 0, new Color(1.0f, 1.0f, 1.0f, 0.5f));
            texture.SetPixel(1, 0, Color.clear);
            texture.SetPixel(0, 1, Color.white);
            texture.SetPixel(1, 1, Color.black);

            texture.Apply();

            Debug.Log($"Generate field end");
        }
    }
}