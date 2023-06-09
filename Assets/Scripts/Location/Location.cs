using UnityEngine;
using UnityEngine.UI;

namespace Location {
    public class Location : MonoBehaviour {

        [SerializeField] private Image generationField;

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private int seed;

        public void GenerateField(){
            Debug.Log($"Generate field begin");
            TextureGenerator.Update(generationField.sprite.texture, width, height, seed);
            Debug.Log($"Generate field end");
        }
    }
}