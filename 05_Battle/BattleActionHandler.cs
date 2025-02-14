﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class BattleActionHandler
    {
        private readonly Player _player;
        private readonly BattleDisplay _battleUI;
        private readonly BattleSystem _battleSystem;
        private readonly TargetingSystem _targetingSystem;
        private readonly List<Skill> _availableSkills;
        private readonly Func<int, int> HandleInput; // BattleScene의 HandleInput 메서드를 사용

        /// <summary>
        /// BattleActionHandler의 생성자. 필요한 인자들을 받아 필드에 할당
        /// </summary>
        public BattleActionHandler(Player player, BattleDisplay battleUI, BattleSystem battleSystem, TargetingSystem targetingSystem, List<Skill> availableSkills, Func<int, int> handleInput)
        {
            _player = player;
            _battleUI = battleUI;
            _battleSystem = battleSystem;
            _targetingSystem = targetingSystem;
            _availableSkills = availableSkills;
            HandleInput = handleInput;
        }

        /// <summary>
        /// 플레이어의 턴을 처리하는 함수. 전투 기본 메뉴를 출력하고 선택에 따라 공격, 스킬, 포션을 처리함
        /// </summary>
        /// <param name="monsters"></param>
        public void HandlePlayerTurn(List<Monster> monsters) // 전투 기본 메뉴 선택(1. 공격, 2. 스킬, 3. 포션)
        {
            bool actionTaken = false;
            while (!actionTaken)
            {
                _battleUI.DisplayBattleMenu();

                int command = HandleInput(3);
                switch (command)
                {
                    case 1:
                        actionTaken = HandleAttack(monsters);
                        break;
                    case 2:
                        actionTaken = HandleSkill(monsters);
                        break;
                    case 3:
                        actionTaken = HandlePotion();
                        break;
                    default:
                        BattleDisplay.DisplayInvalidInput();  // 잘못된 입력이 들어오면 InvalidInput 메시지를 출력하고 다시 반복
                        continue;
                }
            }
        }

        /// <summary>
        /// 공격 메뉴를 처리하는 함수. SelectTarget()에서 몬스터를 선택해줘야됨
        /// </summary>
        /// <param name="monsters"></param>
        private bool HandleAttack(List<Monster> monsters) // 공격 메뉴(SelectTarget()에서 몬스터를 선택해줘야됨)
        {
            Monster target = _targetingSystem.SelectTarget(monsters);
            if (target == null) return false;

            _battleSystem.PlayerAttack(target);
            return true;
        }

        /// <summary>
        /// 스킬 메뉴를 처리하는 함수. GetTargetsForSkill()에서 몬스터를 선택해줘야됨
        /// </summary>
        /// <param name="monsters"></param>
        private bool HandleSkill(List<Monster> monsters) // 스킬 선택(역시 GetTargetsForSkill()에서 몬스터를 선택해줘야됨)
        {
            _battleUI.DisplaySkillList();

            while (true)
            {
                int input = HandleInput(_availableSkills.Count);
                if (input == 0) return false;

                if (input < 0 || input > _availableSkills.Count)
                {
                    BattleDisplay.DisplayInvalidInput();  // 잘못된 입력에 대한 처리
                    continue;
                }

                Skill selectedSkill = _availableSkills[input - 1];
                if (_player.mp < selectedSkill.MpCost) // 마나 부족 시 다시
                {
                    BattleDisplay.DisplayNotEnoughMP();
                    continue;
                }

                List<Monster> targets = _targetingSystem.GetTargetsForSkill(selectedSkill, monsters);
                if (targets.Count == 0) return false;

                _battleSystem.UseSkill(selectedSkill, targets);
                return true;
            }
        }

        /// <summary>
        /// 포션 메뉴를 처리하는 함수
        /// </summary>
        /// <returns></returns>
        private bool HandlePotion() // 포션 선택
        {
            _battleUI.DisplayPotionList();

            while (true)
            {
                int input = HandleInput(Enum.GetValues(typeof(PotionType)).Length);
                if (input == 0) return false;

                if (input < 0 || input >= Enum.GetValues(typeof(PotionType)).Length)
                {
                    BattleDisplay.DisplayInvalidInput();  // 잘못된 입력에 대해 처리
                    continue;
                }

                PotionType selectedType = (PotionType)(input - 1);
                if (_player.inventory.potion.GetPotionCount(selectedType) > 0)
                {
                    _player.inventory.potion.UsePotion(selectedType);
                    BattleDisplay.DisplayPotionEffect(selectedType);
                    return true;
                }
                else
                {
                    BattleDisplay.DisplayNoPotions();
                    continue;
                }
            }
        }
    }
}
