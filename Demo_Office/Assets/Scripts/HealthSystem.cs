using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] GameObject gameUI, gameOverUI;
    public int life = 2;
    [SerializeField] float deathVanishSeconds = 2.0f;
    bool isDead = false;
    public void TakeDamage()
    {
        life = life - 1;
        bool characterDies = (life <= 0);
        if (characterDies)
        {
            StartCoroutine(Die());
        }
    }
    public bool GetIsDead()
    {
        return isDead;
    }
    public IEnumerator Die()
    {
        isDead = true;
        if (gameObject.tag == "Enemy")
        {
            gameObject.tag = "Untagged";
            if (GetComponent<EnemyAI>().GetLastMan())//If it's last enemy activate last man camera 
            {

                CameraFollow cam = FindObjectOfType<CameraFollow>();
                StartCoroutine(cam.LastManCamera(this.gameObject));
            }
        }
        GetComponentInChildren<Animator>().enabled = false;
        SetRigidbodyState(false);
        SetColliderState(true);
 
        yield return new WaitForSecondsRealtime(2);
        if (gameObject.tag == "Player")//If player died, activate gameOverUI
        {
            gameUI.SetActive(false);
            gameOverUI.SetActive(true);
        }
        else//If enemy died, Destroy gameObject after death
        {
            Destroy(gameObject, deathVanishSeconds);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    //For activating ragdoll
    public void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody.gameObject != this.gameObject)
                rigidbody.isKinematic = state;
        }
    }
    public void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag != this.gameObject.tag)
                collider.enabled = state;
        }
    }
}
