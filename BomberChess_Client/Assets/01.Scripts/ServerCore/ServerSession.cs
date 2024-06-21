using ServerCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 서버 세션 서버와의 지속적인 할 수 있게 도와줌 서버의 상태와 주소를 가주고 있음
/// </summary>
public class ServerSession : PacketSession
{
	public override void OnConnected(EndPoint endPoint)
	{
		UnityEngine.Debug.Log($"OnConnected : {endPoint}");
		PacketManager.Instance.CustomHandler = (s, m, i) =>
		{

			PacketQueue.Instance.Push(i, m);
		};  // id와 패킷을 큐에 입력
	}

	public override void OnDisconnected(EndPoint endPoint)
	{

	}

	public override void OnRecvPacket(ArraySegment<byte> buffer)
	{
		PacketManager.Instance.OnRecvPacket(this, buffer);
	}

	public override void OnSend(int numOfBytes)
	{
	}
}

