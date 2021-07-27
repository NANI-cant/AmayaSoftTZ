using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[RequireComponent(typeof(Image))]
public class FadeInOut : MonoBehaviour
{
    public void FadeIn(float endValue)
    {
        Image image = GetComponent<Image>();
        image.DOFade(endValue, 1f);
    }

    public void FadeOut(float endValue)
    {
        Image image = GetComponent<Image>();
        image.DOFade(endValue, 1f);
    }
}
