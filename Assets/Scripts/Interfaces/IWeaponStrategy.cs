using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponStrategy : IStrategy
{
    string Name { get; }
    int Ammo { get; }
    void AddAmmo(int extraAmmo);
}
