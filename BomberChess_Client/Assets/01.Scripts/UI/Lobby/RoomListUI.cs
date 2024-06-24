using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Protocol;
using UnityEngine;
using UnityEngine.UI;

public class RoomListUI : LobbyUI
{
    public static List<RoomInfo> rooms = new();
    [SerializeField] private Button roomBtnPref;
    [SerializeField]private ScrollRect scroll;

    private void Awake() {
        ReloadRoom();
    }
    private void OnEnable() {
        AddRoomList();
    }
    public void AddRoomList()
    {
        scroll.content.DetachChildren();
        foreach(var info in rooms)
        {
            Button btn = Instantiate(roomBtnPref);
            btn.transform.SetParent(scroll.content);
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
