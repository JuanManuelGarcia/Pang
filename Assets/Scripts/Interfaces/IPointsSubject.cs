using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPointsSubject : ISubject
{
    int Points { get; }
}
