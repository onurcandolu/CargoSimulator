using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    [SerializeField] GameObject newLevel;
    [SerializeField] GameObject nextLevelArea;
    int nextPoint = 6;
    int newLevelPoint;
    int currLevel=1;

    // Start is called before the first frame update
    void Start()
    {
        newLevelPoint = nextPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextLevel()
    {
        nextLevelArea.transform.position += new Vector3(0, 0, -nextPoint);
        var nextLevel = Instantiate(newLevel, new Vector3(0, 0, -newLevelPoint), new Quaternion(0, 180, 0, 0));
        nextLevel.GetComponentInChildren<ReciveArea>().section = ++currLevel;
        newLevelPoint +=6;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Next Level"))
        {
            nextLevel();
        }
    }
}
