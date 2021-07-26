using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TaskGenerationResult
{
    private string identifier;
    private Collection collection;

    public TaskGenerationResult(string id, Collection col)
    {
        identifier = id;
        collection = col;
    }

    public string Identifier => identifier;
    public Collection Collection => collection;
}

[RequireComponent(typeof(CardRandomizer))]
public class TaskGenerator : MonoBehaviour
{
    [SerializeField] private List<Collection> cardCollections;

    public UnityAction<TaskGenerationResult> OnTaskGenerated;

    private List<string> usedIdentifierds = new List<string>();
    private CardRandomizer _randomizer;

    private void Awake()
    {
        Debug.Log("TaskGenerator");
        _randomizer = GetComponent<CardRandomizer>();
    }

    private void Start()
    {
        GenerateTask();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void GenerateTask()
    {
        int currentCollection = Random.Range(0, cardCollections.Count);
        Card newCard = new Card();
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
        OnTaskGenerated?.Invoke(new TaskGenerationResult(newCard.Identifier, cardCollections[currentCollection]));
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
