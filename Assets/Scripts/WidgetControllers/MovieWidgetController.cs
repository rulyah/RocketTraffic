using DTOs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WidgetControllers
{
    public class MovieWidgetController : MonoBehaviour
    {
        [SerializeField] private RawImage _poster;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _overview;
        
        
        public void SetMovie(MovieDTO movie)
        {
            _title.text = movie.title;
            _overview.text = movie.overview;
            Connection.Instance.LoadMoviePoster(movie, texture 
                => _poster.texture = texture);
        }
    }
}