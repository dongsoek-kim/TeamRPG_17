using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Inventory
    {
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
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
            //ItemType=Armor면 new Armor , ItemType=Weapon이면 new Weapon
            inventory = new Item[ItemManager.Instance.items.Length];
            potion = new Potion();
            if (equipedArmor == null) equipedArmor= new Armor[4];
            for (int i = 0; i < Enum.GetValues(typeof(ItemName)).Length; i++)
            {
                AddItem((ItemName)i);
            }
        }
        [JsonConstructor] // JSON 직렬화/역직렬화를 위해 생성자 지정
        public Inventory(Item[] inventory, Armor[]? equipedArmor, Weapon? equipedWeapon, int equipSlot, Potion potion)
        {
            this.inventory = inventory ?? new Item[ItemManager.Instance.items.Length];
            this.equipedArmor = equipedArmor ?? new Armor[4];
            this.equipedWeapon = equipedWeapon;
            this.equipSlot = equipSlot;
            this.potion = potion ?? new Potion();

            // ItemType에 따라 아이템 추가
            for (int i = 0; i < Enum.GetValues(typeof(ItemName)).Length; i++)
            {
                AddItem((ItemName)i);
            }
        }
        public void ShowInventory(int nowPage,out int totalPages)
        {
            int itemsPerPage = 7; // 한 페이지에 표시할 아이템 수
            int  itemsHeld = inventory.Count(i => i != null); // 보유 중인 아이템 개수
            totalPages = (itemsHeld + itemsPerPage - 1) / itemsPerPage; // 전체 페이지 수
            List<Item> itemList = inventory.Where(i => i != null).ToList();
            int startIndex = nowPage * itemsPerPage;
            List<Item> pageList = itemList.Skip(startIndex).Take(itemsPerPage).ToList();
            ConsoleColor color=ConsoleColor.White;
            foreach (Item item in pageList)
            {
                string prefix = ""; // 기본적으로 장착 여부 없음

                if (item.itemType == ItemType.Armor && equipedArmor[(int)item.EquipSlot] == item)
                {
                    prefix = "[E] ";
                }
                else if (item.itemType == ItemType.Weapon && equipedWeapon == item)
                {
                    prefix = "[E] ";
                }
                switch (item.grade)
                {
                    case Grade.Common:
                        color = ConsoleColor.DarkGray;
                        break;
                    case Grade.Rare:
                        color = ConsoleColor.Blue;
                        break;                     
                    case Grade.Unique:
                        color = ConsoleColor.Yellow;
                        break;
                }
                Render.ColorWriteLine($"{prefix}{item.ItemInfo()}",color);
            }
        }
        /// <summary>
        /// 포션갯수 출력함수
        /// </summary>
        public void showPotion()
        {
            var originalPosition = Console.GetCursorPosition();
            int potionType = 0;
            int coorX = Console.WindowWidth -60;
            int coorY = 15;
            for (int i = 0;i<6;i++)
            {
                Console.SetCursorPosition(coorX, coorY);
                Console.WriteLine("|");
                coorY++;
            }
            coorX = Console.WindowWidth - 50;
            coorY = 16;
            foreach (int potion in potion.potionCount) 
            {
                Console.SetCursorPosition(coorX, coorY);
                Console.WriteLine($"{(PotionType)potionType}포션의 갯수 : {potion}개");
                potionType++;
                coorY++;
            }
            Console.SetCursorPosition(originalPosition.Left, originalPosition.Top);
        }
        /// <summary>
        /// 장비 정보 출력함수
        /// 착용중인 장비는 [E] 포함해서 출력
        /// </summary>
        public void ShowEquip(int nowPage, out int totalPages)
        {
            int itemCount = 1;
            int itemsPerPage = 7; // 한 페이지에 표시할 아이템 수
            int itemsHeld = inventory.Count(i => i != null); // 보유 중인 아이템 개수
            totalPages = (itemsHeld + itemsPerPage - 1) / itemsPerPage; // 전체 페이지 수
            List<Item> itemList = inventory.Where(i => i != null).ToList();
            int startIndex = nowPage * itemsPerPage;
            List<Item> pageList = itemList.Skip(startIndex).Take(itemsPerPage).ToList();
            ConsoleColor color = ConsoleColor.White;
            foreach (Item item in pageList)
            {
                string prefix = "";

                // 아이템 종류에 따라 장착 여부를 확인하여 접두어를 설정합니다.
                if (item.itemType == ItemType.Armor && equipedArmor[(int)item.EquipSlot] == item)
                {
                    prefix = "[E]";
                }
                else if (item.itemType == ItemType.Weapon && equipedWeapon == item)
                {
                    prefix = "[E]";
                }
                switch (item.grade)
                {
                    case Grade.Common:
                        color = ConsoleColor.DarkGray;
                        break;
                    case Grade.Rare:
                        color = ConsoleColor.Blue;
                        break;
                    case Grade.Unique:
                        color = ConsoleColor.Yellow;
                        break;
                }
                Render.ColorWriteLine($"{itemCount}.{prefix}{item.ItemInfo()}", color);
                itemCount++;
            }
        }
        /// <summary>
        /// 장비 창
        /// 부위별로 착용중인 장비 출력
        /// </summary>
        public void showNowEquip()
        {   
            Console.WriteLine("착용 중인 장비");
            if (equipedWeapon != null)
                nowEquipInfo(EquipSlot.Weapon);
            else Console.WriteLine("무기 : 없음");

            if (equipedArmor[(int)EquipSlot.Head] != null)
            {
                Console.Write("머리");
                nowEquipInfo(EquipSlot.Head);
            }
            else Console.WriteLine("머리방어구 : 없음");

            if (equipedArmor[(int)EquipSlot.Body] != null)
            {
                Console.Write("전신");
                nowEquipInfo(EquipSlot.Body);
            }
            else Console.WriteLine("전신방어구 : 없음");

            if (equipedArmor[(int)EquipSlot.Arm] != null)
            {
                Console.Write("팔");
                nowEquipInfo(EquipSlot.Arm);
            }
            else Console.WriteLine("팔방어구 : 없음");

            if (equipedArmor[(int)EquipSlot.Foot] != null)
            {
                Console.Write("다리");
                nowEquipInfo(EquipSlot.Foot);
                Console.WriteLine();
            }
            else Console.WriteLine("다리방어구 : 없음");
            int sumDefens=0;
            var itemStats = ItemStat();
            Console.Write($"총방어력:");ArmorStat();
            Console.WriteLine($"총 힘 : {itemStats.sumStr}, 총 민첩 : {itemStats.sumDex},총 지능 : {itemStats.sumInte},총 행운 : {itemStats.sumLuk}");
        }
        public void nowEquipInfo(EquipSlot equipSlot)
        {
            if (equipSlot == EquipSlot.Weapon)
            {
                Console.WriteLine($"무기 : {equipedWeapon.itemName}");
                Console.WriteLine($"공격력: {equipedWeapon.damage}, 힘 : { equipedWeapon.str}, 민첩 : {equipedWeapon.dex},지능 : {equipedWeapon.inte}, 행운 : {equipedWeapon.luk}");
            }
            else
            {
                Console.WriteLine($"방어구 : {equipedArmor[(int)equipSlot].itemName}");
                Console.WriteLine($"방어력 : {equipedArmor[(int)equipSlot].defense}, 힘 : {equipedArmor[(int)equipSlot].str},민첩 : {equipedArmor[(int)equipSlot].dex}, 지능 : {equipedArmor[(int)equipSlot].inte}, 행운 : {equipedArmor[(int)equipSlot].luk}");
            }

        }
        /// <summary>
        /// 방어구의 추가스탯 출력 함수
        /// </summary>
        public int ArmorStat()
        {
            string stat = "";
            int sumDefense = 0;
            if (equipedArmor != null)
            {
                foreach (Armor equipedArmor in equipedArmor)
                {
                    if (equipedArmor != null)
                        sumDefense += equipedArmor.defense;
                        
                }
            }
            if(sumDefense>0) stat = $" ( +{sumDefense} )";
            //Console.WriteLine($"{stat}");
            return sumDefense;
        }

        /// <summary>
        /// 무기의 추가스탯 출력 함수
        /// </summary>
        public int WeaponStat()
        {
            int weaponDamage = 0;
            string stat = "";
            if (equipedWeapon != null)
            {
                stat = $" ( +{equipedWeapon.damage} )";
                weaponDamage = equipedWeapon.damage;
            }
            //Console.WriteLine($"{stat}");
            return weaponDamage;
        }
        public (int sumStr,int sumDex,int sumInte,int sumLuk) ItemStat()
        {
            int sumStr = 0,sumDex=0,sumInte=0,sumLuk=0;
            foreach (Armor equipedArmor in equipedArmor)
            {
                if (equipedArmor == null) continue;
                else
                { sumStr += equipedArmor.str; sumDex += equipedArmor.dex; sumInte += equipedArmor.inte; sumLuk += equipedArmor.luk; }
            }
            if(equipedWeapon!=null)
            {
                { sumStr += equipedWeapon.str; sumDex += equipedWeapon.dex; sumInte += equipedWeapon.inte; sumLuk += equipedWeapon.luk; }
            }
            return (sumStr, sumDex, sumInte, sumLuk);
        }

        /// <summary>
        /// 장비 착용 / 착용해제 함수
        /// </summary>
        public void Equipment(int _inputCount)
        {
            int itemCount = 1;

            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                    continue;

                if (_inputCount != itemCount)
                {
                    itemCount++;
                    continue;
                }
                int thisEquipSlot = (int)inventory[i].EquipSlot;
                switch (inventory[i].itemType)
                {
                    case ItemType.Armor:
                        {
                            if (equipedArmor[thisEquipSlot] == (Armor)inventory[i])
                            {
                                equipedArmor[thisEquipSlot] = null;
                            }

                            else 
                            {
                                equipedArmor[thisEquipSlot] = (Armor)inventory[i]; 
                            }
                            break;
                        }
                    case ItemType.Weapon:
                        {
                            // 선택된 무기가 착용중인 무기와 같을때 착용해제
                            if (equipedWeapon == (Weapon)inventory[i])
                            {
                                equipedWeapon = null;
                            }
                            else
                            {
                                equipedWeapon = (Weapon)inventory[i];  // 새로운 무기라면 장착
                            }
                            break;
                        }
                }
                break;
            }
        }

        /// <summary>
        /// 아이템 추가함수
        /// </summary>
        public void AddItem(ItemName? itemName)
        {
            inventory[(int)itemName] = ItemManager.Instance.items[(int)itemName];
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
