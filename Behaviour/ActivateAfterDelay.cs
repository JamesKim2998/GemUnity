using System;
using UnityEngine;
using System.Collections;

public class ActivateAfterDelay : MonoBehaviour
{
    public GameObject target;
    public float delay;
    public bool active_ = true;
    public bool destroyAfterExecute = true;

    void Start()
    {
        Invoke("Execute", delay);
    }

    void Execute()
    {
        target.SetActive(active_);
        if (destroyAfterExecute)
            Destroy(this);
    }
}
