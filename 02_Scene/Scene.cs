using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public abstract class Scene
    {
        public abstract void Update();

        public virtual void Exit()
        {
            if (GameManager.Instance.player.name != null)
                DataManager.SaveGameData(GameManager.Instance.player, GameManager.Instance.player.inventory, DataManager.currentSlot);
        }
    }
}
