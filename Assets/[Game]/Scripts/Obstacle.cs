using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SelimSahinUtils;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Transform mesh;
    [SerializeField] private float moveDuration = 1f;

    private void Awake()
    {
        mesh.DOLocalRotate(Vector3.back * 360f, 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
        transform.DOMove(transform.position + transform.forward * 2f, moveDuration).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            EventManager.NotifyListeners(EventNames.OnObstacleHitOccured);
        }
    }
}
