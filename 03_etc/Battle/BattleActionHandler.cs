using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class BattleActionHandler
    {
        private readonly Player _player;
        private readonly BattleScene _battleUI;
        private readonly BattleSystem _battleSystem;
        private readonly TargetingSystem _targetingSystem;
        private readonly List<Skill> _availableSkills;
        private readonly Func<int, int> HandleInput; // BattleScene의 HandleInput 메서드를 사용

        public BattleActionHandler(Player player, BattleScene battleUI, BattleSystem battleSystem, TargetingSystem targetingSystem, List<Skill> availableSkills, Func<int, int> handleInput)
        {
            _player = player;
            _battleUI = battleUI;
            _battleSystem = battleSystem;
            _targetingSystem = targetingSystem;
            _availableSkills = availableSkills;
            HandleInput = handleInput;
        }

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
                        BattleScene.DisplayInvalidInput();  // 잘못된 입력이 들어오면 InvalidInput 메시지를 출력하고 다시 반복
                        continue;
                }
            }
        }

        private bool HandleAttack(List<Monster> monsters) // 공격 메뉴(SelectTarget()에서 몬스터를 선택해줘야됨)
        {
            Monster target = _targetingSystem.SelectTarget(monsters);
            if (target == null) return false;

            _battleSystem.PlayerAttack(target);
            return true;
        }

        private bool HandleSkill(List<Monster> monsters) // 스킬 선택
        {
            while (true)
            {
                _battleUI.DisplaySkillList();

                int input = HandleInput(_availableSkills.Count);
                if (input == 0) return false;

                if (input < 0 || input > _availableSkills.Count)
                {
                    BattleScene.DisplayInvalidInput();  // 잘못된 입력에 대한 처리
                    continue;
                }

                Skill selectedSkill = _availableSkills[input - 1];
                if (_player.mp < selectedSkill.MpCost) // 마나 부족 시 다시
                {
                    BattleScene.DisplayNotEnoughMP();
                    continue;
                }

                List<Monster> targets = _targetingSystem.GetTargetsForSkill(selectedSkill, monsters);
                if (targets.Count == 0) return false;

                _battleSystem.UseSkill(selectedSkill, targets);
                return true;
            }
        }

        private bool HandlePotion()
        {
            while (true)
            {
                _battleUI.DisplayPotionList();

                int input = HandleInput(Enum.GetValues(typeof(PotionType)).Length);
                if (input == 0) return false;

                if (input < 0 || input >= Enum.GetValues(typeof(PotionType)).Length)
                {
                    BattleScene.DisplayInvalidInput();  // 잘못된 입력에 대해 처리
                    continue;
                }

                PotionType selectedType = (PotionType)(input - 1);
                if (_player.inventory.potion.GetPotionCount(selectedType) > 0)
                {
                    _player.inventory.potion.UsePotion(selectedType);
                    BattleScene.DisplayPotionEffect(selectedType);
                    return true;
                }
                else
                {
                    BattleScene.DisplayNoPotions();
                }
            }
        }
    }
}
