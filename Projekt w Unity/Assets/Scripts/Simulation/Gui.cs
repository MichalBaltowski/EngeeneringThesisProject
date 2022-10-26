using UnityEngine;
using UnityEngine.UI;

public class Gui 
{
    private Text generationNumberText;
    private Text timeText;
    private Text mutationChanceText;
    private Text mutationStrengthText;
    private Text populationText;
    private float time;

    public void initializeGui() {
        generationNumberText = GameObject.Find("GenerationNumberText").GetComponent<Text>();
        mutationChanceText = GameObject.Find("MutationChanceText").GetComponent<Text>();
        mutationStrengthText = GameObject.Find("MutationStrengthText").GetComponent<Text>();
        populationText = GameObject.Find("PopulationText").GetComponent<Text>();
        timeText = GameObject.Find("DurationTimeText").GetComponent<Text>();
    }

    public void updateGui() {
        displayStatsOnGUI();
        time += Time.deltaTime;
    }

    private void displayStatsOnGUI() {
        //var bestCarFitnessValue = geneticAlgorithm.getBestCar(carPopulationList).getFitnessValue();
        displayGeneration();
        displayMutationChance();
        displayMutationStrength();
        displayTime(time);
        displayPopulation();
    }

    private void displayGeneration() {
        generationNumberText.text = "Generation: " + ParametersDto.getGenerationNumber();
    }

    private void displayMutationChance() {
        mutationChanceText.text = "Mutation chance: " + ParametersDto.getMutationChance();
    }

    private void displayMutationStrength() {
        mutationStrengthText.text = "Mutation strength: " + ParametersDto.getMutationStrength();
    }

    private void displayTime(float timeToDisplay) {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void displayPopulation() {
        populationText.text = "Units alive: " + ParametersDto.getAliveUnitsNumber() + "/" + ParametersDto.getPopulationSize();
    }
}
