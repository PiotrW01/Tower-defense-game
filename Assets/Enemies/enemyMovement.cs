using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    private float speed; // The speed at which the enemy moves
    private readonly float turnSpeed = 5f;

    private float targetZ;
    private Vector2 target; // The next point on the path the enemy needs to reach

    public int waypointIndex = 1; // The index of the current waypoint in the path
    public float distance;
    //public float toWaypointDistance;

    void Start()
    {
        speed = gameObject.GetComponent<baseEnemy>().GetSpeed();
        target = Waypoints.waypoints[1]; // Set the target to the first waypoint
        //transform.LookAt(target);
        //transform.Rotate(new Vector3(0, 90, 0));
        Vector2 targetDirection = new Vector3(target.x, target.y, 0) - transform.position;
        targetZ = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZ - 90);
        transform.rotation = targetRotation;
        distance = Vector2.Distance(gameObject.transform.position, target);
    }

    private void FixedUpdate()
    {
        // Move the enemy towards the target waypoint
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // Calculate the remaining distance between enemy and target waypoint
        distance = Vector2.Distance(gameObject.transform.position, target);

        // If the enemy has reached the target waypoint, set the next target waypoint
        if (Vector2.Distance(transform.position, target) < 0.001f)
        {
            SetNextTarget();
        }

        Vector2 targetDirection = new Vector3(target.x,target.y,0) - transform.position;
        RotateToTarget(targetDirection);


    }

    private void SetNextTarget()
    {
        waypointIndex++;
        if (waypointIndex < Waypoints.waypoints.Length)
        {
            target = Waypoints.waypoints[waypointIndex];
        }
        else
        {
            Player.DamagePlayer(gameObject.GetComponent<baseEnemy>().GetDamageToPlayer());
            Destroy(gameObject);
        }
        
    }

    private void RotateToTarget(Vector2 direction)
    {
        targetZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZ - 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
