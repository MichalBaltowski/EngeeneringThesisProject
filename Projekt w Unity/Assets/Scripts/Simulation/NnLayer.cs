using System.Collections.Generic;

public class NnLayer {
    //Każda warstwa posiada listę neuronów
    public List<Neuron> neuronsList;

    //konstruktor
    public NnLayer(int numberOfNeuron) {
        this.neuronsList = new List<Neuron>();
        initializeNeurons(numberOfNeuron);
    }

    //inicjalizuje neurony
    public void initializeNeurons(int numberOfNeuron) {
        for (int i = 0; i < numberOfNeuron; i++) {
            this.neuronsList.Add(initializeNeuron());
        }
    }

    //inicjalizuje pojedynczy neuron
    public Neuron initializeNeuron() {
        return new Neuron();
    }
}
