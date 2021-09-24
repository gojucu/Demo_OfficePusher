using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public FloatingJoystick floatingJoystick;
    public Rigidbody rb;  ///Bunları GetComponent ile bul
    public Rotate rotate;// Bunları  GetComponent ile bul
    public ThrowProjectile throwProjectile;

    Vector3 defaultValue = new Vector3(0, 0, 0);

    HealthSystem healthSystem;
    Pushback pushback;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetRigidbodyState(true);
        healthSystem.SetColliderState(false);
        pushback = GetComponent<Pushback>();

    }

    public void Update()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        if (healthSystem.GetIsDead() == false)
        {
            if (!pushback.GetIsPushed())//While not getting pushed move
            {
                rb.velocity = direction * speed * Time.fixedDeltaTime;
            }
            if (!direction.Equals(defaultValue))//While moving rotate around
            {
                rotate.RotateAround();
            }
            if (Input.GetMouseButtonUp(0))
            {
                throwProjectile.FireProjectile();
            }
            throwProjectile.MakeShotReady();
        }
    }
  
    //If player is outside of area kill player.
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Area")
        {
            StartCoroutine(healthSystem.Die());
        }
    }
}
