using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm {

    private float mutationChance;
    private float mutationStrength;
    private Car bestCar;

    public GeneticAlgorithm(float mutationChance, float mutationStrength) {
        this.mutationChance = mutationChance;
        this.mutationStrength = mutationStrength;
    }

    public void createNextPopulation(List<Car> previousGenCarlist, List<Car> nextGenCarList) {
        eliminateCarsWithLowFitnessValue(previousGenCarlist);
        copyNetworkFromPrevToNewGeneration(previousGenCarlist, nextGenCarList);
        mutate(nextGenCarList);
    }
    private void eliminateCarsWithLowFitnessValue(List<Car> previousGenCarlist) {
        sortCarListByFitnessValue(previousGenCarlist);
        replaceWorstCarsWithBestCars(previousGenCarlist);

        sortCarListByFitnessValue(previousGenCarlist);
        multiplyBestCarGen(previousGenCarlist);
    }

    private void sortCarListByFitnessValue(List<Car> unsortedCarList) {
        unsortedCarList.Sort((x, y) => y.getFitnessValue().CompareTo(x.getFitnessValue()));
    }

    // 40% populacji z najgorszym wynikiem fitness zostaje zast¹pionych przez 
    // 40% populacji z najlepszym wynikiem fitness
    private void replaceWorstCarsWithBestCars(List<Car> previousGenCarlist) {
        int numberOfBestCars = setNumberOfBestCars(previousGenCarlist.Count);
        int lastIndex = previousGenCarlist.Count - 1;
        //Pobiera dane z najlepszych aut(pocz¹tkowe indeksy) i wkleja je do najgorszych aut(koñcowe indeks
        for (int i = 0; i < numberOfBestCars; i++) {
            NeuralNetworkData dataFromBestCars = previousGenCarlist[i].network.getNetworkData();
            previousGenCarlist[lastIndex - i].network.loadNewNetworkData(dataFromBestCars);
            previousGenCarlist[lastIndex - i].setFitnessValue((int)previousGenCarlist[i].getFitnessValue());
        }
    }

    private int setNumberOfBestCars(int listCarCount) {
        if (listCarCount < 10) {
            return 2;
        } else {
            return (int)(listCarCount * 0.4);
        }
    }

    //powiela 'gen' tj. obiekt o typie NeuralNetworkData najlepszego samochodu
    //tak by zosta³ zreprodukowany na liczbê osobników która stanowi 7% populacji
    private void multiplyBestCarGen(List<Car> previousGenCarlist) {
        NeuralNetworkData dataFromBestCar = previousGenCarlist[0].network.getNetworkData();
        int lastIndex = previousGenCarlist.Count - 1;
        for (int i = 0; i < previousGenCarlist.Count * 0.07; i++) {
            previousGenCarlist[lastIndex - i].network.loadNewNetworkData(dataFromBestCar);
            previousGenCarlist[lastIndex - i].setFitnessValue((int)previousGenCarlist[0].getFitnessValue());
        }
    }

    //skopiowanie m¹droœci obiektów z poprzedniej generacji do kolekcji obiektów z nowej generacji
    private void copyNetworkFromPrevToNewGeneration(List<Car> previousGenCarlist, List<Car> nextGenCarList) {
        for (int i = 0; i < previousGenCarlist.Count; i++) {
            NeuralNetworkData data = previousGenCarlist[i].network.getNetworkData();
            nextGenCarList[i].network.loadNewNetworkData(data);
        }
    }

    private void mutate(List<Car> nextGenCarList) {
        foreach (Car car in nextGenCarList) {
            car.network.mutate(mutationChance, mutationStrength);
        }
    }

    //Test use only
    private void writeCarList(List<Car> tempListCar, bool special) {
        int i = 0;
        foreach (Car car in tempListCar) {
            if (special) {
                Debug.Log(i + ". " + car.name + "____Fitness:" + car.getFitnessValue());
            } else {
                Debug.Log(i + ". " + car.name);
            }
            i++;
        }
    }
}
