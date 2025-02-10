using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Potion
    {
        public int health = 30;
        public int str = 5;
        public int dex = 5;
        public int inte = 5;
        public int luk = 5;
        public int[] potionCount = new int[Enum.GetValues(typeof(PotionType)).Length];

        //public Potion()
        //{
        //    int PotionType = Enum.GetValues(typeof(PotionType)).Length;
        //}
        public void UsePotion(PotionType potionType)
        {
            switch (potionType)
            {
                case PotionType.Health:
                    {
                        GameManager.Instance.player.hp = +health;
                        GameManager.Instance.player.hp = (GameManager.Instance.player.hp >= 100) ? GameManager.Instance.player.hp = 100 : GameManager.Instance.player.hp;
                        potionCount[(int)PotionType.Health]--;
                        break;
                    }
                case PotionType.str:
                    {//GameManager.Instance.player.str = +str;
                        potionCount[(int)PotionType.str]--;
                        break;
                    }
                case PotionType.dex:
                    {//GameManager.Instance.player.str = +str;
                        potionCount[(int)PotionType.dex]--;
                        break;
                    }
                case PotionType.inte:
                    {//GameManager.Instance.player.str = +str;
                        potionCount[(int)PotionType.inte]--;
                        break;
                    }
                case PotionType.luk:
                    {//GameManager.Instance.player.str = +str;
                        potionCount[(int)PotionType.luk]--;
                        break;
                    }
                default: break;

            }
        }
        public void GetPotion(PotionType potionType, int _getNum)
        {
            switch (potionType)
            {
                case PotionType.Health:
                    {
                        potionCount[(int)PotionType.Health] += _getNum;
                        break;
                    }
                case PotionType.str:
                    {
                        potionCount[(int)PotionType.str] += _getNum;
                        break;
                    }
                case PotionType.dex:
                    {
                        potionCount[(int)PotionType.dex] += _getNum;
                        break;
                    }
                case PotionType.inte:
                    {
                        potionCount[(int)PotionType.inte] += _getNum;
                        break;
                    }
                case PotionType.luk:
                    {
                        potionCount[(int)PotionType.luk] += _getNum;
                        break;
                    }
                default: break;

            }
        }

        public int GetPotionCount(PotionType potionType)
        {
            return potionCount[(int)potionType];
        }
    }
}
