using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class BattleReward
    {
        public int Exp { get; private set; }
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
            bool getReward;
            int itemNum;

            player.AddExp(Exp);
            player.gold += Gold;

            Array potionValues = Enum.GetValues(typeof(PotionType));
            PotionType randomPotion = (PotionType)potionValues.GetValue(RandomGenerator.Instance.Next(potionValues.Length));

            player.inventory.potion.GetPotion(randomPotion, 1);
            EquipmentReward(out getReward, out itemNum);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"보상 획득!!");
            Console.WriteLine($"\n획득 Exp : {Exp}");
            Console.WriteLine($"{randomPotion}포션 획득");
            if (getReward)
            {
                Console.WriteLine($"{ItemManager.Instance.items[itemNum].itemName} 획득!");
            }
            Console.WriteLine($"획득 Gold : {Gold}");
            Console.WriteLine($"총 보유 Gold : {player.gold}");
            Console.ResetColor();

        }
        public void EquipmentReward(out bool getReward, out int itemNum)
        {
            getReward= false;
            itemNum=0;
            Random random = new Random();
            int newerandom = random.Next(1, 1001);
            if (newerandom > 1 && newerandom < 500)
            {
                if (QuestManager.Instance.IsClearQuest("고블린의 음모"))
                {
                    if (GameManager.Instance.player.inventory[(int)ItemName.가죽모자] == null)
                    {
                        GameManager.Instance.player.inventory.AddItem(ItemName.가죽모자);
                        getReward= true;
                        itemNum = (int)ItemName.가죽모자;
                    }
                }
            }
            if (newerandom > 500 && newerandom < 800)
            {
                if (QuestManager.Instance.IsClearQuest("이 누더기들은 뭐야"))
                {
                    if (GameManager.Instance.player.inventory[(int)ItemName.누더기] == null)
                    {
                        GameManager.Instance.player.inventory.AddItem(ItemName.누더기);
                        getReward = true;
                        itemNum = (int)ItemName.누더기;
                    }

                }
            }
            if (newerandom > 800 && newerandom < 950) 
            {
                if (QuestManager.Instance.IsClearQuest("또다시 얻은 쓸모없는 드랍템"))
                {
                    if (GameManager.Instance.player.inventory[(int)ItemName.붕대] == null)
                    {
                        GameManager.Instance.player.inventory.AddItem(ItemName.붕대);
                        getReward = true;
                        itemNum = (int)ItemName.붕대;
                    }
                }
            }
            if (newerandom > 950)
            {
                {
                    if (QuestManager.Instance.IsClearQuest("엉망이 된 숙소"))
                    {
                        if (GameManager.Instance.player.inventory[(int)ItemName.구멍난양말] == null)
                        {
                            GameManager.Instance.player.inventory.AddItem(ItemName.구멍난양말);
                            getReward = true;
                            itemNum = (int)ItemName.구멍난양말;
                        }
                    }
                }
            }


        }
    }
}
