using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeamRPG_17
{
    public class BattleSystem
    {
        private readonly Player _player;

        /// <summary>
        /// BattleSystem의 생성자. 플레이어 정보를 받아 필드에 할당
        /// </summary>
        /// <param name="player"></param>
        public BattleSystem(Player player)
        {
            _player = player;
        }

        /// <summary>
        /// 전투가 진행 중인지 확인하는 함수
        /// </summary>
        /// <param name="monsters"></param>
        /// <returns></returns>
        public bool IsBattleActive(List<Monster> monsters)
        {
            return _player.hp > 0 && monsters.Any(m => !m.IsDead);
        }

        /// <summary>
        /// 플레이어의 공격 함수
        /// </summary>
        /// <param name="target"></param>
        public void PlayerAttack(Monster target)
        {
            if (target.IsDead) return;
            int tempHp = target.CurrentHp;

            float dodgeChance = 0.1f;
            bool isDodge = RandomGenerator.Instance.NextDouble() < dodgeChance;

            int damage = _player.LuckyDamage() - (int)target.Defense;
            bool isCritical = damage > _player.TotalDamage;

            if (isCritical)
            {
                Console.WriteLine($"{_player.name}의 치명타 공격!");
            }
            else if (isDodge)
            {
                Console.WriteLine($"{target.GetInfo()}가 공격을 회피했습니다!");
                BattleDisplay.PrintContinuePrompt();
                return;
            }
            else
            {
                Console.WriteLine($"{_player.name}의 공격!");
            }
            target.CurrentHp = Math.Max(target.CurrentHp - damage, 0);
            BattleDisplay.DisplayDamageTaken(target, tempHp, damage);
        }

        /// <summary>
        /// 몬스터의 공격 함수
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        public int ProcessMonsterAttack(Monster monster)
        {
            int tempHp = _player.hp;
            return PlayerTakeDamage(monster);
        }

        /// <summary>
        /// 플레이어가 몬스터의 공격을 받았을 때 처리하는 함수
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        private int PlayerTakeDamage(Monster monster)
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

        /// <summary>
        /// 스킬 사용 함수
        /// </summary>
        /// <param name="skill"> 선택한 스킬 </param>
        /// <param name="targets"> 현재 대상(단일 / 전체) </param>
        public void UseSkill(Skill skill, List<Monster> targets)
        {
            if (targets.Count == 0) return;
            Console.Clear();
            Console.WriteLine($"{_player.name}가 {skill.Name} 사용!");
            Console.ReadLine();
            foreach (var target in targets)
            {
                if (target.IsDead) continue;
                int tempHp = target.CurrentHp;

                int dmg = (int)(_player.TotalDamage * (skill.Damage / 100f)) - (int)target.Defense;
                target.CurrentHp = Math.Max(target.CurrentHp - dmg, 0);
                BattleDisplay.DisplayDamageTaken(target, tempHp, dmg);
            }
            _player.mp -= skill.MpCost;
        }
    }
}
