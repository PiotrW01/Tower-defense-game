using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;
    public static string username = "username1";
    private static string password = "password1";

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

    // porozdzielac korutyny do odpowiednich skryptow zamiast w tym jednym
    IEnumerator RegisterAsync()
    {
        RegisterRequest request = new RegisterRequest(username, password);
        string jsonData = JsonUtility.ToJson(request);
        UnityWebRequest www = CreateJsonRequest("http://localhost:5000/auth/register", "POST", jsonData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.responseCode);
        }

        www.Dispose();
        yield break;
    }

    // top 10
    IEnumerator GetHighscoresAsync(int mapID)
    {
        string jsonData = "{\"mapID\":\"" + mapID + "\"}";
        var www = CreateJsonRequest("http://localhost:5000/scores/", "GET", jsonData);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            ScoresRequest request = JsonUtility.FromJson<ScoresRequest>(www.downloadHandler.text);
            foreach (var score in request.scores)
            {
                Debug.Log(score.username + score.totalScore);
            }
        }

        www.Dispose();
        yield break;
    }

/*    // top 10, top 20...
    IEnumerator GetLatestMapsAsync(int page)
    {
        var www = CreateJsonRequest("http://localhost:5000/maps/" + page, "GET", "");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            MapsRequest req = JsonUtility.FromJson<MapsRequest>(www.downloadHandler.text);
            foreach (var data in req.maps)
            {
                var t = JsonUtility.FromJson<MapData>(data.jsonMapData);
                Debug.Log(t.id = data.mapID);
            }
        }

        www.Dispose();
        yield break;
    }*/

    IEnumerator UploadScoreAsync(int mapID, int score)
    {
        UploadRequest request = new UploadRequest();
        request.username = username;
        request.password = password;
        request.mapID = mapID;
        request.score = score;
        string jsonData = JsonUtility.ToJson(request);

        UnityWebRequest www = CreateJsonRequest("http://localhost:5000/scores/upload", "PUT", jsonData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.responseCode);
        }

        www.Dispose();
        yield break;
    }

    IEnumerator UploadMapAsync(MapData mapData)
    {
        // get back mapID and set it
        UploadRequest request = new UploadRequest();
        request.mapData = mapData;
        request.username = username;
        request.password = password;
        string jsonData = JsonUtility.ToJson(request);

        UnityWebRequest www = CreateJsonRequest("http://localhost:5000/maps/upload", "PUT", jsonData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }

        www.Dispose();
        yield break;
    }

    public void UploadMap(MapData mapData)
    {
        StartCoroutine(UploadMapAsync(mapData));
    }

    public void Register()
    {
        StartCoroutine(RegisterAsync());
    }

    public void GetHighscores(int mapID)
    {
        StartCoroutine(GetHighscoresAsync(mapID));
    }
/*    public void GetLatestMaps(int page)
    {
        StartCoroutine(GetLatestMapsAsync(page));
    }*/

    public void UploadScore(int mapID, int score)
    {
        StartCoroutine(UploadScoreAsync(mapID, score));
    }

    public static void SetUsername(string newUsername)
    {
        username = newUsername;
    }

    public static void SetPassword(string newPassword)
    {
        password = newPassword;
    }

    public bool HasCredentialsSet()
    {
        if (username != "" && password != "") return true;
        return false;
    }

    public static UnityWebRequest CreateJsonRequest(string url, string method, string jsonData)
    {
        UnityWebRequest www = new UnityWebRequest(url, method);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.SetRequestHeader("Content-Type", "application/json");
        return www;
    }

    [System.Serializable]
    public class ScoresRequest
    {
        public PlayerScore[] scores;
    }

    [System.Serializable]
    public class MapsRequest
    {
        public Map[] maps;
    }
    [System.Serializable]
    public class Map
    {
        public int mapID;
        public string mapAuthor;
        public string jsonMapData;
    }

    public class UploadRequest
    {
        public string username;
        public string password;
        public int score;
        public int mapID;
        public MapData mapData;
    }

    public class RegisterRequest
    {
        public RegisterRequest(string username, string password)
        {
            this.username=username;
            this.password=password;
        }

        public string username;
        public string password;
    }
}
