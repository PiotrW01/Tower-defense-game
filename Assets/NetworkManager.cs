using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public static string defaultUser = "lc";
    public static string username = "";
    public static string token = "";
    public static bool isLoggedIn = false;
    private int connectionRetries = 0;

    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public TextMeshProUGUI responseField;
    public Button registerButton;
    public Button logoutButton;
    public Button loginButton;

    public void Start()
    {
        token = PlayerPrefs.GetString("token", "");
        username = PlayerPrefs.GetString("username", defaultUser);
        if (!token.Equals("") && !isLoggedIn)
        {
            StartCoroutine(LoginAsync());
        }
        logoutButton.gameObject.SetActive(false);
        registerButton.gameObject.SetActive(true);
    }

    IEnumerator RegisterAsync()
    {
        CredentialsRequest request = new();
        request.username = usernameField.text;
        request.password = passwordField.text;
        string jsonData = JsonUtility.ToJson(request);
        UnityWebRequest www = CreateJsonRequest("auth/register", "POST", jsonData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            if (www.responseCode == 201)
            {
                responseField.text = "Account successfully created!";
                username = usernameField.text;
                token = www.downloadHandler.text.Trim('"');
                PlayerPrefs.SetString("token", token);
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.Save();
                loginButton.interactable = false;
                logoutButton.gameObject.SetActive(true);
                registerButton.gameObject.SetActive(false);
                isLoggedIn = true;
            } else
            {
                responseField.text = "Failed to create an account";
            }
        }

        www.Dispose();
        yield break;
    }

    IEnumerator LoginAsync()
    {
        responseField.text = "Logging in...";
        usernameField.interactable = false;
        passwordField.interactable = false;

        string jsonData = "";
        if (token.Equals(""))
        {
            CredentialsRequest request = new();
            request.username = usernameField.text;
            request.password = passwordField.text;
            username = usernameField.text;
            jsonData = JsonUtility.ToJson(request);
        }

        UnityWebRequest www = CreateJsonRequest("auth/login", "GET", jsonData);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
            if(www.result == UnityWebRequest.Result.ConnectionError && connectionRetries < 2)
            {
                yield return new WaitForSeconds(5);
                connectionRetries++;
                StartCoroutine(LoginAsync());
            } else
            {
                responseField.text = "Failed to connect to the server";
                token = "";
                username = defaultUser;
                PlayerPrefs.DeleteKey("username");
                PlayerPrefs.DeleteKey("token");
                PlayerPrefs.Save();
                usernameField.interactable = true;
                passwordField.interactable = true;
            }
        }
        else
        {
            if (www.responseCode == 200)
            {
                responseField.text = "Logged in as " + username;
                token = www.downloadHandler.text.Trim('"');
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.SetString("token", token);
                PlayerPrefs.Save();
                loginButton.interactable = false;
                logoutButton.gameObject.SetActive(true);
                registerButton.gameObject.SetActive(false);
                isLoggedIn = true;
            }
            else
            {
                responseField.text = "Failed to log into account";

                token = "";
                username = defaultUser;
                PlayerPrefs.SetString("username", username);
                PlayerPrefs.DeleteKey("token");
                PlayerPrefs.Save();

                usernameField.interactable = true;
                passwordField.interactable = true;
            }
        }

        www.Dispose();
        yield break;
    }

/*    IEnumerator LogoutAsync()
    {
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
        PlayerPrefs.Save();
        JWT = "";

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(www.responseCode);
            if (www.responseCode == 200)
            {
                responseField.text = "Logged out!";
                logoutButton.gameObject.SetActive(false);
                registerButton.gameObject.SetActive(true);
                loginButton.interactable = true;
            }
            else
            {
                responseField.text = "Error while logging out";
            }
        }
    }*/

    public void RegisterUser()
    {
        if(!AreInputsValid())
        {
            responseField.text = "Not all credentials meet the requirements!";
            return;
        }
        StartCoroutine(RegisterAsync());
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
        responseField.text = "Saved Credentials!";
    }

    public void Logout()
    {
        responseField.text = "Logged out!";
        logoutButton.gameObject.SetActive(false);
        registerButton.gameObject.SetActive(true);
        loginButton.interactable = true;
        usernameField.interactable = true;
        passwordField.interactable = true;
        usernameField.text = "";
        passwordField.text = "";
        username = defaultUser;
        PlayerPrefs.SetString("username", defaultUser);
        PlayerPrefs.DeleteKey("token");
        token = "";
        PlayerPrefs.Save();
        isLoggedIn = false;
    }

    public void Login()
    {
        StartCoroutine(LoginAsync());
    }

    public static UnityWebRequest CreateJsonRequest(string route, string method, string jsonData)
    {
        UnityWebRequest www = new UnityWebRequest("https://localhost:5000/" + route, method);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.SetRequestHeader("Content-Type", "application/json");
        www.certificateHandler = new CertHandler();
        if (!token.Equals("")) www.SetRequestHeader("Authorization", token);
        return www;
    }


    public class CertHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
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

        //public string username;
        //public string password;
        public int score;
        public int mapID;
        public MapData mapData;
    }
    public class CredentialsRequest
    {
        public string username;
        public string password;
    }
}
