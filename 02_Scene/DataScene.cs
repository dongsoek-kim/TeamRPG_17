using System.ComponentModel.Design;

namespace TeamRPG_17
{
    public class DataScene : Scene
    {

        private bool onSave;
        private bool onLoad;
        private bool onDelete;
        private Player[] datas;

        public DataScene()
        {
            onSave = false;
            onLoad = false;
            onDelete = false;

            datas = new Player[3]; // 데이터 크기
            for(int i = 0; i<datas.Length; i++)
                datas[i] = DataManager.LoadPlayerData(i+1);
            
        }

        public override void Update()
        {
            if (onSave)
                DataSave();
            else if (onLoad)
                DataLoad();
            else if (onDelete)
                DataDelete();
            else
                DataMain();
        }

        private void DataMain()
        {
            Console.Clear();
            Console.WriteLine("데이터 관리");
            Console.WriteLine("데이터를 관리해주는 씬입니다.\n");
            Console.WriteLine("─────────────────────────");
            DataList(); // 현재 들어있는 데이터 출력
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("1. 저장하기");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine("3. 삭제하기\n");
            Console.WriteLine("0. 나가기");
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
            }
        }

        private void DataSave()
        {
            Console.Clear();
            Console.WriteLine("데이터 저장하기");
            Console.WriteLine("원하시는 공간을 지정해주세요.\n");
            Console.WriteLine("─────────────────────────");

            DataList(); // 현재 들어있는 데이터 출력

            Console.WriteLine("─────────────────────────");
            Console.WriteLine("\n0. 나가기");
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    onSave = false;
                    break;

                default:
                    if(intCommand - 1 < datas.Length)
                    {
                        if (CheckSlot(intCommand, "현재 슬롯에 데이터가 남아있습니다.\n정말로 저장을 하시겠습니까?") == 1) { }
                            //DataManager.SaveGameData(GameManager.Instance.player, GameManager.Instance.player.inventory, );
                    }
                    break;
            }
        }

        private void DataLoad()
        {
            Console.Clear();
            Console.WriteLine("데이터 불러오기");
            Console.WriteLine("원하시는 공간을 지정해주세요.\n");
            Console.WriteLine("─────────────────────────");

            DataList();

            Console.WriteLine("─────────────────────────");
            Console.WriteLine("\n0. 나가기");
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    onLoad = false;
                    break;

                default:
                    if (intCommand - 1 < datas.Length)
                    {
                        if (datas[intCommand - 1].name != null)
                            DataManager.LoadData(intCommand);
                    }
                    break;
            }
        }

        private void DataDelete()
        {
            Console.Clear();
            Console.WriteLine("데이터 삭제하기");
            Console.WriteLine("삭제하실 공간을 지정해주세요.\n");
            Console.WriteLine("─────────────────────────");

            DataList();

            Console.WriteLine("─────────────────────────");
            Console.WriteLine("\n0. 나가기");
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    onDelete = false;
                    break;

                default:
                    if (intCommand - 1 < datas.Length)
                    {
                        if (CheckSlot(intCommand, "현재 슬롯에 데이터가 남아있습니다.\n정말로 삭제를 하시겠습니까?") == 1)
                        {
                            Player p = datas[intCommand];
                            p.name = "";
                            //DataManager.SaveGameData(p, new Inventory(), ); 덮어씌우기
                        }
                    }
                    break;
            }
        }

        private void DataList()
        {
            for (int i = 0; i < datas.Length; i++)
            {
                string str = "비어있음";
                if (datas[i].name != null)
                    str = string.Format("Lv {0:D2}. {1}", datas[i].level, datas[i].name);
                Console.WriteLine($"[{str}]"); // Data[] 안에 원소를 가지고와서 플레이어의 "Lv 0. playerName" 출력 예정 NULL이면 비어있음
            }
        }

        private int CheckSlot(int slotNumber, string checkMessage)
        {
            string pName = datas[slotNumber].name;
            int checkCommand = 1;
            if(pName != null)
            {
                Console.Clear();
                Console.WriteLine(checkMessage);
                Console.WriteLine("1. 확인\n");
                Console.WriteLine("0. 취소");

                while (true)
                {
                    if (GameManager.Instance.SceneInputCommand(out checkCommand))
                    {
                        if (checkCommand >= 0 || checkCommand <= 1)
                            break;
                    }

                }
            }
            return checkCommand;
        }
    }
}
