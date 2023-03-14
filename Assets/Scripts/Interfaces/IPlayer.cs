using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    void Attach(IPlayerObserver observer);
    void Detach(IPlayerObserver observer);
}
