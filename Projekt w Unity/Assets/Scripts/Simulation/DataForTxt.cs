public sealed class DataForTxt {
    private int generation;
    private int numberOfFinishingCar;
    private int populationSize;
    private float mutationChance;
    private float mutationStrength;
    private string duration;

    public DataForTxt(DataForTxtBuilder builder) {
        generation = builder.generation;
        numberOfFinishingCar = builder.numberOfFinishingCar;
        populationSize = builder.populationSize;
        mutationChance = builder.mutationChance;
        mutationStrength = builder.mutationStrength;
        duration = builder.duration;
    }

    public int getGenerationNumber() { return generation; }

    public int getnumberOfFinishingCar() { return numberOfFinishingCar; }

    public int getPopulationSize() { return populationSize; }

    public float getMutationChance() { return mutationChance; }

    public float getMutationStrength() { return mutationStrength; }

    public string getDuration() { return duration; }

}
