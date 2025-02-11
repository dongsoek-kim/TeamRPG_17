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
        private readonly BattleSceneUI _battleUI;
        private readonly BattleSystem _battleSystem;
        private readonly TargetingSystem _targetingSystem;
        private readonly List<Skill> _availableSkills;
        private readonly Func<int, int> HandleInput; // BattleScene의 HandleInput 메서드를 사용

        public BattleActionHandler(
            Player player,
            BattleSceneUI battleUI,
            BattleSystem battleSystem,
            TargetingSystem targetingSystem,
            List<Skill> availableSkills,
            Func<int, int> handleInput)
        {
            _player = player;
            _battleUI = battleUI;
            _battleSystem = battleSystem;
            _targetingSystem = targetingSystem;
            _availableSkills = availableSkills;
            HandleInput = handleInput;
        }

        public void HandlePlayerTurn(List<Monster> monsters)
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
                }
            }
        }

        private bool HandleAttack(List<Monster> monsters)
        {
            Monster target = _targetingSystem.SelectTarget(monsters);
            if (target == null) return false;

            _battleSystem.PlayerAttack(target);
            return true;
        }

        private bool HandleSkill(List<Monster> monsters)
        {
            while (true)
            {
                _battleUI.DisplaySkillList();

                int input = HandleInput(_availableSkills.Count);
                if (input == 0) return false;

                Skill selectedSkill = _availableSkills[input - 1];
                if (_player.mp < selectedSkill.MpCost)
                {
                    _battleUI.DisplayNotEnoughMP();
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

                PotionType selectedType = (PotionType)(input - 1);
                if (_player.inventory.potion.GetPotionCount(selectedType) > 0)
                {
                    _player.inventory.potion.UsePotion(selectedType);
                    return true;
                }
                else
                {
                    _battleUI.DisplayNoPotions();
                }
            }
        }
    }
}
