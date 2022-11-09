using System;

static public class ParametersDto
{
    private static int sizePopulation;
    private static float mutationChance;
    private static float mutationStrength;
    private static int generationNumber;
    private static string duration;
    private static int carLifeSpan;
    private static float carSensorsLength;
    private static bool manualSteering;
    private static int aliveUnitsNumber;

    public static void setDefault() {
        ParametersDto.setPopulationSize(90);
        ParametersDto.setMutationChance(0.5f);
        ParametersDto.setMutationStrength(0.2f);
        ParametersDto.setCarLifeSpan(5);
        ParametersDto.setCarSensorsLength(40f);
        ParametersDto.setManualSteering(false);
    }
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

    public static void setAliveUnitsNumber(int newValue) {
        if(newValue >= 0 ) {
            aliveUnitsNumber = newValue;
        }
    }

    public static void setDuration(string newValue) {
        duration = newValue;
    }

    public static void setCarLifeSpan(int newValue) {
        if (newValue >= 0) {
            carLifeSpan = newValue;
        }
    }

    public static void setManualSteering(bool newValue) {
        manualSteering = newValue;
    }

    public static void setCarSensorsLength(float newValue) {
        carSensorsLength = newValue;
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

    public static int getCarLifeSpan() {
        return carLifeSpan;
    }

    public static bool isManualSteering() {
        return manualSteering;
    }

    public static float getCarSensorsLength() {
        return carSensorsLength;
    }
}
