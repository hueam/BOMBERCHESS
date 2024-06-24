using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LobbyUI : MonoBehaviour
{
    public LobbyUIEnum type;
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
