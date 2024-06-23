using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;
using UnityEngine.UI;

public class RoomListUI : MonoBehaviour
{
    [SerializeField] private Button roomBtnPref;
    [SerializeField]private ScrollRect scroll;

    public void AddRoomList()
    {
        Button btn = Instantiate(roomBtnPref);
        btn.transform.SetParent(scroll.content);
    }
}
