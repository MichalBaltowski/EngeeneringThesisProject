using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour {

    private Gui gui;
    private GeneticAlgorithm geneticAlgorithm;
    private GameObject spawner;
    private CameraScript cameraScript;
    public List<Car> carPopulationList;
    public Car car;

    private Text mutationChanceText;

    private GameObject popup;

    void Start() {
        setSpawner();
        initGeneticAlgorithm();
        initGUI();
        initPupulation();
        setEndSimulationPopup();
    }

    private void setEndSimulationPopup() {
        popup = GameObject.Find("EndSimulationPopup");
        mutationChanceText = GameObject.Find("PopulationSizeStatValueText").GetComponent<Text>();
       
        popup.SetActive(false);
        //StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine() {
        yield return new WaitForSeconds(3);
        Debug.Log("3 sekundy później");
        
    }

    private void setSpawner() {
        spawner = GameObject.FindGameObjectWithTag("Spawn");
    }
    private void initGeneticAlgorithm() {
        geneticAlgorithm = new GeneticAlgorithm();
        geneticAlgorithm.setMutationChance(ParametersDto.getMutationChance());
        geneticAlgorithm.setMutationStrength(ParametersDto.getMutationStrength());
    }
    private void initGUI() {
        gui = new Gui();
        gui.initializeGui();
    }
    public void initPupulation() {
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
        if (countAliveUnits() > 0) {
            return true;
        } else {
            return false;
        }
    }

    private int countAliveUnits() {
        int units = 0;
        carPopulationList.ForEach(car => {
            if (!car.collided) {
                units++;
            }
        });
        ParametersDto.setAliveUnitsNumber(units);
        return units;
    }

    public void endSimulationIfCarsGetsToMeta() {
        bool isSimulationOver = false;
        int howManyCarsEndedSimulation = 0;
        foreach (Car car in carPopulationList) {
            if (car.finishSimulation) {
                Debug.Log(car + "Skończył wyścig!");
                isSimulationOver = true;
                howManyCarsEndedSimulation++;
            }
        }
        if (isSimulationOver) {
            ParametersDto.setDuration(gui.getTime());
            SceneManager.LoadScene("EndSimulationScene");
            //new FileManager().writeScoreToFile(prepareDataToSave(howManyCarsEndedSimulation));

            //UnityEditor.EditorApplication.isPlaying = false;
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
