using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetParamsManager : MonoBehaviour {
    private Slider populationSizeSlider;
    private Slider mutationChanceSlider;
    private Slider mutationStrengthSlider;
    private Slider carLifeSpanSlider;
    private Slider carSensorLengthSlider;

    private Text populationSizeText;
    private Text mutationChanceText;
    private Text mutationStrengthText;
    private Text carLifeSpanText;
    private Text carSensorLengthText;

    private Toggle manualSteeringToggle;

    void Start() {
        initializePopulationSizeInputs();
        initializeMutationChanceInputs();
        initializeMutationStrengthInputs();
        initializeCarLifeSpanInput();
        initializeCarSensorLenghtInput();
        initializeManualSteeringInput();
    }

    void Update() {
        ParametersDto.setPopulationSize((int)populationSizeSlider.value);
        ParametersDto.setMutationChance(mutationChanceSlider.value);
        ParametersDto.setMutationStrength(mutationStrengthSlider.value);
        ParametersDto.setCarLifeSpan((int)carLifeSpanSlider.value);
        ParametersDto.setCarSensorsLength(carSensorLengthSlider.value);
        ParametersDto.setManualSteering(manualSteeringToggle.isOn);
    }

    public void returnToMainMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void initializePopulationSizeInputs() {
        populationSizeSlider = GameObject.Find("PopulationSizeSlider").GetComponent<Slider>();
        populationSizeText = GameObject.Find("PopulationSizeText").GetComponent<Text>();
        updatePopulationSizeOnScreen();
    }

    private void initializeMutationChanceInputs() {
        mutationChanceSlider = GameObject.Find("MutationChanceSlider").GetComponent<Slider>();
        mutationChanceText = GameObject.Find("MutationChanceText").GetComponent<Text>();
        updateMutationChanceOnScreen();
    }

    private void initializeMutationStrengthInputs() {
        mutationStrengthSlider = GameObject.Find("MutationStrengthSlider").GetComponent<Slider>();
        mutationStrengthText = GameObject.Find("MutationStrengthText").GetComponent<Text>();
        updateMutationStrengthOnScrene();
    }

    private void initializeCarLifeSpanInput() {
        carLifeSpanSlider = GameObject.Find("CarLifeSpanSlider").GetComponent<Slider>();
        carLifeSpanText = GameObject.Find("CarLifeSpanText").GetComponent<Text>();
        updateCarLifeSpanOnScreen();
    }

    private void initializeCarSensorLenghtInput() {
        carSensorLengthSlider = GameObject.Find("CarSensorLengthSlider").GetComponent<Slider>();
        carSensorLengthText = GameObject.Find("CarSensorLengthText").GetComponent<Text>();
        updateCarSensorsLengthOnScreen();
    }

    private void initializeManualSteeringInput() {
        manualSteeringToggle = GameObject.Find("ManualSteeringToggle").GetComponent<Toggle>();
        updateManualSteeringOnScreen();
    }

    public void setDefaultParameters() {
        ParametersDto.setDefault();
        updatePopulationSizeOnScreen();
        updateMutationChanceOnScreen();
        updateMutationStrengthOnScrene();
        updateCarLifeSpanOnScreen();
        updateCarSensorsLengthOnScreen();
        updateManualSteeringOnScreen();
    }

    private void updatePopulationSizeOnScreen() {
        populationSizeSlider.value = ParametersDto.getPopulationSize();
        populationSizeText.text = ParametersDto.getPopulationSize().ToString();
    }

    private void updateMutationChanceOnScreen() {
        mutationChanceSlider.normalizedValue = ParametersDto.getMutationChance();
        mutationChanceText.text = ParametersDto.getMutationChance().ToString();
    }

    private void updateMutationStrengthOnScrene() {
        mutationStrengthSlider.normalizedValue = ParametersDto.getMutationStrength();
        mutationStrengthText.text = ParametersDto.getMutationStrength().ToString();
    }

    private void updateCarLifeSpanOnScreen() {
        carLifeSpanSlider.value = ParametersDto.getCarLifeSpan();
        carLifeSpanText.text = ParametersDto.getCarLifeSpan().ToString();
    }

    private void updateCarSensorsLengthOnScreen() {
        carSensorLengthSlider.value = ParametersDto.getCarSensorsLength();
        carSensorLengthText.text = ParametersDto.getCarSensorsLength().ToString();
    }

    private void updateManualSteeringOnScreen() {
        manualSteeringToggle.isOn = ParametersDto.isManualSteering();
    }
}
