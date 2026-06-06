using UnityEngine;

public class PatrolHazard : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool reverseAtEnds = true;
    [SerializeField] private bool faceMovement = true;

    private int currentPointIndex;
    private int direction = 1;

    public void Configure(Transform[] patrolPoints, float patrolSpeed)
    {
        points = patrolPoints;
        speed = patrolSpeed;
    }

    private void Update()
    {
        if (points == null || points.Length == 0 || points[currentPointIndex] == null)
        {
            return;
        }

        Transform target = points[currentPointIndex];
        Vector3 oldPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (faceMovement)
        {
            Vector3 delta = transform.position - oldPosition;
            if (Mathf.Abs(delta.x) > 0.001f)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(delta.x) * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
        }

        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            SelectNextPoint();
        }
    }

    private void SelectNextPoint()
    {
        if (!reverseAtEnds || points.Length < 2)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length;
            return;
        }

        if (currentPointIndex == points.Length - 1)
        {
            direction = -1;
        }
        else if (currentPointIndex == 0)
        {
            direction = 1;
        }

        currentPointIndex += direction;
    }
}
