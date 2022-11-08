public sealed class DataForTxtBuilder
{
    internal int generation;
    internal int numberOfFinishingCar;
    internal int populationSize;
    internal float mutationChance;
    internal float mutationStrength;
    internal string duration;

    private DataForTxtBuilder() {}

    public static DataForTxtBuilder get() {
        return new DataForTxtBuilder();
    }

    public DataForTxtBuilder withGenerationNumber(int value) {
        this.generation = value;
        return this;
    }

    public DataForTxtBuilder withNumberOfFinishingCar(int value) {
        this.numberOfFinishingCar = value;
        return this;
    }

    public DataForTxtBuilder withPopulationSize(int value) {
        this.populationSize = value;
        return this;
    }

    public DataForTxtBuilder withMutationChance(float value) {
        this.mutationChance = value;
        return this;
    }

    public DataForTxtBuilder withMutationStrength(float value) {
        this.mutationStrength = value;
        return this;
    }

    public DataForTxtBuilder withDuration(string value) {
        this.duration = value;
        return this;
    }

    public DataForTxt createNewDataForTxt() {
        return new DataForTxt(this);
    }

}
