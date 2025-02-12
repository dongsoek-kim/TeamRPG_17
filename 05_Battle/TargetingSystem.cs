﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class TargetingSystem
    {
        private readonly BattleDisplay _battleUI;
        private readonly Func<int, int> _handleInput; // Func<int, int> 타입의 델리게이트로 변경

        public TargetingSystem(BattleDisplay battleUI, Func<int, int> handleInput)
        {
            _battleUI = battleUI;
            _handleInput = handleInput; // 델리게이트 필드에 할당
        }

        public Monster SelectTarget(List<Monster> monsters) // 기본 공격 시 대상 판단
        {
            while (true)
            {
                _battleUI.DisplayTargetingPrompt();

                int input = _handleInput(monsters.Count);

                if (input == -1) // 잘못된 입력 처리
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

        public List<Monster> GetTargetsForSkill(Skill skill, List<Monster> monsters) // 스킬이 단일 타겟인지 전체 타겟인지 판단
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
