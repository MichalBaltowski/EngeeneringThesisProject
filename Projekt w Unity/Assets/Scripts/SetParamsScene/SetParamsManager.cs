using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetParamsManager : MonoBehaviour
{
    private Slider populationSizeSlider;
    private Slider mutationChanceSlider;
    private Slider mutationStrengthSlider;

    private Text populationSizeText;
    private Text mutationChanceText;
    private Text mutationStrengthText;

    void Start() {
        initializePopulationSizeInputs();
        initializeMutationChanceInputs();
        initializeMutationStrengthInputs();
    }

    void Update() {
        ParametersDto.setPopulationSize((int)populationSizeSlider.value);
        ParametersDto.setMutationChance(mutationChanceSlider.value);
        ParametersDto.setMutationStrength(mutationStrengthSlider.value);
    }

    public void returnToMainMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void initializePopulationSizeInputs() {
        populationSizeSlider = GameObject.Find("PopulationSizeSlider").GetComponent<Slider>();
        populationSizeText = GameObject.Find("PopulationSizeText").GetComponent<Text>();
        populationSizeSlider.value = ParametersDto.getPopulationSize();
        populationSizeText.text = ParametersDto.getPopulationSize().ToString();
    }

    private void initializeMutationChanceInputs() {
        mutationChanceSlider = GameObject.Find("MutationChanceSlider").GetComponent<Slider>();
        mutationChanceText = GameObject.Find("MutationChanceText").GetComponent<Text>();
        mutationChanceSlider.normalizedValue = ParametersDto.getMutationChance();
        mutationChanceText.text = ParametersDto.getMutationChance().ToString();
    }

    private void initializeMutationStrengthInputs() {
        mutationStrengthSlider = GameObject.Find("MutationStrengthSlider").GetComponent<Slider>();
        mutationStrengthText = GameObject.Find("MutationStrengthText").GetComponent<Text>();
        mutationStrengthSlider.normalizedValue = ParametersDto.getMutationStrength();
        mutationStrengthText.text = ParametersDto.getMutationStrength().ToString();
    }
}
