using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : LobbyUI
{

    private Dictionary<int, RoomUserInfo> _id2Info;

    [SerializeField] Button startBtn;
    public void UpdateInfo()
    {

    }
    public void Ready()
    {
        C_Ready p = new();
        NetworkManager.Instance.Send(p);
    }
}
