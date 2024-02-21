using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBar : MonoBehaviour
{
    public Slider bar;

    public void TakeDamage(float damage)
    {
        bar.value -= damage;
    }

    public void SetBlood(float blood)
    {
        bar.value = blood;
    }
}
