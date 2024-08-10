using Google.Protobuf.Protocol;
using Google.Protobuf;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Session;
using GameServer;


class PacketHandler
{
    public static void C_EnterroomHandler(PacketSession session, IMessage message)
    {
        C_Enterroom packet = message as C_Enterroom;
        GameRoom room = RoomManager.Instance.Find(packet.RoomId);

        S_Enterroom recv = new();
        bool canEnter = false;
        if (room != null)
        {
            canEnter = room.EnterGame(packet.UserId);
        }

        if (canEnter)
        {
            recv.OtherUserId.AddRange(room.userIDs);
            recv.Info = room.Info;
        }
        recv.Enter = canEnter;
        session.Send(ClientSession.MakeSendBuffer(recv));
    }

    public static void C_LeaveroomHandler(PacketSession session, IMessage message)
    {
        C_Leaveroom packet = message as C_Leaveroom;
        GameRoom room = RoomManager.Instance.Find(packet.RoomId);
        if (room != null)
        {
            room.LeaveGame(packet.UserId);
        }
    }

    public static void C_MakeroomHandler(PacketSession session, IMessage message)
    {
        C_Makeroom info = message as C_Makeroom;
        GameRoom room = RoomManager.Instance.Add(info);
        S_Makeroom packet = new();
        packet.Info = room.Info;
        Console.WriteLine($"{room.Info.RoomId}, {room.Info.RoomName}");
        session.Send(ClientSession.MakeSendBuffer(packet));
    }

    public static void C_SearchroomHandler(PacketSession session, IMessage message)
    {
        S_Sendroomlist p = new();
        foreach (var i in RoomManager.Instance.Rooms)
        {
            Console.WriteLine($"{i.RoomName}");
            p.Rooms.Add(i);
        }
        session.Send(ClientSession.MakeSendBuffer(p));
    }

    public static void C_LeaveHandler(PacketSession session, IMessage message)
    {
        C_Leave packet = message as C_Leave;
        GameRoom room = RoomManager.Instance.Find(packet.RoomId);
        if (room != null)
        {
            room.LeaveGame(packet.UserId);
            Console.WriteLine(RoomManager.Instance.Find(packet.RoomId) == null);
        }
        SessionManager.Instance.Remove(packet.UserId);

    }

    public static void C_ReadyHandler(PacketSession session, IMessage message)
    {
        C_Ready p = message as C_Ready;
        GameRoom room = RoomManager.Instance.Find(p.RoomId);
        room.Ready(p.UserId);
        Console.WriteLine($"Ready Player{p.UserId}");
    }

    public static void C_TestHandler(PacketSession session, IMessage message)
    {
        S_Enter p = new();
        ClientSession cs = session as ClientSession;
        p.UserId = cs.SessionId;

        cs.ProtoSend(p);
    }
}
