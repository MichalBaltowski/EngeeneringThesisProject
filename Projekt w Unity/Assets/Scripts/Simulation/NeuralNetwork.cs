using System.Collections.Generic;

public class NeuralNetwork {
    //licznik wykorzystywany do nadawania id sieci
    private static int neuralNetCounter;
    //Obiekt sieć neuronowa posiada listę w której przechowywane są obiekty o typie warstwa
    private List<NnLayer> layersList;
    public string id;


    //konstruktor
    public NeuralNetwork(int[] layersArray) {
        neuralNetCounter++;
        this.id = "Network_" + neuralNetCounter;
        this.layersList = initializeLayers(layersArray);
        initializeWeights(layersList);
        setActivationFunctionOnLastNeuron();
    }

    //Inicjalizuje wartości wag dla wszystkich neuronów w sieci, zaczynając od drugiej warstwy
    private void initializeWeights(List<NnLayer> layersList) {
        //Neuron A ----waga---- Neuron B. Założyłem że waga będzie własnością neuronu b,
        //dlatego iteruje od drugiej warstwy, ponieważ neurony w pierwszej nie posiadają wag
        //do wartości wejściowych(input)
        for (int i = 1; i < layersList.Count; i++) {
            foreach (Neuron neuronInPresentLayer in layersList[i].neuronsList) {

                Dictionary<Neuron, float> neuronWeights = new Dictionary<Neuron, float>();
                for (int j = 0; j < layersList[i - 1].neuronsList.Count; j++) {
                    var neuronFromPreviousLayer = layersList[i - 1].neuronsList[j];
                    var randomWeightValue = StaticRandom.randomFloatNumberFromRange(-0.5f,0.5f);
                    neuronWeights.Add(neuronFromPreviousLayer, randomWeightValue);
                }
                neuronInPresentLayer.weights = neuronWeights;
                /*Debug.Log("Neuron " + neuronInPresentLayer.name + " posiada wagi " 
                    + string.Join(",", neuronInPresentLayer.weights));*/
            }
        }
    }


    //inicjalizuje wszystkie warstwy
    //twrzy obiekty o typie Nnlayer oraz obiekty o typie Neuron
    //Nnlayer posiada kolekcje własnych neuronów.
    private List<NnLayer> initializeLayers(int[] layersArray) {
        List<NnLayer> layersList = new List<NnLayer>();
        for (int i = 0; i < layersArray.Length; i++) {
            layersList.Add(initializeLayer(layersArray[i]));
        }
        return layersList;
    }

    //inicjalizuje pojedynczą warstwe
    private NnLayer initializeLayer(int numberOfNeurons) {
        return new NnLayer(numberOfNeurons);
    }

    //ustawia funkcje aktywacji w neuronie z ostatniej warstwy ktory odpowiada za predkosc
    private void setActivationFunctionOnLastNeuron() {
        getLastLayer().neuronsList[1].activationFunction = "BinaryStep";
    }

    //zapewnia zasilenie sieci danymi wejsciowymi
    public void giveDataToNetwork(float[] valueForNeuronsInFirstLayer) {
        int i = 0;
        foreach (Neuron neuron in this.layersList[0].neuronsList) {
            neuron.output = valueForNeuronsInFirstLayer[i];
            i++;
        }
    }

    //oblicza przepływ danych w sieci
    //zaczyna od 2 warstwy i zlicza sume iloczynow wag i wartosci neuronow w poprzedniej warstwie
    //przepuszcza otrzymana sume przez funkcje aktywacji
    //zapisuje wynik do wyjscia neuronu
    public float[] feedForward() {
        for (int i = 1; i < layersList.Count; i++) {
            foreach (Neuron neuron in layersList[i].neuronsList) {
                float sum = neuron.calculateWeights();
                neuron.output = neuron.activate(sum);
                //Debug.Log("Obliczyłem output dla " + neuron.name + " jest rowne = " + neuron.output);
            }
        }
        return getNetworkOutput();
    }

    //zwraca wartosci output z neuronow w ostatniej warstwie
    public float[] getNetworkOutput() {
        List<float> networkOutput = new List<float>();
        foreach (Neuron neuron in getLastLayer().neuronsList) {
            networkOutput.Add(neuron.output);
        }
        return networkOutput.ToArray();
    }

    //zwraca odwołanie do ostatniej warstwy sieci
    private NnLayer getLastLayer() {
        return layersList[layersList.Count - 1];
    }

    //mutuje wagi dla wszystkich neuronów zaczynając od drugiej warstwy
    public void mutate(float mutationChance, float mutationStrength) {
        for (int i = 1; i < layersList.Count; i++) {
            foreach (Neuron neuron in layersList[i].neuronsList) {
                neuron.mutateWeights(mutationChance, mutationStrength);
            }
        }
    }

    //wywołana na sieci X pobiera z niej kolekcje wag
    //i zwraca je opakowane w nowy obiekt o typie NeuralNetworkData
    public NeuralNetworkData getNetworkData() {
        NeuralNetworkData data = new NeuralNetworkData();
        data.setWeights(copyWeightsFromNetwork());
        return data;
    }

    //pobiera z przesłanego parametru kolekcje wag i ustawia je w sieci
    public void loadNewNetworkData(NeuralNetworkData data) {
        loadWeightsFromOtherNetwork(data.getWeights());
    }

    //Kopiuje wartości wag z sieci i zwraca pod postacją zagnieżdżonej kolekcji 
    //List<List<List<float>>> -> Lista warstw, każda warstwa zawiera listę neuronów, każdy neuron zawiera listę wartości wag o typie float
    private List<List<List<float>>> copyWeightsFromNetwork() {
        List<List<List<float>>> weights = new List<List<List<float>>>();

        weights.Add(new List<List<float>>());//dodanie pustego rekordu za pierwszą warstwę sieci
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

    //Wywołana na sieci X ustawia na niej wartości wag przesłane w parametrze
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

    //Metoda wyświetlatjąca w konsoli wagi podane w parametrze
    //Wykorzystywana tylko przy testach
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

    //Metoda wyświetlatjąca w konsoli wagi sieci na rzecz której jest wywoływana
    //Wykorzystywana tylko przy testach
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
