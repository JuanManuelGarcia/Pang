using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSingleton : IPointsSubject
{
    private static PointsSingleton instance;
    public static PointsSingleton Instance
    {
        get
        {
            if (instance == null) instance = new PointsSingleton();
            return instance;
        }
    }

    private PointsSingleton() 
    {
        Points = 0;
    }

    private List<IObserver> observers = new List<IObserver>();

    public int Points { get; private set; }

    public void AddPoints(int newPoints)
    {
        Points += newPoints;
        Notify();
    }

    public void ResetPoints()
    {
        Points = 0;
        Notify();
    }

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        if (observers.Contains(observer)) observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver o in observers) o.Revise(Instance);
    }
}
