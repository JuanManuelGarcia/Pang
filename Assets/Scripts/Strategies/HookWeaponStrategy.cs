using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookWeaponStrategy : IWeaponStrategy
{
    public string Name { get { return "Hook"; } }
    public int Ammo { get; private set; }

    private GameObject ammoPrefab;
    private GameObject currentHook;

    public HookWeaponStrategy(GameObject ammoPrefab)
    {
        this.ammoPrefab = ammoPrefab;
        Ammo = int.MaxValue;
    }

    public void AddAmmo(int extraAmmo)
    {
        Ammo = int.MaxValue;
    }

    public object Execute(object data) // In: Transform; Out: bool
    {
        if (currentHook == null)
        {
            Transform playerTransform = (Transform)data;

            currentHook = GameObject.Instantiate(ammoPrefab);
            currentHook.transform.position = playerTransform.position;
            currentHook.transform.parent = playerTransform.root;
        }

        return true; // always true, UNLIMITED AMMO
    }
}
