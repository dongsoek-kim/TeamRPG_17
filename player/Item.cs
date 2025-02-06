using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public abstract class Item
    {
        public ItemType itemType        { get; private set; }
        public string   itemName        { get; private set; }
        public string   itemDescription { get; private set; }
        public Item(string _name, string _description, ItemType _type) 
        {
            itemType = _type;
            itemName = _name;
            itemDescription = _description;
        }

        // 아이템 정보 문자열 반환 함수
        public abstract string ItemInfo();
    }

    public class Armor : Item
    {
        public int defense { get; set; }

        public Armor(string _name, string _description, int _defense)
            : base(_name, _description, ItemType.Armor)
        {
            
            defense = _defense;
        }

        public override string ItemInfo()
        {
            return $"{itemName} | 방어력 +{defense} | {itemDescription}";
        }
    }

    public class Weapon : Item
    {
        public int damage { get; set; }
        public Weapon(string _name, string _description, int _damage) 
            : base(_name, _description, ItemType.Weapon)
        {
            damage = _damage;
        }

        public override string ItemInfo()
        {
            return $"{itemName} | 공격력 +{damage} | {itemDescription}";
        }
    }
}
