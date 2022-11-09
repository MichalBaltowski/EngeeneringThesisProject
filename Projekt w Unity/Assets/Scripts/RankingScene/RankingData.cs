using System;

public class RankingData : IComparable<RankingData> {
    private int generation;
    private string duration;
    private int populationSize;
    private float mutationChance;
    private float mutationStrength;

    public RankingData(RankingDataBuilder builder) {
        generation = builder.generation;
        duration = builder.duration;
        populationSize = builder.populationSize;
        mutationChance = builder.mutationChance;
        mutationStrength = builder.mutationStrength;
    }

    public int getGenerationNumber() { return generation; }

    public string getDuration() { return duration; }

    public int getPopulationSize() { return populationSize; }

    public float getMutationChance() { return mutationChance; }

    public float getMutationStrength() { return mutationStrength; }

    public int CompareTo(RankingData dataRecord) {
       return this.generation.CompareTo(dataRecord.generation);
    }
}
