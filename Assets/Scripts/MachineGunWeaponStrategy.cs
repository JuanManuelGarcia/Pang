using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunWeaponStrategy : IWeaponStrategy
{
    public string Name { get { return "Machine Gun"; } }
    public int Ammo { get; private set; }

    private const int InitialAmmo = 50;
    private GameObject ammoPrefab;

    public MachineGunWeaponStrategy(GameObject ammoPrefab)
    {
        this.ammoPrefab = ammoPrefab;
        Ammo = InitialAmmo;
    }

    public void AddAmmo(int extraAmmo)
    {
        Ammo += extraAmmo;
    }

    public object Execute(object data) // In: Transform; Out: bool
    {
        if (Ammo > 0)
        {
            Transform playerTransform = (Transform)data;

            GameObject go = GameObject.Instantiate(ammoPrefab);
            go.transform.position = playerTransform.position;
            go.transform.parent = playerTransform;

            Ammo--;
        }

        return Ammo > 0; // return false if no more ammo, true otherwise
    }
}
