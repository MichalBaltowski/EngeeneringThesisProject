using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour {

    private GeneticAlgorithm geneticAlgorithm;

    public PopulationManager() {
        initGeneticAlgorithm();
    }

    public List<Car> initializeNewPopulation() {
        List<Car> carsPopulationList = new List<Car>();
        initializeCars(carsPopulationList);

        ParametersDto.incrementGenerationNumber();
        return carsPopulationList;
    }

    public List<Car> initializeNextGeneration(List<Car> previousGenerationCars) {
        List<Car> nextGenCarList = new List<Car>();
        initializeCars(nextGenCarList);
        geneticAlgorithm.createNextPopulation(previousGenerationCars, nextGenCarList);
        destroyCarsFromPreviousGeneration();
        saveNewGenerationToDefaultList(nextGenCarList);

        ParametersDto.incrementGenerationNumber();
        return nextGenCarList;
    }

    public bool ifPopulationExists() {
        if (countAliveUnits() > 0) {
            return true;
        } else {
            return false;
        }
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




    private void initGeneticAlgorithm() {
        geneticAlgorithm = new GeneticAlgorithm();
        geneticAlgorithm.setMutationChance(ParametersDto.getMutationChance());
        geneticAlgorithm.setMutationStrength(ParametersDto.getMutationStrength());
    }

    //inicjalizuje siec skladajaca sie 
    //z piêciu neuronow w 1 warstwie, trzech w 2 warstwie i dwóch w 3 warstwie
    private static NeuralNetwork initNeuronalNetwork() {
        return new NeuralNetwork(new int[] { 5, 3, 2 });//przerobiæ na intuicyjny sposob inicjacji
    }

    private int countAliveUnits() {
        int units = 0;
        carsPopulationList.ForEach(car => {
            if (!car.collided) {
                units++;
            }

        });

        ParametersDto.setAliveUnitsNumber(units);
        return units;
    }

   

   


    private void destroyCarsFromPreviousGeneration() {
        for (int i = 0; i < carsPopulationList.Count; i++) {
            GameObject.Destroy(carsPopulationList[i].gameObject);
        }
        carsPopulationList.Clear();
    }

    private void saveNewGenerationToDefaultList(List<Car> newGenerationCarList) {
        for (int i = 0; i < newGenerationCarList.Count; i++) {
            carsPopulationList.Add(newGenerationCarList[i]);
        }
        newGenerationCarList.Clear();
    }



}
