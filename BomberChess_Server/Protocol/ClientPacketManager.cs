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
		_onRecv.Add((ushort)MsgId.SEnter, MakePacket<S_Enter>);
		_handler.Add((ushort)MsgId.SEnter, PacketHandler.S_EnterHandler);		
		_onRecv.Add((ushort)MsgId.SMakeroom, MakePacket<S_Makeroom>);
		_handler.Add((ushort)MsgId.SMakeroom, PacketHandler.S_MakeroomHandler);		
		_onRecv.Add((ushort)MsgId.SEnterroom, MakePacket<S_Enterroom>);
		_handler.Add((ushort)MsgId.SEnterroom, PacketHandler.S_EnterroomHandler);		
		_onRecv.Add((ushort)MsgId.SNewenterroom, MakePacket<S_Newenterroom>);
		_handler.Add((ushort)MsgId.SNewenterroom, PacketHandler.S_NewenterroomHandler);		
		_onRecv.Add((ushort)MsgId.SSendroomlist, MakePacket<S_Sendroomlist>);
		_handler.Add((ushort)MsgId.SSendroomlist, PacketHandler.S_SendroomlistHandler);		
		_onRecv.Add((ushort)MsgId.SReady, MakePacket<S_Ready>);
		_handler.Add((ushort)MsgId.SReady, PacketHandler.S_ReadyHandler);		
		_onRecv.Add((ushort)MsgId.SLeaveplayer, MakePacket<S_Leaveplayer>);
		_handler.Add((ushort)MsgId.SLeaveplayer, PacketHandler.S_LeaveplayerHandler);
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