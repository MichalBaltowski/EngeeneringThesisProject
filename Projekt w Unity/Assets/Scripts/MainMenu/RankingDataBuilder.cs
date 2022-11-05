public class RankingDataBuilder
{
    internal int generation;
    internal string duration;
    internal int populationSize;
    internal float mutationChance;
    internal float mutationStrength;

    public RankingData createRankingData() {
        return new RankingData(this);
    }

    public static RankingDataBuilder get() {
        return new RankingDataBuilder();
    }

    private RankingDataBuilder() { }

    public RankingDataBuilder withGenerationNumber(int value) {
        this.generation = value;
        return this;
    }

    public RankingDataBuilder withDuration(string value) {
        this.duration = value;
        return this;
    }

    public RankingDataBuilder withPopulationSize(int value) {
        this.populationSize = value;
        return this;
    }

    public RankingDataBuilder withMutationChance(float value) {
        this.mutationChance = value;
        return this;
    }

    public RankingDataBuilder withMutationStrength(float value) {
        this.mutationStrength = value;
        return this;
    }
}
