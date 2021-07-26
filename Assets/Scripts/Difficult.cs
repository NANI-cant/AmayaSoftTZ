using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficult : MonoBehaviour
{
    [SerializeField] private int startCardCount;
    [SerializeField] private int addCards;

    private int currentCardCount = 0;
    private TaskGenerator _taskGenerator;

    public int CurrentCardCount => currentCardCount;

    private void Awake()
    {
        Debug.Log("Difficult");
        currentCardCount = startCardCount;
    }
}
