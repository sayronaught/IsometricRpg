using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

public class _LoadAllXmlMods : MonoBehaviour
{
    public ModStatsContainer allStats;
    public ModSkillsContainer allSkills;

    public ModStatsContainer getStats()
    {
        var xmlData = @"<StatSystem><Stats></Stats></StatSystem>";
        var list = ModStatsContainer.LoadFromText(xmlData);
        var dirPaths = Directory.GetFiles(Application.dataPath + "/../Mods/", "Stats_*.xml", SearchOption.AllDirectories);
        for (var i = 0; i < dirPaths.Length; i++)
        {
            var StatSystem = ModStatsContainer.Load(dirPaths[i]);
            list.Stats.AddRange(StatSystem.Stats);
        }
        return list;
    }

    public ModSkillsContainer getSkills()
    {
        var xmlData = @"<SkillSystem><Skills></Skills></SkillSystem>";
        var list = ModSkillsContainer.LoadFromText(xmlData);
        var dirPaths = Directory.GetFiles(Application.dataPath + "/../Mods/", "Skills_*.xml", SearchOption.AllDirectories);
        for (var i = 0; i < dirPaths.Length; i++)
        {
            var SkillSystem = ModSkillsContainer.Load(dirPaths[i]);
            list.Skills.AddRange(SkillSystem.Skills);
        }
        return list;
    }

    // Use this for initialization
    void Awake()
    {
        this.allStats = this.getStats();
        Debug.Log("Stats loaded " + this.allStats.Stats.Count);
        this.allSkills = this.getSkills();
        Debug.Log("Skills loaded " + this.allSkills.Skills.Count);
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
