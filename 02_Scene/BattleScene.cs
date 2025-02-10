using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class BattleScene
    {
        static Player _player = GameManager.Instance.player;
        private Dungeon _currentDungeon;
        MonsterManager _monsterManager = MonsterManager.Instance;
        List<Monster> _monster;
        List<Skill> availableSkills = SkillManager.Instance.GetSkillList(_player.job);

        public void StartBattle(Dungeon dungeon)
        {
            _currentDungeon = dungeon;
            _monster = _monsterManager.RandomMonsterSpawn(dungeon);
            InBattle();
        }
        private void InBattle()
        {
            while (_player.hp > 0 && _monster.Any(m => !m.IsDead))
            {
                DisplayStatus();
                PlayerPhase();

                if (_monster.All(m => m.IsDead)) // 몬스터 다죽었으면
                {
                    BattleResult(true); // 승리
                    return;
                }

                foreach (var monster in _monster.Where(m => !m.IsDead))
                {
                    MonsterPhase(monster);
                }
            }

            if (_player.hp <= 0) // 플레이어 죽었으면
            {
                BattleResult(false); // 패배
            }
        }

        private void DisplayStatus()
        {
            Console.Clear();
            Console.WriteLine("=== ENGAGE!!! ===");
            for (int i = 0; i < _monster.Count; i++) // 그 배열 랜덤 정해진 몬스터 갯수만큼 반복, 동일한 객체가 들어가면 같은 객체로 쳐서 수정 필요
            {
                // 배열 가져와가지고 랜덤하게 정해진 몬스터 정보 출력
                Console.WriteLine($"{i + 1}. {_monster[i].GetInfo()}   " + (_monster[i].CurrentHp > 0 ? $"HP {_monster[i].CurrentHp}" : "Dead"));
            }

            Console.WriteLine("\n\n[내 정보]");
            Console.WriteLine($"\nLv.{_player.level}  {_player.name} ({_player.job})");
            Console.WriteLine($"HP  {_player.hp} / {_player.hpMax}"); // 체력 출력
            Console.WriteLine($"HP  {_player.mp} / {_player.mpMax}"); // mp 출력
        }

        // 플레이어 차례
        private void PlayerPhase()
        {
            bool actionTaken = false;
            while (!actionTaken)
            {
                DisplayStatus();
                Console.WriteLine("\n\n1. 공격\n2. 스킬\n3. 포션"); // 전투 선택지

                int command = HandleInput(3);
                switch (command)
                {
                    case 1:
                        actionTaken = Targeting();
                        break;
                    case 2:
                        actionTaken = SelectSkill();
                        break;
                    case 3:
                        actionTaken = SelectPotion();
                        break;
                }
            }
        }

        private bool Targeting() // 공격할 몬스터 선택
        {
            while (true)
            {
                DisplayStatus();
                Console.WriteLine("\n\n0. 취소\n대상을 선택해 주세요.\n>>");

                int input = HandleInput(_monster.Count);
                if (input == 0) return false;

                Monster target = _monster[input - 1];
                if (target.IsDead)
                {
                    Console.WriteLine("이미 죽은 몬스터입니다!");
                    PrintContinuePrompt();
                    return false;
                }

                PlayerAttack(target);
                return true;
            }
        }

        private void PlayerAttack(Monster target) // 몬스터가 대미지 받을 때 출력
        {
            if (target.IsDead) return;

            float dodgeChance = 0.1f; // 10% 회피 확률
            bool isDodge = RandomGenerator.Instance.NextDouble() < dodgeChance;

            int damage = _player.LuckyDamage();
            bool isCritical = damage > _player.TotalDamage;

            if (isCritical)
            {
                // 치명타는 회피를 무시함
                Console.WriteLine($"{_player.name}의 치명타 공격!\n{target.GetInfo()}을(를) 맞췄습니다. [데미지 : {damage}]");
            }
            else if (isDodge)
            {
                // 회피 처리
                Console.WriteLine($"{target.GetInfo()}가 공격을 회피했습니다!");
                return; // 피해를 입히지 않음
            }
            else
            {
                // 회피하지 않았다면 피해를 입힌다
                Console.WriteLine($"{_player.name}의 공격! {target.GetInfo()}을(를) 맞췄습니다. [데미지 : {damage}]");
            }

            // 피해 계산
            target.CurrentHp = Math.Max(target.CurrentHp - damage, 0);

            if (target.CurrentHp <= 0)
            {
                Console.WriteLine("Dead");
                QuestManager.Instance.MonsterKillCount(target);
            }
            else
            {
                Console.WriteLine($"HP {target.CurrentHp}");
            }

            PrintContinuePrompt();
        }
        // 몬스터 차례
        private void MonsterPhase(Monster monster) // 플레이어 대미지 받을 때 출력
        {
            int damage = PlayerTakeDamage(monster);
            Console.WriteLine($"{monster.GetInfo()}의 공격! {_player.name}을(를) 맞췄습니다. [데미지 : {damage}]");

            if (_player.hp <= 0)
                Console.WriteLine("Dead");
            else
                Console.WriteLine($"HP {_player.hp}");

            PrintContinuePrompt();
        }

        private int PlayerTakeDamage(Monster monster) // 대미지 계산 메서드
        {
            float dodgeChance = _player.luk * 0.5f / 100f;
            if (RandomGenerator.Instance.NextDouble() < dodgeChance)
            {
                Console.WriteLine($"{_player.name}이(가) 공격을 회피했습니다!");
                return 0;
            }

            int damageTaken = Math.Max((int)(monster.Damage - _player.defense), 1);
            _player.hp = Math.Max(_player.hp - damageTaken, 0);

            return damageTaken;
        }

        private bool SelectSkill()
        {
            while (true)
            {
                DisplayStatus();
                Console.WriteLine("\n\n0. 취소");

                // 스킬 목록 출력
                for (int i = 0; i < availableSkills.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {availableSkills[i].Name} (MP {availableSkills[i].MpCost}");
                }

                Console.WriteLine("\n사용할 스킬을 선택해 주세요.\n>>");
                int input = HandleInput(availableSkills.Count); // 사용할 스킬 번호 입력

                if (input == 0) return false; // 0. 취소 입력 시 뒤로가기

                // 스킬 실제 효과 메서드
                Skill selectedSkill = availableSkills[input - 1];
                // 마나가 없을 때 SelectSkill다시 해줘야되고(스킬 요구 mp보다 현재 mp가 적을 때
                if (_player.mp < selectedSkill.MpCost)
                {
                    Console.WriteLine("마나가 부족합니다!");
                    PrintContinuePrompt();
                    continue;
                }

                UseSkill(selectedSkill);
                return true;
                // 스킬 정보는 Skill에서 관리하고 스킬 목록 자체는 SkillManager에서 관리
            }
        }

        private void UseSkill(Skill skill)
        {

        }

        private bool SelectPotion() // 포션 선택
        {
            while (true)
            {
                DisplayStatus();
                Console.WriteLine("\n\n0. 취소");
                foreach (PotionType type in Enum.GetValues(typeof(PotionType)))
                {
                    int count = _player.inventory.potion.GetPotionCount(type);
                    Console.WriteLine($"{(int)type + 1}. {type} 포션({count}개)");
                }

                Console.WriteLine("\n포션을 선택해 주세요.\n>>");
                int input = HandleInput(Enum.GetValues(typeof(PotionType)).Length);

                if (input == 0) return false;

                PotionType selectedType = (PotionType)(input - 1);
                if (_player.inventory.potion.GetPotionCount(selectedType) > 0)
                {
                    _player.inventory.potion.UsePotion(selectedType);
                    return true;
                }
                else
                {
                    Console.WriteLine("\n남은 포션이 없습니다! 다른 포션을 선택해주세요.");
                    PrintContinuePrompt();
                }
            }
        }
        private void BattleResult(bool isWin)
        {
            Console.Clear();
            Console.WriteLine("Battle - Result");
            if (isWin)
            {
                Console.WriteLine("\n!!!   VICTORY   !!!");
                foreach (var mon in _monster.Where(m => m.IsDead))
                {
                    Console.WriteLine($"- {mon.Name} 처치!");
                }
                BattleReward reward = new BattleReward(_currentDungeon.Level, _monster.Count);
                reward.ApplyReward(_player);
            }
            else
            {
                Console.WriteLine("\n~~~  YOU LOSE  ~~~");
            }

            Console.WriteLine($"\nHP {_player.hp} remains");
            PrintContinuePrompt();
        }
        private int HandleInput(int maxOption)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int selection) && selection >= 0 && selection <= maxOption)
                {
                    return selection;
                }
                Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
            }
        }

        private void PrintContinuePrompt()
        {
            Console.WriteLine("\n0. 다음\n>>");
            Console.ReadLine();
        }
    }
}