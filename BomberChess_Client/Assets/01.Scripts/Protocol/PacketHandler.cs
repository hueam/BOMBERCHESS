using Google.Protobuf.Protocol;
using Google.Protobuf;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


class PacketHandler
{
    public static void S_EnterHandler(PacketSession session, IMessage message)
    {
        S_Enter p = message as S_Enter;
        NetworkManager.Instance.userId = p.UserId;
    }

    public static void S_LeaveHandler(PacketSession session, IMessage message)
    {
    }

    public static void S_EnterroomHandler(PacketSession session, IMessage message)
    {
    }

    public static void S_MakeroomHandler(PacketSession session, IMessage message)
    {
        throw new NotImplementedException();
    }

    public static void S_NewenterroomHandler(PacketSession session, IMessage message)
    {
        throw new NotImplementedException();
    }

    public static void S_SendroomlistHandler(PacketSession session, IMessage message)
    {
        throw new NotImplementedException();
    }
}
