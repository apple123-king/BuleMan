using UnityEngine;

public class MoveIteams : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] public float speed = 2f;
    [SerializeField] private bool reverseAtEnds;

    private int currentWayPointIndex = 0;
    private int direction = 1;

    public void Configure(GameObject[] points, float moveSpeed, bool shouldReverseAtEnds)
    {
        wayPoints = points;
        speed = moveSpeed;
        reverseAtEnds = shouldReverseAtEnds;
        currentWayPointIndex = 0;
        direction = 1;
    }

    private void Update()
    {
        if (wayPoints == null || wayPoints.Length == 0 || wayPoints[currentWayPointIndex] == null)
        {
            return;
        }

        Transform target = wayPoints[currentWayPointIndex].transform;
        if (Vector2.Distance(target.position, transform.position) < 0.1f)
        {
            SelectNextPoint();
        }

        target = wayPoints[currentWayPointIndex].transform;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void SelectNextPoint()
    {
        if (!reverseAtEnds || wayPoints.Length < 2)
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
            return;
        }

        if (currentWayPointIndex == wayPoints.Length - 1)
        {
            direction = -1;
        }
        else if (currentWayPointIndex == 0)
        {
            direction = 1;
        }

        currentWayPointIndex += direction;
    }
}
