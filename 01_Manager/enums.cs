using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public enum JobType
    {
        Warrior,
        Rogue,
        Wizard
    }

    public enum SceneName
    {
        UserCreateScene,
        LobbyScene,
        StatScene,
        InventoryScene,
        ShopScene,
        DungeonScene,
        SleepScene,
        TownMoveScene,
        DataScene,
        QusetScene
    }

    public enum ItemName
    {
        TrashArmor,
        NoviceArmor,
        IronArmor,
        SpartaArmor,
        WoodenStick,
        OldSword,
        BronzeAxe,
        SpartaSpear
    }

    public enum ItemType
    {
        Armor,
        Weapon
    }

    public enum TownName
    {
        Elinia,
        Hannesys,
        CunningCity
    }
    public enum PotionNum
    {
        Health,
        str,
        dex,
        inte,
        luk
    }
    public enum EquipSlot
    {
        Head,
        Body,
        Arm,
        Foot,
        Weapon
    }
}
