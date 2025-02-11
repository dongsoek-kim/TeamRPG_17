using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeamRPG_17
{
    public class BattleScene
    {
        private static BattleScene _instance;
        public static BattleScene Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BattleScene();
                return _instance;
            }
        }

        private readonly Player _player;
        public BattleSceneUI _battleUI;
        public BattleSystem _battleSystem;
        public BattleActionHandler _actionHandler;
        public TargetingSystem _targetingSystem;
        private List<Monster> _monsters;
        private Dungeon _currentDungeon;


        public BattleScene()
        {
            _player = GameManager.Instance.player;
            List<Skill> availableSkills = SkillManager.Instance.GetSkillList(_player);

            _battleUI = new BattleSceneUI(availableSkills);
            _battleSystem = new BattleSystem(_player);
            _targetingSystem = new TargetingSystem(_battleUI, HandleInput);
            _actionHandler = new BattleActionHandler(_player, _battleUI, _battleSystem, _targetingSystem, availableSkills, HandleInput);
        }

        public void StartBattle(Dungeon dungeon)
        {
            _currentDungeon = dungeon;
            _monsters = MonsterManager.Instance.RandomMonsterSpawn(dungeon);
            _battleUI.UpdateBattleState(_player, _monsters);
            InBattle();
        }

        private void InBattle()
        {
            while (_battleSystem.IsBattleActive(_monsters))
            {
                _battleUI.DisplayBattleStatus();
                _actionHandler.HandlePlayerTurn(_monsters);

                if (_monsters.All(m => m.IsDead))
                {
                    BattleResult(true);
                    return;
                }

                foreach (var monster in _monsters.Where(m => !m.IsDead))
                {
                    int damage = _battleSystem.ProcessMonsterAttack(monster);
                    _battleUI.DisplayMonsterAttack(monster, _player, damage, _player.hp);
                }

                if (_player.hp <= 0)
                {
                    BattleResult(false);
                }
            }
        }

        private void BattleResult(bool isWin)
        {
            _battleUI.DisplayBattleResult(isWin, _monsters, _player, _currentDungeon.Level);

            if (isWin)
            {
                BattleReward reward = new BattleReward(_currentDungeon.Level, _monsters.Count);
                reward.ApplyReward(_player);
            }

            _battleUI.PrintContinuePrompt();
        }

        private int HandleInput(int maxOption)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int selection) && selection >= 0 && selection <= maxOption)
                {
                    return selection;
                }
                _battleUI.DisplayInvalidInput();
                _battleUI.PrintContinuePrompt();
            }
        }
    }
}