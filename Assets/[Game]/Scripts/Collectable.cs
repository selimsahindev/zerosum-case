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

    private void Awake()
    {
        mesh.DOLocalRotate(Vector3.up * 360f, 1.6f, RotateMode.LocalAxisAdd).SetLoops(-1);
    }

    private void Disappear()
    {
        GetComponent<Collider>().enabled = false;
        mesh.DOKill();
        mesh.DOScale(0f, 0.25f).OnComplete(() => Destroy(gameObject));

        if (type == CollectableType.Currency)
        {
            ParticleSystem particle = Instantiate(LevelManager.Instance.coinSplashPrefab);
            particle.transform.position = mesh.transform.position;
            particle.transform.localScale = Vector3.one * 0.2f;
            particle.Play();
            Destroy(particle, particle.main.duration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.NotifyListeners(EventNames.OnCollectableInteraction, this);
            Disappear();
        }
    }
}
