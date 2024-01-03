using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{
    private float speed; // The speed at which the enemy moves
    private readonly float turnSpeed = 5f;

    private float targetY;
    private Vector3 target; // The next point on the path the enemy needs to reach

    public int waypointIndex = 1; // The index of the current waypoint in the path
    public float distance;
    //public float toWaypointDistance;

    void Start()
    {
        speed = gameObject.GetComponent<baseEnemy>().GetSpeed();
        target = new Vector3(Waypoints.waypoints[1].x, 0.2f, Waypoints.waypoints[1].y); // Set the target to the first waypoint
        //transform.LookAt(target);
        //transform.Rotate(new Vector3(0, 90, 0));
        Vector3 targetDirection = new Vector3(target.x, target.y, target.z) - transform.position;
        targetY = Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, targetY + 90, 0f);
        transform.rotation = targetRotation;
        distance = Vector3.Distance(gameObject.transform.position, target);
    }

    private void Update()
    {
        // Move the enemy towards the target waypoint
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // Calculate the remaining distance between enemy and target waypoint
        distance = Vector3.Distance(gameObject.transform.position, target);

        // If the enemy has reached the target waypoint, set the next target waypoint
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            SetNextTarget();
        }

        Vector3 targetDirection = new Vector3(target.x, target.y, target.z) - transform.position;
        RotateToTarget(targetDirection);


    }

    private void SetNextTarget()
    {
        waypointIndex++;
        if (waypointIndex < Waypoints.waypoints.Length)
        {
            target = new Vector3(Waypoints.waypoints[waypointIndex].x, 0.2f, Waypoints.waypoints[waypointIndex].y);
        }
        else
        {
            Player.DamagePlayer(gameObject.GetComponent<baseEnemy>().GetDamageToPlayer());
            Destroy(gameObject);
        }

    }

    private void RotateToTarget(Vector3 direction)
    {
        targetY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, targetY, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
