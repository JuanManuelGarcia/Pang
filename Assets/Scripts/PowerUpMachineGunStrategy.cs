using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMachineGunStrategy : MonoBehaviour, IPowerUpStrategy
{
    public object Execute(object data)  // In: IPlayerPowerUps; Out: null
    {
        var p = data as IPlayerPowerUps;
        p.ApplyMachineGun();
        return null;
    }
}
