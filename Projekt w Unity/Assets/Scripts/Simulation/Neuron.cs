using System;
using System.Collections.Generic;
using UnityEngine;

public class Neuron {
    private static int neuronCounter;
    private string id;
    public String activationFunction;
    public Dictionary<Neuron, float> weights;
    public float output;

    public Neuron() {
        neuronCounter++;
        this.id = "Neuron_" + neuronCounter;
        this.activationFunction = "Tanh";
    }
    public float activate(float value) {
        float outcome = 0f;
        switch (activationFunction) {
            case ("Tanh"):
                outcome = (float)Math.Tanh(value);
                break;
            case ("BinaryStep"):
                if (value > 0) {
                    outcome = 1;
                } else {
                    outcome = 0;
                }
                break;
        }
        return outcome;
    }

    //oblicza sume iloczynów (wartosc neuronu z poprzedniej warstwy * waga polaczenia z tym neuronem)
    public float calculateWeights() {
        float sum = 0;
        foreach (Neuron neuron in weights.Keys) {
            //mnozy wyjscie neuronu z waga
            sum += neuron.output * weights[neuron];
        }
        return sum;
    }

    public void mutateWeights(float mutationChance, float mutationStrength) {
        List<Neuron> temporaryListOfKeys = new List<Neuron>(weights.Keys);
        foreach (Neuron neuron in temporaryListOfKeys) {
            if (drawMutationChanceRange() <= mutationChance) {
                weights[neuron] = weights[neuron] + drawMutationStrengthRange(mutationStrength);
            }
        }
    }

    private float drawMutationChanceRange() {
        return StaticRandom.getRandomFloatNumberDefaultRange();
    }
    private float drawMutationStrengthRange(float mutationStrength) {
        return StaticRandom.randomFloatNumberFromRange(-mutationStrength, mutationStrength);
    }

    //test use only
    private void copyWeightFromOtherNeuron(Neuron otherNeuron) {
        if (weights != null) {
            this.weights.Clear();
            foreach (Neuron neuron in otherNeuron.weights.Keys) {
                this.weights.Add(neuron, otherNeuron.weights[neuron]);

            }
        }
    }

    //test use only
    private void writeWeights() {
        Debug.Log("Wyświetlam zawartosc słownika wag dla " + this.id);
        foreach (Neuron neuron in this.weights.Keys) {
            Debug.Log("Neuron -" + neuron.id + "_____waga -" + weights[neuron]);
        }
    }

}
