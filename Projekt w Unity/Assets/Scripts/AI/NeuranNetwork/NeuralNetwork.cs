using System.Collections.Generic;

public class NeuralNetwork {
    private static int neuralNetCounter;
    private List<NnLayer> layersList;
    public string id;

    public NeuralNetwork(int[] layersArray) {
        neuralNetCounter++;
        this.id = "Network_" + neuralNetCounter;
        this.layersList = initializeLayers(layersArray);
        initializeWeights(layersList);
        setActivationFunctionOnLastNeuron();
    }

    private void initializeWeights(List<NnLayer> layersList) {
        //Neuron A ----weight---- Neuron B. Weight is property in neuron B
        for (int i = 1; i < layersList.Count; i++) {
            foreach (Neuron neuronInPresentLayer in layersList[i].neuronsList) {

                Dictionary<Neuron, float> neuronWeights = new Dictionary<Neuron, float>();
                for (int j = 0; j < layersList[i - 1].neuronsList.Count; j++) {
                    var neuronFromPreviousLayer = layersList[i - 1].neuronsList[j];
                    var randomWeightValue = StaticRandom.randomFloatNumberFromRange(-0.5f,0.5f);
                    neuronWeights.Add(neuronFromPreviousLayer, randomWeightValue);
                }
                neuronInPresentLayer.weights = neuronWeights;
            }
        }
    }

    private List<NnLayer> initializeLayers(int[] layersArray) {
        List<NnLayer> layersList = new List<NnLayer>();
        for (int i = 0; i < layersArray.Length; i++) {
            layersList.Add(initializeLayer(layersArray[i]));
        }
        return layersList;
    }

    private NnLayer initializeLayer(int numberOfNeurons) {
        return new NnLayer(numberOfNeurons);
    }

    private void setActivationFunctionOnLastNeuron() {
        getLastLayer().neuronsList[1].activationFunction = "BinaryStep";
    }

    public void giveDataToNetwork(float[] valueForNeuronsInFirstLayer) {
        int i = 0;
        foreach (Neuron neuron in this.layersList[0].neuronsList) {
            neuron.output = valueForNeuronsInFirstLayer[i];
            i++;
        }
    }

    public float[] feedForward() {
        for (int i = 1; i < layersList.Count; i++) {
            foreach (Neuron neuron in layersList[i].neuronsList) {
                float sum = neuron.calculateWeights();
                neuron.output = neuron.activate(sum);  
            }
        }
        return getNetworkOutput();
    }

    public float[] getNetworkOutput() {
        List<float> networkOutput = new List<float>();
        foreach (Neuron neuron in getLastLayer().neuronsList) {
            networkOutput.Add(neuron.output);
        }
        return networkOutput.ToArray();
    }

    public void mutate(float mutationChance, float mutationStrength) {
        for (int i = 1; i < layersList.Count; i++) {
            foreach (Neuron neuron in layersList[i].neuronsList) {
                neuron.mutateWeights(mutationChance, mutationStrength);
            }
        }
    }

    public NeuralNetworkData getNetworkData() {
        NeuralNetworkData data = new NeuralNetworkData();
        data.setWeights(copyWeightsFromNetwork());
        return data;
    }

    public void loadNewNetworkData(NeuralNetworkData data) {
        loadWeightsFromOtherNetwork(data.getWeights());
    }


    private List<List<List<float>>> copyWeightsFromNetwork() {
        List<List<List<float>>> weights = new List<List<List<float>>>();

        weights.Add(new List<List<float>>());//Added empty record as a first network layer 
        for (int i = 1; i < layersList.Count; i++) {
            List<List<float>> subNeuronsList = new List<List<float>>();
            foreach (Neuron neuronInPresentLayer in layersList[i].neuronsList) {
                List<float> subNeuronWeightsList = new List<float>();
                for (int j = 0; j < layersList[i - 1].neuronsList.Count; j++) {
                    var neuronFromPreviousLayer = layersList[i - 1].neuronsList[j];
                    var weight = neuronInPresentLayer.weights[neuronFromPreviousLayer];
                    subNeuronWeightsList.Add(weight);
                }
                subNeuronsList.Add(subNeuronWeightsList);
            }
            weights.Add(subNeuronsList);
        }
        return weights;
    }

    private void loadWeightsFromOtherNetwork(List<List<List<float>>> weights) {
        for (int i = 1; i < layersList.Count; i++) {
            for (int j = 0; j < layersList[i].neuronsList.Count; j++) {
                for (int z = 0; z < layersList[i - 1].neuronsList.Count; z++) {
                    var neuronFromPreviousLayer = layersList[i - 1].neuronsList[z];
                    layersList[i].neuronsList[j].weights[neuronFromPreviousLayer] = weights[i][j][z];
                }
            }
        }
    }
    private NnLayer getLastLayer() {
        return layersList[layersList.Count - 1];
    }


    //test use only
    private void showCopiedWeights(List<List<List<float>>> weights) {
        //Console.WriteLine("Wyświetlanie skopiowanych wag ");
        for (int i = 0; i < weights.Count; i++) {
            //Console.WriteLine("Warstwa nr " + i);
            for (int j = 0; j < weights[i].Count; j++) {
                //Console.WriteLine("Neuron nr " + j);
                for (int z = 0; z < weights[i][j].Count; z++) {
                    //Console.WriteLine("Wartość - " + weights[i][j][z]);
                }
            }
        }
    }

    //test use only
    private void showWeights() {
        //Console.WriteLine("Wyświetlanie wag sieci" + this.id);
        for (int i = 1; i < layersList.Count; i++) {
            //Console.WriteLine("Warstwa nr " + i);
            foreach (Neuron neuronInPresentLayer in layersList[i].neuronsList) {
                //Console.WriteLine("Neuron " + neuronInPresentLayer.name);
                for (int j = 0; j < layersList[i - 1].neuronsList.Count; j++) {
                    var neuronFromPreviousLayer = layersList[i - 1].neuronsList[j];
                    var weight = neuronInPresentLayer.weights[neuronFromPreviousLayer];
                    //Console.WriteLine("Waga do neuronu " + neuronFromPreviousLayer.name + ". Wartość wagi: " + weight);
                }
            }
        }
    }

}
