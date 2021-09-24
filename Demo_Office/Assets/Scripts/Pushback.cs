using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushback : MonoBehaviour
{
    // force is how forcefully we will push the player away from the enemy.
    [SerializeField] float force = 3;
    [SerializeField] float waitTime = 1;
    bool isPushing = false;
    public bool GetIsPushed()
    {
        return isPushing;
    }
    void OnCollisionEnter(Collision c)
    {
        // If the object  hit is the enemy or the player
        if (c.gameObject.tag == "Player" || c.gameObject.tag == "Enemy")
        {
            isPushing = true;
            // Calculate Angle Between the collision point and the player
            Vector3 dir = c.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().velocity=dir * force * Time.fixedDeltaTime;
            StartCoroutine(PushObject());
        }
    }
    private IEnumerator PushObject()
    {
        //Wait for the push to end.
        yield return new WaitForSeconds(waitTime);
        isPushing = false;
    }

}
