using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TeamRPG_17
{
    static class DataManager//Player,Inventory,Quest의 데이터 저장,불러오기
    {
      static public Player LoadPlayerData(int _input)
        {
            string relativePath = @"..\..\..\Json\";
            string jsonFile = "PlayerData";  // JSON 파일명
            string slot;
            switch(_input)
            {
                case 1:
                    slot = "Slot1.json";
                    break;
                case 2:
                    slot = "Slot2.json";
                    break;
                case 3:
                    slot = "Slot3.json";
                    break;
                default:
                    slot = "Slot1.json";
                    break;
            }
            jsonFile =jsonFile + slot;
            string jsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, jsonFile));
            try
            {
                if (File.Exists(jsonPath))
                {
                    string json = File.ReadAllText(jsonPath);
                    Player player = JsonConvert.DeserializeObject<Player>(json);  
                    return player;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        static public Inventory LoadInventoryData(int _input)
        {
            string relativePath = @"..\..\..\Json\";
            string jsonFile = "InventoryData";  // JSON 파일명
            string slot;
            switch (_input)
            {
                case 1:
                    slot = "Slot1.json";
                    break;
                case 2:
                    slot = "Slot2.json";
                    break;
                case 3:
                    slot = "Slot3.json";
                    break;
                default:
                    slot = "Slot1.json";
                    break;
            }
            jsonFile = jsonFile + slot;

            string jsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, jsonFile));
            try
            {
                if (File.Exists(jsonPath))
                {
                    string json = File.ReadAllText(jsonPath);
                    Inventory inventory = JsonConvert.DeserializeObject<Inventory>(json); 
                    return inventory;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        static public void LoadQuestManagerData(int _input)
        {
            string relativePath = @"..\..\..\Json\";                            // 파일 위치
            string itemQuestjsonFile = $"itemQuestDataSlot{_input}.json";       // 템퀘스트 JSON 파일명
            string killQuestjsonFile = $"killQuestDataSlot{_input}.json";     // 킬퀘스트 JSON 파일명

            string itemQuestjsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, itemQuestjsonFile));
            string killQuestjsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, killQuestjsonFile));

            try
            {
                // _input슬롯의 퀘스트 데이터 있으면
                if (File.Exists(itemQuestjsonPath) && File.Exists(killQuestjsonPath))
                {
                    string iQuestJson = File.ReadAllText(itemQuestjsonPath);
                    string kQuestJson = File.ReadAllText(killQuestjsonPath);
                    QuestManager.Instance.LoadQuest(iQuestJson, kQuestJson);
                    return;
                }
                else
                {
                    // 없으면 baseItemQuest.json / baseKillQuest.json 으로 만들기
                    itemQuestjsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, "baseItemQuest.json"));
                    killQuestjsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, "baseKillQuest.json"));
                    string iQuestJson = File.ReadAllText(itemQuestjsonPath);
                    string kQuestJson = File.ReadAllText(killQuestjsonPath);
                    QuestManager.Instance.LoadQuest(iQuestJson, kQuestJson);
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        static public void LoadData(int _input)
        {
            GameManager.Instance.player=LoadPlayerData(_input);
            //GameManager.Instance.player.inventory=LoadInventoryData(_input);
            LoadQuestManagerData(_input);
        }
        public static void SaveGameData(Player player, Inventory inventory, int userInput = 1)
        {
            string relativePath = @"..\..\..\Json\";
            string PlayerDatajsonPath;
            //string InventoryDatajsonPath;
            string itemQuestjsonPath;
            string killQuestjsonPath;

            // 세이브 슬롯에 맞는 파일 경로 설정
            switch (userInput)
            {
                case 1:
                    PlayerDatajsonPath = "PlayerDataSlot1.json";
                    //InventoryDatajsonPath = "InventoryDataSlot1.json";
                    itemQuestjsonPath = "itemQuestDataSlot1.json";
                    killQuestjsonPath = "killQuestDataSlot1.json";
                    break;
                case 2:
                    PlayerDatajsonPath = "PlayerDataSlot2.json";
                    //InventoryDatajsonPath = "InventoryDataSlot2.json";
                    itemQuestjsonPath = "itemQuestDataSlot2.json";
                    killQuestjsonPath = "killQuestDataSlot2.json";
                    break;
                case 3:
                    PlayerDatajsonPath = "PlayerDataSlot3.json";
                    //InventoryDatajsonPath = "InventoryDataSlot3.json";
                    itemQuestjsonPath = "itemQuestDataSlot3.json";
                    killQuestjsonPath = "killQuestDataSlot4.json";
                    break;
                default:
                    PlayerDatajsonPath = "PlayerDataSlot1.json";
                    //InventoryDatajsonPath = "InventoryDataSlot1.json";
                    itemQuestjsonPath = "itemQuestDataSlot1.json";
                    killQuestjsonPath = "killQuestDataSlot1.json";
                    break;
            }

            // 파일 경로 생성
            string PlayerDatajson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, PlayerDatajsonPath));
            //string InventoryDatajson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, InventoryDatajsonPath));
            string itemQuestJson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, itemQuestjsonPath));
            string killQuestJson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, killQuestjsonPath));

            try
            {
                // 데이터 직렬화
                string PlayerData = JsonConvert.SerializeObject(player, Formatting.Indented);
                string InventoryData = JsonConvert.SerializeObject(inventory, Formatting.Indented);
                string itemQuestData = JsonConvert.SerializeObject(QuestManager.Instance.itemQuests, Formatting.Indented);
                string killQuestData = JsonConvert.SerializeObject(QuestManager.Instance.killQuests, Formatting.Indented);

                // 데이터 저장
                File.WriteAllText(PlayerDatajson, PlayerData);
                //File.WriteAllText(InventoryDatajson, InventoryData);
                File.WriteAllText(itemQuestJson, itemQuestData);
                File.WriteAllText(killQuestJson, killQuestData);

                Console.WriteLine("Game data saved successfully!");
            }
            catch (Exception ex)
            {
                // 예외 발생 시 오류 메시지 출력
                Console.WriteLine($"Error saving game data: {ex.Message}");
            }
        }

    }
}
