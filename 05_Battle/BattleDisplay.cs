using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class BattleDisplay
    {
        private readonly List<Skill> _availableSkills;
        private Player _player;
        private List<Monster> _monsters;

        /// <summary>
        /// BattleDisplay의 생성자. 사용 가능한 스킬 목록을 받아 필드에 할당
        /// </summary>
        /// <param name="availableSkills"></param>
        public BattleDisplay(List<Skill> availableSkills)
        {
            _availableSkills = availableSkills;
        }

        /// <summary>
        /// 전투 상태를 갱신하는 함수. 플레이어와 몬스터 정보를 받아 필드에 할당
        /// </summary>
        /// <param name="player"></param>
        /// <param name="monsters"></param>
        public void UpdateBattleState(Player player, List<Monster> monsters)
        {
            _player = player;
            _monsters = monsters;
        }

        /// <summary>
        /// 전투 기본 정보들 출력
        /// </summary>
        public void DisplayBattleStatus() // 전투 기본 정보들 출력
        {
            Console.Clear();
            Console.WriteLine("=== ENGAGE!!! ===");
            DisplayMonsterStatus();
            Console.WriteLine("\n\n[내 정보]");
            DisplayPlayerStatus();
        }

        /// <summary>
        /// 몬스터 상태 출력
        /// </summary>
        private void DisplayMonsterStatus() // 몬스터 목록
        {
            for (int i = 0; i < _monsters.Count; i++)
            {
                Console.Write($"{i + 1}. {_monsters[i].GetInfo()}   ");
                //_monsters[i].CurrentHp > 0 ? $"HP _monsters[i].CurrentHp" : "Dead");
                if (_monsters[i].CurrentHp > 0)
                {
                    Console.Write($"HP ");
                    Render.ColorWriteLine($"{_monsters[i].CurrentHp}", ConsoleColor.Red);
                }
                else
                {
                    Render.ColorWriteLine("Dead", ConsoleColor.DarkRed);
                }
            }
        }

        /// <summary>
        /// 플레이어 상태 출력
        /// </summary>
        private void DisplayPlayerStatus()
        {
            Console.WriteLine($"\nLv.{_player.level}  {_player.name} ({_player.job})");
            Console.Write($"HP  ");
            Render.ColorWriteLine($"{_player.hp} / {_player.hpMax}", ConsoleColor.Red);

            Console.Write($"MP  ");
            Render.ColorWriteLine($"{_player.mp} / {_player.mpMax}", ConsoleColor.DarkCyan);
        }

        /// <summary>
        /// 전투 메뉴 출력
        /// </summary>
        public void DisplayBattleMenu() // 전투 메뉴 출력
        {
            DisplayBattleStatus();
            Console.WriteLine("\n\n1. 공격\n2. 스킬\n3. 포션");
        }

        /// <summary>
        /// 몬스터 대상 출력
        /// </summary>
        public void DisplayTargetingPrompt() // 몬스터 대상 출력
        {
            DisplayBattleStatus();
            Console.Write("\n\n0. 취소\n대상을 선택해 주세요.\n>>");
        }

        /// <summary>
        /// 스킬 목록 출력
        /// </summary>
        public void DisplaySkillList() // 스킬 목록 출력
        {
            DisplayBattleStatus();
            Console.WriteLine("\n\n0. 취소");
            for (int i = 0; i < _availableSkills.Count; i++)
            {
                Console.Write($"{i + 1}. {_availableSkills[i].Name}  MP ");
                Render.ColorWrite($"{_availableSkills[i].MpCost}", ConsoleColor.DarkCyan);
                Console.WriteLine(" 소모");
            }
            Console.Write("\n사용할 스킬을 선택해 주세요.\n>>");
        }

        /// <summary>
        /// 포션 목록 출력
        /// </summary>
        public void DisplayPotionList() // 포션 목록 출력
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

        /// <summary>
        /// 전투 종료 결과 출력
        /// </summary>
        /// <param name="isWin"> 전투 승리 여부 </param>
        /// <param name="monsters"> 현재 전투에 들어간 몬스터 목록 </param>
        /// <param name="player"></param>
        public void DisplayBattleResult(bool isWin, List<Monster> monsters, Player player, int currentDungeon)
        {
            Console.Clear();
            if (isWin)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("Battle - Result");

            if (isWin)
            {
                DisplayVictoryResult(monsters);
            }
            else
            {
                Console.WriteLine("\n~~~  YOU LOSE  ~~~");
            }

            Console.ResetColor();

            Console.Write($"\nHP ");
            Render.ColorWrite($"{player.hp}", ConsoleColor.Red);
            Console.WriteLine(" 남음");

        }

        /// <summary>
        /// 전투에서 잡은 몬스터 출력
        /// </summary>
        /// <param name="monsters"></param>
        private void DisplayVictoryResult(List<Monster> monsters) 
        {
            Console.WriteLine("\n!!!   VICTORY   !!!");
            foreach (var monster in monsters.Where(m => m.IsDead))
            {
                Console.WriteLine($"- {monster.Name} 처치!");
            }
        }

        /// <summary>
        /// 플레이어의 공격 출력 (몬스터가 맞을 때)
        /// </summary>
        /// <param name="target"> 대상 </param>
        /// <param name="prevHp"> 공격 받기 전 체력 </param>
        /// <param name="dmg"> 댐지 </param>
        public static void DisplayDamageTaken(Monster target, int prevHp, int dmg)
        {
            Console.Clear();
            Console.WriteLine($"{target.GetInfo()}을(를) 맞췄습니다. [데미지 : {dmg}]");
            Console.Write("HP ");
            Render.ColorWrite($"{prevHp}", ConsoleColor.Red);
            Console.Write(" -> ");

            if (target.CurrentHp <= 0)
            {
                Render.ColorWriteLine("Dead", ConsoleColor.DarkRed);
                QuestManager.Instance.MonsterKillCount(target);
            }
            else
            {
                Render.ColorWriteLine($"{target.CurrentHp}", ConsoleColor.Red);
            }

            PrintContinuePrompt();
        }

        /// <summary>
        /// 몬스터의 공격 출력(플레이어가 맞을 때)
        /// </summary>
        /// <param name="monster"></param>
        /// <param name="player"></param>
        /// <param name="damage"></param>
        /// <param name="prevHp"> 공격 받기 전 체력 </param>
        public static void DisplayMonsterAttack(Monster monster, Player player, int damage, int prevHp)
        {
            Console.Clear();
            Console.WriteLine($"{monster.GetInfo()}의 공격! {player.name}을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.Write("HP ");
            Render.ColorWrite($"{prevHp}", ConsoleColor.Red);
            Console.Write(" -> ");

            if (player.hp <= 0)
                Render.ColorWriteLine("Dead", ConsoleColor.DarkRed);
            else
                Render.ColorWriteLine($"{player.hp}", ConsoleColor.Red);

            PrintContinuePrompt();
        }

        /// <summary>
        ///     포션 사용 시 효과 출력
        /// </summary>
        /// <param name="potion"> 선택한 포션 </param>
        public static void DisplayPotionEffect(PotionType potion)
        {
            Console.Clear();
            Console.WriteLine($"{potion}포션을 마셨습니다.");

            switch (potion)
            {
                case PotionType.Health:
                    {
                        Console.WriteLine($"당신의 상처가 아뭅니다.");
                        break;
                    }
                case PotionType.str:
                    {
                        Console.WriteLine($"당신의 악력이 강해지는 것 같습니다.");
                        break;
                    }
                case PotionType.dex:
                    {
                        Console.WriteLine($"당신의 몸놀림이 빨라진 것 같습니다.");
                        break;
                    }
                case PotionType.inte:
                    {
                        Console.WriteLine($"당신은 더 똑똑해진 것 같습니다.");
                        break;
                    }
                case PotionType.luk:
                    {
                        Console.WriteLine($"당신의 뭘 해도 잘될 것 같은 느낌을 받습니다.");
                        break;
                    }
                default: break;
            }

            PrintContinuePrompt();
        }

        /// <summary>
        ///     잘못된 입력 출력
        /// </summary>
        public static void DisplayInvalidInput()
        {
            Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
        }

        /// <summary>
        ///     마나 부족 시 출력
        /// </summary>
        public static void DisplayNotEnoughMP()
        {
            Console.WriteLine("마나가 부족합니다! 다시 입력해주세요.");
            Console.Write(">> ");
        }

        /// <summary>
        ///    포션이 없을 때 출력
        /// </summary>
        public static void DisplayNoPotions()
        {
            Console.WriteLine("\n남은 포션이 없습니다! 다른 포션을 선택해주세요.");
            Console.Write(">> ");
        }

        /// <summary>
        ///   계속 진행할 수 있도록 출력
        /// </summary>
        public static void PrintContinuePrompt()
        {
            Console.Write("\n아무 키나 눌러 진행...\n>>");
            Console.ReadKey(true);
        }
    }
}
