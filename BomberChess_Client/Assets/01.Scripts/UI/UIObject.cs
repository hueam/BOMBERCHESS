using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIObject : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
        OnOpen();
    }
    protected abstract void OnOpen();
    public void Close()
    {
        gameObject.SetActive(false);
        OnClose();
    }
    protected abstract void OnClose();
}
