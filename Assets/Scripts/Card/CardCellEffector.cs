using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CardCellEffector : MonoBehaviour
{
    [SerializeField] private Transform objectInCard;
    [SerializeField] private UnityEvent onWrong;
    [SerializeField] private UnityEvent onCorrect;

    private bool isSelected = false;

    private void Start()
    {
        Difficult dif = FindObjectOfType<Difficult>();
        if (dif.StartCardCount == dif.CurrentCardCount)
        {
            DoBounce();
        }
    }

    public void CardSelected(bool isCorrect)
    {
        if (!isSelected)
        {
            isSelected = true;
            if(isCorrect){
                onCorrect?.Invoke();
            }else{
                onWrong?.Invoke();
            }
            Invoke(nameof(ResetStatus), 0.5f);
        }
    }

    private void ResetStatus()
    {
        isSelected = false;
    }

    public void DoBounce()
    {
        objectInCard.DOShakeScale(0.5f, new Vector3(0.15f, 0.15f, 0f), 10, 0);
    }

    public void DoShake()
    {
        objectInCard.DOShakePosition(0.5f, 0.15f);
    }
}
