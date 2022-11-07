using System;

static public class ParametersDto
{
    private static int sizePopulation;
    private static float mutationChance;
    private static float mutationStrength;
    private static int generationNumber;
    private static int aliveUnitsNumber;
    private static string duration;

    public static void setPopulationSize(int newValue) {
        if(newValue > 1 && newValue < 300) {
            sizePopulation = newValue;
        }
    }

    public static void setMutationChance(float newValue) {
        if (newValue > 0.0 && newValue < 1) {
            mutationChance = (float)Math.Round(newValue, 2);
        }
    }

    public static void setMutationStrength(float newValue) {
        if (newValue > 0.0 && newValue < 1) {
            mutationStrength = (float)Math.Round(newValue, 2);
        }
    }

    public static void setGenerationNumber(int newValue) {
        if(newValue > 1) {
            generationNumber = newValue;
        }
    }

    public static void setAliveUnitsNumber(int newValue) {
        if(newValue >= 0 ) {
            aliveUnitsNumber = newValue;
        }
    }

    public static void setDuration(string newValue) {
        duration = newValue;
    }

    public static void incrementGenerationNumber() {
        generationNumber++;
    }

    public static int getPopulationSize() {
        return sizePopulation;
    }

    public static float getMutationChance() {
        return (float)Math.Round(mutationChance, 2);
    }

    public static float getMutationStrength() {
        return (float)Math.Round(mutationStrength, 2); ;
    }

    public static bool isParamsInitialized() {
        return sizePopulation != 0;
    }

    public static int getGenerationNumber() {
        return generationNumber;
    }

    public static int getAliveUnitsNumber() {
        return aliveUnitsNumber;
    }

    public static string getDuration() {
        return duration;
    }
}
