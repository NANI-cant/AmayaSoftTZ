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
    private Restarter _restarter;

    public int CurrentCardCount => currentCardCount;
    public int StartCardCount => startCardCount;

    private void Awake()
    {
        Debug.Log("Difficult");
        currentCardCount = startCardCount;
        _selecter = GetComponent<CardSelecter>();
        //_restarter = FindObjectOfType<Restarter>();
    }

    private void OnEnable()
    {
        _selecter.OnTaskCardSelected += ChangeDifficult;
        //_restarter.OnRestart += ResetDifficult;
    }

    private void OnDisable()
    {
        _selecter.OnTaskCardSelected -= ChangeDifficult;
        //_restarter.OnRestart -= ResetDifficult;
    }

    private void ChangeDifficult()
    {
        currentCardCount += addCards;
        if (currentCardCount > maxCards)
        {
            gameEnding?.Invoke();
            OnGameEnd?.Invoke();
            ResetDifficult();
        }
        else
        {
            OnLevelChange?.Invoke();
        }
    }

    private void ResetDifficult(){
        currentCardCount = startCardCount;
    }
}
