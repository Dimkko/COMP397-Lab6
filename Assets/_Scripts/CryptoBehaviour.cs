using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    RUN, 
    JUMP
}

public class CryptoBehaviour : MonoBehaviour
{
    [Header("Line Of Sight")]
    public GameObject player;
    public bool HasLOS;

    private NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);

            if (Vector3.Distance(transform.position, player.transform.position) < 2.5)
            {
                animator.SetInteger("AnimState", (int)CryptoState.IDLE);
                transform.LookAt(transform.position - player.transform.forward);
            }
            else
            {
                animator.SetInteger("AnimState", (int)CryptoState.RUN);
            }

        }
        else
        {
            animator.SetInteger("AnimState", (int)CryptoState.IDLE);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = false;
        }
    }
}
