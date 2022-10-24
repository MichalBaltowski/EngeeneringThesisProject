using UnityEngine;
using UnityEngine.UI;

public class MutationChanceScript : MonoBehaviour
{
    [SerializeField] Slider mutataionChanceSlider;
    [SerializeField] Text mutationChanceText;
  
    void Start()
    {
        mutataionChanceSlider.onValueChanged.AddListener((v) => {
            mutationChanceText.text = v.ToString("0.00");
        });
    }
}
