using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SelimSahinUtils;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float xBounds = 2f;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float sideSpeed = 20f;
    [SerializeField] private int maximumStackLimit = 3;

    private bool isMoving = false;
    private int currentStackAmount = 0;
    private Animator animator;
    private CustomSplineFollower splineFollower;
    private StackBar stackBar;
    private Tween animatorTween;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        splineFollower = GetComponentInParent<CustomSplineFollower>();
        stackBar = GetComponentInChildren<StackBar>();

        EventManager.AddListener(EventNames.OnGameStart, HandleGameStartEvent);
        EventManager.AddListener(EventNames.OnGameOver, data => HandleGameOverEvent((bool)data));
        EventManager.AddListener(EventNames.OnCollectableInteraction, data => HandleCollectableInteraction((Collectable)data));
    }

    private void Update()
    {
        if (isMoving)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector3 pos = transform.localPosition;

        pos = Vector3.Lerp(pos, pos + transform.right * InputManager.Instance.Input.x * sideSpeed, Time.deltaTime);
        pos.x = Mathf.Clamp(pos.x, -xBounds, xBounds);

        transform.localPosition = pos;
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

    private void HandleCollectableInteraction(Collectable collectable)
    {
        if (collectable.Type == Collectable.CollectableType.Stack)
        {
            currentStackAmount++;

            float stackPercentage = maximumStackLimit == 0f ? 0f : currentStackAmount / maximumStackLimit;

            stackBar.SetFillAmount(stackPercentage);

            SetAnimatorVelocity(stackPercentage);
        }
    }

    private void SetAnimatorVelocity(float velocity, float duration = 0.25f)
    {
        animatorTween.Kill();
        animatorTween = DOTween.To(() => animator.GetFloat("Velocity"), val => animator.SetFloat("Velocity", val), velocity, duration);
    }
}
