using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GameObject[] enemies;
    Pushback pushback;
    HealthSystem healthSystem;
    Area area;
    Rotate rotate;
    ThrowProjectile throwProjectile;
    bool isInside = true;
    bool isLastEnemy = false;
    public float speed;
    private Vector3 wayPoint, NewWaypoint, vec3;//TODO adını düzenle
    int enemyCount;

    // Use this for initialization
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetRigidbodyState(true);
        healthSystem.SetColliderState(false);


        rotate = GetComponent<Rotate>();
        throwProjectile = GetComponent<ThrowProjectile>();
        pushback = GetComponent <Pushback>();
        area = GetComponent<Area>();

        vec3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        NewWaypoint = area.GetRandomPointInsideCollider();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = 0;
        foreach(GameObject go in enemies)
        {
            enemyCount++;
        }
        if (enemyCount<=1)
        {
            isLastEnemy = true;
        }

        if (healthSystem.GetIsDead() == false)
        {
            if (!pushback.GetIsPushed())
            {
                WalkRandomly();
                rotate.RotateAround();
                throwProjectile.FireProjectile();
                throwProjectile.MakeShotReady();
            }
        }
    }
 
    public bool GetLastMan()
    {
        return isLastEnemy;
    }
    void WalkRandomly()
    {
        BoxCollider box = area.GetBoxCollider();


        if (!box.bounds.Contains(transform.position))
        {
            if (isInside == true)
            {
                vec3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                NewWaypoint = area.GetRandomPointInsideCollider();
                isInside = false;
            }
        }
        else
        {
            isInside = true;
        }

        wayPoint = NewWaypoint - vec3;
        this.GetComponent<Rigidbody>().velocity = wayPoint * speed * Time.fixedDeltaTime;
    }

    //If enemy is outside of area kill enemy.
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Area")
        {
            StartCoroutine(healthSystem.Die());
        }
    }



}
