using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSimulationManager : MonoBehaviour {

    private Text generationText;
    private Text durationText;
    private Text populationSizeText;
    private Text mutationChanceText;
    private Text mutationStrengthText;
    private Text carsSpanLifeText;
    private Text carsSensorsLengthText;
    private Text manualSteeringText;

    void Start() {
        initializeTextElements();
        initializeTextValue();
        saveParametersInFile();
    }

    public void returnToMainManu() {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void initializeTextElements() {
        generationText = GameObject.Find("GenerationText").GetComponent<Text>();
        durationText = GameObject.Find("DurationText").GetComponent<Text>();
        mutationChanceText = GameObject.Find("MutationChanceText").GetComponent<Text>();
        mutationStrengthText = GameObject.Find("MutationStrengthText").GetComponent<Text>();
        populationSizeText = GameObject.Find("PopulationSizeText").GetComponent<Text>();
        carsSpanLifeText = GameObject.Find("CarsSpanLifeText").GetComponent<Text>();
        carsSensorsLengthText = GameObject.Find("CarsSensorsLengthText").GetComponent<Text>();
        manualSteeringText = GameObject.Find("ManualSteeringText").GetComponent<Text>();
    }

    private void initializeTextValue() {
        generationText.text = ParametersDto.getGenerationNumber().ToString();
        durationText.text = ParametersDto.getDuration().ToString();
        mutationChanceText.text = ParametersDto.getMutationChance().ToString();
        mutationStrengthText.text = ParametersDto.getMutationStrength().ToString();
        populationSizeText.text = ParametersDto.getPopulationSize().ToString();
        carsSpanLifeText.text = ParametersDto.getCarLifeSpan().ToString(); ;
        carsSensorsLengthText.text = ParametersDto.getCarSensorsLength().ToString();
        manualSteeringText.text = ParametersDto.isManualSteering().ToString();
    }

    private void saveParametersInFile() {
        new FileManager().writeScoreToFile(prepareDataToSave());
    }

    private DataForTxt prepareDataToSave() {
        return DataForTxtBuilder.get()
            .withGenerationNumber(ParametersDto.getGenerationNumber())
            .withDuration(ParametersDto.getDuration())
            .withPopulationSize(ParametersDto.getPopulationSize())
            .withMutationChance(ParametersDto.getMutationChance())
            .withMutationStrength(ParametersDto.getMutationStrength())
            .withCarLifeSpan(ParametersDto.getCarLifeSpan())
            .withCarSensorLength(ParametersDto.getCarSensorsLength())
            .withManualSteering(ParametersDto.isManualSteering())
            .createNewDataForTxt();
    }
}
