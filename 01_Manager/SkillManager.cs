using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class SkillManager : Singleton<SkillManager>
    {
        private List<Skill> skills = new List<Skill>
        {
            new ("강타", 1, 125, 35, SkillType.SingleTarget, JobType.Warrior),
            new ("회전 베기", 2, 145, 55, SkillType.AllTarget, JobType.Warrior),

            new ("표창던지기", 1, 115, 20, SkillType.SingleTarget, JobType.Rogue),
            new ("베놈 익스플로전", 2, 135, 45, SkillType.AllTarget, JobType.Rogue),

            new ("매직 미사일", 1, 145, 50, SkillType.SingleTarget, JobType.Wizard),
            new ("파이어 볼", 2, 180, 75, SkillType.AllTarget, JobType.Wizard),
        };
        public List<Skill> GetSkillList(Player player) // jobType에 따라서 스킬을 가져오는 메서드
        {
            return skills.Where(s => s.JobType == player.job && s.Level <= player.level).ToList(); ;
        }
    }
}
