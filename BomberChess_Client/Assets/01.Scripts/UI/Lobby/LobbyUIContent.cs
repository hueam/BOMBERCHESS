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

public class LobbyUIContent : UIContent<LobbyUIEnum>
{
    public void ReloadRoomList()
    {
        RoomListUI ui = GetUI<RoomListUI>(LobbyUIEnum.RoomListUI);
        ui.AddRoomList();
    }

    public override void CloseAllUI()
    {
        foreach (var ui in _uiDic.Values)
        {
            ui.gameObject.SetActive(false);
        }
    }

    public override void OpenUI(LobbyUIEnum type)
    {
        _uiDic[type].Open();
    }

    public override void CloseUI(LobbyUIEnum type)
    {
        _uiDic[type].Close();
    }
}
