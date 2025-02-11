using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class BattleSceneUI
    {
        private readonly List<Skill> _availableSkills;
        private Player _player;
        private List<Monster> _monsters;

        public BattleSceneUI(List<Skill> availableSkills)
        {
            _availableSkills = availableSkills;
        }

        public void UpdateBattleState(Player player, List<Monster> monsters)
        {
            _player = player;
            _monsters = monsters;
        }

        public void DisplayBattleStatus()
        {
            Console.Clear();
            Console.WriteLine("=== ENGAGE!!! ===");
            DisplayMonsterStatus();
            Console.WriteLine("\n\n[내 정보]");
            DisplayPlayerStatus();
        }

        private void DisplayMonsterStatus()
        {
            for (int i = 0; i < _monsters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_monsters[i].GetInfo()}   " +
                    (_monsters[i].CurrentHp > 0 ? $"HP {_monsters[i].CurrentHp}" : "Dead"));
            }
        }

        private void DisplayPlayerStatus()
        {
            Console.WriteLine($"\nLv.{_player.level}  {_player.name} ({_player.job})");
            Console.WriteLine($"HP  {_player.hp} / {_player.hpMax}");
            Console.WriteLine($"MP  {_player.mp} / {_player.mpMax}");
        }

        public void DisplayBattleMenu()
        {
            DisplayBattleStatus();
            Console.WriteLine("\n\n1. 공격\n2. 스킬\n3. 포션");
        }

        public void DisplayTargetingPrompt()
        {
            DisplayBattleStatus();
            Console.Write("\n\n0. 취소\n대상을 선택해 주세요.\n>>");
        }

        public void DisplaySkillList()
        {
            DisplayBattleStatus();
            Console.WriteLine("\n\n0. 취소");
            for (int i = 0; i < _availableSkills.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_availableSkills[i].Name} (MP {_availableSkills[i].MpCost})");
            }
            Console.Write("\n사용할 스킬을 선택해 주세요.\n>>");
        }

        public void DisplayPotionList()
        {
            DisplayBattleStatus();
            Console.WriteLine("\n\n0. 취소");
            foreach (PotionType type in Enum.GetValues(typeof(PotionType)))
            {
                int count = _player.inventory.potion.GetPotionCount(type);
                Console.WriteLine($"{(int)type + 1}. {type} 포션({count}개)");
            }
            Console.Write("\n포션을 선택해 주세요.\n>>");
        }

        public void DisplayBattleResult(bool isWin, List<Monster> monsters, Player player, int dungeonLevel)
        {
            Console.Clear();
            Console.WriteLine("Battle - Result");

            if (isWin)
            {
                DisplayVictoryResult(monsters);
            }
            else
            {
                Console.WriteLine("\n~~~  YOU LOSE  ~~~");
            }

            Console.WriteLine($"\nHP {player.hp} remains");
        }

        private void DisplayVictoryResult(List<Monster> monsters)
        {
            Console.WriteLine("\n!!!   VICTORY   !!!");
            foreach (var monster in monsters.Where(m => m.IsDead))
            {
                Console.WriteLine($"- {monster.Name} 처치!");
            }
        }

        public void DisplayDamageTaken(Monster target, int prevHp, int dmg)
        {
            Console.Clear();
            Console.WriteLine($"{target.GetInfo()}을(를) 맞췄습니다. [데미지 : {dmg}]");
            Console.Write($"HP {prevHp} -> ");

            if (target.CurrentHp <= 0)
            {
                Console.WriteLine("Dead");
            }
            else
            {
                Console.WriteLine($"{target.CurrentHp}");
            }

            PrintContinuePrompt();
        }

        public void DisplayMonsterAttack(Monster monster, Player player, int damage, int prevHp)
        {
            Console.Clear();
            Console.WriteLine($"{monster.GetInfo()}의 공격! {player.name}을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.Write($"HP {prevHp} -> ");

            if (player.hp <= 0)
                Console.WriteLine("Dead");
            else
                Console.WriteLine($"{player.hp}");

            PrintContinuePrompt();
        }

        public void DisplayInvalidInput()
        {
            Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
        }

        public void DisplayNotEnoughMP()
        {
            Console.WriteLine("마나가 부족합니다!");
            PrintContinuePrompt();
        }

        public void DisplayNoPotions()
        {
            Console.WriteLine("\n남은 포션이 없습니다! 다른 포션을 선택해주세요.");
            PrintContinuePrompt();
        }

        public void PrintContinuePrompt()
        {
            Console.Write("\n0. 다음\n>>");
            Console.ReadLine();
        }
    }
}
