using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HourglassOnState : IHourglassState
{
    private const float SecondsHourglassOn = 5f;

    private IHourglassController controller;
    private float timeStarted;

    public HourglassOnState(IHourglassController controller)
    {
        this.controller = controller;
        ResetTime();
    }

    public void ResetTime()
    {
        timeStarted = Time.time;
    }

    public void Do()
    {
        if ((Time.time - timeStarted) < SecondsHourglassOn)
        {
            var stoppablesNotStopped = Object.FindObjectsOfType<MonoBehaviour>().OfType<IStoppable>().Where(x => !x.IsStopped);
            foreach (IStoppable s in stoppablesNotStopped)
            {
                s.Stop();
            }
        }
        else
        {
            controller.HourglassState = new HourglassOffState(controller);
        }
    }
}
