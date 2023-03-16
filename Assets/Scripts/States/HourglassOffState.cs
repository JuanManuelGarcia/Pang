using System.Linq;
using UnityEngine;

public class HourglassOffState : IHourglassState
{
    public HourglassOffState(IHourglassController controller)
    {
        var stoppablesStopped = Object.FindObjectsOfType<MonoBehaviour>().OfType<IStoppable>().Where(x => x.IsStopped);
        foreach (IStoppable s in stoppablesStopped)
        {
            s.Resume();
        }
    }

    public void Do()
    {
    }
}
