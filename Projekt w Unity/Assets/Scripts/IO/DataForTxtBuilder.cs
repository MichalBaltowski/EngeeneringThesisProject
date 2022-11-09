public sealed class DataForTxtBuilder
{
    internal int generation;
    internal int populationSize;
    internal float mutationChance;
    internal float mutationStrength;
    internal string duration;
    internal int carLifeSpan;
    internal float carSensorLength;
    internal bool isManualSteering;

    private DataForTxtBuilder() {}

    public static DataForTxtBuilder get() {
        return new DataForTxtBuilder();
    }

    public DataForTxtBuilder withGenerationNumber(int value) {
        this.generation = value;
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

    public DataForTxtBuilder withCarLifeSpan(int value) {
        this.carLifeSpan = value;
        return this;
    }

    public DataForTxtBuilder withCarSensorLength(float value) {
        this.carSensorLength = value;
        return this;
    }

    public DataForTxtBuilder withManualSteering(bool value) {
        this.isManualSteering = value;
        return this;
    }

    public DataForTxt createNewDataForTxt() {
        return new DataForTxt(this);
    }

}
