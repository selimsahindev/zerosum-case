using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SelimSahinUtils;

public class PlayerController : MonoBehaviour
{
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
        splineFollower.SetSpeed(10f);
    }

    private void HandleMovement()
    {

    }
}
