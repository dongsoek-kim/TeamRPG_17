using System;
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
        private ConsoleColor defaultColor = Console.ForegroundColor;

        private int healPrice = 500;
        public override void Update()
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"{healPrice} G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {Player.Instance.gold} G)\n");
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");

            HealMessage();
            if (!SceneManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch(intCommand)
            {
                case 0:
                    SceneManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;
                case 1:
                    HealPlayer();
                    break;
            }
        }

        public void HealPlayer()
        {
            onMessage = true;

            if(healPrice <= Player.Instance.gold)
            {
                // 메세지 출력
                message = "휴식을 완료했습니다.";
                messageColor = ConsoleColor.Blue;

                Player.Instance.gold -= healPrice;
                Player.Instance.hp = 100;
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

            Console.ForegroundColor = messageColor;
            Console.WriteLine($"\n{message}");
            Console.ForegroundColor = defaultColor;

            onMessage = false;
        }
    }
}
