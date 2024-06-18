
using GameServer.Session;
using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class Player : GameObject
    {
        public ClientSession Session { get; set; }  // 내가 어느 세션에 속해있는가?

        public Player()
        {   // Protocol.proto의 Enum
            ObjectType = GameObjectType.Player;
            //Info.Hp = 100;
            Level = 1;
            MaxHp = 100;
            Hp = MaxHp;
            Atk = 2;
            Speed = 10.0f;
        }
        public override void OnDamaged(GameObject attacker, GameObject fired, int damage)
        {
            base.OnDamaged(attacker, fired, damage);
        }
        public override void OnDead(GameObject attacker)
        {
            base.OnDead(attacker);
        }
    }

}
