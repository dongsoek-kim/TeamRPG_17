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
            Console.WriteLine($"Lv. {GameManager.Instance.player.level}");
            Console.WriteLine($"{GameManager.Instance.player.name} ( {GameManager.Instance.player.job} )");

            Console.Write($"힘 : {GameManager.Instance.player.str}");
            GameManager.Instance.player.inventory.WeaponStat();
            Console.Write($"민첩 : {GameManager.Instance.player.dex}");
            GameManager.Instance.player.inventory.WeaponStat();
            Console.Write($"지능 : {GameManager.Instance.player.inte}");
            GameManager.Instance.player.inventory.WeaponStat();
            Console.Write($"행운 : {GameManager.Instance.player.luk}");
            GameManager.Instance.player.inventory.WeaponStat();
            Console.Write($"공격력 : {GameManager.Instance.player.TotalDamage}");
            GameManager.Instance.player.inventory.WeaponStat();

            Console.Write($"방어력 : {GameManager.Instance.player.defense}");
            GameManager.Instance.player.inventory.ArmorStat();

            Console.WriteLine($"체 력 : {GameManager.Instance.player.hp} / {GameManager.Instance.player.hpMax}");
            Console.WriteLine($"마 력 : {GameManager.Instance.player.mp} / {GameManager.Instance.player.mpMax}");
            Console.WriteLine($"Gold : {GameManager.Instance.player.gold}");
            Console.WriteLine("─────────────────────────");

            Console.WriteLine("0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            if (intCommand == 0)
                GameManager.Instance.ChangeScene(SceneName.LobbyScene);
        }
    }
}
