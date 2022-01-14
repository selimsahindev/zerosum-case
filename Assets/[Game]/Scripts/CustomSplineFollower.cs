using Dreamteck.Splines;
using UnityEngine;

public class CustomSplineFollower : MonoBehaviour
{
    [SerializeField] private SplineComputer spline;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float distance = 0f;

    [HideInInspector] public bool isMoving = false;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        //if (!isMoving) return;

        distance += speed * Time.deltaTime;

        SplineSample ss = spline.Evaluate(spline.Travel(0, distance));

        transform.position = ss.position;
        transform.rotation = Quaternion.Euler(0f, ss.rotation.eulerAngles.y, ss.rotation.eulerAngles.z); ;
    }
}
