using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currenntWayPointIndex = 0;

    [SerializeField] private float speed = 2f;
 
   private void Update()
    {
      if (Vector2.Distance(waypoints[currenntWayPointIndex].transform.position, transform.position) < .1f)
        {
            currenntWayPointIndex++;
            if (currenntWayPointIndex >= waypoints.Length)
            {
                currenntWayPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currenntWayPointIndex].transform.position,Time.deltaTime * speed);
    }
}
