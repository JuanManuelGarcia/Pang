using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBallSubject : ISubject
{
    List<IBallSubject> BallsSpawned { get; }
}
