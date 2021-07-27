using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Restarter : MonoBehaviour
{
    public UnityAction OnRestart;

    public void Restart(){
        OnRestart?.Invoke();
    }
}
