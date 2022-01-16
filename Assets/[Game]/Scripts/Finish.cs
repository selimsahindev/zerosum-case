using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using SelimSahinUtils;

public class Finish : MonoBehaviour
{
    [SerializeField] private SplineComputer spline;
    [SerializeField] private float distanceFromEnd = 10f;
    [SerializeField] private ParticleSystem[] confettiRainbows;

    private void Awake()
    {
        AlignToSpline();

        for (int i = 0; i < confettiRainbows.Length; i++)
        {
            confettiRainbows[i].gameObject.SetActive(false);
        }
    }

    private void AlignToSpline()
    {
        if (spline != null)
        {
            float length = spline.CalculateLength();

            SplineSample ss = spline.Evaluate(spline.Travel(0, length - distanceFromEnd));

            transform.position = ss.position;
            transform.rotation = ss.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            EventManager.NotifyListeners(EventNames.OnGameOver, true);

            for (int i = 0; i < confettiRainbows.Length; i++)
            {
                confettiRainbows[i].gameObject.SetActive(true);
                confettiRainbows[i].Play();
            }
        }
    }
}
