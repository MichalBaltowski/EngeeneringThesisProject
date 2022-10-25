using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm {

    private float mutationChance;
    private float mutationStrength;
    private Car bestCar;

    public void setMutationChance(float value) {
        mutationChance = value;
    }

    public void setMutationStrength(float value) {
        mutationStrength = value;
    }

    //G��wna metoda wykorzystywana przy tworzeniu nowej populacji przez klas� Manager
    public void createNextPopulation(List<Car> previousGenCarlist, List<Car> nextGenCarList) {
        eliminateCarsWithLowFitnessValue(previousGenCarlist);
        copyNetworkFromPrevToNewGeneration(previousGenCarlist, nextGenCarList);
        mutateNewGeneration(nextGenCarList);
    }

    //sortuje liste samochod�w malej�co wzgl�dem warto�ci fitness
    private void sortCarListByFitnessValue(List<Car> unsortedCarList) {
        unsortedCarList.Sort((x, y) => y.getFitnessValue().CompareTo(x.getFitnessValue()));
    }

    //G��wna logika algorytmu genetycznego
    private void eliminateCarsWithLowFitnessValue(List<Car> previousGenCarlist) {
        sortCarListByFitnessValue(previousGenCarlist);
        replaceWorstCarsWithBestCars(previousGenCarlist);

        sortCarListByFitnessValue(previousGenCarlist);
        multiplyBestCarGen(previousGenCarlist);
    }

    // 40% populacji z najgorszym wynikiem fitness zostaje zast�pionych przez 
    // 40% populacji z najlepszym wynikiem fitness
    private void replaceWorstCarsWithBestCars(List<Car> previousGenCarlist) {
        int numberOfBestCars = setNumberOfBestCars(previousGenCarlist.Count);
        int lastIndex = previousGenCarlist.Count - 1;
        //Pobiera dane z najlepszych aut(pocz�tkowe indeksy) i wkleja je do najgorszych aut(ko�cowe indeks
        for (int i = 0; i < numberOfBestCars; i++) {
            NeuralNetworkData dataFromBestCars = previousGenCarlist[i].network.getNetworkData();
            previousGenCarlist[lastIndex - i].network.loadNewNetworkData(dataFromBestCars);
            previousGenCarlist[lastIndex - i].setFitnessValue((int)previousGenCarlist[i].getFitnessValue());
        }
    }

    //powiela 'gen' tj. obiekt o typie NeuralNetworkData najlepszego samochodu
    //tak by zosta� zreprodukowany na liczb� osobnik�w kt�ra stanowi 7% populacji
    private void multiplyBestCarGen(List<Car> previousGenCarlist) {
        NeuralNetworkData dataFromBestCar = previousGenCarlist[0].network.getNetworkData();
        int lastIndex = previousGenCarlist.Count - 1;
        for (int i = 0; i < previousGenCarlist.Count * 0.07; i++) {
            previousGenCarlist[lastIndex - i].network.loadNewNetworkData(dataFromBestCar);
            previousGenCarlist[lastIndex - i].setFitnessValue((int)previousGenCarlist[0].getFitnessValue());
        }
    }

    //skopiowanie m�dro�ci obiekt�w z poprzedniej generacji do kolekcji obiekt�w z nowej generacji
    private void copyNetworkFromPrevToNewGeneration(List<Car> previousGenCarlist, List<Car> nextGenCarList) {
        for (int i = 0; i < previousGenCarlist.Count; i++) {
            NeuralNetworkData data = previousGenCarlist[i].network.getNetworkData();
            nextGenCarList[i].network.loadNewNetworkData(data);
        }
    }

    //Wyznacza liczb� najlepszych aut. Je�li populacja jest ma�a to b�dzie to zawsze 2
    //Je�li jest wi�ksza to b�dzie to 40% populacji
    private int setNumberOfBestCars(int listCarCount) {
        if (listCarCount < 10) {
            return 2;
        } else {
            return (int)(listCarCount * 0.4);
        }
    }

    //mutacja wag wykonana na wszystkich osobnikach
    private void mutateNewGeneration(List<Car> nextGenCarList) {
        foreach (Car car in nextGenCarList) {
            car.network.mutate(mutationChance, mutationStrength);
        }
    }

    //wyznacza najlepszy pojazd w danej populacji w danym momencie
    //publiczna metoda wykorzystywana by na bie��co pokazywa� wwarto�� fitness najlepszego samochodu
    public Car getBestCar(List<Car> carList) {
        if (bestCar == null) {
            bestCar = carList[0];
        }
        foreach (Car car in carList) {
            if (car.getFitnessValue() > bestCar.getFitnessValue()) {
                bestCar = car;
            }
        }
        return bestCar;
    }

    //wypisuje liste samochod�w z ich wartosciami fitness
    //wykorzystanie tylko przy testach
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
