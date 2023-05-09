using UnityEngine;
using UnityEngine.UI;

namespace Location {
    public class Location : MonoBehaviour {

        [SerializeField] private Image generationField;

        [SerializeField] private int width;
        [SerializeField] private int height;

        public void GenerateField(){
            Debug.Log($"Generate field begin");
            TextureGenerator.Update(generationField.sprite.texture);
            Debug.Log($"Generate field end");
        }
    }
}