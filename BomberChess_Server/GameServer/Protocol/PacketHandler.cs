using Google.Protobuf.Protocol;
using Google.Protobuf;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Session;
using System.ComponentModel;
using GameServer;
using System.Globalization;


class PacketHandler
{
    public static void C_EnterroomHandler(PacketSession session, IMessage message)
    {
        
    }

    public static void C_LeaveroomHandler(PacketSession session, IMessage message)
    {
    }

    public static void C_MakeroomHandler(PacketSession session, IMessage message)
    {
        C_Makeroom info = message as C_Makeroom;
        ClientSession cs = session as ClientSession;   
        GameRoom room = RoomManager.Instance.Add(cs,info.Info);

        S_Makeroom packet = new();
        packet.Info = room.Info;
        Console.WriteLine($"{room.Info.RoomId}, {room.Info.RoomName}");
        cs.ProtoSend(packet);
    }

    public static void C_SearchroomHandler(PacketSession session, IMessage message)
    {
        ClientSession cs = session as ClientSession;
        S_Sendroomlist p = new();
        foreach(var i in RoomManager.Instance.Rooms)
        {
            Console.WriteLine($"{i.RoomName}");
            p.Rooms.Add(i);
        }
        cs.ProtoSend(p);
    }

    public static void C_LeaveHandler(PacketSession session, IMessage message)
    {
    }

    public static void C_ReadyHandler(PacketSession session, IMessage message)
    {
        C_Ready p = message as C_Ready;
        GameRoom room = RoomManager.Instance.Find(p.RoomId);

        S_Ready readyP = new();
        readyP.Ready = p.Ready;

        room.BroadCast(readyP);
    }
}
