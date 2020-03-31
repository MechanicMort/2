using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmolRatFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform spin;
    private Transform goal;
    private Transform playerTransform;
    private Transform catcherTransform;
    private GameObject[] catchers;
    private bool followPlayer;
    private bool coolDown;
    [SerializeField]
    private float health = 1;
    [SerializeField]
    private float damage = 1;
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
        CheckDead();
        MakeTarget();
    }
    private void MakeTarget()
    {
        if (followPlayer == true && coolDown == true)
        {
            agent.destination = playerTransform.position;
        }
        else if (catcherTransform != null && coolDown == false)
        {
            agent.destination = catcherTransform.position;
        }
        else
        {
            agent.destination = spin.position;
        }
    }

    private void CheckDead()
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Catcher")
        {
            col.SendMessage("Damage", damage);
            StartCoroutine("SelfDamage");
        }
    }

    private IEnumerator SelfDamage()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }

    public void SetFollow(bool follow)
   {
        followPlayer = follow;
   }

    public void RatAttack()
    {       
        catchers = GameObject.FindGameObjectsWithTag("Catcher");
        float closest = 100;
        for (int i = 0; i < catchers.Length; i++)
        {
            if (Vector3.Distance(transform.position, catchers[i].transform.position) < closest)
            {
                closest = Vector3.Distance(transform.position, catchers[i].transform.position);
                catcherTransform = catchers[i].transform;
            }
        }
        coolDown = false;
        StartCoroutine("CoolDown");
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(3f);
        coolDown = true;
    }


}
