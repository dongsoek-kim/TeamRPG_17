using Newtonsoft.Json;
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
        public int equipSlot {  get; private set; }
        public ItemManager()
        {
            itemLength = Enum.GetValues(typeof(ItemName)).Length;
            items = new Item[itemLength];
            itemPrice = new int[itemLength];
            LoadItemsData();
        }
        public void LoadItemsData()
        {
            string relativePath = @"..\..\..\Json\";
            string jsonFile = "ItemData.json";  // JSON 파일명
            string jsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, jsonFile));
            try
            {
                if (File.Exists(jsonPath))
                {
                    string json = File.ReadAllText(jsonPath);
                    // JSON 데이터를 불러와 아이템 생성
                    var rawItems = JsonConvert.DeserializeObject<dynamic[]>(json);
                    for (int i = 0; i < rawItems.Length; i++)
                    {
                        var rawItem = rawItems[i];
                        ItemType itemType = (ItemType)Enum.Parse(typeof(ItemType), rawItem.itemType.ToString());

                        // 아이템 타입에 맞게 생성
                        if (itemType == ItemType.Armor)
                        {
                            int defense = int.Parse(rawItem.Defense.ToString());
                            int str = int.Parse(rawItem.str.ToString());
                            int dex = int.Parse(rawItem.dex.ToString());
                            int inte = int.Parse(rawItem.inte.ToString());
                            int luk = int.Parse(rawItem.luk.ToString());
                            Grade grade = (Grade)Enum.Parse(typeof(Grade), rawItem.Grade.ToString());  // 대소문자 구분 안함
                            EquipSlot equipSlot = (EquipSlot)Enum.Parse(typeof(EquipSlot), rawItem.EquipSlot.ToString());
                            items[i] = new Armor(
                                rawItem.itemName.ToString(),
                                rawItem.itemDescription.ToString(),
                                defense,    // int로 변환된 값
                                str,
                                dex,
                                inte,
                                luk,
                                equipSlot,
                                grade
                            );
                        }
                        else if (itemType == ItemType.Weapon)
                        {
                            int damage = int.Parse(rawItem.damage.ToString());
                            int str = int.Parse(rawItem.str.ToString());
                            int dex = int.Parse(rawItem.dex.ToString());
                            int inte = int.Parse(rawItem.inte.ToString());
                            int luk = int.Parse(rawItem.luk.ToString());
                            Grade grade = (Grade)Enum.Parse(typeof(Grade), rawItem.Grade.ToString());  // 대소문자 구분 안함
                            EquipSlot equipSlot = (EquipSlot)Enum.Parse(typeof(EquipSlot), rawItem.EquipSlot.ToString());
                            // Weapon 객체 생성
                            items[i] = new Weapon(
                                rawItem.itemName.ToString(),
                                rawItem.itemDescription.ToString(),
                                damage,    // int로 변환된 값
                                str,
                                dex,
                                inte,
                                luk,
                                equipSlot,
                                grade
                            );
                        }
                    }
                }
                else
                {
                }
            }
            catch { }
        }
    }
}

