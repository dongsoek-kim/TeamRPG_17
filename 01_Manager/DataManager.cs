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
            string jsonFile = "InventoryData.json";  // JSON 파일명
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
        static public QuestManager LoadQuestManagerData()
        {
            string relativePath = @"..\..\..\Json\";
            string jsonFile = "QuestManagerData.json";  // JSON 파일명
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
            string jsonPath;
            switch (nowSlot)
            {
                case 1:
                    jsonPath = "PlayerDataSlot1.json";
                    break;
                case 2:
                    jsonPath = "PlayerDataSlot2.json";
                    break;
                case 3:
                    jsonPath = "PlayerDataSlot3.json";
                    break;
                default:
                    jsonPath = "PlayerDataSlot1.json";
                    break;
            }
            string PlyerDatajsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, jsonPath));
            string InventoryDatajsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, "InventoryData.json"));
            string QusetManagerDatajsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, "QuestManagerData.json"));
    
            try// JSON 파일로 저장
            { 
                string PlayerDatajson = JsonConvert.SerializeObject(player, Formatting.Indented);
                File.WriteAllText(PlyerDatajsonPath, PlayerDatajson); 
                string InventoryDatajson = JsonConvert.SerializeObject(inventory, Formatting.Indented);
                File.WriteAllText(InventoryDatajsonPath, InventoryDatajson); 
                string QuestManagerData = JsonConvert.SerializeObject(questManager, Formatting.Indented);
                File.WriteAllText(QusetManagerDatajsonPath, QuestManagerData); 
            }
            catch (Exception ex)
            {
                // 예외 발생 시 오류 메시지 출력
                Console.WriteLine($"Error saving game data: {ex.Message}");
            }
        }
    }
}
