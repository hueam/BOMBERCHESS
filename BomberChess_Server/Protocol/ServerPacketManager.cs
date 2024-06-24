using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;

class PacketManager
{
	#region Singleton
	static PacketManager _instance = new PacketManager();
	public static PacketManager Instance { get { return _instance; } }
	#endregion

	PacketManager()
	{
		Register();
	}

	Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
	Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();
		
	public Action<PacketSession, IMessage, ushort> CustomHandler { get; set; }


	public void Register()
	{		
		_onRecv.Add((ushort)MsgId.CMakeroom, MakePacket<C_Makeroom>);
		_handler.Add((ushort)MsgId.CMakeroom, PacketHandler.C_MakeroomHandler);		
		_onRecv.Add((ushort)MsgId.CEnterroom, MakePacket<C_Enterroom>);
		_handler.Add((ushort)MsgId.CEnterroom, PacketHandler.C_EnterroomHandler);		
		_onRecv.Add((ushort)MsgId.CLeaveroom, MakePacket<C_Leaveroom>);
		_handler.Add((ushort)MsgId.CLeaveroom, PacketHandler.C_LeaveroomHandler);		
		_onRecv.Add((ushort)MsgId.CSearchroom, MakePacket<C_Searchroom>);
		_handler.Add((ushort)MsgId.CSearchroom, PacketHandler.C_SearchroomHandler);		
		_onRecv.Add((ushort)MsgId.CReady, MakePacket<C_Ready>);
		_handler.Add((ushort)MsgId.CReady, PacketHandler.C_ReadyHandler);		
		_onRecv.Add((ushort)MsgId.CLeave, MakePacket<C_Leave>);
		_handler.Add((ushort)MsgId.CLeave, PacketHandler.C_LeaveHandler);
	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		Action<PacketSession, ArraySegment<byte>, ushort> action = null;
		if (_onRecv.TryGetValue(id, out action))
			action.Invoke(session, buffer, id);
	}

	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
	{
		T pkt = new T();
		pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

		if (CustomHandler != null)
		{
			CustomHandler.Invoke(session, pkt, id);
		}
		else
		{
			Action<PacketSession, IMessage> action = null;
            if (_handler.TryGetValue(id, out action))
                action.Invoke(session, pkt);
		}

	}

	public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
	{
		Action<PacketSession, IMessage> action = null;
		if (_handler.TryGetValue(id, out action))
			return action;
		return null;
	}
}