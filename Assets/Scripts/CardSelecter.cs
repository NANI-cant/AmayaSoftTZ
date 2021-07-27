using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Difficult))]
public class CardSelecter : MonoBehaviour
{
    [SerializeField] private LayerMask cardLayer;
    [SerializeField] private Camera mainCamera;

    public UnityAction OnTaskCardSelected;

    private string taskIdentifier = "";
    private TaskGenerator _taskGenerator;
    private Difficult _difficult;
    private bool canSelect = true;

    private void Awake()
    {
        Debug.Log("Selecter");
        _taskGenerator = GetComponent<TaskGenerator>();
        _difficult = GetComponent<Difficult>();
    }

    private void OnEnable()
    {
        _taskGenerator.OnTaskGenerated += SetTaskIdentifier;
        _difficult.OnGameEnd += OnCantSelect;
    }

    private void OnDisable()
    {
        _taskGenerator.OnTaskGenerated -= SetTaskIdentifier;
        _difficult.OnGameEnd -= OnCantSelect;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canSelect)
        {
            if (TrySelectCard(mainCamera.ScreenToWorldPoint(Input.mousePosition), out CardCell cardCell))
            {
                if (cardCell.Identifier == taskIdentifier)
                {
                    cardCell.GetComponent<CardCellEffector>().CorrectCardSelected();
                    Invoke(nameof(WaitForEffect), 0.6f);
                }
                else
                {
                    cardCell.GetComponent<CardCellEffector>().WrongCardSelected();
                }
            }
        }
    }

    private bool TrySelectCard(Vector2 point, out CardCell cardCell)
    {
        RaycastHit2D hitResult = Physics2D.Raycast(point, Vector2.zero);
        if (hitResult.collider != null)
        {
            cardCell = hitResult.collider.GetComponent<CardCell>();
            return true;
        }
        else
        {
            cardCell = null;
            return false;
        }
    }

    private void WaitForEffect()
    {
        OnTaskCardSelected?.Invoke();
    }

    private void SetTaskIdentifier(TaskGenerationResult result)
    {
        taskIdentifier = result.Identifier;
    }

    private void OnCantSelect()
    {
        canSelect = false;
    }

    private void OnCanSelect()
    {
        canSelect = true;
    }
}
