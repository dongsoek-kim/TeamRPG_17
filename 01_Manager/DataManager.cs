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
      private static int nowSlot;
      static public Player LoadPlayerData(int _input)
        {
            nowSlot = _input;
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
        static public QuestManager LoadQuestManagerData(int _input)
        {
            string relativePath = @"..\..\..\Json\";
            string jsonFile = "QuestManagerData";  // JSON 파일명
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
                    QuestManager questManager = JsonConvert.DeserializeObject<QuestManager>(json); 
                    return questManager;
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
        public static void SaveGameData(Player player, Inventory inventory, QuestManager questManager)
        {


            string relativePath = @"..\..\..\Json\";
            string PlayerDatajsonPath;
            string InventoryDatajsonPath;
            string QusetManagerDatajsonPath;

            // 세이브 슬롯에 맞는 파일 경로 설정
            switch (nowSlot)
            {
                case 1:
                    PlayerDatajsonPath = "PlayerDataSlot1.json";
                    InventoryDatajsonPath = "InventoryDataSlot1.json";
                    QusetManagerDatajsonPath = "QuestManagerDataSlot1.json";
                    break;
                case 2:
                    PlayerDatajsonPath = "PlayerDataSlot2.json";
                    InventoryDatajsonPath = "InventoryDataSlot2.json";
                    QusetManagerDatajsonPath = "QuestManagerDataSlot2.json";
                    break;
                case 3:
                    PlayerDatajsonPath = "PlayerDataSlot3.json";
                    InventoryDatajsonPath = "InventoryDataSlot3.json";
                    QusetManagerDatajsonPath = "QuestManagerDataSlot3.json";
                    break;
                default:
                    PlayerDatajsonPath = "PlayerDataSlot1.json";
                    InventoryDatajsonPath = "InventoryDataSlot1.json";
                    QusetManagerDatajsonPath = "QuestManagerDataSlot1.json";
                    break;
            }

            // 파일 경로 생성
            string PlayerDatajson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, PlayerDatajsonPath));
            string InventoryDatajson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, InventoryDatajsonPath));
            string QusetManagerDatajson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, QusetManagerDatajsonPath));

            try
            {
                // 데이터 직렬화
                string PlayerData = JsonConvert.SerializeObject(player, Formatting.Indented);
                string InventoryData = JsonConvert.SerializeObject(inventory, Formatting.Indented);
                string QuestManagerData = JsonConvert.SerializeObject(questManager, Formatting.Indented);

                // 데이터 저장
                File.WriteAllText(PlayerDatajson, PlayerData);
                File.WriteAllText(InventoryDatajson, InventoryData);
                File.WriteAllText(QusetManagerDatajson, QuestManagerData);

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
