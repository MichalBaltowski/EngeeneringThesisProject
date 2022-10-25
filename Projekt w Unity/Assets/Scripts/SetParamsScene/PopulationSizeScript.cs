using UnityEngine;
using UnityEngine.UI;

public class PopulationSizeScript : MonoBehaviour
{
    [SerializeField] Slider populationSizeSlider;
    [SerializeField] Text populationSizeText;

    void Start() {
        populationSizeSlider.onValueChanged.AddListener((v) => {
            populationSizeText.text = v.ToString("0");
        });
    }
}
