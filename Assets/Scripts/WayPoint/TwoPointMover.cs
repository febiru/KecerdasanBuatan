using UnityEngine;

public class TwoPointMover : MonoBehaviour
{
    public RouteToPointPOS route;
    public float speed = 2f;

    private Vector3 target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = route.pointA;
        target = route.pointB;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == target)
        {
            target = (target == route.pointA) ? route.pointB : route.pointA;
        }
    }
}