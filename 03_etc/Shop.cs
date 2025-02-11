using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Shop
    {
        private bool onMessage;
        private string message;
        private ConsoleColor messageColor;
        private ConsoleColor defaultColor = Console.ForegroundColor;

        private float sellRatio = 0.85f;

        /// <summary>
        /// 아이템 구매 출력/입력
        /// </summary>
        public void BuyItem(int _num)
        {
            // 현재 도시에 따라서 아이템 인덱스 변경
            Town town = GameManager.Instance.currentTown;
            int itemIndex = _num - 1;
            onMessage = true;

            // 잘못된 입력
            // 범위 초과 ( 0 미만 , 게임 내 아이템 개수 초과 )
            if (_num < 0 || (town.startItemIdx + town.count) < _num)
            {
                message = "잘못된 입력입니다";
                messageColor = ConsoleColor.Red;
                return;
            }

            // 이미 보유한 아이템
            if (GameManager.Instance.player.inventory[itemIndex] != null)
            {
                message = "이미 구매한 아이템입니다.";
                messageColor = ConsoleColor.Blue;
                return;
            }

            // 구매 시도
            // 돈이 충분하지않을때
            if (GameManager.Instance.player.gold < ItemManager.Instance.itemPrice[itemIndex])
            {
                message = "Gold 가 부족합니다.";
                messageColor = ConsoleColor.Red;
                return;
            }

            // 범위를 초과하지않음 / 보유중이 아님 / 돈이 충분함
            message = "구매를 완료했습니다.";
            messageColor = ConsoleColor.Blue;

            // 돈 차감 및 아이템 추가
            GameManager.Instance.player.gold -= ItemManager.Instance.itemPrice[itemIndex];
            GameManager.Instance.player.inventory.AddItem((ItemName)itemIndex);
        }

        /// <summary>
        /// 아이템 판매 출력/입력
        /// </summary>
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
                // 판매된 아이템이 착용중인 아이템인지 확인 및 착용해제
                // 판매된 아이템 -> null
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

                // 판매된 아이템 -> null , Gold 획득
                message = $"{GameManager.Instance.player.inventory[i].itemName} 판매되었습니다. ( + {getGold}G)";
                GameManager.Instance.player.inventory[i] = null;
                GameManager.Instance.player.gold += getGold;

                messageColor = ConsoleColor.Blue;
                return;
            }

            // 잘못된 입력으로 판매되지않았을때
            message = "잘못된 입력입니다";
            messageColor = ConsoleColor.Red;
        }

        /// <summary>
        /// 판매 아이템 리스트 출력
        /// </summary>
        public int PrintItemList(int itemsPerPage, int nowPage,out int startIndex, out int totalPages, bool _isNumber = false)
        {

            int? number;
            startIndex = 0;
            Town town = GameManager.Instance.currentTown; // 현재 타운

            // 예외 처리사항
            if (!town.CanGetItem())
            {
                Console.WriteLine("PrintItemList() 메서드에서 아이템 리스트를 가져올 수 없습니다.");
                totalPages = 0;
                return 0;
            }

            int itemCount = 1;
            int townItemIdx = town.startItemIdx + town.count;
            totalPages = (town.count + itemsPerPage - 1) / itemsPerPage; // 전체 페이지 수

            startIndex = town.startItemIdx + nowPage * itemsPerPage;
            int endIndex;

            if ((startIndex + itemsPerPage > townItemIdx))
                endIndex = townItemIdx;
            else
                endIndex = startIndex + itemsPerPage;

            // 판매 목록
            for (int i = startIndex; i < endIndex; i++)
            {
                number = _isNumber ? itemCount : null;

                Console.Write($"- {number} {ItemManager.Instance.items[i].ItemInfo()}");

                if (GameManager.Instance.player.inventory[i] == null)
                    Console.WriteLine($"  | {ItemManager.Instance.itemPrice[i]}G");

                else
                    Console.WriteLine("  | 구매완료");

                itemCount++;
            }

            return itemCount;
        }

        /// <summary>
        /// 판매 아이템 리스트 출력
        /// </summary>
        public int SellItemList(int itemsPerPage, int nowPage, out int totalPages)
        {
            Item[] inventory = GameManager.Instance.player.inventory.inventory;

            int num = 1;

            int itemsHeld = inventory.Count(i => i != null); // 보유 중인 아이템 개수
            totalPages = (itemsHeld + itemsPerPage - 1) / itemsPerPage; // 전체 페이지 수
            List<Item> itemList = inventory.Where(i => i != null).ToList();
            int startIndex = nowPage * itemsPerPage;
            List<Item> pageList = itemList.Skip(startIndex).Take(itemsPerPage).ToList();

            foreach(Item item in pageList)
            {
                Console.Write($"- {num++} {item.ItemInfo()}");
                Console.WriteLine($"  | {(int)(ItemManager.Instance.itemPrice[(int)item.itemType] * sellRatio)}G");
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
