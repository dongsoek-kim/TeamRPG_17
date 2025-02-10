using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Inventory
    {
        public Item[] inventory { get; set; }
        public Item this[int index]
        {
            get { return inventory[index]; }
            set { inventory[index] = value; }
        }

        public Armor[]? equipedArmor { get; private set; }
        public Weapon? equipedWeapon { get; private set; }

        public int equipSlot { get; private set; }
        public Potion potion { get; private set; }

        public Inventory()
        {
            equipSlot = Enum.GetValues(typeof(EquipSlot)).Length;
            int length = Enum.GetValues(typeof(ItemName)).Length;
            inventory = new Item[length];
            potion = new Potion();
            equipedArmor = new Armor[equipSlot];
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
                if (item.itemType == ItemType.Armor)// item이 장착중인 방어구일때
                {
                    if (equipedArmor[(int)item.EquipSlot] == item)
                    {
                        Console.WriteLine($"{itemCount}. [E]{item.ItemInfo()}");
                    }
                    else 
                    {
                        Console.WriteLine($"{itemCount}. {item.ItemInfo()}");
                    }
                }
                else if(item.itemType == ItemType.Weapon)
                {
                    if (equipedWeapon == item)
                    {
                        Console.WriteLine($"{itemCount}. [E]{item.ItemInfo()}");
                    }
                    else
                    {
                        Console.WriteLine($"{itemCount}. {item.ItemInfo()}");
                    }
                }                
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
            {
                foreach (Armor equipedArmor in equipedArmor)
                {
                    if (equipedArmor != null)
                        stat = $" ( +{equipedArmor.defense} )";
                }
            }

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
                int thisEquipSlot = (int)inventory[i].EquipSlot;
                if (equipedArmor[thisEquipSlot] != null)
                {
                    equipedArmor[thisEquipSlot] = null;
                }
                switch (inventory[i].itemType)
                {
                    case ItemType.Armor:
                        equipedArmor[thisEquipSlot] = (Armor)inventory[i];
                        break;

                    case ItemType.Weapon:
                        // 선택된 무기가 착용중인 무기와 같을때 착용해제
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
        public void Unequip(ItemType _itemType,EquipSlot _equipSlot)
        {
            if (_itemType == ItemType.Armor)

                equipedArmor[(int)_equipSlot] = null;
            else
                equipedWeapon = null;
        }
        public int SumDefense()
        {
            int sumDefense = 0;
            foreach (Armor equipArmor in equipedArmor)
            {
                sumDefense += (equipArmor != null) ? equipArmor.defense : 0;
            }
            return sumDefense;
        }

        /// <summary>
        /// 아이템 보유여부
        /// </summary>
        public bool haveItem(string itemName)
        {
            foreach (Item item in inventory)
            {
                if (item == null)
                    continue;

                if (item.itemName == itemName)
                {
                    return true;
                }
            }
            return false;
        }
        public bool DeleteItem(string itemName)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                    continue;

                if (inventory[i].itemName == itemName)
                {
                    if (inventory[i].itemType == ItemType.Armor)
                    {
                        if (GameManager.Instance.player.inventory.equipedArmor[(int)inventory[i].EquipSlot] == inventory[i])
                        {
                            GameManager.Instance.player.inventory.equipedArmor[(int)inventory[i].EquipSlot] = null;
                        }
                        else
                        {

                        }
                    }
                    else if (inventory[i].itemType == ItemType.Weapon)
                    {
                        if (GameManager.Instance.player.inventory.equipedWeapon == inventory[i])
                        {
                            GameManager.Instance.player.inventory.equipedWeapon = null;
                        }
                    }
                    inventory[i] = null;
                    return true;
                }
            }
            return false;
        }
    }
}
