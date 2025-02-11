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

            Render.ColorWriteLine(("상태보기"), ConsoleColor.Cyan);
            Render.ColorWriteLine(("캐릭터의 정보가 표시됩니다."), ConsoleColor.Cyan);

            Console.WriteLine("─────────────────────────");
            Console.WriteLine($"Lv. {GameManager.Instance.player.level}");
            Console.WriteLine($"{GameManager.Instance.player.name} ( {GameManager.Instance.player.job} )");

            Console.Write($"힘 : {GameManager.Instance.player.str + itemStats.sumStr}");
            Render.ColorWriteLine($" ({itemStats.sumStr})", ConsoleColor.Green);
            Console.Write($"민첩 : {GameManager.Instance.player.dex + itemStats.sumDex}");
            Render.ColorWriteLine($" ({itemStats.sumDex})", ConsoleColor.Green);
            Console.Write($"지능 : {GameManager.Instance.player.inte + itemStats.sumInte}");
            Render.ColorWriteLine($" ({itemStats.sumInte})", ConsoleColor.Green);
            Console.Write($"행운 : {GameManager.Instance.player.luk + itemStats.sumLuk}");
            Render.ColorWriteLine($" ({itemStats.sumDex})", ConsoleColor.Green);
            Console.Write($"공격력 : {GameManager.Instance.player.TotalDamage}");
            Render.ColorWriteLine($" ({GameManager.Instance.player.inventory.WeaponStat()})", ConsoleColor.Green);
            Console.Write($"방어력 : {GameManager.Instance.player.TotalDefens}");
            Render.ColorWriteLine($" ({GameManager.Instance.player.inventory.ArmorStat()})", ConsoleColor.Green);
            Console.Write("체 력 : ");
            Render.ColorWriteLine($"{GameManager.Instance.player.hp} / {GameManager.Instance.player.hpMax}", ConsoleColor.Red);
            Console.Write("마 력 : ");
            Render.ColorWriteLine($"{GameManager.Instance.player.mp} / {GameManager.Instance.player.mpMax}", ConsoleColor.Blue);
            Console.Write("Gold : ");
            Render.ColorWriteLine($"{GameManager.Instance.player.gold}", ConsoleColor.Yellow);
            Console.WriteLine("─────────────────────────");

            Console.WriteLine("0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            if (intCommand == 0)
                GameManager.Instance.ChangeScene(SceneName.LobbyScene);
        }
    }
}
