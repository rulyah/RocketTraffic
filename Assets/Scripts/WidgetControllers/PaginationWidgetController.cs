using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WidgetControllers
{
    public class PaginationWidgetController : MonoBehaviour
    {
        [SerializeField] private Button _previousButton;
        [SerializeField] private TextMeshProUGUI _pageText;
        [SerializeField] private Button _nextButton;
    
        private void Start()
        {
            Connection.Instance.OnPageChanged += InstanceOnOnPageChanged;
            _previousButton.onClick.AddListener(OnPreviousButtonClicked);
            _nextButton.onClick.AddListener(OnNextButtonClicked);
            Connection.Instance.SetPage(1);
        }

        private void OnDestroy()
        {
            Connection.Instance.OnPageChanged -= InstanceOnOnPageChanged;
            _previousButton.onClick.RemoveListener(OnPreviousButtonClicked);
            _nextButton.onClick.RemoveListener(OnNextButtonClicked);
        }
    
        private void OnNextButtonClicked()
        {
            if (Connection.Instance.pageIndex == Connection.Instance.movies.total_pages) return;
            Connection.Instance.SetPage(Connection.Instance.pageIndex + 1);
        }

        private void OnPreviousButtonClicked()
        {
            if (Connection.Instance.pageIndex == 1) return;
            Connection.Instance.SetPage(Connection.Instance.pageIndex - 1);
        }

        private void InstanceOnOnPageChanged()
        {
            _pageText.text = Connection.Instance.pageIndex.ToString();
        
            if (Connection.Instance.pageIndex == 1)
            {
                _previousButton.interactable = false;
            }
            else
            {
                _previousButton.interactable = true;
            }
        }
    }
}
