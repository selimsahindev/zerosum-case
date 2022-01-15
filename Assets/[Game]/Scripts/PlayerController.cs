using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SelimSahinUtils;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float xBounds = 2f;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float sideSpeed = 20f;

    private Animator animator;
    private CustomSplineFollower splineFollower;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        splineFollower = GetComponentInParent<CustomSplineFollower>();

        EventManager.AddListener(EventNames.OnGameStart, OnGameStart);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void OnGameStart()
    {
        animator.SetTrigger("Run");
        splineFollower.SetSpeed(forwardSpeed);
    }

    private void HandleMovement()
    {
        Vector3 pos = transform.localPosition;

        pos = Vector3.Lerp(pos, pos + transform.right * InputManager.Instance.Input.x * sideSpeed, Time.deltaTime);
        pos.x = Mathf.Clamp(pos.x, -xBounds, xBounds);

        transform.localPosition = pos;
    }
}
