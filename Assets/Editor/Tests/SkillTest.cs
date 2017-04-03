using NUnit.Framework;
using System.Collections.Generic;

public class SkillTest
{

    [Test]
    public void SkillCountDictionaryEquate()
    {
        CollectionAssert.AreEquivalent(
            Skill.SkillCountDictionaryEquate(Skills()),
            Expect()
        );
    }

    private List<Skill> Skills()
    {
        var skills = new List<Skill>();
        skills.Add(new Skill(1, 1));
        skills.Add(new Skill(1, 1));
        skills.Add(new Skill(2, 1));
        skills.Add(new Skill(2, 2));
        skills.Add(new Skill(2, 2));
        skills.Add(new Skill(2, 2));
        return skills;
    }

    private Dictionary<Skill, int> Expect()
    {
        return new Dictionary<Skill, int>
        {
            {new Skill(1, 1), 2},
            {new Skill(2, 1), 1},
            {new Skill(2, 2), 3}
        };
    }

}
