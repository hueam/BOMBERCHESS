using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class RoomMaker : MonoBehaviour
{
    [SerializeField] TMP_InputField roomName;

    public void GenerateRoom()
    {
        C_Makeroom packet = new();
        RoomInfo info = new();
        info.RoomId = string.Empty;
        info.RoomName = roomName.text;
        packet.UserId = NetworkManager.Instance.userId;
        packet.Info = info;
        NetworkManager.Instance.Send(packet);
    }
}
