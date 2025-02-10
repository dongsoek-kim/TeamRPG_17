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

            Array potionValues = Enum.GetValues(typeof(PotionType));
            PotionType randomPotion = (PotionType)potionValues.GetValue(RandomGenerator.Instance.Next(potionValues.Length));

            player.inventory.potion.GetPotion(randomPotion, 1);

            Console.WriteLine($"보상 획득!!");
            Console.WriteLine($"\n획득 Exp : {Exp}");
            Console.WriteLine($"{randomPotion}포션 획득");

            Console.WriteLine($"획득 Gold : {Gold}");
            Console.WriteLine($"총 보유 Gold : {player.gold}");

        }
    }
}
