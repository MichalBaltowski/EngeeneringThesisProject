using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour {

    private PopulationManager populationManager;
    private GuiManager gui;
    private GameObject spawner;
    private CameraScript cameraScript;
    public List<Car> carsPopulationList;

    void Start() {
        setSpawner();
        initGui();
        initPupulation();
        spawnCars();
    }

    private void setSpawner() {
        spawner = GameObject.FindGameObjectWithTag("Spawn");
    }

    private void initGui() {
        gui = new GuiManager();
        gui.initializeGui();
    }

    private void initPupulation() {
        populationManager = new PopulationManager();
        carsPopulationList = populationManager.initializeNewPopulation();
    }

    private void spawnCars() {
       
    }

    void Update() {
        if (populationManager.ifPopulationExists()) {
            gui.updateGui();
        } else {
            endRaceIfCarsGetsToMeta();
            carsPopulationList = populationManager.initializeNextGeneration(carsPopulationList);
        }
    }

    private void endRaceIfCarsGetsToMeta() {
        bool isSimulationOver = false;
        int howManyCarsEndedSimulation = 0;
        foreach (Car car in carsPopulationList) {
            if (car.finishSimulation) {
                Debug.Log(car + "Skończył wyścig!");
                isSimulationOver = true;
                howManyCarsEndedSimulation++;
            }
        }
        if (isSimulationOver) {
            new FileManager().writeScoreToFile(prepareDataToSave(howManyCarsEndedSimulation));
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private DataForTxt prepareDataToSave(int howManyCarsEndedRace) {
        return DataForTxtBuilder.get()
            .withGenerationNumber(ParametersDto.getGenerationNumber())
            .withNumberOfFinishingCar(howManyCarsEndedRace)
            .withPopulationSize(ParametersDto.getPopulationSize())
            .withMutationChance(ParametersDto.getMutationChance())
            .withMutationStrength(ParametersDto.getMutationStrength())
            .withTimeInSecondsSinceStartup((int)Time.realtimeSinceStartup)
            .createNewDataForTxt();
    }
}
