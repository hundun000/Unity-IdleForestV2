using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarTest : MonoBehaviour
{
    [SerializeField]
    float value;
    [SerializeField]
    StatusBarController statusBarController;

    void Update()
    {
        statusBarController.SetValue(value);
    }
    
}
