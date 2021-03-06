using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SelimSahinUtils;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public StackBar stackBar { get; private set; }

    [SerializeField] private float xBounds = 2f;
    [SerializeField] private float forwardSpeed = 10f;
    [SerializeField] private float sideSpeed = 20f;
    [SerializeField] private int maximumStackLimit = 3;
    [SerializeField] private ParticleSystem windlines;

    private bool isMoving = false;
    private int currentStackAmount = 0;
    private Animator animator;
    private CustomSplineFollower splineFollower;
    private Tween animatorTween;
    private ParticleSystem cuteEmojiPrefab;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        splineFollower = GetComponentInParent<CustomSplineFollower>();
        stackBar = GetComponentInChildren<StackBar>();
        cuteEmojiPrefab = Resources.Load<ParticleSystem>("Particles/EmojiCute");

        stackBar.Active(false);
        windlines.gameObject.SetActive(false);

        EventManager.AddListener(EventNames.OnGameStart, HandleGameStartEvent);
        EventManager.AddListener(EventNames.OnGameOver, data => HandleGameOverEvent((bool)data));
        EventManager.AddListener(EventNames.OnCollectableInteraction, data => HandleCollectableInteraction((Collectable)data));
        EventManager.AddListener(EventNames.OnObstacleHitOccured, HandleObstacleHit);
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

        pos = Vector3.Lerp(pos, pos + Vector3.right * InputManager.Instance.Input.x * sideSpeed, Time.deltaTime);
        pos.x = Mathf.Clamp(pos.x, -xBounds, xBounds);

        transform.localPosition = pos;
    }

    private void HandleGameStartEvent()
    {
        isMoving = true;
        animator.SetTrigger("Run");
        splineFollower.SetSpeed(forwardSpeed);
        stackBar.ActiveSmooth(true);
    }

    private void HandleGameOverEvent(bool success)
    {
        isMoving = false;
        stackBar.ActiveSmooth(false);
        windlines.gameObject.SetActive(false);

        ParticleSystem emoji = Instantiate(cuteEmojiPrefab, transform);
        emoji.transform.localPosition = Vector3.up * 3.5f;
        emoji.Play();

        DelayManager.WaitAndInvoke(() => splineFollower.SetSpeed(0f, onComplete: () => animator.SetTrigger("Dance")), 0.1f);
    }

    private void HandleObstacleHit()
    {
        SetStack(currentStackAmount - 3);
    }

    private void HandleCollectableInteraction(Collectable collectable)
    {
        if (collectable.Type == Collectable.CollectableType.Stack)
        {
            SetStack(currentStackAmount + 1);
        }
    }

    private void SetStack(int stack)
    {
        currentStackAmount = Mathf.Clamp(stack, 0, maximumStackLimit);

        float stackPercentage = maximumStackLimit == 0f ? 0f : (float)currentStackAmount / maximumStackLimit;

        stackBar.SetFillAmount(stackPercentage);

        SetAnimatorVelocity(stackPercentage);

        if (stackPercentage >= 0.7f)
        {
            windlines.gameObject.SetActive(true);
            windlines.Play();
        }
        else
        {
            windlines.gameObject.SetActive(false);
        }
    }

    private void SetAnimatorVelocity(float velocity, float duration = 0.25f)
    {
        float currentVelocity = animator.GetFloat("Velocity");

        animatorTween.Kill();
        animatorTween = DOTween.To(() => currentVelocity, val => {
            currentVelocity = val;
            animator.SetFloat("Velocity", val);
        }, velocity, duration);
    }
}
