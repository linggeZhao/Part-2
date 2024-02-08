using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject weaponS;
    
    public void knife()
    {
        Instantiate(weaponS);
    }
}
