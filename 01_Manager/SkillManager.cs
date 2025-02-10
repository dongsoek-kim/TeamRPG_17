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
            new ("강타", 1, 125, 35, JobType.Warrior),
            new ("회전 베기", 2, 145, 55, JobType.Warrior),

            new ("표창던지기", 1, 115, 20, JobType.Rogue),
            new ("베놈 바이트", 2, 135, 45, JobType.Rogue),

            new ("매직 미사일", 1, 145, 50, JobType.Wizard),
            new ("파이어 볼", 2, 180, 75, JobType.Wizard),
        };
        public List<Skill> GetSkillList(JobType jobType) // jobType에 따라서 스킬을 가져오는 메서드
        {
            List<Skill> skillList = skills.Where(s => s.JobType == jobType).ToList();
            return skillList;
        }
    }
}
