using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSimulationManager : MonoBehaviour {

    private Text generationText;
    private Text durationText;
    private Text populationSizeText;
    private Text mutationChanceText;
    private Text mutationStrengthText;


    void Start() {
        initializeTextElements();
        initializeTextValue();
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
    }

    private void initializeTextValue() {
        generationText.text = ParametersDto.getGenerationNumber().ToString();
        durationText.text = ParametersDto.getDuration().ToString();
        mutationChanceText.text = ParametersDto.getPopulationSize().ToString();
        mutationStrengthText.text = ParametersDto.getPopulationSize().ToString();
        populationSizeText.text = ParametersDto.getPopulationSize().ToString();

    }
}
