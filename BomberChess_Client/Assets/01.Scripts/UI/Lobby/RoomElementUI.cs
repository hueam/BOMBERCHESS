using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Google.Protobuf.Protocol;

public class RoomElementUI : PoolableMono
{
    [SerializeField] private TextMeshProUGUI roomNameTxt;

    public override void Init()
    {
    }

    public void RoomSetting(RoomInfo info)
    {
        roomNameTxt.text = info.RoomName;
    }
}
