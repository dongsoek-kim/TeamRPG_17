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
        QusetScene,
        DataScene
    }

    public enum ItemName
    {
        누가쓰다버린목검,
        어디서본듯한검,
        평범한쇠검,
        고급대장간의검,
        누가쓰다버린단검,
        어디서본듯한단검,
        평범한쇠단검,
        유명한도둑의단검,
        누가쓰다버린나무지팡이,
        어디서본듯한지팡이,
        평범한나무지팡이,
        상급위저드의지팡이,
        가죽모자,
        녹슨철모,
        양동이,
        낡은도둑의복면,
        종이모자,
        누더기모자,
        누더기,
        가죽튜닉,
        체인메일,
        비단로브,
        청동흉갑,
        강철갑옷,
        붕대,
        가죽장갑,
        노가다목장갑,
        털장갑,
        브레이슬렛,
        마력팔찌,
        구멍난양말,
        꼬질한신발,
        가죽샌들,
        장화,
        크록스,
        인기많은컨버스,
        엑스칼리버,
        글라디우스,
        카타르,
        은의발톱,
        미스릴스태프,
        전설의레전드윙봉,
        해골헬멧,
        불타는비전모자,
        그림자망토후드,
        은빛체인메일,
        붉은망토,
        서릿발의코트,
        고양이손,
        판금장갑,
        틱스미실드,
        아이젠,
        전투화,
        완벽한운동화,
        나히니르,
        연장점검,
        아이쿠손이미끄러졌검,
        장미칼,
        부채,
        도넛을먹었더니단검,
        이럴수가마법하나를빠트렸어,
        미스틸테인세계수의가지,
        세상을구하는빛,
        거인의투구,
        전설의사냥꾼모자,
        광휘의마법왕관,
        용의심장,
        빛과어둠의그림자,
        영원의파편,
        촌장의야망,
        오우거파워건틀렛,
        마법사의손,
        매조련사의장갑,
        깃털신발,
        마력이깃든신발,
        힘이깃든신발,
        광전사의신발
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
    public enum PotionType
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

    public enum Grade
    { 
        Common,
        Rare,
        Unique
    }
}
