using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CatcherChase : MonoBehaviour
{
    [SerializeField]
    private float health;
    private NavMeshAgent agent;
    private Transform playerTransform;
    private bool followPlayer;
    private bool coolDown;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("BigRat").transform;
        agent = GetComponent<NavMeshAgent>();
        followPlayer = false;
        coolDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBegRatDist();
        CheckDead();
        if (followPlayer == true && coolDown == true)
        {
            agent.destination = playerTransform.position;
        }
        else
        {
            agent.destination = agent.transform.position;
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        print(health);
    }

    private void CheckDead()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void CheckBegRatDist()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 10f)
        {
            followPlayer = true;
            StartCoroutine("StillInRange");
        }
    }

    private IEnumerator StillInRange()
    {
        yield return new WaitForSeconds(2f);
        if (Vector3.Distance(transform.position, playerTransform.position) < 10f)
        {
            followPlayer = true;
            StartCoroutine("StillInRange");
        }
        else
        {
            followPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BigRat")
        {
            print("aaa");
            SceneManager.LoadScene(0);
        }
    }

    public void SetFollow(bool follow)
    {
        followPlayer = follow;
    }
}
