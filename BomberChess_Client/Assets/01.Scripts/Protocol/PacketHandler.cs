using Google.Protobuf.Protocol;
using Google.Protobuf;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using UnityEngine;


class PacketHandler
{
    public static void S_EnterHandler(PacketSession session, IMessage message)
    {
        S_Enter p = message as S_Enter;
        Debug.Log(p.UserId);
        NetworkManager.Instance.userId = p.UserId;
    }

    public static void S_MakeroomHandler(PacketSession session, IMessage message)
    {
        S_Makeroom p = (S_Makeroom)message;
        Debug.Log($"{p.Info.RoomId},{p.Info.RoomName}");
        NetworkManager.Instance.roomId = p.Info.RoomId;
        LobbyUIContent ui = UIManager.Instance.GetContent<LobbyUIContent>();
        ui.OpenUI(LobbyUIEnum.Room);
        RoomUI room = ui.GetUI<RoomUI>(LobbyUIEnum.Room);
        room.EnterRoom(true);
    }

    public static void S_NewenterroomHandler(PacketSession session, IMessage message)
    {
        S_Newenterroom pakcet = message as S_Newenterroom;

        LobbyUIContent ui = UIManager.Instance.GetContent<LobbyUIContent>();
        ui.OpenUI(LobbyUIEnum.Room);
        RoomUI room = ui.GetUI<RoomUI>(LobbyUIEnum.Room);
        room.AddUser(pakcet.UserId, false,false);
    }

    public static void S_SendroomlistHandler(PacketSession session, IMessage message)
    {
        S_Sendroomlist p = message as S_Sendroomlist;
        RoomListUI.rooms = p.Rooms.ToList();
        UIManager.Instance.GetContent<LobbyUIContent>().ReloadRoomList(); ;
    }

    public static void S_ReadyHandler(PacketSession session, IMessage message)
    {
        S_Ready packet = message as S_Ready;

        RoomUI room = UIManager.Instance.GetContent<LobbyUIContent>().GetUI<RoomUI>(LobbyUIEnum.Room);
        room.UpdateReady(packet.UserId,packet.Ready);
        Debug.Log(packet.Ready);
    }

    public static void S_EnterroomHandler(PacketSession session, IMessage message)
    {
        S_Enterroom packet = message as S_Enterroom;
        LobbyUIContent ui = UIManager.Instance.GetContent<LobbyUIContent>();
        if (packet.Enter)
        {
            NetworkManager.Instance.roomId = packet.Info.RoomId;


            ui.OpenUI(LobbyUIEnum.Room);
            RoomUI room = ui.GetUI<RoomUI>(LobbyUIEnum.Room);
            room.SetOwner(packet.Info.OwnerId);
            room.EnterRoom(false);
            room.InitRoom(packet.OtherUserId.ToList());
        }
    }

    public static void S_LeaveplayerHandler(PacketSession session, IMessage message)
    {
        S_Leaveplayer pakcet = message as S_Leaveplayer;

        LobbyUIContent ui = UIManager.Instance.GetContent<LobbyUIContent>();
        ui.OpenUI(LobbyUIEnum.Room);
        RoomUI room = ui.GetUI<RoomUI>(LobbyUIEnum.Room);
        room.SetOwner(pakcet.RoomInfo.OwnerId);
        room.LeaveUser(pakcet.UserId);
    }
}