using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour {

    private GeneticAlgorithm geneticAlgorithm;
    private TextMeshProUGUI gui;
    private GameObject spawner;
    private CameraScript cameraScript;
    private int generation = 0;
    public List<Car> carPopulationList;
    public Car car;
    [Header("Population size")]
    public int populationSize;
    [Range(0f, 1f)]
    public float mutationChance = 0.05f;
    [Range(0f, 1f)]
    public float mutationStrength = 0.5f;
   
    void Start() {
        setSpawner();
        initGeneticAlgorithm();
        initGUI();
        initPupulation();
    }
    public void setSpawner() {
        spawner = GameObject.FindGameObjectWithTag("Spawn");
    }
    private void initGeneticAlgorithm() {
        geneticAlgorithm = new GeneticAlgorithm();
        geneticAlgorithm.setMutationChance(mutationChance);
        geneticAlgorithm.setMutationStrength(mutationStrength);
    }
    public void initGUI() {
        gui = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
    }
    public void initPupulation() {
        if (carPopulationList.Count == 0) {
            initializeNewPopulation();
        } else {
            initializeNextGeneration();
        }
    }
   
    //inicjalizuje nowa populacje
    public void initializeNewPopulation() {
        carPopulationList = new List<Car>();
        initializeCars(carPopulationList);
        generation++;
    }

    //inicjalizuje następną generacje
    //tworzy nowe pojazdy i przekopiowuje sieci ze starych aut do nowych
    //niszczy stare obiekty i zapisuje nowe auta do domyslnej listy
    public void initializeNextGeneration() {
        List<Car> nextGenCarList = new List<Car>();
        initializeCars(nextGenCarList);
        geneticAlgorithm.createNextPopulation(carPopulationList, nextGenCarList);
        destroyCarsFromPreviousGeneration();
        saveNewGenerationToDefaultList(nextGenCarList);
        generation++;
    }

    //Usunięcie obiektów i wyczyszczenie listy aby fizycznie samochody zniknęły z trasy
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

    private void initCameraMovement() {
        var cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        cameraScript = cameraObject.GetComponent<CameraScript>();
        cameraScript.setBestCar(carPopulationList[0]);
    }

    void Update() {
        if (ifPopulationExists()) {
            displayStatsOnGUI();
        } else {
            endSimulationIfCarsGetsToMeta();
            initPupulation();
        }
    }

    //zwraca true jesli wszystkie osobniki posiadają collided = true
    public bool ifPopulationExists() {
        foreach (Car car in carPopulationList) {
            if (!car.collided) {
                return true;
            }
        }
        return false;
    }

    private void endSimulationIfCarsGetsToMeta() {
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
            new FileManager().writeScoreToFile(prepareDataToSave(howManyCarsEndedSimulation));
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private DataForTxt prepareDataToSave(int howManyCarsEndedRace) {
        return DataForTxtBuilder.get()
            .withGenerationNumber(generation)
            .withNumberOfFinishingCar(howManyCarsEndedRace)
            .withPopulationSize(populationSize)
            .withMutationChance(mutationChance)
            .withMutationStrength(mutationStrength)
            .withTimeInSecondsSinceStartup((int)Time.realtimeSinceStartup)
            .createNewDataForTxt();
    }

    //inicjalizuje auta nadajac im imie. 
    //Parametr initWithNetwork decyduje czy nowy pojazd ma posiadac utworzony nowy obiekt sieci neuronowej
    public void initializeCars(List<Car> carList) {
        for (int i = 0; i < populationSize; i++) {
            Car clone = spawnNewCar();
            clone.name = "CAR" + i;
            clone.network = initNeuronalNetwork();
            carList.Add(clone);
        }
    }

    //fizycznie umieszcza pojazdy w punkcie start
    private Car spawnNewCar() {
        return Instantiate(car, spawner.transform.position, Quaternion.Euler(0, 0, 0));
    }

    //inicjalizuje siec skladajaca sie 
    //z pięciu neuronow w 1 warstwie, trzech w 2 warstwie i dwóch w 3 warstwie
    public static NeuralNetwork initNeuronalNetwork() {
        return new NeuralNetwork(new int[] { 5, 3, 2 });
    }

    public void displayStatsOnGUI() {
        var bestCarFitnessValue = geneticAlgorithm.getBestCar(carPopulationList).getFitnessValue();

        gui.text = "Generacja: " + generation;
        gui.text += "\nBest Car Fitness: " + bestCarFitnessValue;
    }

    private void updateBestCar() {
       // cameraScript.updateBestCar(geneticAlgorithm.getBestCar(carPopulationList));
    }

    //zmienia kolor pojazu w zaleznosci od stanu.
    public void changeCarsColour() {
        /*   bestCar.GetComponent<Renderer>().material.color = Color.green;
           foreach (Car car in carList) {
               if (car.collided) {
                   car.GetComponent<Renderer>().material.color = Color.clear;
               } else {
                   if (car != bestCar)
                       car.GetComponent<Renderer>().material.color = Color.blue;
               }
           }*/
    }

}
