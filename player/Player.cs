using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Player
    {
        public string name      { get; set; }
        public JobType job      { get; set; }

        public int level        { get; private set; }
        public int exp          { get; private set; }
        public int str          { get; private set; }
        public int dex          { get; private set; }
        public int inte         { get; private set; }
        public int luk          { get; private set; }
        public float damage     { get; private set; }
        public float defense    { get; private set; }
        public int hp           { get; set; }
        public int gold         { get; set; }

        public Inventory inventory { get; private set; }

        private Random random = new Random();

        public Player()
        {
            level = 1;
            exp = 0;
            // 스탯(str 등)은 추후에 확정하여 변경예정
            str = 0;
            dex = 0;
            inte = 0;
            luk = 0;
            damage = 100;
            defense = 5;
            hp = 100;
            gold = 1000;

            inventory = new Inventory();
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

        // 총 데미지 계산식 (스탯/직업별)
        public int TotalDamage 
        {
            get
            {
                float totalDamage = damage;
                float bonusDamage = 0;

                switch (job)
                {
                    case JobType.Warrior:
                        damage = (((float)str * 1.5f) + ((float)dex * 0.5f) + ((float)inte * 0.1f));
                        break;

                    case JobType.Rogue:
                        damage = (((float)str * 0.5f) + ((float)dex * 1.5f) + ((float)inte * 0.1f));
                        break;

                    case JobType.Wizard:
                        damage = (((float)str * 0.1f) + ((float)dex * 0.5f) + ((float)inte * 1.5f));
                        break;
                }
                return (int)(totalDamage + bonusDamage);
            }
         }

        public int LuckyDamage()
        {
            float finalDamage = TotalDamage;

            // 회피확률
            float dodge = luk * 0.5f / 100f;  // Luk 10이면 5% 확률 회피
            if (random.NextDouble() < dodge)
            {
                return 0;  // 회피하면 데미지를 0으로 설정
            }
            // 크리티컬
            float critical = luk * 0.3f / 100f; // Luk 10이면 3% 확률 회피
            if (random.NextDouble() < critical)
            {
                finalDamage *= 1.5f;
            }

            return (int)finalDamage;
        }
    }
}
