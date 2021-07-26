using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TaskText : MonoBehaviour
{
    private Text text;
    private TaskGenerator _taskGenerator;

    private void Awake()
    {
        Debug.Log("TaskText");
        text = GetComponent<Text>();
        _taskGenerator = FindObjectOfType<TaskGenerator>();
    }

    private void OnEnable()
    {
        _taskGenerator.OnTaskGenerated += SetText;
    }

    private void OnDisable()
    {
        _taskGenerator.OnTaskGenerated -= SetText;
    }

    private void SetText(TaskGenerationResult result)
    {
        text.text = "Find " + result.Identifier;
    }
}
