using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CardCell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    private string identifier;
    private TaskGenerator _generator;
    private Restarter _restarter;

    public string Identifier => identifier;

    private void Awake()
    {
        Debug.Log("CardCell");
        _generator = FindObjectOfType<TaskGenerator>();
    }

    private void OnEnable()
    {
        _generator.OnTaskGenerated += OnDelete;
    }

    private void OnDisable()
    {
        _generator.OnTaskGenerated -= OnDelete;
    }

    public void Initialize(Sprite newSprite, string newIdentifier)
    {
        _renderer.sprite = newSprite;
        identifier = newIdentifier;
    }

    private void OnDelete(TaskGenerationResult result)
    {
        Destroy(gameObject);
    }
}
