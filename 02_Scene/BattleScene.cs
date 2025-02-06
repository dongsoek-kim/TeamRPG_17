using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    internal class BattleScene : Scene
    {
        Player _player = GameManager.Instance.player;
        public override void Update()
        {
            // 전투 개시
            BattleEngage();
        }
        public BattleScene(Player player)
        {
            _player = player;
        }
        public void BattleEngage()
        { 
            // 몬스터 랜덤 생성

        }

        private void DisplayStatus()
        {
            Console.Clear();
            Console.WriteLine("=== ENGAGE!!! ===");
            Console.WriteLine($"Lv.{}");

            Console.WriteLine("\n\n[내 정보]");
            Console.WriteLine($"Lv.{_player.level}  {_player.name} ({_player.job})");
            Console.WriteLine($"HP  {_player.hp}");
            Console.WriteLine("====================\n");
        }
    }
}
