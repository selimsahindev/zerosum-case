using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SelimSahinUtils;

public class Collectable : MonoBehaviour
{
    public enum CollectableType { Currency, Stack }

    [SerializeField] private CollectableType type;
    public CollectableType Type => type;

    [SerializeField] private int value;
    public int Value => value;

    [SerializeField] private Transform mesh;
    private PlayerController player;

    private void Awake()
    {
        mesh.DOLocalRotate(Vector3.up * 360f, 1.6f, RotateMode.LocalAxisAdd).SetLoops(-1);
    }

    private void Disappear()
    {
        float duration = 0.45f;

        GetComponent<Collider>().enabled = false;

        mesh.DOKill();
        Sequence seq = DOTween.Sequence();
        seq.Append(mesh.DOScale(0f, duration));
        seq.OnComplete(() => Destroy(gameObject));

        if (type == CollectableType.Currency)
        {
            ParticleSystem particle = LevelManager.Instance.coinSplashParticlePool.Pop();
            particle.transform.position = mesh.transform.position;
            particle.transform.localScale = Vector3.one * 0.2f;
            particle.Play();

            // Recycle the object
            DelayManager.WaitAndInvoke(() => {
                LevelManager.Instance.coinSplashParticlePool.Push(particle);
            }, particle.main.duration);
        }
        else if (type == CollectableType.Stack)
        {
            transform.SetParent(player.transform);
            //seq.Join(mesh.DOLocalMove(Vector3.up * 3.5f + Vector3.left * 1.25f, duration));
            seq.Join(mesh.DOLocalMove(player.stackBar.diamondImage.transform.position - player.transform.position, duration));
        }

        seq.SetEase(Ease.InCirc);
        seq.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.NotifyListeners(EventNames.OnCollectableInteraction, this);
            player = other.GetComponent<PlayerController>();
            Disappear();
        }
    }
}
