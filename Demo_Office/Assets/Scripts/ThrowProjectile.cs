using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectileToUse, projectileSocket;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0);

    bool isShotReady = true;
    float timer;
    int waitingBetweenProjectilesTime = 2;
    // Start is called before the first frame update

    public void MakeShotReady()
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
    public void FireProjectile()
    {
        if (isShotReady)
        {
            GameObject newProjectile;
            Projectile projectileComponent;
            SpawnProjectile(out newProjectile, out projectileComponent);

            Vector3 unitVectorToPlayer = (transform.forward + aimOffset).normalized;
            float projectileSpeed = projectileComponent.GetDefaultLaunchSpeed();
            newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileSpeed;

            CameraFollow cam = FindObjectOfType<CameraFollow>();
            if (cam.enemies.Length==1)//IF last man follow cake with slowmo
            {
                RaycastHit _hitInfo;
                if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out _hitInfo))
                {
                    if (_hitInfo.collider.gameObject.tag == "Enemy"&&_hitInfo.collider.gameObject.GetComponent<HealthSystem>().life==1)
                    {
                        StartCoroutine(cam.FollowCake(newProjectile));
                    }
                }
            }
            isShotReady = false;
        }

    }
    private void SpawnProjectile(out GameObject newProjectile, out Projectile projectileComponent)
    {

        newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        projectileComponent = newProjectile.GetComponent<Projectile>();
        projectileComponent.SetShooter(gameObject);
    }
}
