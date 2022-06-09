using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCarScript : MonoBehaviour
{
    private GameObject car;
    private GameObject pointA;
    private GameObject pointB;
    private float step;
    public float speed;
    private Vector3 alturaY;

    void Start()
    {
        car = this.gameObject.transform.GetChild(0).gameObject;
        pointA = this.gameObject.transform.GetChild(1).gameObject;
        pointB = this.gameObject.transform.GetChild(2).gameObject;
        speed = 30f;
    }

    void Update()
    {
        step =  speed * Time.deltaTime; // calculate distance to move
        car.transform.position = Vector3.MoveTowards(car.transform.position, pointB.transform.position, step);
        alturaY = new Vector3 (car.transform.position.x,0.5f,car.transform.position.z);
        car.transform.position = alturaY;
        if (Vector3.Distance(car.transform.position, pointB.transform.position) < 0.1f)
        {
            car.transform.position = pointA.transform.position;
        }
    }
}
