using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event System.Action Destroyed;

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
}
