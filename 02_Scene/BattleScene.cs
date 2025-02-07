using System;
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
            else DisplayStatus();
        }

        public List<Monster> BattleEngage()
        {
            // 몬스터 랜덤 생성
            onGame = true;
            return _monster.RandomMonsterSpawn();
        }


        private void DisplayStatus()
        {
            // arr[0] = 2 List 정보가 있는데 monster[arr[0]].Name = 오크 이렇게 되지 않겠냐 이거지
            int deathCount = 0;
            while (_player.hp > 0 && deathCount < monster.Count)
            {
                Console.Clear();
                Console.WriteLine("=== ENGAGE!!! ===");
                for (int i = 0; i < monster.Count; i++) // 그 배열 랜덤 정해진 몬스터 갯수만큼 반복
                {
                    // 배열 가져와가지고 랜덤하게 정해진 몬스터 정보 출력
                    Console.WriteLine(monster[i].GetInfo());
                }

                Console.WriteLine("\n\n[내 정보]");
                Console.WriteLine($"Lv.{_player.level}  {_player.name} ({_player.job})");
                Console.WriteLine($"HP  {_player.hp}");
                PlayerPhase();

                for (int i = 0; i < monster.Count; i++) if (monster[i].IsDead) deathCount++;

                if (deathCount > 0) break;

                MonsterPhase();
            }

        }

        // 플레이어 차례
        private void PlayerPhase()
        {
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 포션 처먹기");

            bool flag = true;
            while (flag)
            {
                if (GameManager.Instance.SceneInputCommand(out int intCommand))
                {
                    switch (intCommand)
                    {
                        case 1:
                            DisplayStatus();
                            Targeting();
                            flag = false;
                            break;
                        case 2:
                            PotionDrink();
                            flag = false;
                            break;
                    }
                }
            }
        }
        // 몬스터 차례
        private void MonsterPhase()
        {
            Console.Clear();
            Console.WriteLine();

        }
        // 대미지 계산
        private void TakeDamage() // 플레이어가 대미지 받을 때 계산 메서드
        {

        }

        private void Targeting()
        {
            Console.WriteLine("\n대상을 선택해 주세요.\n>>");
            int input = int.Parse(Console.ReadLine());
            DisplayStatus();

            while (true)
            {
                if (input <= monster.Count && input > 0)
                {
                    MonsterTakeDamage(monster[input - 1]);
                    break;
                }
                else Console.WriteLine("잘못된 입력입니다.");
            }
        }
        private void MonsterTakeDamage(Monster monster) // 몬스터가 대미지 받을 때 계산 메서드
        {
            int dmg = Math.Max((int)(_player.damage - monster.Defense), 1);
            Console.WriteLine($"{_player.name}의 공격!\n{monster.GetInfo()}을(를) 맞췄습니다. [데미지 : {dmg}]");

            Console.Write($"{monster.GetInfo()}\nHP {monster.CurrentHp} -> ");

            if (monster.CurrentHp <= 0) Console.WriteLine("Dead");
            else Console.WriteLine($"{monster.CurrentHp -= dmg}");

            Console.WriteLine("0. 다음\n\n>>");
            string input = Console.ReadLine();

            while(true)
            {
                if (input == "0")
                    break;
                else Console.WriteLine("잘못된 입력입니다.");
            }
        }
        
        private void PotionDrink() // 포션 먹었을 때 회복 메서드
        {
            _player.hp += 100;
        }

    }
}
