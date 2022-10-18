using System;
using System.Threading;

public static class StaticRandom {
    //ziarno uzywane do utworzenia instancji klasy Random. Dzieki wydzieleniu
    //do osobnej zmiennej inkrementowanej przez threadLocal
    //mam pewnoœæ ¿e Random wywo³ywane dla ca³ej populacji bedzie zwracal rozne wartosci
    private static int seed;

    //Statyczny konstruktor
    //Pobiera liczbê milisekund, które up³ynê³y od momentu uruchomienia systemu
    static StaticRandom() {
        seed = Environment.TickCount;
    }

    //prywatne odwo³anie do klasy random
    private static Random Instance { get { return threadLocal.Value; } }

    //przy ka¿dym wywo³aniu tworzy now¹ instancje klasy Random z parametrem seed
    //inkrementuje zmienn¹ seed i przechowuje j¹ jako zmienn¹ atomow¹
    private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
        (() => new Random(Interlocked.Increment(ref seed)));



    //losuje liczbê z zakresu min range do max range
    public static float randomFloatNumberFromRange(float minRange, float maxRange) {
        return (float)Instance.NextDouble() * (maxRange - minRange) + minRange; ;
    }

    //losuje liczbê z domyœlnego zakresu od 0.0 do 1.0
    public static float randomFloatNumberDefaultRange() {
        return (float)Instance.NextDouble();
    }
}