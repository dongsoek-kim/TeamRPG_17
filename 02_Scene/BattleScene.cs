﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    internal class BattleScene : Scene
    {
        Player _player = GameManager.Instance.player;
        MonsterManager _monster;
        List<Monster> monster;
        private bool onGame = false;
        public override void Update()
        {
            // 전투 개시
            if (!onGame)
            {
                monster = BattleEngage();
            }
            else InBatte();
        }

        public List<Monster> BattleEngage()
        {
            // 몬스터 랜덤 생성
            onGame = true;
            return _monster.RandomMonsterSpawn();
        }

        private void InBatte()
        {
            int deathCount = 0;
            while (_player.hp > 0 && deathCount < monster.Count)
            {
                DisplayStatus();
                PlayerPhase();

                for (int i = 0; i < monster.Count; i++) if (monster[i].IsDead) deathCount++;
                if (deathCount > 0) break; // 모두 죽였으면 끝

                for (int i = 0; i < monster.Count; i++) // 위에 표시된 몬스터부터 차례대로 공격
                {
                    if (!monster[i].IsDead)
                        MonsterPhase(monster[i]);
                }
            }
        }

        private void DisplayStatus()
        {
            Console.Clear();
            Console.WriteLine("=== ENGAGE!!! ===");
            for (int i = 0; i < monster.Count; i++) // 그 배열 랜덤 정해진 몬스터 갯수만큼 반복
            {
                // 배열 가져와가지고 랜덤하게 정해진 몬스터 정보 출력
                Console.WriteLine($"{i + 1} {monster[i].GetInfo()}");
            }

            Console.WriteLine("\n\n[내 정보]");
            Console.WriteLine($"Lv.{_player.level}  {_player.name} ({_player.job})");
            Console.WriteLine($"HP  {_player.hp}");
        }

        // 플레이어 차례
        private void PlayerPhase()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("1. 공격");
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
            Console.WriteLine("\n0. 취소");

            Console.WriteLine("\n대상을 선택해 주세요.\n>>");
            int input = int.Parse(Console.ReadLine());
            DisplayStatus();

            while (true)
            {
                if (input <= monster.Count && input > 0)
                {
                    PlayerAttack(monster[input - 1]);
                    break;
                }
                else if (input == 0) return false;
                else Console.WriteLine("잘못된 입력입니다.");
            }
            return true;
        }

        private void PlayerAttack(Monster monster) // 몬스터가 대미지 받을 때 출력
        {
            int dmg = MonsterTakeDamage(monster);
            Console.WriteLine($"{_player.name}의 공격!\n{monster.GetInfo()}을(를) 맞췄습니다. [데미지 : {dmg}]");

            Console.Write($"{monster.GetInfo()}\nHP {monster.CurrentHp} -> ");

            if (monster.CurrentHp <= 0) Console.WriteLine("Dead");
            else Console.WriteLine($"{monster.CurrentHp -= dmg}");

            Console.WriteLine("0. 다음\n\n>>");
            string input = Console.ReadLine();

            while (true)
            {
                if (input == "0")
                    break;
                else Console.WriteLine("잘못된 입력입니다.");
            }
        }
        // 몬스터 차례
        private void MonsterPhase(Monster monster) // 플레이어 대미지 받을 때 출력
        {
            Console.Clear();
            int dmg = PlayerTakeDamage(monster);
            Console.WriteLine($"{monster.GetInfo()}의 공격!\n{_player.name}을(를) 맞췄습니다. [데미지 : {dmg}]");

            Console.Write($"{monster.GetInfo()}\nHP {monster.CurrentHp} -> ");

            if (monster.CurrentHp <= 0) Console.WriteLine("Dead");
            else Console.WriteLine($"{monster.CurrentHp -= dmg}");

            Console.WriteLine("0. 다음\n\n>>");
            string input = Console.ReadLine();

            while (true)
            {
                if (input == "0")
                    break;
                else Console.WriteLine("잘못된 입력입니다.");
            }
        }
        private int MonsterTakeDamage(Monster monster) // 대미지 계산 메서드
        {
            int dmg = Math.Max((int)(_player.damage - monster.Defense), 1); // 플레이어 대미지 - 몬스터 방어력
            return dmg;
        }

        private int PlayerTakeDamage(Monster monster) // 대미지 계산 메서드
        {
            int dmg = Math.Max((int)(monster.Damage - _player.defense), 1); // 몬스터 대미지 - 플레이어 방어력
            return dmg;
        }


        private bool SelectPotion() // 포션 선택
        {
            while (true)
            {
                DisplayStatus();
                Console.WriteLine($"0. 취소");
                Console.WriteLine($"1. 회복 포션");
                Console.WriteLine($"2. 힘 포션");
                Console.WriteLine($"3. 민첩 포션");
                Console.WriteLine($"4. 지능 포션");
                Console.WriteLine($"5. 행운 포션");
                Console.WriteLine("\n포션 종류를 입력해 주세요.\n>>");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        return false;
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                }
            }
            return true;
        }
    }
}
