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



        /// <summary>
        /// 스킬 생성자. 스킬 이름, 레벨, 데미지, 마나 소모량, 스킬 타입, 직업 타입을 받아 생성
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <param name="damage"></param>
        /// <param name="mpCost"></param>
        /// <param name="skillType"></param>
        /// <param name="jobType"></param>
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
