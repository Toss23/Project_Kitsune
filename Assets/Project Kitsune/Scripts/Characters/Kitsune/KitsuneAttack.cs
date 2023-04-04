using UnityEngine;

public class KitsuneAttack : Ability
{
    private void Update()
    {
        float speed = 10 * Time.deltaTime;
        transform.position += new Vector3(speed, 0, 0);
    }
}