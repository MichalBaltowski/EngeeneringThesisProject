using UnityEngine;
using UnityEngine.UI;

public class CarLifeSpanScript : MonoBehaviour
{
    [SerializeField] Slider carLifeSpanSlider;
    [SerializeField] Text carLifeSpanText;

    void Start() {
        carLifeSpanSlider.onValueChanged.AddListener((v) => {
            carLifeSpanText.text = v.ToString("0");
        });
    }
}
