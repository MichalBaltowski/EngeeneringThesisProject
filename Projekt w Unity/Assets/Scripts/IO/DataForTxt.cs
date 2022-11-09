public sealed class DataForTxt {
    private int generation;
    private int populationSize;
    private float mutationChance;
    private float mutationStrength;
    private string duration;
    private int carLifeSpan;
    private float carSensorLength;
    private bool isManualSteering;

    public DataForTxt(DataForTxtBuilder builder) {
        duration = builder.duration;
        generation = builder.generation;
        populationSize = builder.populationSize;
        mutationChance = builder.mutationChance;
        mutationStrength = builder.mutationStrength;
        carLifeSpan = builder.carLifeSpan;
        carSensorLength = builder.carSensorLength;
        isManualSteering = builder.isManualSteering;
    }

    public string getDuration() { return duration; }

    public int getGenerationNumber() { return generation; }

    public int getPopulationSize() { return populationSize; }

    public float getMutationChance() { return mutationChance; }

    public float getMutationStrength() { return mutationStrength; }

    public int getCarLifeSpan() { return carLifeSpan; }

    public float getCarSensorsLength() { return carSensorLength; }
}
