using System;
using System.Threading;
public static class StaticRandom {
    //ziarno uzywane do utworzenia instancji klasy Random. Dzieki wydzieleniu
    //do osobnej zmiennej inkrementowanej przez threadLocal
    //mam pewnoœæ ¿e Random wywo³ywane dla ca³ej populacji bedzie zwracal rozne wartosci
    private static int seed;

    //Get miliseconds from system start
    static StaticRandom() {
        seed = Environment.TickCount;
    }

    //get random number from 0.0 to 1.0
    public static float getRandomFloatNumberDefaultRange() {
        return (float)Instance.NextDouble();
    }

    //get random number from min range to max range
    public static float randomFloatNumberFromRange(float minRange, float maxRange) {
        return (float)Instance.NextDouble() * (maxRange - minRange) + minRange; ;
    }

    private static Random Instance { get { return threadLocal.Value; } }

    //przy ka¿dym wywo³aniu tworzy now¹ instancje klasy Random z parametrem seed
    //inkrementuje zmienn¹ seed i przechowuje j¹ jako zmienn¹ atomow¹
    private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
        (() => new Random(Interlocked.Increment(ref seed)));
}