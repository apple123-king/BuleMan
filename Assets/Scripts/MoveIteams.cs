using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveIteams : MonoBehaviour
{
    //// Start is called before the first frame update
    //[SerializeField] private float speed=10f;
    //[SerializeField] GameObject Point1;
    //[SerializeField] GameObject Point2;
    //[SerializeField] private Rigidbody2D rb;
    //private float checkDistance = 0.2f; // 检测距离，可以根据需要调整

    //void Start()
    //{
    //    rb=GetComponent<Rigidbody2D>();
    //    if (rb == null)
    //    {
    //        print("Rigidbody2D component not found on " + gameObject.name);
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //if (transform.position==Point1.transform.position)
    //    //{
    //    //   rb.velocity=new Vector2(0,-speed);
    //    //}
    //    //if (transform.position==Point2.transform.position)
    //    //{
    //    //    rb.velocity=new Vector2(0,speed);
    //    //}
    //    if (Vector2.Distance(transform.position, Point1.transform.position) < checkDistance)
    //    {
    //        rb.velocity = new Vector2(speed,0);
    //    }
    //    // 检测是否到达Point2（下边界）
    //    else if (Vector2.Distance(transform.position, Point2.transform.position) < checkDistance)
    //    {
    //        rb.velocity = new Vector2(-speed,0);
    //    }
    //}

    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] public float speed = 2f;
    private int currentWayPointIndex = 0;

    private void Update()
    {
        if(Vector2.Distance(wayPoints[currentWayPointIndex].transform.position,transform.position) < 0.1f)
        {
            currentWayPointIndex++;
            if(currentWayPointIndex >= wayPoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);
    }


}
