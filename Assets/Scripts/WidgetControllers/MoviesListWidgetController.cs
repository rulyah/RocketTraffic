using System.Collections.Generic;
using UnityEngine;

namespace WidgetControllers
{
    public class MoviesListWidgetController : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private MovieWidgetController _movieWidgetPrefab;
        
        private List<MovieWidgetController> _movieWidgets = new();

        private void Start()
        {
            Connection.Instance.OnPageChanged += InstanceOnOnPageChanged;
            Connection.Instance.SetPage(1);
        }

        private void InstanceOnOnPageChanged()
        {
            int iterationsCount = Mathf.Max(_movieWidgets.Count, Connection.Instance.movies.results.Count);

            for (int i = 0; i < iterationsCount; i++)
            {
                if (i >= Connection.Instance.movies.results.Count)
                {
                    Destroy(_movieWidgets[i].gameObject);
                    _movieWidgets.RemoveAt(i);
                    continue;
                }
                
                if (i >= _movieWidgets.Count)
                {
                    MovieWidgetController movieWidget = Instantiate(_movieWidgetPrefab, _content);
                    movieWidget.SetMovie(Connection.Instance.movies.results[i]);
                    _movieWidgets.Add(movieWidget);
                    continue;
                }
                
                _movieWidgets[i].SetMovie(Connection.Instance.movies.results[i]);
            }
        }
    }
}