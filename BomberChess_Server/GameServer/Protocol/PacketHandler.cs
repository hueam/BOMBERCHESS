﻿using Google.Protobuf.Protocol;
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
        ClientSession cs = SessionManager.Instance.Find(info.UserId);
        GameRoom room = RoomManager.Instance.Add(cs,info.Info);

        S_Makeroom packet = new();
        packet.RoomId = room.Info.RoomId;
        Console.WriteLine($"{room.Info.RoomId}, {room.Info.RoomName}");
        cs.ProtoSend(packet);
    }

    public static void C_SearchroomHandler(PacketSession session, IMessage message)
    {

    }
}
