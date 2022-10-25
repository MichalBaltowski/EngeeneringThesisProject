using UnityEngine;
using UnityEngine.UI;

public class MutationStrenghtScript : MonoBehaviour
{
    [SerializeField] Slider mutataionStrengthSlider;
    [SerializeField] Text mutationStrengthText;

    void Start() {
        mutataionStrengthSlider.onValueChanged.AddListener((v) => {
            mutationStrengthText.text = v.ToString("0.00");
        });
    }
}
