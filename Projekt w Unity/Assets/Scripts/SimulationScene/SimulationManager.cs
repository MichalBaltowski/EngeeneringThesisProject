using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationManager : MonoBehaviour {

    private Gui gui;
    private GeneticAlgorithm geneticAlgorithm;
    private GameObject spawner;
    public List<Car> carPopulationList;
    public Car car;

    void Start() {
        setSpawner();
        initGUI();
        initGeneticAlgorithm();
        initPupulation();
    }

    public void returnToMainManu() {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void setSpawner() {
        spawner = GameObject.FindGameObjectWithTag("Spawn");
    }
    private void initGeneticAlgorithm() {
        geneticAlgorithm = new GeneticAlgorithm(ParametersDto.getMutationChance(), ParametersDto.getMutationStrength());
    }
    private void initGUI() {
        gui = new Gui();
        gui.initializeGui();
    }
    private void initPupulation() {
        if (carPopulationList.Count == 0) {
            initializeNewPopulation();
        } else {
            initializeNextGeneration();
        }
    }

    private void initializeNewPopulation() {
        carPopulationList = new List<Car>();
        initializeCars(carPopulationList);
        ParametersDto.incrementGenerationNumber();
    }

    private void initializeNextGeneration() {
        List<Car> nextGenCarList = new List<Car>();
        initializeCars(nextGenCarList);
        geneticAlgorithm.createNextPopulation(carPopulationList, nextGenCarList);
        destroyCarsFromPreviousGeneration();
        saveNewGenerationToDefaultList(nextGenCarList);
        ParametersDto.incrementGenerationNumber();
    }

    private void destroyCarsFromPreviousGeneration() {
        for (int i = 0; i < carPopulationList.Count; i++) {
            GameObject.Destroy(carPopulationList[i].gameObject);
        }
        carPopulationList.Clear();
    }

    private void saveNewGenerationToDefaultList(List<Car> newGenerationCarList) {
        for (int i = 0; i < newGenerationCarList.Count; i++) {
            carPopulationList.Add(newGenerationCarList[i]);
        }
        newGenerationCarList.Clear();
    }

    void Update() {
        if (ifPopulationExists()) {
            gui.updateGui();
        } else {
            endSimulationIfCarsGetsToMeta();
            initPupulation();
        }
    }

    private bool ifPopulationExists() {
        return countAliveUnits() > 0;
    }

    private int countAliveUnits() {
        int units = 0;
        carPopulationList.ForEach(car => {
            if (!car.eliminated) {
                units++;
            }
        });
        ParametersDto.setAliveUnitsNumber(units);
        return units;
    }

    public void endSimulationIfCarsGetsToMeta() {
        bool isSimulationOver = false;
        foreach (Car car in carPopulationList) {
            if (car.finishSimulation) {
                isSimulationOver = true;
            }
        }
        if (isSimulationOver) {
            setParamsInDto();
            SceneManager.LoadScene("EndSimulationScene");
        }
    } 

    private void setParamsInDto() {
        ParametersDto.setDuration(gui.getTime());
    }

    private void initializeCars(List<Car> carList) {
        for (int i = 0; i < ParametersDto.getPopulationSize(); i++) {
            Car clone = spawnNewCar();
            clone.name = "CAR" + i;
            clone.network = initNeuronalNetwork();
            carList.Add(clone);
        }
    }

    private Car spawnNewCar() {
        return Instantiate(car, spawner.transform.position, Quaternion.Euler(0, 0, 0));
    }

    //inicjalizuje siec skladajaca sie 
    //z pięciu neuronow w 1 warstwie, trzech w 2 warstwie i dwóch w 3 warstwie
    private static NeuralNetwork initNeuronalNetwork() {
        return new NeuralNetwork(new int[] { 5, 3, 2 });
    }
}
