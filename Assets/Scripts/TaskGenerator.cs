using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TaskGenerationResult
{
    private string identifier;
    private Sprite sprite;
    private Collection collection;

    public TaskGenerationResult(string id, Sprite sp, Collection col)
    {
        identifier = id;
        sprite = sp;
        collection = col;
    }

    public string Identifier => identifier;
    public Sprite Sprite => sprite;
    public Collection Collection => collection;
}

[RequireComponent(typeof(CardRandomizer))]
[RequireComponent(typeof(CardSelecter))]
public class TaskGenerator : MonoBehaviour
{
    [SerializeField] private List<Collection> cardCollections;

    public UnityAction<TaskGenerationResult> OnTaskGenerated;

    private List<string> usedIdentifierds = new List<string>();
    private CardRandomizer _randomizer;
    private Difficult _difficult;
    private Restarter _restarter;

    private void Awake()
    {
        Debug.Log("TaskGenerator");
        _randomizer = GetComponent<CardRandomizer>();
        _difficult = GetComponent<Difficult>();
        _restarter = FindObjectOfType<Restarter>();
    }

    private void Start()
    {
        GenerateTask();
    }

    private void OnEnable()
    {
        _difficult.OnLevelChange += GenerateTask;
        _restarter.OnRestart += GenerateTask;
    }

    private void OnDisable()
    {
        _difficult.OnLevelChange -= GenerateTask;
        _restarter.OnRestart -= GenerateTask;
    }

    private void GenerateTask()
    {
        int currentCollection = Random.Range(0, cardCollections.Count);
        Card newCard;
        while (true)
        {
            newCard = _randomizer.GetCard(cardCollections[currentCollection]);

            Debug.Log("New Card " + newCard.Identifier);

            if (CheckIdentifier(newCard.Identifier))
            {
                usedIdentifierds.Add(newCard.Identifier);
                break;
            }
        }
        OnTaskGenerated?.Invoke(new TaskGenerationResult(newCard.Identifier, newCard.Sprite, cardCollections[currentCollection]));
    }

    private bool CheckIdentifier(string id)
    {
        for (int i = 0; i < usedIdentifierds.Count; i++)
        {
            if (usedIdentifierds[i] == id)
            {
                return false;
            }
        }
        return true;
    }
}
