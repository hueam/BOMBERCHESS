using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Lobby씬의 UIContent이다.
/// Lobby에서 필요한 것들
/// 현제 서버에 있는 모든 방정보를 표현할 UI,
/// 매치메이킹 UI,
/// SettingUI,
/// Exit
/// 정도가 필요하다
/// </summary>

public enum LobbyUIEnum
{
    RoomListUI,
    RoomMake,
    Room
}

public class LobbyUIContent : UIContent
{
    [SerializeField] private List<LobbyUI> uis;
    private Dictionary<LobbyUIEnum,LobbyUI> uiDic = new();
    
    private void Awake() 
    {
        foreach(var ui in uis)
        {
            uiDic.Add(ui.type,ui);
        }
    }
    public void ActiveUI(LobbyUIEnum type,bool value)
    {
        uiDic[type].gameObject.SetActive(value);
    }

    public void ReloadRoomList()
    {
        RoomListUI ui = uiDic[LobbyUIEnum.RoomListUI] as RoomListUI;
        ui.AddRoomList();
    }
}
