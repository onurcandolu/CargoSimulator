
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    public int shelf;
    public int region;
    public int section;

    public List<Vector3> cargoPosition;
    [SerializeField] GameObject CargoPrefab;
    [SerializeField] ReciveArea reciveArea;
    public List<GameObject> PlacedCargo;

    private int counter;
    private List<int> rand;
    private void Start()
    {
        section = reciveArea.section;
        PlacedCargo = new List<GameObject>();
        cargoPosition = new List<Vector3>();
        cargoPosition.Add(new Vector3(0,0.8f,1.3f));
        cargoPosition.Add(new Vector3(0, 1.3f, 1.3f));
        cargoPosition.Add(new Vector3(0, 1.8f, 1.3f));
        cargoPosition.Add(new Vector3(0, 2.2f, 1.3f));
        cargoPosition.Add(new Vector3(0, 2.55f, 1.3f));
        rand = Enumerable.Range(0, 5).OrderBy(x => Guid.NewGuid()).ToList();
    }
    public void cargoPlacement()
    {
        var randomPosition = cargoPosition[rand[counter]];
        counter++;
        PlacedCargo.Add(Instantiate(CargoPrefab,randomPosition+transform.position,Quaternion.identity));
    }
    public void cargoUnPlacement()
    {
        Destroy(PlacedCargo[PlacedCargo.Count - 1]);
        PlacedCargo.RemoveAt(PlacedCargo.Count-1);
        counter--;
    }

}
