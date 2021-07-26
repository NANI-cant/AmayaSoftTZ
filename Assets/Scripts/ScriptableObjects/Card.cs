using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ObjectsCreating/CardData")]
public class Card : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private string identifier;

    public Sprite Sprite => sprite;
    public string Identifier => identifier;
}
