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

        public BattleSystem(Player player)
        {
            _player = player;
        }

        public bool IsBattleActive(List<Monster> monsters)
        {
            return _player.hp > 0 && monsters.Any(m => !m.IsDead);
        }

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
                Battle.Instance._battleUI.PrintContinuePrompt();
                return;
            }
            else
            {
                Console.WriteLine($"{_player.name}의 공격!");
            }
            target.CurrentHp = Math.Max(target.CurrentHp - damage, 0);
            Battle.Instance._battleUI.DisplayDamageTaken(target, tempHp, damage);
        }

        public int ProcessMonsterAttack(Monster monster)
        {
            int tempHp = _player.hp;
            return PlayerTakeDamage(monster);
        }

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
                Battle.Instance._battleUI.DisplayDamageTaken(target, tempHp, dmg);
            }
            _player.mp -= skill.MpCost;
        }
    }
}
