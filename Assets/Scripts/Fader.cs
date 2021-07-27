using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class Fader : MonoBehaviour
{
    public void Fade(float endValue)
    {
        Image image = GetComponent<Image>();
        image.DOFade(endValue, 1f);
    }
}
