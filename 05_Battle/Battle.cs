using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeamRPG_17
{
    public class Battle
    {
        private static Battle _instance;
        public static Battle Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Battle();
                return _instance;
            }
        }

        private readonly Player _player;
        public BattleScene _battleUI;
        public BattleSystem _battleSystem;
        public BattleActionHandler _actionHandler;
        public TargetingSystem _targetingSystem;
        private List<Monster> _monsters;
        private Dungeon _currentDungeon;


        public Battle()
        {
            _player = GameManager.Instance.player;
            List<Skill> availableSkills = SkillManager.Instance.GetSkillList(_player);

            _battleUI = new BattleScene(availableSkills);
            _battleSystem = new BattleSystem(_player);
            _targetingSystem = new TargetingSystem(_battleUI, HandleInput);
            _actionHandler = new BattleActionHandler(_player, _battleUI, _battleSystem, _targetingSystem, availableSkills, HandleInput);
        }

        public void StartBattle(Dungeon dungeon) // 전투 개시(던전 정보, 몬스터 리스트 받아옴) (플레이어 정보랑 몬스터 리스트 BattleScene에 전달)
        {
            _currentDungeon = dungeon;
            _monsters = MonsterManager.Instance.RandomMonsterSpawn(dungeon);
            _battleUI.UpdateBattleState(_player, _monsters);
            InBattle();
        }

        private void InBattle() // 전투 흐름
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
                    BattleScene.DisplayMonsterAttack(monster, _player, damage, _player.hp);
                }

                if (_player.hp <= 0)
                {
                    BattleResult(false);
                }
            }
        }

        private void BattleResult(bool isWin) // 전투 결과
        {
            _battleUI.DisplayBattleResult(isWin, _monsters, _player, _currentDungeon.Level);

            if (isWin) // 승리 시엔 보상 적용
            {
                BattleReward reward = new BattleReward(_currentDungeon.Level, _monsters.Count);
                reward.ApplyReward(_player);
            }

            BattleScene.PrintContinuePrompt();
        }

        private int HandleInput(int maxOption) // 선택지 입력 일괄 처리
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int selection) && selection >= 0 && selection <= maxOption)
                {
                    return selection;
                }
                BattleScene.DisplayInvalidInput(); // 잘못된 입력 메시지 출력
                Console.Write(">> "); // 다시 입력 프롬프트 표시 (엔터 없이 입력 가능)
            }
        }
    }
}