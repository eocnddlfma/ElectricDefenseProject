using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform CurrentHealthBar;
    public Vector3 HealthBarRotation;
    void Update()
    {
        transform.rotation = Quaternion.Euler(HealthBarRotation);
    }

    public void UpdateHealthbar(float ratio)
    {
        CurrentHealthBar.transform.localScale = new Vector3(ratio, 1, 1);
    }
}
