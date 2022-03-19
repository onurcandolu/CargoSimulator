using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoInfo 
{
    public int id { get; private set; }
    public int section { get; private set; }
    public int shelf { get; private set; }
    public int region { get; private set; }


    public CargoInfo(int _id,int _section,int _shelf,int _region)
    {
        id = _id;
        section = _section;
        shelf = _shelf; 
        region = _region;
    }
}
