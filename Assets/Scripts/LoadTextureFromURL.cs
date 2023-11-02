using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadTextureFromURL : MonoBehaviour
{
    [SerializeField] private string _url;
    [SerializeField] private Image _img;
    private void Start()
    {
        StartCoroutine(DownloadImage(_url));
    }

    private IEnumerator DownloadImage(string mediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        request.method = UnityWebRequest.kHttpVerbGET;
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to download image from {mediaUrl}: {request.error}");
            yield break;
        }

        Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
        Sprite sprite = CreateSprite(tex);
        _img.sprite = sprite;
    }

    private Sprite CreateSprite(Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
    }
}
