using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Bomb : GameObject
    {
        public GameObject Owner { get; set; }
        public int BombDamage { get; set; }
        public Bomb()
        {
            ObjectType = GameObjectType.Bomb;
            BombDamage = 50;
        }

        public async void Update()
        {
            //  쏜 주인도 없고, 방도 없으면 나가기
            if (Owner == null || Room == null) return;
        }

        public void ActiveBomb(Dictionary<int, Player> _players)
        {
            if (this == null) return;
            List<Vector3> splashCells = this.GetSplashCellPos();
            foreach (Player player in _players.Values)
            {
                Vector3 playerPos
                    = new Vector3(player.Info.PosInfo.PosX, player.Info.PosInfo.PosY, player.Info.PosInfo.PosZ);
                if (splashCells.Contains(playerPos))
                {
                    this.OnDamaged(this, player, BombDamage * player.Info.StatInfo.Atk);
                }

                Room.LeaveGame(this.Id);
            }
        }
    }
}
