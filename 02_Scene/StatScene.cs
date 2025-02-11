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
            var itemStats = GameManager.Instance.player.inventory.ItemStat();

            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");

            Console.WriteLine("─────────────────────────");
            Console.WriteLine($"Lv. {GameManager.Instance.player.level}");
            Console.WriteLine($"{GameManager.Instance.player.name} ( {GameManager.Instance.player.job} )");

            Console.WriteLine($"힘 : {GameManager.Instance.player.str + itemStats.sumStr} ({itemStats.sumStr})");
            Console.WriteLine($"민첩 : {GameManager.Instance.player.dex + itemStats.sumDex} ({itemStats.sumDex})");
            Console.WriteLine($"지능 : {GameManager.Instance.player.inte + itemStats.sumInte} ({itemStats.sumInte})");
            Console.WriteLine($"행운 : {GameManager.Instance.player.luk + itemStats.sumLuk} ({itemStats.sumLuk})");
            Console.WriteLine($"공격력 : {GameManager.Instance.player.TotalDamage} ({GameManager.Instance.player.inventory.WeaponStat()})");
            Console.WriteLine($"방어력 : {GameManager.Instance.player.TotalDefens} ({GameManager.Instance.player.inventory.ArmorStat()})");
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
