
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    public int shelf;
    public int region;

    public List<Vector3> cargoPosition;
    public List<Vector3> hasCargo;
    [SerializeField] GameObject CargoPrefab;

    private int counter;
    private List<int> rand;
    private void Start()
    {
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
        hasCargo.Add(randomPosition);
        Instantiate(CargoPrefab,randomPosition+transform.position,Quaternion.identity);
    }

}
