using System.Collections;
using System.Collections.Generic;
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
public class LobbyUIContent : UIContent
{
    [SerializeField]private RoomListUI roomList;
    [SerializeField]private RoomMaker roomMaker;
    public void ActiveRoomList()
    {
        
    }
    
}
