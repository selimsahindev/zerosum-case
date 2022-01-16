using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SelimSahinUtils;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float xBounds = 2f;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float sideSpeed = 20f;

    private bool isMoving = false;
    private Animator animator;
    private CustomSplineFollower splineFollower;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        splineFollower = GetComponentInParent<CustomSplineFollower>();

        EventManager.AddListener(EventNames.OnGameStart, HandleGameStartEvent);
        EventManager.AddListener(EventNames.OnGameOver, data => HandleGameOverEvent((bool)data));
    }

    private void Update()
    {
        if (isMoving)
        {
            HandleMovement();
        }
    }

    private void HandleGameStartEvent()
    {
        isMoving = true;
        animator.SetTrigger("Run");
        splineFollower.SetSpeed(forwardSpeed);
    }

    private void HandleGameOverEvent(bool success)
    {
        DelayManager.WaitAndInvoke(() => splineFollower.SetSpeed(0f, onComplete: () => animator.SetTrigger("Dance")), 0.25f);

        isMoving = false;
    }

    private void HandleMovement()
    {
        Vector3 pos = transform.localPosition;

        pos = Vector3.Lerp(pos, pos + transform.right * InputManager.Instance.Input.x * sideSpeed, Time.deltaTime);
        pos.x = Mathf.Clamp(pos.x, -xBounds, xBounds);

        transform.localPosition = pos;
    }
}
