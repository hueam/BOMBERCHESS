using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class RoomMaker : UIObject
{
    [SerializeField] private TMP_InputField _roomName;

    public void GenerateRoom()
    {
        C_Makeroom packet = new();
        packet.UserId = NetworkManager.Instance.userId;
        RoomInfo info = new();
        info.RoomId = string.Empty;
        info.RoomName = _roomName.text;
        info.MaxUser = 2;
        // info.Start = false;
        packet.Info = info;
        NetworkManager.Instance.Send(packet);
    }

    protected override void OnClose()
    {
    }

    protected override void OnOpen()
    {
    }
}
