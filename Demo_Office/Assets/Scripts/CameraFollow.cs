using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followedObject;
    public TimeManager timeManager;
    public GameObject lastEnemy;
    public GameObject[] enemies;


    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 1)
        {
            lastEnemy=GameObject.FindGameObjectWithTag("Enemy").gameObject;
        }
        transform.position = followedObject.transform.position;

    }
    public IEnumerator FollowCake(GameObject cake)
    {

        timeManager.DoSlowMotion();
        followedObject = cake;

        yield return new WaitForSecondsRealtime(3);

        timeManager.ReturnNormal();
        followedObject = GameObject.FindGameObjectWithTag("Player");
    }
    public IEnumerator LastManCamera(GameObject lastEnemy)
    {
        timeManager.DoSlowMotion();
        followedObject = lastEnemy;
        yield return new WaitForSecondsRealtime(2);
        timeManager.ReturnNormal();
        followedObject = GameObject.FindGameObjectWithTag("Player");
    }
}
