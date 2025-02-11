using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class TargetingSystem
    {
        private readonly BattleScene _battleUI;
        private readonly Func<int, int> _handleInput; // Func<int, int> 타입의 델리게이트로 변경

        public TargetingSystem(BattleScene battleUI, Func<int, int> handleInput)
        {
            _battleUI = battleUI;
            _handleInput = handleInput; // 델리게이트 필드에 할당
        }

        public Monster SelectTarget(List<Monster> monsters)
        {
            while (true)
            {
                _battleUI.DisplayTargetingPrompt();

                int input = _handleInput(monsters.Count);

                if (input == -1) // 잘못된 입력 처리
                {
                    BattleScene.DisplayInvalidInput();
                    continue;
                }

                if (input == 0) return null;

                Monster target = monsters[input - 1];
                if (target.IsDead)
                {
                    Console.WriteLine("이미 죽은 몬스터입니다!");
                    BattleScene.PrintContinuePrompt();
                    continue;
                }

                return target;
            }
        }

        public List<Monster> GetTargetsForSkill(Skill skill, List<Monster> monsters)
        {
            if (skill.SkillType == SkillType.AllTarget)
            {
                return monsters.Where(m => !m.IsDead).ToList();
            }

            Monster target = SelectTarget(monsters);
            return target != null ? new List<Monster> {target} : new List<Monster>();
        }
    }
}
