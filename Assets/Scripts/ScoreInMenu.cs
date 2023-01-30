using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class ScoreInMenu 
{
    static bool CheckLeader(string name)
    {
        for (int i = 1; i <= 10; i++)
        {
            if (name == PlayerPrefs.GetString($"Top{i}[name]"))
            {
                return true;
            }
        }
        return false;
    }
    public static void SetLeader(string name, int score)
    {
        if (CheckLeader(name))
        {
            for (int i = 1; i <= 10; i++)
            {
                if (name == PlayerPrefs.GetString($"Top{i}[name]"))
                {
                    if (score > PlayerPrefs.GetInt($"Top{i}[score]"))
                    {
                        PlayerPrefs.SetInt($"Top{i}[score]", score);
                        PlayerPrefs.Save();
                        SortLeaders(CreateListLeaders());
                        return;
                    }
                }
            }
        }
        else
        {
            for (int i = 1; i <= 10; i++)
            {
                if (score > PlayerPrefs.GetInt($"Top{i}[score]"))
                {
                    SortLeaders(ChangeLeader(name, score));
                    return;
                }
            }
        }
    }

    static List<Leader> ChangeLeader(string name, int score)
    {
        Leader leader = new Leader();
        leader.name = name;
        leader.score = score;

        List<Leader> leaders = CreateListLeaders();
        leaders.Add(leader);
        return leaders;
    }
    static void SortLeaders(List<Leader> leaders)
    {
        Leader temp = new Leader();
        for (int i = 0; i < leaders.Count; i++)
        {
            for (int j = i + 1; j < leaders.Count; j++)
            {
                if (leaders[i].score > leaders[j].score)
                {
                    temp = leaders[i];
                    leaders[i] = leaders[j];
                    leaders[j] = temp;
                }
            }
        }
        if(leaders.Count > 10)
        {
            int topPosition = 1;
            for (int i = leaders.Count - 1; topPosition <= 10; i--)
            {
                PlayerPrefs.SetString($"Top{topPosition}[name]", $"{leaders[i].name}"); PlayerPrefs.SetInt($"Top{topPosition}[score]", leaders[i].score);
                PlayerPrefs.Save();
                topPosition++;
            }
        }
        else
        {
            for (int i = 0; i < leaders.Count; i++)
            {
                PlayerPrefs.SetString($"Top{10 - i}[name]", $"{leaders[i].name}"); PlayerPrefs.SetInt($"Top{10 - i}[score]", leaders[i].score);
                PlayerPrefs.Save();
            }
        }
        
    }

    static List<Leader> CreateListLeaders()
    {
        List<Leader> list = new List<Leader>();

        Leader leader = new Leader();
        for (int i = 1; i <= 10; i++)
        {
            leader.name = PlayerPrefs.GetString($"Top{i}[name]");
            leader.score = PlayerPrefs.GetInt($"Top{i}[score]");
            list.Add(leader);
        }
        return list;
    }
    struct Leader
    {
        public string name;
        public int score;
    }
}
