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

        public void UsePotion(PotionNum potionNum)
        {
            switch (potionNum)
            {
                case PotionNum.Health:
                    {
                        GameManager.Instance.player.hp = +health;
                        GameManager.Instance.player.hp = (GameManager.Instance.player.hp >= 100) ? GameManager.Instance.player.hp = 100 : GameManager.Instance.player.hp;
                        break;
                    }
                case PotionNum.str:
                    //GameManager.Instance.player.str = +str;
                    break;
                case PotionNum.dex:
                    //GameManager.Instance.player.dex = +str;
                    break;
                case PotionNum.inte:
                    //GameManager.Instance.player.inte = +str;
                    break;
                case PotionNum.luk:
                    //GameManager.Instance.player.luk = +str;
                    break;
                default: break;

            }
        }
    }

}

public enum PotionNum
{
    Health,
    str,
    dex,
    inte,
    luk
}
