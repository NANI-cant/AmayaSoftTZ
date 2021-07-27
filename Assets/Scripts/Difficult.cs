using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CardSelecter))]
public class Difficult : MonoBehaviour
{
    [SerializeField] private int startCardCount;
    [SerializeField] private int addCards;
    [SerializeField] private int maxCards;
    [SerializeField] private UnityEvent gameEnding;

    public UnityAction OnLevelChange;
    public UnityAction OnGameEnd;

    private int currentCardCount = 0;
    private CardSelecter _selecter;

    public int CurrentCardCount => currentCardCount;
    public int StartCardCount => startCardCount;

    private void Awake()
    {
        Debug.Log("Difficult");
        currentCardCount = startCardCount;
        _selecter = GetComponent<CardSelecter>();
    }

    private void OnEnable()
    {
        _selecter.OnTaskCardSelected += ChangeDifficult;
    }

    private void OnDisable()
    {
        _selecter.OnTaskCardSelected -= ChangeDifficult;
    }

    private void ChangeDifficult()
    {
        currentCardCount += addCards;
        if (currentCardCount > maxCards)
        {
            gameEnding?.Invoke();
            OnGameEnd?.Invoke();
        }
        else
        {
            OnLevelChange?.Invoke();
        }
    }
}
