using UnityEngine;
using UnityEngine.UI;

namespace Gui {
    public class Parallax : MonoBehaviour {
        private const float Border = 3400;
        [SerializeField] private float speed = 1;
        [SerializeField] private Image background;

        private void Update(){
            var pos = background.rectTransform.anchoredPosition.x;
            pos -= speed;
            if (pos < 0){
                pos = Border;
            }

            background.rectTransform.anchoredPosition = new Vector2(pos, background.rectTransform.anchoredPosition.y);

        }
    }
}
