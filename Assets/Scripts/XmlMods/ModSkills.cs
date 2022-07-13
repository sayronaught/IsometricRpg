using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class ModSkills
{
    [XmlAttribute("name")]
    public string Name;
    public string Description = "";
    public string AmountRange = "";
    public int Amount = 0;
    public int xp = 0;

    public ModSkills Clone()
    {
        ModSkills output = new ModSkills();
        output.Name = (string)this.Name;
        return output;
    }

    public void xpGain(int gain = 1)
    {
        this.xp += gain;
        while (this.xp > this.Amount)
        {
            this.xp -= this.Amount;
            this.Amount++;
        }
    }
}
