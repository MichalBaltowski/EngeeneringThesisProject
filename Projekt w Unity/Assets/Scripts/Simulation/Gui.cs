using UnityEngine;
using UnityEngine.UI;

public class Gui {
    private Text generationNumberText;
    private Text timeText;
    private Text mutationChanceText;
    private Text mutationStrengthText;
    private Text populationText;
    private Button stopSpeedButton;
    private Button normalSpeedButton;
    private Button fasterSpeedButton;
    private Button fastestSpeedButton;
    private float time;
    private TimeType selectedTimeSpeed;

    public void initializeGui() {
        initText();
        initSpeedControllers();
    }

    private void initText() {
        generationNumberText = GameObject.Find("GenerationNumberText").GetComponent<Text>();
        mutationChanceText = GameObject.Find("MutationChanceText").GetComponent<Text>();
        mutationStrengthText = GameObject.Find("MutationStrengthText").GetComponent<Text>();
        populationText = GameObject.Find("PopulationText").GetComponent<Text>();
        timeText = GameObject.Find("DurationTimeText").GetComponent<Text>();
    }

    private void initSpeedControllers() {
        initButtons();
        setBehaviour();
    }

    private void initButtons() {
        stopSpeedButton = GameObject.Find("StopSpeedButton").GetComponent<Button>();
        normalSpeedButton = GameObject.Find("NormalSpeedButton").GetComponent<Button>();
        fasterSpeedButton = GameObject.Find("FasterSpeedButton").GetComponent<Button>();
        fastestSpeedButton = GameObject.Find("FastestSpeedButton").GetComponent<Button>();
    }

    private void setBehaviour() {
        stopSpeedButton.onClick.AddListener(() => setTimeType(TimeType.STOP));
        normalSpeedButton.onClick.AddListener(() => setTimeType(TimeType.NORMAL));
        fasterSpeedButton.onClick.AddListener(() => setTimeType(TimeType.FASTER));
        fastestSpeedButton.onClick.AddListener(() => setTimeType(TimeType.FASTEST));

        normalSpeedButton.Select();
    }

    public void updateGui() {
        displayStatsOnGUI();
        time += Time.deltaTime;
    }

    private void displayStatsOnGUI() {
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

    private void setTimeType(TimeType newTimeType) {
        if (selectedTimeSpeed != newTimeType) {
            setNewTimeType(newTimeType);
        }
    }
    private void setNewTimeType(TimeType newTimeType) {
        Time.timeScale = ((int)newTimeType);
        selectedTimeSpeed = newTimeType;
    }
}

enum TimeType : int {
    STOP = 0,
    NORMAL = 1,
    FASTER = 3,
    FASTEST = 5
}

