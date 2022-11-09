using UnityEngine;
using UnityEngine.UI;

public class CarSensorLengthScript : MonoBehaviour
{
    [SerializeField] Slider carSensorLengthSlider;
    [SerializeField] Text carSensorLengthText;

    void Start() {
        carSensorLengthSlider.onValueChanged.AddListener((v) => {
            carSensorLengthText.text = v.ToString("0");
        });
    }
}
