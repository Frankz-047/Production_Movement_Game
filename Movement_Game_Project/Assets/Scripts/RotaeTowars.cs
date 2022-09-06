using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaeTowars : MonoBehaviour {
	private Transform target;
    private GameObject player;
    private int time;
    public float attackRange;
    public float rotaeSpeed;
    private bool rotae = true;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        target  = player.GetComponent<Transform>();
        StartCoroutine(outPut());
    }

    // Update is called once per frame
    void Update() {
        if (rotae)
        {
            //transform.LookAt(target); 
            Vector3 targetDirection = target.position - transform.position;
            Vector3 currentDirection = transform.forward;

            float step = rotaeSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(currentDirection, targetDirection, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    public bool ReadyToAttack()
    {
        return WithInRange() && CanSeePlayer();
    }

    private bool WithInRange()
    {
        return (Vector3.Distance(transform.position, player.transform.position) <= attackRange);
    }

    private bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 dir = (player.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, dir, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player")) return true;
        }
        return false;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    private IEnumerator outPut()
    {
        yield return new WaitForSeconds(1);
        print("can see player =  " + CanSeePlayer());
        if (CanSeePlayer())
        {
            ++time;
        }
        else
        {
            time = 0;
        }
            print("Player insight Time : " + time);
        if (time == 10)
        {
            print("10 second have past the player is hit");
            time = 0;
        }
        StartCoroutine(outPut());
    }
    public void RotationStatus(bool state)
    {
        rotae = state;
    }
}
