using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;

public class Room : LobbyUI
{

    private Dictionary<int, RoomUserInfo> _id2Info;
    public void UpdateInfo()
    {

    }
    public void Ready()
    {
        C_Ready p = new();
        NetworkManager.Instance.Send(p);
    }
}
