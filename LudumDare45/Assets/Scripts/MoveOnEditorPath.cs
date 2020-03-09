using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnEditorPath : MonoBehaviour
{
    public EditorPath pathToFollow;
    public int currentWaypointID = 0;
    public float speed;
    public float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;

    Vector3 lastPos;
    Vector3 currentPos;
    
    private bool isMovingForward = true;


    void Start()
    {
        pathToFollow = GameObject.Find(pathName).GetComponent<EditorPath>();
        lastPos = transform.position;
    }
    
    void Update()
    {
        float distance = Vector3.Distance(pathToFollow.path_objs[currentWaypointID].position,transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathToFollow.path_objs[currentWaypointID].position,Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(pathToFollow.path_objs[currentWaypointID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if(distance <= reachDistance)
        {
            if (isMovingForward)
            {
                currentWaypointID++;
            }
            else
            {
                currentWaypointID--;
            }
        }
        if(currentWaypointID >= pathToFollow.path_objs.Count)
        {
            isMovingForward = false;
        }

        if (currentWaypointID == 0 && !isMovingForward)
        {
            isMovingForward = true;
        }
    }
}
