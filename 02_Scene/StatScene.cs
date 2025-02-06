using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class StatScene : Scene
    {
        public override void Update()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");

            Console.WriteLine("─────────────────────────");
            Console.WriteLine($"Lv. {Player.Instance.level}");
            Console.WriteLine($"{Player.Instance.name} ( {Player.Instance.job} )");

            Console.Write($"공격력 : {Player.Instance.damage}");
            Player.Instance.inventory.WeaponStat();

            Console.Write($"방어력 : {Player.Instance.defense}");
            Player.Instance.inventory.ArmorStat();

            Console.WriteLine($"체 력 : {Player.Instance.hp}");
            Console.WriteLine($"Gold : {Player.Instance.gold}");
            Console.WriteLine("─────────────────────────");

            Console.WriteLine("0. 나가기");

            if (!SceneManager.Instance.SceneInputCommand(out int intCommand))
                return;

            if (intCommand == 0)
                SceneManager.Instance.ChangeScene(SceneName.LobbyScene);
        }
    }
}
