using UnityEngine;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("vehicles")]
public class vehicleSummon : MonoBehaviour
{
    [XmlElement("vehicle")]
    public List<Vehicle> vehiclesSel { get; set; }
}

public class Vehicle
{
    [XmlAttribute("id")]
    public string vehicleID { get; set; }

    [XmlAttribute("description")]
    public string vehicleDesc { get; set; }

    [XmlAttribute("damageRating")]
    public float damageRate { get; set; }

    [XmlAttribute("topSpeed")]
    public string speedMPH { get; set; }

    [XmlAttribute("class")]
    public string vehicleClass { get; set; }

    [XmlAttribute("cost")]
    public float vehicleCost { get; set; }

    [XmlAttribute("acceleration")]
    public float accelerationTime { get; set; }

    [XmlAttribute("handling")]
    public float handlingNum { get; set; }

    [XmlAttribute("brakeHorsePower")]
    public float brakeHP { get; set; }
}
