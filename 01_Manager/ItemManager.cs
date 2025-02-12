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
            // 아이템을 다 불러오고 초기화
        }
        public void ItemPrice()
        {
            for (int i = 0; i < itemPrice.Length; i++)
                itemPrice[i] = items[i].price;
        }
    }
}

