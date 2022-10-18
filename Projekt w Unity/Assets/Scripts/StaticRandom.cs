using System;
using System.Threading;

public static class StaticRandom {
    //ziarno uzywane do utworzenia instancji klasy Random. Dzieki wydzieleniu
    //do osobnej zmiennej inkrementowanej przez threadLocal
    //mam pewno�� �e Random wywo�ywane dla ca�ej populacji bedzie zwracal rozne wartosci
    private static int seed;

    //Statyczny konstruktor
    //Pobiera liczb� milisekund, kt�re up�yn�y od momentu uruchomienia systemu
    static StaticRandom() {
        seed = Environment.TickCount;
    }

    //prywatne odwo�anie do klasy random
    private static Random Instance { get { return threadLocal.Value; } }

    //przy ka�dym wywo�aniu tworzy now� instancje klasy Random z parametrem seed
    //inkrementuje zmienn� seed i przechowuje j� jako zmienn� atomow�
    private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
        (() => new Random(Interlocked.Increment(ref seed)));



    //losuje liczb� z zakresu min range do max range
    public static float randomFloatNumberFromRange(float minRange, float maxRange) {
        return (float)Instance.NextDouble() * (maxRange - minRange) + minRange; ;
    }

    //losuje liczb� z domy�lnego zakresu od 0.0 do 1.0
    public static float randomFloatNumberDefaultRange() {
        return (float)Instance.NextDouble();
    }
}