using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class TestAPI : MonoBehaviour
{
    public void Register()
    {
        if (!NetworkManager.Instance.HasCredentialsSet()) return;
        NetworkManager.Instance.Register();
    }

    public void GetHighscores()
    {
        NetworkManager.Instance.GetHighscores(1000001);
    }

    public void GetLatestMaps()
    {
        //NetworkManager.Instance.GetLatestMaps(0);
    }

    public void UploadScore()
    {
        NetworkManager.Instance.UploadScore(1000000, 99999);
    }

    public void UploadMap()
    {
        MapData data = new MapData();
        data.name = "test000";

        NetworkManager.Instance.UploadMap(data);
    }


/*    //add a refresh token too
    private static string JWT = "";
    private int triedConnections = 0;

    public void Start()
    {
        byte[] bytes = UTF8Encoding.UTF8.GetBytes("lol");
        var sha = SHA256.Create();
        var hashBytes = sha.ComputeHash(bytes);
        string hashString = BitConverter.ToString(hashBytes).Replace("-","").ToLower();
        Debug.Log(hashString);
        sha = SHA256.Create();
        hashBytes = sha.ComputeHash(bytes);
        hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        Debug.Log(hashString);
    }

    IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:5000/scores/0");
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Wrapper<Player> players = JsonUtility.FromJson<Wrapper<Player>>(www.downloadHandler.text);
            foreach (var player in players.results)
            {
                Debug.Log(player.player_name + " " + player.password_hash);
            }
        }
    }

    IEnumerator SendData()
    {
        Player player = new Player();
        player.player_name = "jojo";
        player.password_hash = "abc123";

        string jsonData = JsonUtility.ToJson(player);

        UnityWebRequest www = new UnityWebRequest("http://localhost:5000/auth/register", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Debug.Log(JsonUtility.FromJson<Player>(www.downloadHandler.text));
        }

        www.Dispose();
        yield break;
    }

    IEnumerator Login()
    {
        if(triedConnections > 3)
        {
            triedConnections = 0;
            yield break;
        }
        triedConnections++;
        UnityWebRequest www = new UnityWebRequest("http://localhost:5000/auth/login");
        www.downloadHandler = new DownloadHandlerBuffer();

        if (JWT == "")
        {
            www.method = "POST";
            Player player = new Player();
            player.player_name = "jojo";
            player.password_hash = "abc123";

            string jsonData = JsonUtility.ToJson(player);
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + www.error);
                StartCoroutine(Login());
            }
            else
            {
                JWT = www.downloadHandler.text;
                Debug.Log(JWT);
            }
        } 
        else
        {
            www.method = "GET";
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", JWT);
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + www.error);
                JWT = "";
                StartCoroutine(Login());
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }


        www.Dispose();
        yield break;
    }

    public void TryFetchData()
    {
        StartCoroutine(GetData());
    }

    public void TrySendData()
    {
        StartCoroutine(SendData());
    }

    public void TryLogin()
    {
        StartCoroutine(Login());
    }

    [System.Serializable]
    public class Player
    {
        public string player_name;
        public string password_hash;

        public override string ToString()
        {
            return "Player_NAME:" + player_name + ", Password_HASH " + password_hash;
        }
    }

    public class Wrapper<T>
    {
        public T[] results;
    }*/
}
