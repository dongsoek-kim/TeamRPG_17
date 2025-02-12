using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Skill
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Damage { get; private set; }
        public int MpCost { get; private set; }
        public SkillType SkillType { get; private set; }
        public JobType JobType { get; private set; }




        public Skill(string name, int level, int damage, int mpCost, SkillType skillType, JobType jobType)
        {
            Name = name;
            Level = level;
            Damage = damage;
            MpCost = mpCost;
            SkillType = skillType;
            JobType = jobType;
        }
    }
}
