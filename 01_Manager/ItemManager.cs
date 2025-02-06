using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class ItemManager : Singleton<ItemManager> 
    {
        public Item[] items { get; private set; }
        public int[] itemPrice { get; private set; }
        public int itemLength { get; private set; }
        public ItemManager()
        {
            itemLength = Enum.GetValues(typeof(ItemName)).Length;
            items = new Item[itemLength];
            itemPrice = new int[itemLength];

            items[(int)ItemName.TrashArmor]
                = new Armor("쓸모없는 갑옷", "진짜 쓸모없는 갑옷입니다.", 1);
            items[(int)ItemName.NoviceArmor]    
                = new Armor("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 5);
            items[(int)ItemName.IronArmor]      
                = new Armor("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 9);
            items[(int)ItemName.SpartaArmor]
                = new Armor("스파르타 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 15); 

            items[(int)ItemName.WoodenStick]
                = new Weapon("나무 젓가락", "나무 젓가락입니다.", 0);
            items[(int)ItemName.OldSword]
                = new Weapon("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2);
            items[(int)ItemName.BronzeAxe]      
                = new Weapon("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5);
            items[(int)ItemName.SpartaSpear]    
                = new Weapon("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7);

            itemPrice[(int)ItemName.TrashArmor]    = 100;
            itemPrice[(int)ItemName.NoviceArmor]    = 1000;
            itemPrice[(int)ItemName.IronArmor]      = 2000;
            itemPrice[(int)ItemName.SpartaArmor]    = 3500;
            itemPrice[(int)ItemName.WoodenStick]    = 50;
            itemPrice[(int)ItemName.OldSword]       = 600;
            itemPrice[(int)ItemName.BronzeAxe]      = 1500;
            itemPrice[(int)ItemName.SpartaSpear]    = 3000;
        }
    }
}
