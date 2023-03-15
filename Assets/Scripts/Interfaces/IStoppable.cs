using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStoppable
{
    bool IsStopped { get; }
    void Stop();
    void Resume();
}
