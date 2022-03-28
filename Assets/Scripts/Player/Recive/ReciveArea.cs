using UnityEngine;

public class ReciveArea : MonoBehaviour
{
    public int section;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(section);
    }
}
