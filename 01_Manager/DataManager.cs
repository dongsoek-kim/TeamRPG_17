using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace TeamRPG_17
{
    static class DataManager
    {
        /// <summary>
        /// 현재 데이터 파일 슬롯
        /// </summary>
      static public int currentSlot = 1;
        /// <summary>
        /// 플레이어데이터 Json 파일 로드
        /// </summary>
        /// <param name="_input">몇번째 슬롯인지 판별</param>
        /// <returns></returns>
      static public Player LoadPlayerData(int _input)
        {
            string relativePath = @"..\..\..\Json\";
            string jsonFile = $"PlayerDataSlot{_input}.json"; 

            string jsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, jsonFile));
            try
            {
                if (File.Exists(jsonPath))
                {
                    string json = File.ReadAllText(jsonPath);
                    var settings = new JsonSerializerSettings
                    {
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };
                    settings.Converters.Add(new ItemConverter());
                    Player player = JsonConvert.DeserializeObject<Player>(json, settings);
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
        /// <summary>
        /// 퀘스트 Json파일 로드
        /// </summary>
        /// <param name="_input">몇번째 슬롯인지 판별</param>
        static public void LoadQuestManagerData(int _input)
        {
            string relativePath = @"..\..\..\Json\";                           
            string itemQuestjsonFile = $"itemQuestDataSlot{_input}.json";      
            string killQuestjsonFile = $"killQuestDataSlot{_input}.json";    

            string itemQuestjsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, itemQuestjsonFile));
            string killQuestjsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, killQuestjsonFile));

            try
            {
                
                if (File.Exists(itemQuestjsonPath) && File.Exists(killQuestjsonPath))
                {
                    string iQuestJson = File.ReadAllText(itemQuestjsonPath);
                    string kQuestJson = File.ReadAllText(killQuestjsonPath);
                    QuestManager.Instance.LoadQuest(iQuestJson, kQuestJson);
                    return;
                }
                else
                {                   
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
        /// <summary>
        /// 각각의 로드 함수들을 한번에 부르는 메서드
        /// </summary>
        /// <param name="_input">몇번째 슬롯인지 판별</param>
        static public void LoadData(int _input)
        {
            GameManager.Instance.player = LoadPlayerData(_input);
            LoadQuestManagerData(_input);
        }
        /// <summary>
        /// json파일에 저장하는 메서드
        /// </summary>
        /// <param name="player"></param>
        /// <param name="inventory"></param>
        /// <param name="userInput"></param>
        public static void SaveGameData(Player player, Inventory inventory, int userInput = 1)
        {
            string relativePath = @"..\..\..\Json\";
            string PlayerDatajsonPath = $"PlayerDataSlot{userInput}.json";
            string itemQuestjsonPath = $"itemQuestDataSlot{userInput}.json";
            string killQuestjsonPath = $"killQuestDataSlot{userInput}.json";

            
            string PlayerDatajson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, PlayerDatajsonPath));
            string itemQuestJson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, itemQuestjsonPath));
            string killQuestJson = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, killQuestjsonPath));

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());

            try
            {                
                string PlayerData = JsonConvert.SerializeObject(player, Formatting.Indented);
                string itemQuestData = JsonConvert.SerializeObject(QuestManager.Instance.itemQuests, Formatting.Indented);
                string killQuestData = JsonConvert.SerializeObject(QuestManager.Instance.killQuests, Formatting.Indented);
                
                File.WriteAllText(PlayerDatajson, PlayerData);
                File.WriteAllText(itemQuestJson, itemQuestData);
                File.WriteAllText(killQuestJson, killQuestData);

                Console.WriteLine("Game data saved successfully!");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error saving game data: {ex.Message}");
            }
        }
        /// <summary>
        /// ItemData를 로드하는 메서드
        /// </summary>
        public static void LoadItemsData()
        {
            string relativePath = @"..\..\..\Json\";
            string jsonFile = "ItemData.json"; 
            string jsonPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, jsonFile));
            try
            {
                if (File.Exists(jsonPath))
                {
                    string json = File.ReadAllText(jsonPath);
                   
                    var rawItems = JsonConvert.DeserializeObject<dynamic[]>(json);
                    for (int i = 0; i < rawItems.Length; i++)
                    {
                        var rawItem = rawItems[i];
                        ItemType itemType = (ItemType)Enum.Parse(typeof(ItemType), rawItem.itemType.ToString());

                       
                        if (itemType == ItemType.Armor)
                        {
                            int defense = int.Parse(rawItem.Defense.ToString());
                            int str = int.Parse(rawItem.str.ToString());
                            int dex = int.Parse(rawItem.dex.ToString());
                            int inte = int.Parse(rawItem.inte.ToString());
                            int luk = int.Parse(rawItem.luk.ToString());
                            Grade grade = (Grade)Enum.Parse(typeof(Grade), rawItem.Grade.ToString());  // 대소문자 구분 안함
                            EquipSlot equipSlot = (EquipSlot)Enum.Parse(typeof(EquipSlot), rawItem.EquipSlot.ToString());
                            ItemManager.Instance.items[i] = new Armor(
                                rawItem.itemName.ToString(),
                                rawItem.itemDescription.ToString(),
                                defense,    
                                str,
                                dex,
                                inte,
                                luk,
                                equipSlot,
                                grade
                            );
                        }
                        else if (itemType == ItemType.Weapon)
                        {
                            int damage = int.Parse(rawItem.damage.ToString());
                            int str = int.Parse(rawItem.str.ToString());
                            int dex = int.Parse(rawItem.dex.ToString());
                            int inte = int.Parse(rawItem.inte.ToString());
                            int luk = int.Parse(rawItem.luk.ToString());
                            Grade grade = (Grade)Enum.Parse(typeof(Grade), rawItem.Grade.ToString());  
                            EquipSlot equipSlot = (EquipSlot)Enum.Parse(typeof(EquipSlot), rawItem.EquipSlot.ToString());
                           
                            ItemManager.Instance.items[i] = new Weapon(
                                rawItem.itemName.ToString(),
                                rawItem.itemDescription.ToString(),
                                damage,   
                                str,
                                dex,
                                inte,
                                luk,
                                equipSlot,
                                grade
                            );
                        }
                    }
                }
                else
                {
                }
            }
            catch { }
        }

    }
    public class ItemConverter : JsonConverter
    {/// <summary>
    /// json컨버터
    /// </summary>
    /// <param name="objectType"></param>
    /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(Item).IsAssignableFrom(objectType);
        }
        /// <summary>
        /// 아이템데이터를 올바르게 직렬화하는 메서드
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            JObject jObject = JObject.Load(reader);
            string itemType = jObject["itemType"]?.Value<string>();

            Item item;
            switch (itemType)
            {
                case "0":
                    {
                        string _name = jObject["itemName"]?.Value<string>() ?? "";
                        string _description = jObject["itemDescription"]?.Value<string>() ?? "";
                        int _str = jObject["str"]?.Value<int>() ?? 0;
                        int _dex = jObject["dex"]?.Value<int>() ?? 0;
                        int _inte = jObject["inte"]?.Value<int>() ?? 0;
                        int _luk = jObject["luk"]?.Value<int>() ?? 0;
                        int _defense = jObject["defense"]?.Value<int>() ?? 0;
                        string equipSlotString = jObject["EquipSlot"]?.Value<string>();
                        EquipSlot _equipslot = default(EquipSlot);
                        if (!string.IsNullOrEmpty(equipSlotString))
                        {
                            if (!Enum.TryParse<EquipSlot>(equipSlotString, true, out _equipslot))
                            {                                
                                _equipslot = default(EquipSlot);
                            }
                        }
                        string gradeString = jObject["Grade"]?.Value<string>();
                        Grade grade = default(Grade);
                        if (!string.IsNullOrEmpty(gradeString))
                        {
                            if (!Enum.TryParse<Grade>(gradeString, true, out grade))
                            {                                
                                grade = default(Grade);
                            }
                        }
                        item = new Armor(_name, _description, _defense, _str, _dex, _inte, _luk, _equipslot, grade);
                        break;
                    }
                case "1":
                    {
                        string _name = jObject["itemName"]?.Value<string>() ?? "";
                        string _description = jObject["itemDescription"]?.Value<string>() ?? "";
                        int _str = jObject["str"]?.Value<int>() ?? 0;
                        int _dex = jObject["dex"]?.Value<int>() ?? 0;
                        int _inte = jObject["inte"]?.Value<int>() ?? 0;
                        int _luk = jObject["luk"]?.Value<int>() ?? 0;
                        int _damage = jObject["damage"]?.Value<int>() ?? 0;
                        string equipSlotString = jObject["EquipSlot"]?.Value<string>();
                        EquipSlot _equipslot = default(EquipSlot);
                        if (!string.IsNullOrEmpty(equipSlotString))
                        {
                            if (!Enum.TryParse<EquipSlot>(equipSlotString, true, out _equipslot))
                            {
                                _equipslot = default(EquipSlot);
                            }
                        }
                        string gradeString = jObject["Grade"]?.Value<string>();
                        Grade grade = default(Grade);
                        if (!string.IsNullOrEmpty(gradeString))
                        {
                            if (!Enum.TryParse<Grade>(gradeString, true, out grade))
                            {
                                grade = default(Grade);
                            }
                        }
                        item = new Weapon(_name,_description,_damage,_str,_dex,_inte,_luk,_equipslot,grade);
                        break;
                    }

                default:
                    throw new Exception("알 수 없는 itemType: " + itemType);
            }

            serializer.Populate(jObject.CreateReader(), item);
            return item;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}

