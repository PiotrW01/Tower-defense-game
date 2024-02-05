using System.Collections;
using System.Net.Cache;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    //change class name to Networking
    public static string username = "";
    public static string password = "";

    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TextMeshProUGUI confirmField;
    public TextMeshProUGUI responseField;

    public void Start()
    {
        if (username != "") usernameField.text = username;
        if (password != "") passwordField.text = password;
    }

    IEnumerator RegisterAsync()
    {
        RegisterRequest request = new RegisterRequest();
        request.username = usernameField.text;
        request.password = passwordField.text;
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
            if (www.responseCode == 201)
            {
                responseField.text = "Account successfully created!";
                username = usernameField.text;
                password = passwordField.text;
            } else
            {
                responseField.text = "Failed to create an account";
            }
        }

        www.Dispose();
        yield break;
    }

    public void RegisterUser()
    {
        if(!AreInputsValid())
        {
            responseField.text = "Not all credentials meet the requirements!";
            return;
        }
        StartCoroutine(RegisterAsync());
    }

    public static bool HasCredentialsSet()
    {
        if (username != "" && password != "") return true;
        return false;
    }

    public bool AreInputsValid()
    {
        if (passwordField.text.Length < 5) return false;
        if (usernameField.text.Length < 3) return false;
        return true;
    }

    public void SetCredentials()
    {
        if (!AreInputsValid())
        {
            responseField.text = "Not all credentials meet the requirements!";
            return;
        }
        username = usernameField.text;
        password = passwordField.text;
        Debug.Log(username + password);
        responseField.text = "Saved Credentials!";
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
        public string username;
        public string password;
    }
}
