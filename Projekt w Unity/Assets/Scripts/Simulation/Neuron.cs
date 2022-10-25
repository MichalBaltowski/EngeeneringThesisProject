using System;
using System.Collections.Generic;
using UnityEngine;

public class Neuron {
    //licznik obiektów typu Neuron.
    private static int neuronCounter;
    public Dictionary<Neuron, float> weights;
    public string id;
    public float output;
    public String activationFunction;

    public Neuron() {
        neuronCounter++;
        this.id = "Neuron_" + neuronCounter;
        this.activationFunction = "Tanh";
    }

    //zwraca waartosc po 'przepuszczeniu' przez funkcje aktywacji
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

    //kopiuje wagi z innego neuronu i ustawia w neuronie dla ktorego zostala wywolana
    public void copyWeightFromOtherNeuron(Neuron otherNeuron) {
        if (weights != null) {
            this.weights.Clear();
            foreach (Neuron neuron in otherNeuron.weights.Keys) {
                this.weights.Add(neuron, otherNeuron.weights[neuron]);
                //Debug.Log("Skopiowano wagę: " + otherNeuron.weights[neuron] + " z " + otherNeuron.name + " do neuronu: " + this.name);
            }
        }
    }

    //funkcja zmienia wagi jesli wylosowana wartosc z zakresu [0.0;1.0] <= mutationchance
    //jesli tak to modyfikuje wage o wylosowana wartosc
    public void mutateWeights(float mutationChance, float mutationStrength) {
        List<Neuron> temporaryListOfKeys = new List<Neuron>(weights.Keys);
        foreach (Neuron neuron in temporaryListOfKeys) {
            if (drawMutationChanceRange() <= mutationChance) {
                weights[neuron] = weights[neuron] + drawMutationStrengthRange(mutationStrength);
            }
        }
    }

    //losuje liczby od 0 do 1
    private float drawMutationChanceRange() {
        return StaticRandom.randomFloatNumberDefaultRange();
    }

    //losuje liczbę dziesiętną z przedziału od -mutationStrength do mutationStrength (np, -0.2;0.2)
    private float drawMutationStrengthRange(float mutationStrength) {
        return StaticRandom.randomFloatNumberFromRange(-mutationStrength, mutationStrength);
    }

    //wypisuje zawartosc weights - slownika<neuron,waga>
    private void writeWeights() {
        Debug.Log("Wyświetlam zawartosc słownika wag dla " + this.id);
        foreach (Neuron neuron in this.weights.Keys) {
            Debug.Log("Neuron -" + neuron.id + "_____waga -" + weights[neuron]);
        }
    }

}
