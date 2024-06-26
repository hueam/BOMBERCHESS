using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;
using UnityEngine.UI;

public class RoomListUI : LobbyUI
{
    public static List<RoomInfo> rooms = new();
    [SerializeField] private RoomElementUI roomBtnPref;
    [SerializeField]private ScrollRect scroll;

    private List<RoomElementUI> roomElements = new();

    private void Awake() {
        ReloadRoom();
    }
    private void OnEnable() {
        AddRoomList();
    }
    public void AddRoomList()
    {
        scroll.content.DetachChildren();
        foreach (var room in roomElements)
        {
            PoolManager.Instance.Push(room);
        }
        roomElements.Clear();
        foreach(var info in rooms)
        {
            RoomElementUI elem = PoolManager.Instance.Pop(PoolType.RoomElement) as RoomElementUI; 
            elem.transform.SetParent(scroll.content);
            elem.RoomSetting(info);
            roomElements.Add(elem);
        }
    }
    public void ActiveMakeRoom()
    {
        UIManager.Instance.GetContent<LobbyUIContent>().ActiveUI(LobbyUIEnum.RoomMake,true);
    }
    public void ReloadRoom()
    {
                C_Searchroom p = new();
        NetworkManager.Instance.Send(p);
    }
}
