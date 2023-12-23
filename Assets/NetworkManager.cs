using System.Collections;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    private string player_name;
    private string player_password;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator RegisterAsync()
    {
        yield return 0;
    }

    IEnumerator LoginAsync()
    {
        yield return 0;
    }

    // top 10
    IEnumerator GetHighscoresAsync(int mapID)
    {
        yield return 0;
    }

    // top 10, top 20...
    IEnumerator GetLatestMapsAsync(int page)
    {
        yield return 0;
    }

    IEnumerator DownloadMapAsync(int mapID)
    {
        yield return 0;
    }

    IEnumerator UploadMapAsync(GameObject map)
    {
        Debug.Log("one");

        yield return new WaitForSeconds(5);

        Debug.Log("two");
    }

    public void UploadMap(GameObject map)
    {
        StartCoroutine(UploadMapAsync(map));
    }
    public void Login()
    {

    }
    public void Register()
    {

    }
    public void GetHighscores(int mapID)
    {

    }
    public void GetLatestMaps(int page)
    {

    }
    public void DownloadMap(int mapID)
    {

    }
}
