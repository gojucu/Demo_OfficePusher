using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public FloatingJoystick floatingJoystick;
    public Rigidbody rb;

    [SerializeField] float RotationsPerMinute = 1f;
    [SerializeField] GameObject projectileToUse;
    [SerializeField] GameObject projectileSocket;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);

    bool isShotReady=true;
    float timer;
    int waitingBetweenProjectilesTime=2;

    Vector3 defaultValue = new Vector3(0,0,0);

    public void Update()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        rb.velocity = direction * speed * Time.fixedDeltaTime;
        if (!direction.Equals(defaultValue))
        {
            RotateAround();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(isShotReady)
                FireProjectile();
        }
        MakeShotReady();
    }

    void MakeShotReady()
    {
        if (isShotReady == false)
        {
            timer += Time.deltaTime;
            if (timer > waitingBetweenProjectilesTime)
            {
                isShotReady = true;
                timer = 0;
            }
        }
    }

    void FireProjectile()
    {
        GameObject newProjectile;
        Projectile projectileComponent;
        SpawnProjectile(out newProjectile, out projectileComponent);

        Vector3 unitVectorToPlayer = (transform.forward + aimOffset /*- projectileSocket.transform.position*/).normalized;
        float projectileSpeed = projectileComponent.GetDefaultLaunchSpeed();
        newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;
        isShotReady = false;
    }
    private void SpawnProjectile(out GameObject newProjectile, out Projectile projectileComponent)
    {

        newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.SetShooter(gameObject);
    }
    private void RotateAround()
    {
        float yDegreesPerFrame = Time.deltaTime / 60 * 360 * RotationsPerMinute; ; 
        transform.RotateAround(transform.position, transform.up, yDegreesPerFrame);
    }
}