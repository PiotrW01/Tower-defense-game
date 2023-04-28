using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    private float speed; // The speed at which the enemy moves
    private float turnSpeed = 5f;

    private float targetZ;
    private Transform target; // The next point on the path the enemy needs to reach

    public int waypointIndex = 1; // The index of the current waypoint in the path
    public float distance;
    //public float toWaypointDistance;

    void Start()
    {
        speed = gameObject.GetComponent<baseEnemy>().getSpeed();
        target = Waypoints.waypoints[1]; // Set the target to the first waypoint
        distance = Vector2.Distance(gameObject.transform.localPosition, target.transform.localPosition);
        Vector2 direction = target.position - transform.position;
        //toWaypointDistance = distance;
        //targetZ = transform.eulerAngles.z;
        //transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    private void FixedUpdate()
    {
        // Move the enemy towards the target waypoint
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        distance = Vector2.Distance(gameObject.transform.localPosition, target.transform.localPosition);

        // If the enemy has reached the target waypoint, set the next target
        if (Vector2.Distance(transform.position, target.position) < 0.001f)
        {
            setNextTarget();
        }

        // If the direction is not zero, rotate 
        /*        Vector2 direction = target.position - transform.position;
                if (direction != Vector2.zero)
                {
                    rotateToTarget(direction);
                }*/


    }

    private void setNextTarget()
    {
        waypointIndex++;
        if (waypointIndex < Waypoints.waypoints.Length)
        {
            target = Waypoints.waypoints[waypointIndex];
            //toWaypointDistance = Vector2.Distance(gameObject.transform.localPosition, target.transform.localPosition);
        }
        else
        {
            Player.health -= gameObject.GetComponent<baseEnemy>().getDamageToPlayer();
            if (Player.health < 0) Player.health = 0;
            Destroy(gameObject);
        }
        
    }

    private void rotateToTarget(Vector2 direction)
    {
        /*        Vector3 direction = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                float angle = rotation.eulerAngles.y;
                Debug.Log("Angle: " + angle);*/
        //Mathf.Rad2Deg - 90f
        targetZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
