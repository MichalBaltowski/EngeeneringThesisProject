using System;
using System.Threading;
public static class StaticRandom {
    //ziarno uzywane do utworzenia instancji klasy Random. Dzieki wydzieleniu
    //do osobnej zmiennej inkrementowanej przez threadLocal
    //mam pewno�� �e Random wywo�ywane dla ca�ej populacji bedzie zwracal rozne wartosci
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

    //przy ka�dym wywo�aniu tworzy now� instancje klasy Random z parametrem seed
    //inkrementuje zmienn� seed i przechowuje j� jako zmienn� atomow�
    private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
        (() => new Random(Interlocked.Increment(ref seed)));
}