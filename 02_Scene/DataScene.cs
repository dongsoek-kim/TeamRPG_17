using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;

namespace TeamRPG_17
{
    public class DataScene : Scene
    {

        private bool onSave;
        private bool onLoad;
        private bool onDelete;
        private bool onSelect;
        private Player[] datas;

        public DataScene()
        {
            onSave = false;
            onLoad = false;
            onDelete = false;
            onSelect = true;

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
            Console.WriteLine("캐릭터 선택");
            Console.WriteLine("사용하실 캐릭터를 선택해주세요.\n");
            Console.WriteLine("─────────────────────────");
            DataList(); // 현재 들어있는 데이터 출력
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("1. 새게임");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine("3. 삭제하기\n");
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 1:
                    onSave = true;
                    onSelect = false;
                    break;
                case 2:
                    onLoad = true;
                    onSelect = false;
                    break;
                case 3:
                    onDelete = true;
                    onSelect = false;
                    break;
            }
        }

        private void DataSave()
        {
            Console.Clear();
            Console.WriteLine("데이터 생성하기");
            Console.WriteLine("원하시는 공간을 지정해주세요.\n");
            Console.WriteLine("─────────────────────────");

            DataList(); // 현재 들어있는 데이터 출력

            Console.WriteLine("─────────────────────────");
            Console.WriteLine("\n0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            // 1 2 3
            switch (intCommand)
            {
                case 0:
                    onSave = false;
                    onSelect = true;
                    break;

                default:
                    int slotIndex = intCommand - 1; // 선택된 슬롯 배열 index
                    bool isSlot = true;

                    // 범위를 벗어난 입력 ( 0 미만 , datas.Length 이상 )
                    if (slotIndex < 0 || slotIndex >= datas.Length)
                        break;

                    isSlot = CheckSlot(intCommand, "현재 슬롯에 데이터가 남아있습니다.\n정말로 저장을 하시겠습니까?", out int innerCommand);   // 슬롯 덮어쓰기 여부 확인

                    // 슬롯O & 덮어쓰기O  or  슬롯X
                    if((isSlot && innerCommand == 1) || isSlot == false)
                    {
                        onSave = false;
                        onSelect = true;

                        GameManager.Instance.ChangeScene(SceneName.UserCreateScene);
                    }
                    break;
            }
        }

        private void DataLoad()
        {
            Console.Clear();
            Console.WriteLine("캐릭터 선택하기");
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
                    onSelect = true;
                    break;

                default:
                    if (intCommand - 1 < datas.Length)
                    {
                        DataManager.currentSlot = intCommand;
                        if (datas[intCommand - 1] != null && datas[intCommand - 1].name != null)
                        {
                            DataManager.LoadData(intCommand);
                            Console.WriteLine("불러오기 완료!!");
                            Console.ReadKey(true);
                            GameManager.Instance.ChangeScene(SceneName.LobbyScene);
                        }
                        else
                        {
                            GameManager.Instance.ChangeScene(SceneName.UserCreateScene);
                        }
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
                    onSelect = true;
                    break;

                default:
                    if (intCommand - 1 < datas.Length)
                    {
                        int innerCommand = 0;
                        bool isSlot = CheckSlot(intCommand, "현재 슬롯에 데이터가 남아있습니다.\n정말로 삭제를 하시겠습니까?", out innerCommand);
                        
                        if (isSlot && innerCommand == 1)
                        {              
                            Player p = datas[intCommand - 1];
                            p.name = null;
                            DataManager.SaveGameData(p, new Inventory(), intCommand);
                            Console.WriteLine("삭제완료!!");
                            SyncSlot(); // 다시 불러오기
                            Console.ReadKey(true);
                        }
                        else
                        {
                            Console.WriteLine("해당 슬롯은 비어있습니다.");
                            Console.ReadKey(true);
                        }
                    }
                    break;
            }
        }

        private void DataList()
        {
            for (int i = 0; i < datas.Length; i++)
            {
                string strNumber = !onSelect ? (i + 1).ToString() : "";
                string str = "비어있음";   

                if (datas[i] != null && datas[i].name != null)
                    str = string.Format("Lv {0:D2}. {1}", datas[i].level, datas[i].name);
                Console.WriteLine($"{strNumber} [{str}]"); // Data[] 안에 원소를 가지고와서 플레이어의 "Lv 0. playerName" 출력 예정 NULL이면 비어있음
            }
        }

        private bool CheckSlot(int slotNumber, string checkMessage, out int _innerCommand)
        {
            string pName = "";

            if (datas[slotNumber - 1] != null)
                pName = datas[slotNumber - 1].name;
            else
                pName = null;

            if(pName != null)
            {
                Console.Clear();
                Console.WriteLine(checkMessage);
                Console.WriteLine("1. 확인\n");
                Console.WriteLine("0. 취소");

                while (true)
                {
                    if (GameManager.Instance.SceneInputCommand(out _innerCommand))
                    {
                        if (_innerCommand >= 0 || _innerCommand <= 1)
                            break;
                    }

                }
                return true;
            }

            // 제어문을 무시하면 innerCommand = 0;
            _innerCommand = 0;
            return false;
        }

        private void SyncSlot()
        {
            for(int i = 0; i<datas.Length; i++)
                datas[i] = DataManager.LoadPlayerData(i+1);
        }
    }
}
