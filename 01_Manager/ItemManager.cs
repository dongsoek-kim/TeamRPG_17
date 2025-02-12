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
        /// <summary>
        /// 아이템매니저 생성자
        /// 아이템매니저에 items배열에 아이템을 넣어준다
        /// </summary>
        public ItemManager()
        {
            itemLength = Enum.GetValues(typeof(ItemName)).Length;
            items = new Item[itemLength];
            itemPrice = new int[itemLength];
        }
        /// <summary>
        /// 아이템가격을 결정해주는 메서드,Shop에서 사용
        /// </summary>
        public void ItemPrice()
        {
            for (int i = 0; i < itemPrice.Length; i++)
                itemPrice[i] = items[i].price;
        }
    }
}

