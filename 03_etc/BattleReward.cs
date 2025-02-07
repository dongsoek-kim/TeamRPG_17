using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class BattleReward
    {
        public int Exp {  get; private set; }
        public int Gold { get; private set; }

        public List<Item> Items { get; private set; }
        public Potion Potion { get; private set; }

        public BattleReward(int dungeonLevel, int monsterCount)
        {
            CalculateReward(dungeonLevel, monsterCount);
        }

        private void CalculateReward(int dungeonLevel, int monsterCount)
        {
            //Exp = dungeonLevel * monsterCount * 10;
            Exp = 1;
            Gold = dungeonLevel * monsterCount * 5;
        }
        public void ApplyReward(Player player)
        {
            player.AddExp(Exp);
            player.gold += Gold;

            Console.WriteLine($"보상 획득!!");
            Console.WriteLine($"\n획득 Exp : {Exp}");
            Console.WriteLine($"획득 Gold : {Gold}");
            Console.WriteLine($"총 보유 Gold : {player.gold}");

        }
    }
}
