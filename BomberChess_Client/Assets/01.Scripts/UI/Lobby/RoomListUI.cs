using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListUI : MonoBehaviour
{
    [SerializeField] private Button RoomInfo;
    [SerializeField]private ScrollRect scroll;

    public void AddRoomList()
    {
        RoomInfo room = null;
        room.transform.SetParent(scroll.content);
    }
}
