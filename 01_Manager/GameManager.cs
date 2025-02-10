namespace TeamRPG_17
{
    public class GameManager : Singleton<GameManager>
    {
        private Scene currentScene;
        private Scene[] scenes;
        public Player player=new Player();
        public Town[] towns;
        public Town currentTown;
        public GameManager() 
        {
            towns = new Town[Enum.GetValues(typeof(SceneName)).Length];
            towns[(int)TownName.Elinia] = new Town(TownName.Elinia, "엘리니아","엘리니아 마을이다.",1 ,0, 20);
            towns[(int)TownName.Hannesys] = new Town(TownName.Hannesys, "헤네시스", "헤네시스 마을이다.",1 ,20, 15);
            towns[(int)TownName.CunningCity] = new Town(TownName.CunningCity, "커닝시티", "커닝시티 마을이다.",1,35, 15);
            currentTown = towns[0];
            

            int sceneCount = Enum.GetValues(typeof(SceneName)).Length;
            scenes = new Scene[sceneCount];
            

            scenes[0] = new UserCreateScene();
            scenes[1] = new LobbyScene();
            scenes[2] = new StatScene();
            scenes[3] = new InventoryScene();
            scenes[4] = new ShopScene();
            scenes[5] = new DungeonScene();
            scenes[6] = new SleepScene();
            scenes[7] = new TownMoveScene();
            scenes[8] = new QuestScene();
            scenes[9] = new DataScene();
            //seenes[8]
            ChangeScene(SceneName.UserCreateScene);
            DataManager.LoadQuestManagerData(1);
        }

        /// <summary>
        /// 현재 씬 Update
        /// </summary>
        public void SceneUpdate()
        {
            currentScene.Update();
        }

        /// <summary>
        /// 씬 전환 함수
        /// </summary>
        public void ChangeScene(SceneName _name)
        {
            currentScene = scenes[(int)_name];
        }

        public bool SceneInputCommand(out int intCommand)
        {
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");

            Console.Write("> ");
            string command = Console.ReadLine();

            // 숫자입력 확인
            if (int.TryParse(command, out intCommand))
                return true;

            // 숫자가 아닌것이 입력됨
            return false;
        }
    }
}
