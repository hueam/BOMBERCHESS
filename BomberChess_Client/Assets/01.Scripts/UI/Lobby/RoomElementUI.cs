using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Google.Protobuf.Protocol;

public class RoomElementUI : PoolableMono
{
    private RoomInfo _info;
    [SerializeField] private TextMeshProUGUI roomNameTxt;

    public override void Init()
    {
    }

    public void RoomSetting(RoomInfo info)
    {
        _info = info;
        roomNameTxt.text = info.RoomName;
    }
    public void EnterRoom()
    {
        C_Enterroom packet = new();
        packet.UserId = NetworkManager.Instance.userId;
        packet.RoomId = _info.RoomId; 

        NetworkManager.Instance.Send(packet);
    }
}
