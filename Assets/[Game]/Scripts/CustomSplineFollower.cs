using System.Collections;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class CustomSplineFollower : MonoBehaviour
{
    [SerializeField] private SplineComputer spline;
    [SerializeField] private float distance = 0f;

    private float speed = 0f;
    private Tween speedChangeTween;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        distance += speed * Time.deltaTime;

        SplineSample ss = spline.Evaluate(spline.Travel(0, distance));

        transform.position = ss.position;
        transform.rotation = Quaternion.Euler(0f, ss.rotation.eulerAngles.y, ss.rotation.eulerAngles.z); ;
    }

    public void SetSpeed(float speed, float duration = 0.25f)
    {
        speedChangeTween.Kill();
        speedChangeTween = DOTween.To(() => this.speed, val => this.speed = val, speed, duration);
    }
}
