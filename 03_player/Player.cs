using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Player
    {
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public string name      { get; set; }
        public JobType job      { get; set; }
        public TownName nowTown {  get; set; }
        public int level        { get; private set; }
        public int exp          { get; private set; }
        public int str          { get; private set; }
        public int dex          { get; private set; }
        public int inte         { get; private set; }
        public int luk          { get; private set; }
        public float damage     { get; private set; }
        public float baseDamage { get; private set; }
        public float defense    { get; private set; }
        public int hp           { get; set; }
        public int hpMax        { get; set; }
        public int mp           { get; set; }
        public int mpMax        { get; set; }
        public int gold         { get; set; }

        public Inventory inventory { get; set; }

        private Random random = new Random();

        public Player()
        {
            level = 1;
            exp = 0;

            damage = 20;
            defense = 5;

            hpMax = 100;
            mpMax = 100;
            hp = hpMax;
            mp = mpMax;
            nowTown = TownName.Elinia;
            gold = 1000;

            inventory = new Inventory();
        }

        /// <summary>
        /// 플레이어 직업설정 및 직업별 초기 스탯 지정
        /// </summary>
        /// <param name="_job"></param>
        public void SetJob(JobType _job)
        {
            job = _job;

            switch (job)
            {
                case JobType.Warrior:
                    str = 4;
                    dex = 2;
                    inte = 2;
                    luk = 2;
                    break;
                case JobType.Rogue:
                    str = 2;
                    dex = 4;
                    inte = 2;
                    luk = 2;
                    break;
                case JobType.Wizard:
                    str = 2;
                    dex = 2;
                    inte = 4;
                    luk = 2;
                    break;
            }
        }

        [JsonConstructor]
        public Player(int level, int exp, int str, int dex, int inte, int luk,float damage,float defense,int hpMax, int mpMax, int gold,TownName nowTown,Inventory inventory)
        {
            this.level = level;
            this.exp = exp;
            this.str = str;
            this.dex = dex;
            this.inte = inte;
            this.luk = luk;
            this.hpMax = hpMax;
            this.mpMax = mpMax;
            this.damage = damage;
            this.defense = defense;
            this.nowTown = nowTown;
            this.gold = gold;
            this.inventory = inventory ?? new Inventory();
        }
        // 던전 클리어 후 경험치 획득 함수
        public void AddExp(int addExp)
        {
            exp++;
            if (exp == level)
            {
                level++;
                exp = 0;

                str += 1;
                dex += 1;
                inte += 1;
                luk += 1;
                damage += 0.5f;
                defense += 1;
                mp = mpMax;
            }
        }

        /// <summary>
        /// 현재 착용중인 방어구의 방어력 스탯 반환 함수
        /// </summary>
        public int GetArmorStat()
        {   
            if (inventory.equipedArmor == null)
                return 0;
            else
                return inventory.SumDefense();
        }

        /// <summary>
        /// 현재 착용중인 무기의 공격력 스탯 반환 함수
        /// </summary>
        public int GetWeaponStat()
        {
            if (inventory.equipedWeapon == null)
                return 0;
            else
                return inventory.equipedWeapon.damage;
        }

        /// <summary>
        /// 총 공격력/데미지 계산 속성
        /// </summary>
        public int TotalDamage 
        {
            get
            {
                var itemStats = GameManager.Instance.player.inventory.ItemStat();

                float baseDamage = damage;
                float bonusDamage = 0;

                switch (job)
                {
                    case JobType.Warrior:
                        bonusDamage += (((float)itemStats.sumStr * 1.5f) + ((float)itemStats.sumDex * 0.5f) + ((float)itemStats.sumInte * 0.1f));
                        break;

                    case JobType.Rogue:
                        bonusDamage += (((float)itemStats.sumStr * 0.5f) + ((float)itemStats.sumDex * 1.5f) + ((float)itemStats.sumInte * 0.1f));
                        break;

                    case JobType.Wizard:
                        bonusDamage += (((float)itemStats.sumStr * 0.1f) + ((float)itemStats.sumDex * 0.5f) + ((float)itemStats.sumInte * 1.5f));
                        break;
                }

                return (int)(baseDamage + bonusDamage + GameManager.Instance.player.inventory.WeaponStat());
            }
         }


        /// <summary>
        /// 총 방어력 계산 속성
        /// </summary>
        public int TotalDefens
        {
            get
            {
                float baseDefense = defense;
                int itemDefense = GameManager.Instance.player.inventory.ArmorStat();

                return (int)(baseDefense + itemDefense);
            }
        }

        /// <summary>
        /// 행운 스탯에 따른 크리티컬 확률/데미지 함수
        /// </summary>
        /// <returns></returns>
        public int LuckyDamage()
        {
            float finalDamage = TotalDamage;

            // 크리티컬
            float critical = luk * 0.3f / 100f;
            if (random.NextDouble() < critical)
            {
                finalDamage *= 1.5f;
            }

            return (int)finalDamage;
        }

    }
}
