using System;
using System.Collections.Generic;

public class Skill : IEquatable<Skill>
{
    public int Code { get; private set; }
    public int Level { get; private set; }

    public Skill(int code, int level)
    {
        Code = code;
        Level = level;
    }

    public override int GetHashCode()
    {
        //return Tuple.Create(Code, Level).GetHashCode();
        return Code.GetHashCode() ^ Level.GetHashCode();
    }

    bool IEquatable<Skill>.Equals(Skill other)
    {
        if (other == null)
        {
            return false;
        }

        return Code == other.Code && Level == other.Level;
    }

    public static IDictionary<Skill, int> SkillCountDictionaryEquate(List<Skill> skills)
    {
        var testCountDictionary = new Dictionary<Skill, int>();
        foreach (var skill in skills)
        {
            if (!testCountDictionary.ContainsKey(skill))
            {
                testCountDictionary[skill] = 0;
            }
            testCountDictionary[skill]++;
        }
        return testCountDictionary;
    }
}
