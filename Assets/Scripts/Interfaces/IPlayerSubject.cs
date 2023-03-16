using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSubject : ISubject
{
    bool IsDead { get; }
    string CurrentWeaponName { get; }
    int CurrentWeaponAmmo { get; }
}
