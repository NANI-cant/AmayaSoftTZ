using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardCollection", menuName = "ObjectsCreating/CardCollection")]
public class Collection : ScriptableObject
{
    [SerializeField] List<Card> cards;

    public int Count => cards.Count;

    public Card GetCard(int index)
    {
        if (index < 0 || index >= cards.Count)
        {
            Debug.LogException(new System.Exception("GetCard out of range"));
            return null;
        }
        return cards[index];
    }
}
