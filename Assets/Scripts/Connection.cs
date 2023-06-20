using System;
using System.Collections;
using DTOs;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class Connection : MonoBehaviour
{
    public static Connection Instance { get; private set; }

    public event Action OnPageChanged;

    public int pageIndex { get; private set; } = -1;
    public MoviesDTO movies { get; private set; }

    private void Awake()
    {
        if (Instance == default)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPage(int pageIndex)
    {
        if (this.pageIndex == pageIndex) return;
        StartCoroutine(LoadMoviesJson(pageIndex));
    }

    public void LoadMoviePoster(MovieDTO movieDto, Action<Texture2D> callback)
    {
        string url = $"https://image.tmdb.org/t/p/w500/{movieDto.backdrop_path}";
        StartCoroutine(LoadImage(url, callback));
    }
    
    private IEnumerator LoadImage(string url, Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(request.error);
                break;
            
            case UnityWebRequest.Result.Success:
                Texture2D texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
                callback(texture);
                break;
                
        }
    }
    
    private IEnumerator LoadMoviesJson(int pageIndex)
    {
        this.pageIndex = pageIndex;
        string url = $"https://api.themoviedb.org/3/discover/movie?include_adult=false" +
                     $"&include_video=false&langu age=en-US&page={pageIndex}&sort_by=popularity.desc" +
                     $"&api_key=00243336e2f949edba05fc655da4510e";
        
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwMDI0MzMzNmUyZjk0OWVkYmEwNWZjNjU1ZGE0N " +
                                                  "TEwZSIsInN1YiI6IjVhYzFjM2IxMGUwYTI2NGE1NzA1NmEwMSIsInNjb3BlcyI6WyJhcGlfcm VhZCJdLC" +
                                                  "J2ZXJzaW9uIjoxfQ.uy3Lj5gCGGhxulu3ocPzJVh10f7KE_x1IDSE16CGzKw");
        
        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(request.error);
                break;
            
            case UnityWebRequest.Result.Success:
                string json = request.downloadHandler.text;
                Debug.Log(json);
                movies = JsonConvert.DeserializeObject<MoviesDTO>(json);
                OnPageChanged?.Invoke();
                break;
        }
    }
}
