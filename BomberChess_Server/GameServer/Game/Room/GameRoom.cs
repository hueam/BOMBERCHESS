
using GameServer.Session;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class GameRoom
    {
        object _lock = new object();
        public RoomInfo Info { get; set; }
        private int _ownerId;
        private Dictionary<ClientSession, RoomUserInfo> _players = new();   // List -> Dictionary 

        public List<RoomUserInfo> userIDs => _players.Values.ToList();
        public void SetOwner(int sessionId)
        {
            _ownerId = sessionId;
            Info.OwnerId = _ownerId;
            EnterGame(sessionId);
        }   
        // 방에 들어가기
        public bool EnterGame(int userId)
        {
            ClientSession session = SessionManager.Instance.Find(userId);
            if (_players.ContainsKey(session))  // 새로운 플레이어 유/무 확인
                return false;
            
            if(_players.Count >= Info.MaxUser)
                return false;

            lock (_lock)    // 무언가 새로 추가하는 건 Lock이 꼭 필요
            {
                S_Newenterroom packet = new();
                packet.UserId = session.SessionId;
                BroadCast(packet);
                RoomUserInfo info = new RoomUserInfo()
                {
                    UserId = userId,
                    Ready = false
                };
                _players.Add(session, info);
                return true;
            }
        }

        public void LeaveGame(int userId)
        {
            ClientSession session = SessionManager.Instance.Find(userId);
            if (!_players.ContainsKey(session))
            {
                Console.WriteLine($"{userId} is not have this Room");
                return;
            }
            lock (_lock)
            {
                _players.Remove(session);
                S_Leaveplayer packet = new();
                packet.UserId = userId;
                packet.RoomInfo = Info;
                if(_players.Count <= 0)
                {
                    RoomManager.Instance.Remove(Info.RoomId);
                }
                else if(userId == _ownerId)
                {
                    int ownerId = userIDs[0].UserId;
                    Info.OwnerId = ownerId;
                    _ownerId = ownerId;
                }
                BroadCast(packet);
            }
        }

        public void Ready(int userId)
        {
            ClientSession session = SessionManager.Instance.Find(userId);
            if(_players.TryGetValue(session, out var data))
            {
                data.Ready = !data.Ready;
            }
            S_Ready p = new()
            {
                UserId = userId,
                Ready = data.Ready
            };
            BroadCast(p);
        }

        public void BroadCast(IMessage packet)
        {
            lock (_lock)
            {
                foreach (var p in _players.Values)
                {
                    ClientSession session = SessionManager.Instance.Find(p.UserId);
                    session.ProtoSend(packet);
                }
            }
        }
    }
}
