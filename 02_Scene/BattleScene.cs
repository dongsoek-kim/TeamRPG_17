﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class BattleScene
    {
        static Player _player = GameManager.Instance.player;
        private Dungeon currentDungeon;
        MonsterManager _monster = MonsterManager.Instance;
        List<Monster> monster;

        public int playerHp = _player.hp;


        public void StartBattle(Dungeon dungeon)
        {
            currentDungeon = dungeon;
            monster = BattleEngage(dungeon);
            InBattle();
        }
        public List<Monster> BattleEngage(Dungeon dungeon)
        {
            // 몬스터 랜덤 생성
            return _monster.RandomMonsterSpawn(dungeon);
        }

        public void InBattle()
        {
            int deathCount = 0;
            while (playerHp > 0 && deathCount < monster.Count)
            {
                DisplayStatus();
                PlayerPhase();


                deathCount = monster.Count(m => m.IsDead);

                if (deathCount >= monster.Count)
                {
                    BattleResult(true);
                    return;
                }


                foreach (var mon in monster)
                {
                    if (!mon.IsDead)
                        MonsterPhase(mon);
                }
            }

            if (playerHp <= 0)
            {
                BattleResult(false);
            }
        }

        private void DisplayStatus()
        {
            Console.Clear();
            Console.WriteLine("=== ENGAGE!!! ===");
            for (int i = 0; i < monster.Count; i++) // 그 배열 랜덤 정해진 몬스터 갯수만큼 반복, 동일한 객체가 들어가면 같은 객체로 쳐서 수정 필요
            {
                // 배열 가져와가지고 랜덤하게 정해진 몬스터 정보 출력
                Console.WriteLine($"{i + 1}. {monster[i].GetInfo()}   " + (monster[i].CurrentHp > 0 ? $"HP {monster[i].CurrentHp}" : "Dead"));
            }

            Console.WriteLine("\n\n[내 정보]");
            Console.WriteLine($"\nLv.{_player.level}  {_player.name} ({_player.job})");
            Console.WriteLine($"HP  {playerHp}");
        }

        // 플레이어 차례
        private void PlayerPhase()
        {
            bool flag = true;
            while (flag)
            {
                DisplayStatus();
                Console.WriteLine("\n\n1. 공격");
                Console.WriteLine("2. 포션");
                if (GameManager.Instance.SceneInputCommand(out int intCommand))
                {
                    switch (intCommand)
                    {
                        case 1:
                            DisplayStatus();
                            if (Targeting())
                                flag = false;
                            break;
                        case 2:
                            DisplayStatus();
                            if (SelectPotion())
                                flag = false;
                            break;
                    }
                }
            }
        }

        private bool Targeting()
        {
            while (true)
            {
                DisplayStatus();
                Console.WriteLine("\n\n0. 취소");

                Console.WriteLine("\n대상을 선택해 주세요.\n>>");
                int input;

                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

                if (input == 0) return false;

                if (input <= monster.Count && input > 0)
                {
                    Monster targetMonster = monster[input - 1];

                    if (targetMonster.IsDead)
                    {
                        Console.WriteLine("이미 죽은 몬스터입니다!");
                        Console.ReadLine();
                        return false;
                    }
                    else
                    {
                        PlayerAttack(targetMonster);
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadLine();
                }
            }
        }

        private void PlayerAttack(Monster monster) // 몬스터가 대미지 받을 때 출력
        {
            if (monster.IsDead) return;
            int dmg = MonsterTakeDamage(monster);
            Console.WriteLine($"{_player.name}의 공격!\n{monster.GetInfo()}을(를) 맞췄습니다. [데미지 : {dmg}]");

            Console.Write($"{monster.GetInfo()}\nHP {monster.CurrentHp} -> ");

            monster.CurrentHp -= dmg;

            if (monster.CurrentHp <= 0)
            {
                Console.WriteLine("Dead");
                monster.CurrentHp = 0;
                QuestManager.Instance.MonsterKillCount(monster);
            }
            else Console.WriteLine($"{monster.CurrentHp}");

            Console.WriteLine("\n\n0. 다음\n\n>>");
            string input = Console.ReadLine();

            while (true)
            {
                if (input == "0")
                    break;
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
                }
            }
        }
        // 몬스터 차례
        private void MonsterPhase(Monster monster) // 플레이어 대미지 받을 때 출력
        {
            Console.Clear();
            int dmg = PlayerTakeDamage(monster);
            Console.WriteLine($"{monster.GetInfo()}의 공격!\n{_player.name}을(를) 맞췄습니다. [데미지 : {dmg}]");

            Console.Write($"Lv.{_player.level} {_player.name}\nHP {playerHp} -> ");

            if (playerHp <= 0) Console.WriteLine("Dead");
            else Console.WriteLine($"{playerHp -= dmg}");

            Console.WriteLine("\n\n0. 다음\n\n>>");
            string input = Console.ReadLine();

            while (true)
            {
                if (input == "0")
                    break;
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
                }
            }
        }
        private int MonsterTakeDamage(Monster monster) // 대미지 계산 메서드
        {
            int dmg = _player.LuckyDamage();

            if (dmg == 0) // 몬스터는 따로 회피 기능이 없으므로 플레이어의 럭키 데미지가 0이면 회피한 것으로 처리
            {
                Console.WriteLine($"{monster.GetInfo()}가 공격을 회피했습니다!");
            }
            else // 회피하지 않았다면 데미지 출력
            {
                bool isCritical = dmg > _player.TotalDamage;

                Console.WriteLine($"{_player.name}의 공격! {monster.GetInfo()}을(를) 맞췄습니다. [데미지 : {dmg}]");

                if (isCritical)
                {
                    Console.WriteLine("치명타 적중!!!"); // 치명타 메시지 출력
                }
            }

            return dmg;
        }

        private int PlayerTakeDamage(Monster monster) // 대미지 계산 메서드
        {
            float dodgeChance = _player.luk * 0.5f / 100f; // 플레이어 회피 확률 적용

            if (RandomGenerator.Instance.NextDouble() < dodgeChance) // 회피 성공
            {
                Console.WriteLine($"{_player.name}이(가) 공격을 회피했습니다!"); // 회피 메시지 출력
                return 0; // 피해 없음
            }

            // 몬스터는 치명타 없음 -> 기본 데미지만 적용
            return Math.Max((int)(monster.Damage - _player.defense), 1); // 최소 1 이상의 피해
        }


        private bool SelectPotion() // 포션 선택
        {
            bool potionSelect = false;
            while (!potionSelect)
            {
                DisplayStatus();
                Console.WriteLine($"\n\n0. 취소");

                foreach (PotionType type in Enum.GetValues(typeof(PotionType)))
                {
                    int count = _player.inventory.potion.GetPotionCount(type);
                    Console.WriteLine($"{(int)type + 1}. {type} 포션({count}개)");
                }

                Console.WriteLine("\n포션을 선택해 주세요.\n>>");
                string input = Console.ReadLine();
                int selectedPotion;

                if (!int.TryParse(input, out selectedPotion) || selectedPotion < 0 || selectedPotion > Enum.GetValues(typeof(PotionType)).Length)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }

                if (selectedPotion == 0)
                    return false;

                PotionType selectedType = (PotionType)(selectedPotion - 1);
                int potionCount = _player.inventory.potion.GetPotionCount(selectedType);

                if (potionCount > 0)
                {
                    _player.inventory.potion.UsePotion(selectedType);
                    potionSelect = true;
                }
                else
                {
                    Console.WriteLine("\n남은 포션이 없습니다! 다른 포션을 선택해주세요.");
                    Console.ReadLine();
                }
            }
            return true;
        }
        private void BattleResult(bool isWin)
        {
            Console.Clear();
            Console.WriteLine("Battle - Result");
            if (isWin)
            {
                Console.WriteLine("\n\n!!!   VICTORY   !!!");
                foreach (var mon in monster)
                {
                    if (mon.IsDead)
                    {
                        Console.WriteLine($"- {mon.Name} 처치!");
                    }
                }
                int dungeonLevel = currentDungeon.Level;
                BattleReward reward = new BattleReward(dungeonLevel, monster.Count);
                reward.ApplyReward(_player);
            }
            else
            {
                Console.WriteLine("\n\n~~~  YOU LOSE  ~~~");
            }

            Console.WriteLine($"\n\nHP {_player.hp} remains");
            Console.WriteLine(">>");
            Console.ReadLine();
        }
    }
}
