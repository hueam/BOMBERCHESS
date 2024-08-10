using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyObjcetUI : MeshObjectUI
{
    public LobbyUIEnum type;
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        UIManager.Instance.GetContent<LobbyUIContent>().OpenUI(type);
    }
}
