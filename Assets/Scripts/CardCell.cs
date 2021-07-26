using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CardCell : MonoBehaviour
{
    private string identifier;
    private SpriteRenderer _renderer;

    private void Awake(){
        Debug.Log("CardCell");
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Sprite newSprite, string newIdentifier){
        _renderer.sprite = newSprite;
        identifier = newIdentifier;
    }
}
