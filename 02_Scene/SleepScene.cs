﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    internal class SleepScene : Scene
    {
        private bool onMessage;
        private string message;
        private ConsoleColor messageColor;
        //private ConsoleColor defaultColor = Console.ForegroundColor;

        private int healPrice = 500;
        public override void Update()
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"{healPrice} G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {GameManager.Instance.player.gold} G)\n");
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");

            HealMessage();
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch(intCommand)
            {
                case 0:
                    GameManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;
                case 1:
                    HealPlayer();
                    GiftPotion();
                    break;
            }
        }
        /// <summary>
        ///  플레이어 회복
        /// </summary>
        public void HealPlayer()
        {
            onMessage = true;

            if(healPrice <= GameManager.Instance.player.gold)
            {
                message = "휴식을 완료했습니다."; // 메세지 출력
                messageColor = ConsoleColor.Blue;

                GameManager.Instance.player.gold -= healPrice;
                GameManager.Instance.player.hp = 100;
                GameManager.Instance.player.mp = 100;
            }
            else
            {
                message = "Gold 가 부족합니다.";
                messageColor = ConsoleColor.Red;
            }
        }

        private void HealMessage()
        {
            if (!onMessage)
                return;

            Render.ColorWriteLine($"\n{message}", messageColor);

            onMessage = false;
        }

        /// <summary>
        /// 회복포션 소지개수 3개이하 시 충전
        /// </summary>
        public void GiftPotion()
        {
            int potionCount = GameManager.Instance.player.inventory.potion.potionCount[(int)PotionType.Health];

            if (potionCount < 3)
            {
                int givePotions = 3 - potionCount;

                for (int i = 0; i < givePotions; i++)
                {
                    GameManager.Instance.player.inventory.potion.GetPotion(PotionType.Health, 1);
                    Console.WriteLine("포션이 지급되었습니다!");
                }
            }
            else
            {
                Console.WriteLine("포션은 이미 3개 이상 보유 중입니다.");
            }
        }
    }
}
