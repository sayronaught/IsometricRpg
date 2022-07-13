using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

[XmlRoot("SkillSystem")]
public class ModSkillsContainer
{

    [XmlArray("Skills"), XmlArrayItem("Skill")]
    public List<ModSkills> Skills;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(ModSkillsContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static ModSkillsContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(ModSkillsContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as ModSkillsContainer;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static ModSkillsContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(ModSkillsContainer));
        return serializer.Deserialize(new StringReader(text)) as ModSkillsContainer;
    }

    public ModSkillsContainer Clone()
    {
        ModSkillsContainer other = new ModSkillsContainer();
        other.Skills = new List<ModSkills>();
        for (var i = 0; i < this.Skills.Count; i++)
        {
            other.Skills.Add(this.Skills[i].Clone());
        }
        return other;
    }

    public ModSkills getSkillByName(string name)
    {
        for (var i = 0; i < this.Skills.Count; i++)
        {
            if (this.Skills[i].Name == name)
                return this.Skills[i];
        }
        return null;
    }

    public int rollSkill(string name)
    {
        var skill = this.getSkillByName(name);
        skill.xpGain();
        return Random.Range(1, skill.Amount);
    }
}
