using UnityEngine;

public class SpikedBallSwing : MonoBehaviour
{
    [SerializeField] private float swingAngle = 45f;
    [SerializeField] private float swingSpeed = 2f;
    [SerializeField] private float phaseOffset;

    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        float angle = Mathf.Sin(Time.time * swingSpeed + phaseOffset) * swingAngle;
        transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, angle);
    }
}
