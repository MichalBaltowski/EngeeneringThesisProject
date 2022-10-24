using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetParamsManager : MonoBehaviour
{
    float mutationchance;
    private float mutationStrength;
    private Slider mutationChanceSlider;
    private Slider mutationStrengthSlider;
    private Text mutationChanceText;

    void Start() {
        mutationChanceSlider = GameObject.Find("MutationChanceSlider").GetComponent<Slider>();
        mutationChanceText = GameObject.Find("MutationChanceText").GetComponent<Text>();
        mutationChanceText.text = "0.10";
    }

    private void Update() {
        mutationchance = mutationChanceSlider.value;
        mutationStrength = mutationStrengthSlider.value; 
    }

    public void returnToMainMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }
}
