﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{

    public abstract class Item
    {
        public ItemType itemType        { get; private set; }
        public Grade grade { get; private set; }
        public EquipSlot EquipSlot { get; private set; }
        public string   itemName        { get; private set; }
        public string   itemDescription { get; private set; }
        
        public int str {  get; private set; }//힘
        public int dex { get; private set; }//민첩
        public  int inte {  get; private set; }//지력
        public int luk {  get; private set; }

        public int price {  get; private set; }
        
        public Item(string _name, string _description, ItemType _type,int _str, int _dex, int _inte,int _luk,EquipSlot _equipslot,Grade _grade) 
        {
            
            itemType = _type;
            itemName = _name;
            itemDescription = _description;
            str = _str;
            dex = _dex;
            inte = _inte;
            luk = _luk;
            EquipSlot = _equipslot;
            grade = _grade;
            //switch(grade)
            //{
            //    case Grade.Common:
            //        price = 500;
            //        break;
            //    case Grade.rare:
            //        price = 1000;
            //        break;
            //    case Grade.Unique:
            //        price = 2000;
            //        break;
            //    default:
            //        break;
            //}    
        }

        // 아이템 정보 문자열 반환 함수
        public abstract string ItemInfo();
    }

    public class Armor : Item
    {
        public int defense { get; set; }

        public Armor(string _name, string _description, int _defense, int _str, int _dex, int _inte,int _luk, EquipSlot _equipslot, Grade grade)
            : base(_name, _description, ItemType.Armor,_str,_dex,_inte, _luk,_equipslot, grade)
        {
            
            defense = _defense;
        }

        public override string ItemInfo()
        {
            return $"{itemName} | 방어력 +{defense} | 힘 +{str} | 민첩 + {dex} | 지력 + {inte} {itemDescription}";
        }
    }

    public class Weapon : Item
    {
        public int damage { get; set; }
        public Weapon(string _name, string _description, int _damage, int _str, int _dex, int _inte,int _luk, EquipSlot _equipslot, Grade grade) 
            : base(_name, _description, ItemType.Weapon, _str, _dex, _inte, _luk, _equipslot, grade)
        {
            damage = _damage;
        }

        public override string ItemInfo()
        {
            return $"{itemName} | 공격력 +{damage} | 힘 +{str} | 민첩 + {dex} | 지력 + {inte} {itemDescription}";
        }
    }
}

