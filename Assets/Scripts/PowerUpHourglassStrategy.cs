using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHourglassStrategy : MonoBehaviour, IPowerUpStrategy
{
    public object Execute(object data)  // In: IPlayerPowerUps; Out: null
    {
        var p = data as IPlayerPowerUps;
        p.ApplyHourglass();
        return null;
    }
}
