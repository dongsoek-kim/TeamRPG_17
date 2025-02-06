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
            int itemIndex = _num - 1;
            onMessage = true;

            // 잘못된 입력
            // 범위 초과 ( 0 미만 , 게임 내 아이템 개수 초과 )
            if (_num < 0 || ItemManager.Instance.itemLength < _num)
            {
                message = "잘못된 입력입니다";
                messageColor = ConsoleColor.Red;
                return;
            }

            // 이미 보유한 아이템
            if (Player.Instance.inventory[itemIndex] != null)
            {
                message = "이미 구매한 아이템입니다.";
                messageColor = ConsoleColor.Blue;
                return;
            }

            // 구매 시도
            // 돈이 충분하지않을때
            if (Player.Instance.gold < ItemManager.Instance.itemPrice[itemIndex])
            {
                message = "Gold 가 부족합니다.";
                messageColor = ConsoleColor.Red;
                return;
            }

            // 범위를 초과하지않음 / 보유중이 아님 / 돈이 충분함
            message = "구매를 완료했습니다.";
            messageColor = ConsoleColor.Blue;

            // 돈 차감 및 아이템 추가
            Player.Instance.gold -= ItemManager.Instance.itemPrice[itemIndex];
            Player.Instance.inventory.AddItem(itemIndex);
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
                if (Player.Instance.inventory[i] == null)
                    continue;

                if (_num != itemCount)
                {
                    itemCount++;
                    continue;
                }
                // 판매된 아이템이 착용중인 아이템인지 확인 및 착용해제
                // 판매된 아이템 -> null
                switch (Player.Instance.inventory[i].itemType)
                {
                    case ItemType.Armor:
                        if (Player.Instance.inventory[i] == Player.Instance.inventory.equipedArmor)
                            Player.Instance.inventory.Unequip(ItemType.Armor);
                        break;
                    case ItemType.Weapon:
                        if (Player.Instance.inventory[i] == Player.Instance.inventory.equipedWeapon)
                            Player.Instance.inventory.Unequip(ItemType.Weapon);
                        break;
                }

                int getGold = (int)(ItemManager.Instance.itemPrice[i] * sellRatio);

                // 판매된 아이템 -> null , Gold 획득
                Player.Instance.inventory[i] = null;
                Player.Instance.gold += getGold;

                message = $"판매되었습니다. ( + {getGold}G)";
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
        public void PrintItemList(bool _isNumber = false)
        {
            int? number;
            for (int i = 0; i < ItemManager.Instance.itemLength; i++)
            {
                number = _isNumber ? i + 1 : null;

                Console.Write($"- {number} {ItemManager.Instance.items[i].ItemInfo()}");

                if (Player.Instance.inventory[i] == null)
                    Console.WriteLine($"  | {ItemManager.Instance.itemPrice[i]}G");

                else
                    Console.WriteLine("  | 구매완료");
            }
        }

        /// <summary>
        /// 판매 아이템 리스트 출력
        /// </summary>
        public void SellItemList()
        {
            int num = 1;
            for (int i = 0; i < ItemManager.Instance.itemLength; i++)
            {
                if (Player.Instance.inventory[i] == null)
                    continue;

                Console.Write($"- {num++} {ItemManager.Instance.items[i].ItemInfo()}");
                Console.WriteLine($"  | {(int)(ItemManager.Instance.itemPrice[i] * sellRatio)}G");
            }
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
