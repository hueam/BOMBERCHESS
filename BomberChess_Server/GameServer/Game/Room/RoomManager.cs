using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Session;
using Google.Protobuf.Protocol;

namespace GameServer
{
    public class RoomManager
    {
        public static RoomManager Instance { get; } = new RoomManager();

        object _lock = new object();
        Dictionary<string, GameRoom> _rooms = new Dictionary<string, GameRoom>();
        List<RoomInfo> _info = new();
        public List<RoomInfo> Rooms => _info;
        public GameRoom Add(C_Makeroom packet)               // 게임룸 새로 생성
        {
            GameRoom gameRoom = new GameRoom();

            lock (_lock)
            {
                packet.Info.RoomId = Guid.NewGuid().ToString();
                gameRoom.Info = packet.Info;
                gameRoom.SetOwner(packet.UserId);
                _rooms.Add(packet.Info.RoomId, gameRoom);
                _info.Add(packet.Info);
            }

            return gameRoom;
        }

        public void Remove(string roomId)      // 게임룸 제거
        {
            lock (_lock)
            {
                if(_rooms.TryGetValue(roomId, out GameRoom room))
                {
                    _info.Remove(_rooms[roomId].Info);
                    _rooms.Remove(roomId);
                }
            }
        }

        public GameRoom Find(string roomId)    // 게임룸 찾기
        {
            lock (_lock)
            {
                GameRoom room = null;
                if (_rooms.TryGetValue(roomId, out room))
                    return room;

                return null;
            }
        }
    }
}
