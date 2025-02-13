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
        public BattleDisplay _battleUI;
        public BattleSystem _battleSystem;
        public BattleActionHandler _actionHandler;
        public TargetingSystem _targetingSystem;
        private List<Monster> _monsters;
        private Dungeon _currentDungeon;
        private int[] _initialPotionCount= new int[4];
        /// <summary>
        /// Battle 클래스의 생성자. 싱글턴 패턴을 사용하여 하나의 인스턴스만 유지
        /// 전투 관련 UI, 시스템, 타겟팅, 액션 핸들러를 초기화
        /// </summary>
        public Battle()
        {
            _player = GameManager.Instance.player;
            List<Skill> availableSkills = SkillManager.Instance.GetSkillList(_player);

            _battleUI = new BattleDisplay(availableSkills);
            _battleSystem = new BattleSystem(_player);
            _targetingSystem = new TargetingSystem(_battleUI, HandleInput);
            _actionHandler = new BattleActionHandler(_player, _battleUI, _battleSystem, _targetingSystem, availableSkills, HandleInput);
        }

        /// <summary>
        /// 전투를 시작하는 함수. 던전 정보를 받아 몬스터 리스트를 생성하고 전투 UI를 갱신함.
        /// </summary>
        /// <param name="dungeon">전투가 일어날 던전 정보</param>"
        public void StartBattle(Dungeon dungeon) 
        {
            int i = 0;
            _currentDungeon = dungeon;
            _monsters = MonsterManager.Instance.RandomMonsterSpawn(dungeon);
            _battleUI.UpdateBattleState(_player, _monsters);
            foreach (int potionCount in GameManager.Instance.player.inventory.potion.potionCount)
            {            
                if(i>0)
                {
                    _initialPotionCount[i-1] = potionCount;
                }
                i++;
            }   
            InBattle();
        }

        /// <summary>
        /// 전투의 흐름을 관리하는 함수. 플레이어의 턴과 몬스터의 턴을 진행하며 전투가 종료될 때까지 반복함.
        /// </summary>
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
                    BattleDisplay.DisplayMonsterAttack(monster, _player, damage, _player.hp);
                }

                if (_player.hp <= 0)
                {
                    BattleResult(false);
                }
            }
        }

        /// <summary>
        /// 전투 결과를 처리하는 함수. 승패에 따라 보상을 지급하거나 패배 시 처리함.
        /// </summary>
        /// <param name="isWin">전투 승리 여부</param>"
        private void BattleResult(bool isWin)
        {
            _battleUI.DisplayBattleResult(isWin, _monsters, _player, _currentDungeon.Level);
            GameManager.Instance.player.undoUesedPotion(_initialPotionCount);
            if (isWin) // 승리 시엔 보상 적용
            {
                BattleReward reward = new BattleReward(_currentDungeon.Level, _monsters.Count);
                reward.ApplyReward(_player);
            }

            BattleDisplay.PrintContinuePrompt();
        }

        /// <summary>
        /// 플레이어의 입력을 처리하는 함수. 주어진 최대 선택지 범위 내에서 올바른 입력을 받을 때까지 반복함.
        /// </summary>
        /// <param name="maxOption"> 최대 선택지 범위 </param>"
        /// <returns> 유효한 사용자 입력 값 </returns>
        private int HandleInput(int maxOption)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int selection) && selection >= 0 && selection <= maxOption)
                {
                    return selection;
                }
                BattleDisplay.DisplayInvalidInput();
                Console.Write(">> ");
            }
        }
    }
}