using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class GameManager : Singleton<GameManager>
    {
        private Scene currentScene;
        private Scene[] scenes;
        public Player player=new Player();
        public GameManager() 
        {
            int sceneCount = Enum.GetValues(typeof(SceneName)).Length;
            scenes = new Scene[sceneCount];

            scenes[0] = new UserCreateScene();
            scenes[1] = new LobbyScene();
            scenes[2] = new StatScene();
            scenes[3] = new InventoryScene();
            scenes[4] = new ShopScene();
            scenes[5] = new DungeonScene();
            scenes[6] = new SleepScene();
            //scenes[7]
            //seenes[8]
            ChangeScene(SceneName.UserCreateScene);
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
