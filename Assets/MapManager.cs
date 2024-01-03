using UnityEngine;
public class MapManager : MonoBehaviour
{
    public GameObject MapPrefab;
    private void Start()
    {
        Instantiate(MapPrefab, Vector3.zero, Quaternion.identity);
    }
}