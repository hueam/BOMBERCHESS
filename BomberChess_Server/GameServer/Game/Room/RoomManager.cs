using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class RoomManager
    {
        public static RoomManager Instance { get; } = new RoomManager();

        object _lock = new object();
        Dictionary<string, GameRoom> _rooms = new Dictionary<string, GameRoom>();

        public GameRoom Add()               // 게임룸 새로 생성
        {
            GameRoom gameRoom = new GameRoom();

            lock (_lock)
            {
                string id = Guid.NewGuid().ToString();
                gameRoom.RoomId = id;
                _rooms.Add(id, gameRoom);
            }

            return gameRoom;
        }

        public bool Remove(string roomId)      // 게임룸 제거
        {
            lock (_lock)
            {
                return _rooms.Remove(roomId);
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
