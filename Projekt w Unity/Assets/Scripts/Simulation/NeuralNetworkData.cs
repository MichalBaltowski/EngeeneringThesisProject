using System;
using System.Collections.Generic;
public class NeuralNetworkData {
    private List<List<List<float>>> weights;
    
    public void setWeights(List<List<List<float>>> weight) {
        weights = weight;
    }

    public List<List<List<float>>> getWeights() {
        return weights;
    }
}
