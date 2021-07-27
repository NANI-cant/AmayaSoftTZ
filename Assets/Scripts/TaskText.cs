using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Text))]
public class TaskText : MonoBehaviour
{
    private Text text;
    private TaskGenerator _taskGenerator;
    private Difficult _difficult;

    private void Awake()
    {
        Debug.Log("TaskText");
        text = GetComponent<Text>();
        _taskGenerator = FindObjectOfType<TaskGenerator>();
        _difficult = FindObjectOfType<Difficult>();
    }

    private void Start()
    {
        text.DOColor(new Color(0, 0, 0, 0), 0f);
        text.DOFade(1, 2f);
    }

    private void OnEnable()
    {
        _taskGenerator.OnTaskGenerated += SetText;
        _difficult.OnGameEnd += Hide;
    }

    private void OnDisable()
    {
        _taskGenerator.OnTaskGenerated -= SetText;
        _difficult.OnGameEnd -= Hide;
    }

    private void SetText(TaskGenerationResult result)
    {
        text.text = "Find " + result.Identifier;
    }

    private void Hide()
    {
        text.text = "";
    }
}
