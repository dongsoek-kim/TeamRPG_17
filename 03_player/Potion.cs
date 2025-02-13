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

        /// <summary>
        /// 생성자. 체력 포션 3개를 가지고 시작
        /// </summary>
        public Potion()
        {
            potionCount[0] = 3;
        }

        /// <summary>
        /// 포션 사용 함수
        /// </summary>
        /// <param name="potionType"></param>
        public void UsePotion(PotionType potionType)
        {
            switch (potionType)
            {
                case PotionType.Health:
                    {
                        GameManager.Instance.player.hp += health;
                        GameManager.Instance.player.hp = (GameManager.Instance.player.hp >= 100) ? GameManager.Instance.player.hp = 100 : GameManager.Instance.player.hp;
                        potionCount[(int)PotionType.Health]--;
                        break;
                    }
                case PotionType.str:
                    {
                        GameManager.Instance.player.usePotion(PotionType.str);
                        potionCount[(int)PotionType.str]--;
                        break;
                    }
                case PotionType.dex:
                    {
                        GameManager.Instance.player.usePotion(PotionType.dex);
                        potionCount[(int)PotionType.dex]--;
                        break;
                    }
                case PotionType.inte:
                    {
                        GameManager.Instance.player.usePotion(PotionType.inte);
                        potionCount[(int)PotionType.inte]--;
                        break;
                    }
                case PotionType.luk:
                    {
                        GameManager.Instance.player.usePotion(PotionType.luk);
                        potionCount[(int)PotionType.luk]--;
                        break;
                    }
                default: break;

            }
        }

        /// <summary>
        /// 포션을 획득하는 함수
        /// </summary>
        /// <param name="potionType"> 포션 종류 </param>
        /// <param name="_getNum"> 획득량 </param>
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

        /// <summary>
        /// 포션 개수 반환
        /// </summary>
        /// <param name="potionType"></param>
        /// <returns></returns>
        public int GetPotionCount(PotionType potionType)
        {
            return potionCount[(int)potionType];
        }
    }
}
