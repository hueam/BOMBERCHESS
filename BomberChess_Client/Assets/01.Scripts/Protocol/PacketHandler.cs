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
        NetworkManager.Instance.Send(p);
    }

    public static void S_LeaveHandler(PacketSession session, IMessage message)
    {
    }

    public static void S_EnterroomHandler(PacketSession session, IMessage message)
    {
    }

    public static void S_MakeroomHandler(PacketSession session, IMessage message)
    {
        S_Makeroom p = (S_Makeroom)message;
        Debug.Log($"{p.Info.RoomId},{p.Info.RoomName}");
        UIManager.Instance.GetContent<LobbyUIContent>().ActiveUI(LobbyUIEnum.Room,true);
    }

    public static void S_NewenterroomHandler(PacketSession session, IMessage message)
    {
        throw new NotImplementedException();
    }

    public static void S_SendroomlistHandler(PacketSession session, IMessage message)
    {
        S_Sendroomlist p = message as S_Sendroomlist;
        RoomListUI.rooms = p.Rooms.ToList();
        UIManager.Instance.GetContent<LobbyUIContent>().ReloadRoomList();;
    }

    public static void S_ReadyHandler(PacketSession session, IMessage message)
    {
    }
}
