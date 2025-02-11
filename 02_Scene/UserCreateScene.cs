using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class UserCreateScene : Scene
    {
        private bool isNameCheck;
        private bool isJobCheck;

        public UserCreateScene()
        {
            isNameCheck = false;
            isJobCheck = false;
        }
        
        public override void Update()
        {
            CreateName();   // 닉네임 생성
            SelectJob();    // 직업 선택

            // 닉네임과 직업 전부 완료되었을때 LobbyScene으로 이동
            if (isNameCheck && isJobCheck)
                GameManager.Instance.ChangeScene(SceneName.LobbyScene);
            
        }

        private void CreateName()
        {
            while(!isNameCheck)
            {
                Console.Clear();

                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 이름을 설정해주세요.");
                Console.Write("\n> ");
                string name = Console.ReadLine();
                if (name.Length < 1)
                    continue;

                Console.WriteLine($"\n입력하신 이름은 {name} 입니다.\n");
                Console.WriteLine("1. 저장");
                Console.WriteLine("2. 취소");
                Console.Write("\n> ");
                string command = Console.ReadLine();

                // 입력 확인
                if (!int.TryParse(command, out int intCommand))
                    continue;

                if(intCommand == 1)
                {
                    isNameCheck = true;
                    GameManager.Instance.player.name = name;
                }
            }
        }

        private void SelectJob()
        {
            while(!isJobCheck)
            {
                Console.Clear();

                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 직업을 선택해주세요.\n");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 도적");
                Console.WriteLine("3. 마법사");

                Console.Write("\n> ");
                string command = Console.ReadLine();

                // 입력 확인
                if (!int.TryParse(command, out int intCommand))
                    continue;

                switch(intCommand) 
                {
                    case 1:
                        isJobCheck = true;
                        GameManager.Instance.player.job = JobType.Warrior;
                        GameManager.Instance.player.SetJob(JobType.Warrior);
                        break;
                    case 2:
                        GameManager.Instance.player.job = JobType.Rogue;
                        GameManager.Instance.player.SetJob(JobType.Rogue);
                        isJobCheck = true;
                        break;
                    case 3:
                        GameManager.Instance.player.job = JobType.Wizard;
                        GameManager.Instance.player.SetJob(JobType.Wizard);
                        isJobCheck = true;
                        break;
                }
            }
        }
    }
}
