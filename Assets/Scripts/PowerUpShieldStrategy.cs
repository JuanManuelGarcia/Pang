using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShieldStrategy : MonoBehaviour, IPowerUpStrategy
{
    public object Execute(object data)  // In: IPlayerPowerUps; Out: null
    {
        var p = data as IPlayerPowerUps;
        p.ApplyShield();
        return null;
    }
}
