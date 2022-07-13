using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

[XmlRoot("StatSystem")]
public class ModStatsContainer
{

    [XmlArray("Stats"), XmlArrayItem("Stat")]
    public List<ModStats> Stats;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(ModStatsContainer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static ModStatsContainer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(ModStatsContainer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as ModStatsContainer;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static ModStatsContainer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(ModStatsContainer));
        return serializer.Deserialize(new StringReader(text)) as ModStatsContainer;
    }

    public ModStatsContainer Clone()
    {
        ModStatsContainer other = new ModStatsContainer();
        other.Stats = new List<ModStats>();
        for (var i = 0; i < this.Stats.Count; i++)
        {
            other.Stats.Add(this.Stats[i].Clone());
        }
        return other;
    }

    public ModStats getStatByName(string name)
    {
        for (var i = 0; i < this.Stats.Count; i++)
        {
            if (this.Stats[i].Name == name)
                return this.Stats[i];
        }
        return null;
    }

    public int rollStat(string name)
    {
        var stat = this.getStatByName(name);
        stat.xpGain();
        return Random.Range(1, stat.Amount);
    }

}
