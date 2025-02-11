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
            new ("돌진", 3, 160, 50, SkillType.SingleTarget, JobType.Warrior),
            new ("지진", 4, 175, 70, SkillType.AllTarget, JobType.Warrior),
            new ("분노의 일격", 5, 190, 80, SkillType.SingleTarget, JobType.Warrior),
            new ("대지 가르기", 6, 205, 95, SkillType.AllTarget, JobType.Warrior),
            new ("광폭렬참", 7, 225, 110, SkillType.SingleTarget, JobType.Warrior),
            new ("지옥의 맹타", 8, 250, 130, SkillType.AllTarget, JobType.Warrior),
            new ("파멸의 일격", 9, 270, 150, SkillType.SingleTarget, JobType.Warrior),
            new ("영웅천하", 10, 300, 180, SkillType.AllTarget, JobType.Warrior),


            new ("표창던지기", 1, 115, 20, SkillType.SingleTarget, JobType.Rogue),
            new ("베놈 익스플로전", 2, 135, 45, SkillType.AllTarget, JobType.Rogue),
            new ("그림자 일격", 3, 150, 40, SkillType.SingleTarget, JobType.Rogue),
            new ("독안개의 춤", 4, 165, 60, SkillType.AllTarget, JobType.Rogue),
            new ("암살자의 칼날", 5, 180, 75, SkillType.SingleTarget, JobType.Rogue),
            new ("그림자 폭발", 6, 200, 90, SkillType.AllTarget, JobType.Rogue),
            new ("맹독의 이빨", 7, 220, 105, SkillType.SingleTarget, JobType.Rogue),
            new ("폭풍 검무", 8, 240, 125, SkillType.AllTarget, JobType.Rogue),
            new ("죽음의 일격", 9, 260, 145, SkillType.SingleTarget, JobType.Rogue),
            new ("어둠의 심판", 10, 290, 170, SkillType.AllTarget, JobType.Rogue),

            new ("매직 미사일", 1, 145, 50, SkillType.SingleTarget, JobType.Wizard),
            new ("파이어 볼", 2, 180, 75, SkillType.AllTarget, JobType.Wizard),
            new ("아이스 스피어", 3, 190, 70, SkillType.SingleTarget, JobType.Wizard),
            new ("썬더 스톰", 4, 210, 90, SkillType.AllTarget, JobType.Wizard),
            new ("마나 블라스트", 5, 230, 100, SkillType.SingleTarget, JobType.Wizard),
            new ("인페르노 스톰", 6, 250, 120, SkillType.AllTarget, JobType.Wizard),
            new ("아크 빔", 7, 270, 140, SkillType.SingleTarget, JobType.Wizard),
            new ("메테오 스웜", 8, 300, 160, SkillType.AllTarget, JobType.Wizard),
            new ("시간 왜곡", 9, 330, 180, SkillType.SingleTarget, JobType.Wizard),
            new ("아포칼립스", 10, 370, 210, SkillType.AllTarget, JobType.Wizard),

        };
        public List<Skill> GetSkillList(Player player) // jobType에 따라서 스킬을 가져오는 메서드
        {
            return skills.Where(s => s.JobType == player.job && s.Level <= player.level).ToList(); ;
        }
    }
}
