using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// https://www.youtube.com/watch?v=j1-OyLo77ss
public class LineOfSight : MonoBehaviour
{
    public float radius;
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionnMask;
    public bool canSeePlayer;

    public void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChjecks = Physics.OverlapSphere(transform.position, radius,targetMask);

        if(rangeChjecks.Length != 0 )
        {
            Transform target = rangeChjecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.position, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget,distanceToTarget,obstructionnMask)) 
                    {
                    canSeePlayer = true;
                    }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else
            canSeePlayer= false;

    }
}
