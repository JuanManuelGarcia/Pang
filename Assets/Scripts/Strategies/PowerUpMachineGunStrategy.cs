using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMachineGunStrategy : MonoBehaviour, IPowerUpStrategy
{
    private const float timeDespawn = 5f;
    private float timeSpawned;

    void Start()
    {
        timeSpawned = Time.time;
    }

    void Update()
    {
        if ((Time.time - timeSpawned) > timeDespawn)
        {
            Destroy(gameObject);
        }
    }

    public object Execute(object data)  // In: IPlayerPowerUps; Out: null
    {
        var p = data as IPlayerPowerUps;
        p.ApplyMachineGun();
        return null;
    }
}
