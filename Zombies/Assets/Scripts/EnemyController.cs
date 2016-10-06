using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public NavMeshAgent agent;
    private Transform player;
    public Animator animator;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(findPath());
        StartCoroutine(playerDetect());
	}

    IEnumerator playerDetect()
    {
        while (true)
        {
            if (player == null)
                break;
            if (Vector3.Distance(transform.position, player.transform.position) < 1f)
            {
                animator.SetBool("attack", true);
                player.SendMessage("attack");
            }
                yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator findPath()
    {
        while (true)
        {
            if (player != null)
            {
                agent.SetDestination(player.position);
                yield return new WaitForSeconds(2);
            }
            else break;
        }
    }

    public void damage()
    {
        StopAllCoroutines();
        agent.enabled = false;
        animator.SetTrigger("dead");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject, 5);
    }
}
