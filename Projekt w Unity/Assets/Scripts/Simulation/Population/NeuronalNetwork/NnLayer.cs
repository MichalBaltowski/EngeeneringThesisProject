using System.Collections.Generic;

public class NnLayer {
    public List<Neuron> neuronsList;

    public NnLayer(int numberOfNeuron) {
        this.neuronsList = new List<Neuron>();
        initializeNeurons(numberOfNeuron);
    }

    public void initializeNeurons(int numberOfNeuron) {
        for (int i = 0; i < numberOfNeuron; i++) {
            this.neuronsList.Add(initializeNeuron());
        }
    }

    public Neuron initializeNeuron() {
        return new Neuron();
    }
}
