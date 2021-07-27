using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Restarter : MonoBehaviour
{
    [SerializeField] private UnityEvent actionsAfterFadeIn;
    [SerializeField] private Fader restartFade;

    public UnityAction OnRestart;

    private void DeleteAllCards(){
        CardCell[] cardCells = FindObjectsOfType<CardCell>();
        foreach (CardCell card in cardCells)
        {
            Destroy(card.gameObject);
        }
    }

    private void WaitForFade(){
        actionsAfterFadeIn?.Invoke();
        DeleteAllCards();
        restartFade.Fade(0);
        restartFade.gameObject.SetActive(false);
        OnRestart?.Invoke();
    }
    
    public void Restart(){
        restartFade.gameObject.SetActive(true);
        restartFade.Fade(1);
        Invoke(nameof(WaitForFade),1f);
    }
}
