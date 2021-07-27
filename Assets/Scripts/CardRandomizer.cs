using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRandomizer : MonoBehaviour
{
    public Card GetCard(Collection collection)
    {
        int randomIndex = Random.Range(0, collection.Count);
        Card newCard = collection.GetCard(randomIndex);
        return newCard;
    }

    public Card GetCard(Collection collection, string taskIdentifier)
    {
        Card newCard;
        while (true)
        {
            int randomIndex = Random.Range(0, collection.Count);
            newCard = collection.GetCard(randomIndex);
            if (newCard.Identifier != taskIdentifier)
            {
                break;
            }
        }
        return newCard;
    }
}
