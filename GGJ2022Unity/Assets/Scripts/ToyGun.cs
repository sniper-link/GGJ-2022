using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGun : MonoBehaviour
{
    public Transform projectStart;
    public GameObject projectilePrefab;
    public float projectilForce = 700f;
    private float fireCooldown = 0f;
    public float cooldownTime = 0.5f;

    private void Update()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && fireCooldown <= 0)
        {
            FireProjectile();
            fireCooldown = cooldownTime;
        }
    }

    private void FireProjectile()
    {
        Rigidbody projectileRB = Instantiate(projectilePrefab, projectStart.position, this.transform.rotation).GetComponent<Rigidbody>();
        projectileRB.AddForce(projectileRB.transform.forward * projectilForce);
    }

    public void Pickup(Vector3 handLocation, Quaternion handRotation)
    {
        transform.position = handLocation;
        transform.rotation = handRotation;
    }
}
