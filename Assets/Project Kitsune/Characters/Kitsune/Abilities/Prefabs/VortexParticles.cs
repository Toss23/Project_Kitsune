using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class VortexParticles : MonoBehaviour
{
    public float speed = 90;
    public float radius = 1;
    public float angle = 0;
    public float offset = 0;

    void Update()
    {
        angle += speed * Time.deltaTime;
        if (angle > 360)
        {
            angle -= 360;
        }
        transform.localPosition = new Vector3(radius * Mathf.Cos((angle + offset) * Mathf.Deg2Rad), radius / 2 * Mathf.Sin((angle + offset) * Mathf.Deg2Rad));
        transform.rotation = Quaternion.Euler(0, 0, (angle + offset) + 180);
    }
}