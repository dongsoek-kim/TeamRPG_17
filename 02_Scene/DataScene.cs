using System.ComponentModel.Design;

namespace TeamRPG_17
{
    public class DataScene : Scene
    {

        private bool onSave;
        private bool onLoad;
        private bool onDelete;
        private bool onSelect;

        public DataScene()
        {
            onSave = false;
            onLoad = false;
            onDelete = false;
            onSelect = false;
        }

        public override void Update()
        {
            if(onSave)
            {

            }
            else if (onLoad)
            {
                
            }
            else if(onDelete)
            {

            }
            else if(onSelect)
            {

            }
            else
                LoadMain();
        }

        private void LoadMain()
        {
            Console.Clear();
            Console.WriteLine("데이터 관리");
            Console.WriteLine("데이터를 관리해주는 씬입니다.\n");
            Console.WriteLine("─────────────────────────");
            for (int i = 0; i < 3; i++) // Data[] 배열의 공간의 길이를 입력
            {
                Console.WriteLine($"[비어있음]"); // Data[] 안에 원소를 가지고와서 플레이어의 "Lv 0. playerName" 출력 예정 NULL이면 비어있음
            }
            Console.WriteLine("1. 저장하기");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine("3. 삭제하기");
            Console.WriteLine("4. 선택하기\n");
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("─────────────────────────");
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    GameManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;
                case 1:
                    onSave = true;
                    break;
                case 2:
                    onLoad = true;
                    break;
                case 3:
                    onDelete = true;
                    break;
                case 4:
                    onSelect = true;
                    break;
            }
        }
    }
}
