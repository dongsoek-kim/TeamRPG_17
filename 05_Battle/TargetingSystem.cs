using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class TargetingSystem
    {
        private readonly BattleDisplay _battleUI;
        private readonly Func<int, int> _handleInput;

        /// <summary>
        /// TargetingSystem의 생성자. BattleDisplay와 HandleInput 메서드를 받아 필드에 할당
        /// </summary>
        /// <param name="battleUI"></param>
        /// <param name="handleInput"></param>
        public TargetingSystem(BattleDisplay battleUI, Func<int, int> handleInput)
        {
            _battleUI = battleUI;
            _handleInput = handleInput;
        }

        /// <summary>
        /// 기본 공격 시 대상을 선택하는 함수
        /// </summary>
        /// <param name="monsters"></param>
        /// <returns></returns>
        public Monster SelectTarget(List<Monster> monsters)
        {
            while (true)
            {
                _battleUI.DisplayTargetingPrompt();

                int input = _handleInput(monsters.Count);

                if (input == -1)
                {
                    BattleDisplay.DisplayInvalidInput();
                    continue;
                }

                if (input == 0) return null;

                Monster target = monsters[input - 1];
                if (target.IsDead)
                {
                    Console.WriteLine("이미 죽은 몬스터입니다!");
                    BattleDisplay.PrintContinuePrompt();
                    continue;
                }

                return target;
            }
        }

        /// <summary>
        /// 스킬에 대한 대상을 선택하는 함수
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="monsters"></param>
        /// <returns></returns>
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
