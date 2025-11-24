using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class BillboardImages : MonoBehaviour
{
    private Texture2D[] cachedImages;

    public string[] urls = new string[]
    {
        "https://images.unsplash.com/photo-1753454116069-a4fba9e75e4c?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "https://images.unsplash.com/photo-1753454116069-a4fba9e75e4c?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
        "https://plus.unsplash.com/premium_photo-1756383544401-6df15ec16a14?q=80&w=1069&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
    };
    public Renderer[] billboards;

    public Renderer[] billboardRenderers = new Renderer[3];

    void Awake()
    {
        cachedImages = new Texture2D[urls.Length];
    }
    void Start()
    {
        cachedImages = new Texture2D[urls.Length];

        for (int i = 0; i < urls.Length; i++)
        {
            int index = i; // capture the current index
            GetWebImage(index, tex =>
            {
                billboards[index].material.mainTexture = tex;
            });
        }
    }
    public void GetWebImage(int index, Action<Texture2D> callback)
    {
        // Check if the image is already cached
        if (cachedImages[index] != null)
        {
            callback(cachedImages[index]);
            return;
        }
        //if not, download it
        StartCoroutine(DownloadImage(index, callback));
    }
    private IEnumerator DownloadImage(int index, Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(urls[index]);
        yield return request.SendWebRequest();

        Texture2D tex = DownloadHandlerTexture.GetContent(request);

        cachedImages[index] = tex;  // store in the cache

        callback(tex);
    }
}
