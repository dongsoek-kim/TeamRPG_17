using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Shop
    {
        private ConsoleColor[] colors;

        private bool onMessage;
        private string message;
        private ConsoleColor messageColor;
        private ConsoleColor defaultColor = Console.ForegroundColor;

        private float sellRatio = 0.85f;

        public Shop()
        {
            colors = new ConsoleColor[Enum.GetValues(typeof(Grade)).Length];
            colors[(int)Grade.Common] = ConsoleColor.Gray;
            colors[(int)Grade.Rare] = ConsoleColor.Blue;
            colors[(int)Grade.Unique] = ConsoleColor.Yellow;
        }

        /// <summary>
        /// 아이템 구매 출력/입력
        /// </summary>
        /// <param name="_num">아이템 인덱스 입력</param>
        public void BuyItem(int _num)
        {
            
            Town town = GameManager.Instance.currentTown;
            int itemIndex = _num - 1;
            onMessage = true;

            if (_num < 0 || (town.startItemIdx + town.count) < _num)
            {
                message = "잘못된 입력입니다";
                messageColor = ConsoleColor.Red;
                return;
            }

            if (GameManager.Instance.player.inventory[itemIndex] != null)
            {
                message = "이미 구매한 아이템입니다.";
                messageColor = ConsoleColor.Blue;
                return;
            }

            if (GameManager.Instance.player.gold < ItemManager.Instance.itemPrice[itemIndex])
            {
                message = "Gold 가 부족합니다.";
                messageColor = ConsoleColor.Red;
                return;
            }

            message = "구매를 완료했습니다.";
            messageColor = ConsoleColor.Blue;

            GameManager.Instance.player.gold -= ItemManager.Instance.itemPrice[itemIndex];
            GameManager.Instance.player.inventory.AddItem((ItemName)itemIndex);
        }

        /// <summary>
        /// 아이템 판매 출력/입력
        /// </summary>
        /// <param name="_num">아이템 인덱스</param>
        public void SellItem(int _num)
        {
            onMessage = true;
            int itemCount = 1;

            for(int i = 0; i < ItemManager.Instance.itemLength; i++)
            {
                if (GameManager.Instance.player.inventory[i] == null)
                    continue;

                if (_num != itemCount)
                {
                    itemCount++;
                    continue;
                }

                switch (GameManager.Instance.player.inventory[i].itemType)
                {
                    case ItemType.Armor:
                        foreach (Armor? equipedArmor in GameManager.Instance.player.inventory.equipedArmor)
                        {
                            if (equipedArmor == null)
                                continue;

                            if (GameManager.Instance.player.inventory[i] == equipedArmor)
                                GameManager.Instance.player.inventory.Unequip(ItemType.Armor, equipedArmor.EquipSlot);
                        }
                        break;
                    case ItemType.Weapon:
                        if (GameManager.Instance.player.inventory[i] == GameManager.Instance.player.inventory.equipedWeapon)
                            GameManager.Instance.player.inventory.Unequip(ItemType.Weapon, EquipSlot.Weapon);
                        break;
                }

                int getGold = (int)(ItemManager.Instance.itemPrice[i] * sellRatio);

                message = $"{GameManager.Instance.player.inventory[i].itemName} 판매되었습니다. ( + {getGold}G)";
                GameManager.Instance.player.inventory[i] = null;
                GameManager.Instance.player.gold += getGold;

                messageColor = ConsoleColor.Blue;
                return;
            }

            message = "잘못된 입력입니다";
            messageColor = ConsoleColor.Red;
        }

        /// <summary>
        /// 판매 아이템 리스트 출력
        /// </summary>
        /// <param name="itemsPerPage">현재 페이지에 최대 출력 수</param>
        /// <param name="nowPage">현재 페이지</param>
        /// <param name="startIndex">시작되는 아이템 인덱스 반환</param>
        /// <param name="totalPages">아이템 개수에 따라서 전체 페이지 반환</param>
        /// <param name="_isNumber">번호를 붙일 것인지 기본값 : false</param>
        /// <returns>현재 페이지 아이템 개수 반환</returns>
        public int PrintItemList(int itemsPerPage, int nowPage,out int startIndex, out int totalPages, bool _isNumber = false)
        {

            int? number;
            startIndex = 0;
            Town town = GameManager.Instance.currentTown;

            // 예외 처리사항
            if (!town.CanGetItem())
            {
                Console.WriteLine("PrintItemList() 메서드에서 아이템 리스트를 가져올 수 없습니다.");
                totalPages = 0;
                return 0;
            }

            int itemCount = 1;
            int townItemIdx = town.startItemIdx + town.count;
            totalPages = (town.count + itemsPerPage - 1) / itemsPerPage;

            startIndex = town.startItemIdx + nowPage * itemsPerPage;
            int endIndex;

            if ((startIndex + itemsPerPage > townItemIdx))
                endIndex = townItemIdx;
            else
                endIndex = startIndex + itemsPerPage;

            
            for (int i = startIndex; i < endIndex; i++)
            {
                number = _isNumber ? itemCount : null;

                Item item = ItemManager.Instance.items[i];
                Render.ColorWrite($"- {number} {item.ItemInfo()}", colors[(int)item.Grade]);
                Console.Write(" | ");

                if (GameManager.Instance.player.inventory[i] == null)
                    Render.ColorWriteLine($"{ItemManager.Instance.itemPrice[i]}G", ConsoleColor.Yellow);

                else
                    Render.ColorWriteLine("구매완료", ConsoleColor.Green);

                itemCount++;
            }

            return itemCount;
        }

        /// <summary>
        /// 판매 아이템 리스트 출력
        /// </summary>
        /// <param name="itemsPerPage">현재 페이지에 최대 출력 수</param>
        /// <param name="nowPage">현재 페이지</param>
        /// <param name="totalPages">아이템 개수에 따라서 전체 페이지 반환</param>
        /// <returns>현재 페이지 아이템 개수 반환</returns>
        public int SellItemList(int itemsPerPage, int nowPage, out int totalPages)
        {
            Item[] inventory = GameManager.Instance.player.inventory.inventory;

            int num = 1;

            int itemsHeld = inventory.Count(i => i != null); // 보유 중인 아이템 개수
            totalPages = (itemsHeld + itemsPerPage - 1) / itemsPerPage;
            List<Item> itemList = inventory.Where(i => i != null).ToList();
            int startIndex = nowPage * itemsPerPage;
            List<Item> pageList = itemList.Skip(startIndex).Take(itemsPerPage).ToList();

            foreach(Item item in pageList)
            {
                Render.ColorWrite($"- {num++} {item.ItemInfo()}", colors[(int)item.Grade]);
                Console.Write(" | ");
                Render.ColorWriteLine($"{(int)(ItemManager.Instance.itemPrice[(int)item.itemType] * sellRatio)}G", ConsoleColor.Yellow);
            }

            return pageList.Count;
        }

        /// <summary>
        /// 추가 메세지 출력
        /// </summary>
        public void ShopMessage()
        {
            if (!onMessage)
                return;

            Console.ForegroundColor = messageColor;
            Console.WriteLine($"\n{message}");
            Console.ForegroundColor = defaultColor;

            onMessage = false;
        }
    }
}
