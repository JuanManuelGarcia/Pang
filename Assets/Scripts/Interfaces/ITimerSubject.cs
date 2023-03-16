using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimerSubject : ISubject
{
    float TimeLeft { get; }
}
