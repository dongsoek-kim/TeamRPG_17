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

        public void UsePotion(PosionNum posionNum)
        {
            switch (posionNum)
            {
                case PosionNum.Health:
                    {
                        GameManager.Instance.player.hp = +health;
                        GameManager.Instance.player.hp = (GameManager.Instance.player.hp >= 100) ? GameManager.Instance.player.hp = 100 : GameManager.Instance.player.hp;
                        break;
                    }
                case PosionNum.str:
                    //GameManager.Instance.player.str = +str;
                    break;
                case PosionNum.dex:
                    //GameManager.Instance.player.dex = +str;
                    break;
                case PosionNum.inte:
                    //GameManager.Instance.player.inte = +str;
                    break;
                case PosionNum.luk:
                    //GameManager.Instance.player.luk = +str;
                    break;
                default: break;

            }
        }
    }

}

public enum PosionNum
{
    Health,
    str,
    dex,
    inte,
    luk
}
