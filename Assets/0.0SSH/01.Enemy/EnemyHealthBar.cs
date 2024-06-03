using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Vector3 HealthBarRotation;
    void Update()
    {
        transform.rotation = Quaternion.Euler(HealthBarRotation);
    }
}
