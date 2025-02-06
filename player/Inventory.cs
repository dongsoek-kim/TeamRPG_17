using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Inventory
    {
        private Item[] inventory;
        public Item this[int index]
        {
            get { return inventory[index]; }
            set { inventory[index] = value; }
        }

        public Armor? equipedArmor { get; private set; }
        public Weapon? equipedWeapon { get; private set; }

        public Inventory()
        {
            int length = Enum.GetValues(typeof(ItemName)).Length;
            inventory = new Item[length];
        }

        public void ShowInventory()
        {
            // 인벤토리 내 보유한 아이템 전부 출력
            foreach (Item item in inventory)
            {
                if (item == null)
                    continue;

                Console.WriteLine(item.ItemInfo());
                // 인벤토리 창에서 보유중인 골드 확인 추가
                Console.WriteLine($"{GameManager.Instance.player.gold} G");
            }
        }

        /// <summary>
        /// 장비 정보 출력함수
        /// 착용중인 장비는 [E] 포함해서 출력
        /// </summary>
        public void ShowEquip()
        {
            int itemCount = 1;

            foreach (Item item in inventory)
            {
                if (item == null)
                    continue;

                // item이 장착중인 방어구일때
                if(equipedArmor == item)
                    Console.WriteLine($"{itemCount}. [E]{item.ItemInfo()}");

                // item이 장착중인 무기일때
                else if(equipedWeapon == item)
                    Console.WriteLine($"{itemCount}. [E]{item.ItemInfo()}");

                // item이 장착중이 아닐때
                else
                    Console.WriteLine($"{itemCount}. {item.ItemInfo()}");

                itemCount++;
            }
        }

        /// <summary>
        /// 방어구의 추가스탯 출력 함수
        /// </summary>
        public void ArmorStat()
        {
            string stat = "";
            if (equipedArmor != null)
                stat = $" ( +{equipedArmor.defense} )";

            Console.WriteLine($"{stat}");
        }

        /// <summary>
        /// 무기의 추가스탯 출력 함수
        /// </summary>
        public void WeaponStat()
        {
            string stat = "";
            if (equipedWeapon != null)
                stat = $" ( +{equipedWeapon.damage} )";
            // 공격력 = +무기공격력*log(str*(직업)+Dex*(직업)+inte(직업))

            Console.WriteLine($"{stat}");
        }

        /// <summary>
        /// 장비 착용 / 착용해제 함수
        /// </summary>
        public void Equipment(int _inputCount)
        {
            int itemCount = 1;

            for(int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                    continue;

                if (_inputCount != itemCount)
                {
                    itemCount++;
                    continue;
                }

                switch (inventory[i].itemType)
                {
                    case ItemType.Armor:
                        // 선택된 방어구가 착용중인 방어구와 같을때 착용해제
                        if (equipedArmor == (Armor)inventory[i])
                            equipedArmor = null;
                        // 착용
                        else
                            equipedArmor = (Armor)inventory[i];
                        break;

                    case ItemType.Weapon:
                        // 선택된 무기가 착용중인 무기와 같을때 착용해제
                        if (equipedWeapon == (Weapon)inventory[i])
                            equipedWeapon = null;
                        //착용
                        else
                            equipedWeapon = (Weapon)inventory[i];
                        break;
                }

                break;
            }
        }

        /// <summary>
        /// 아이템 추가함수
        /// </summary>
        public void AddItem(int _index)
        {
            inventory[_index] = ItemManager.Instance.items[_index];
        }

        /// <summary>
        /// 장비 착용해제 함수
        /// </summary>
        public void Unequip(ItemType _type)
        {
            if (_type == ItemType.Armor)
                equipedArmor = null;
            else
                equipedWeapon = null;
        }
    }
}
