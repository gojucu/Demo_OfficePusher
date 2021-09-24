using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float selfDestroyTime = 10f;
    [SerializeField] GameObject shooter; 

    const float DESTROY_DELAY = 0.01f;


    private void Start()
    {//Destroy self after time
        Destroy(gameObject,selfDestroyTime);
    }
    public void SetShooter(GameObject shooter)
    {
        this.shooter = shooter;
    }
    void OnTriggerEnter(Collider collision)
    {
        var tagCollidedWith = collision.gameObject.tag;
        if (shooter != collision.gameObject&&(tagCollidedWith=="Player" ||tagCollidedWith=="Enemy"))
        {
            collision.GetComponent<HealthSystem>().TakeDamage();
            Destroy(gameObject, DESTROY_DELAY);
        }
    }

    public float GetDefaultLaunchSpeed()
    {
        return projectileSpeed;
    }

}
