
using GameServer.Session;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class GameRoom
    {
        object _lock = new object();
        public string RoomId { get; set; } // Room 고유ID

        Dictionary<int, ClientSession> _players = new Dictionary<int, ClientSession>();   // List -> Dictionary 

        // 방에 들어가기
        public void EnterGame(ClientSession session)
        {
            if (_players.ContainsValue(session))  // 새로운 플레이어 유/무 확인
                return;

            lock (_lock)    // 무언가 새로 추가하는 건 Lock이 꼭 필요
            {
                _players.Add(session.SessionId, session);
            }
        }

        public void LeaveGame(int sessionId)
        {
            if(!_players.ContainsKey(sessionId))
            {
                Console.WriteLine($"{sessionId} is not have this Room");
                return;
            }
            lock(_lock)
            {
                _players.Remove(sessionId);
            }
        }

        public void BroadCast(IMessage packet)
        {
            lock (_lock)
            {
                foreach (ClientSession p in _players.Values)
                {
                    p.ProtoSend(packet);
                }
            }
        }
    }
}
