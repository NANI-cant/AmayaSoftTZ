using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Difficult))]
[RequireComponent(typeof(CardRandomizer))]
[RequireComponent(typeof(TaskGenerator))]
public class CardSpawner : MonoBehaviour
{
    [SerializeField] private CardCell template;

    private TaskGenerator _taskGenerator;
    private Difficult _difficult;
    private CardRandomizer _randomizer;

    readonly private float cardSize = 3.5f;
    readonly private float paddingSize = 1f;

    private void Awake()
    {
        Debug.Log("CardSpawner");
        _taskGenerator = GetComponent<TaskGenerator>();
        _difficult = GetComponent<Difficult>();
        _randomizer = GetComponent<CardRandomizer>();
    }

    private void OnEnable()
    {
        _taskGenerator.OnTaskGenerated += CreateCards;
    }

    private void OnDisable()
    {
        _taskGenerator.OnTaskGenerated -= CreateCards;
    }

    private void CreateCards(TaskGenerationResult result)
    {
        int cardCount = _difficult.CurrentCardCount;
        KeyValuePair<int, int> widthHeight = CalculateWidthHeight(cardCount);
        int width = widthHeight.Key;
        int height = widthHeight.Value;

        Vector2 currentPosition = (Vector2)transform.position - new Vector2((width * cardSize + (width - 1) * paddingSize) / 2 - cardSize / 2, -((height * cardSize + (height - 1) * paddingSize) / 2 - cardSize / 2));
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Card newCard = _randomizer.GetCard(result.Collection,result.Identifier);
                CardCell newCardCell =  Instantiate(template, currentPosition, Quaternion.identity);
                newCardCell.Initialize(newCard.Sprite,newCard.Identifier);

                currentPosition.x += cardSize + paddingSize;
            }
            currentPosition.x -= width * (cardSize + paddingSize);
            currentPosition.y -= cardSize + paddingSize;
        }
    }

    private KeyValuePair<int, int> CalculateWidthHeight(int cardCount)
    {
        KeyValuePair<int, int> widthHeightPair = new KeyValuePair<int, int>();
        int minDif = cardCount;

        for (int height = 1; height <= cardCount; height++)
        {
            if (cardCount % height == 0)
            {
                int width = cardCount / height;
                int newMinDif = Mathf.Abs(height - width);
                if (newMinDif < minDif)
                {
                    minDif = newMinDif;
                    widthHeightPair = new KeyValuePair<int, int>(width, height);
                }
            }
        }

        Debug.Log("Find W h" + widthHeightPair.Key.ToString() + widthHeightPair.Value.ToString());
        return widthHeightPair;
    }
}
