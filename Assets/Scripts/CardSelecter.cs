using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Difficult))]
public class CardSelecter : MonoBehaviour
{
    [SerializeField] private LayerMask cardLayer;
    [SerializeField] private Camera mainCamera;

    private string taskIdentifier = "";
    private TaskGenerator _taskGenerator;
    private Difficult _difficult;
    private Restarter _restarter;
    private bool canSelect = true;

    public UnityAction OnTaskCardSelected;

    private void Awake()
    {
        _taskGenerator = GetComponent<TaskGenerator>();
        _difficult = GetComponent<Difficult>();
        _restarter = FindObjectOfType<Restarter>();
    }

    private void OnEnable()
    {
        _taskGenerator.OnTaskGenerated += SetTaskIdentifier;
        _difficult.OnGameEnd += OnCantSelect;
        _restarter.OnRestart +=OnCanSelect;
    }

    private void OnDisable()
    {
        _taskGenerator.OnTaskGenerated -= SetTaskIdentifier;
        _difficult.OnGameEnd -= OnCantSelect;
        _restarter.OnRestart -=OnCanSelect;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canSelect)
        {
            if (TrySelectCard(mainCamera.ScreenToWorldPoint(Input.mousePosition), out CardCell cardCell))
            {
                if (cardCell.Identifier == taskIdentifier)
                {
                    cardCell.GetComponent<CardCellEffector>().CardSelected(true);
                    Invoke(nameof(WaitForEffect), 0.5f);
                }
                else
                {
                    cardCell.GetComponent<CardCellEffector>().CardSelected(false);
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
