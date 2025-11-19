using LocalisationToolset;
using TMPro;
using UnityEngine;

namespace LocalisationToolset
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string Key;
        private TMP_Text _label;

        void Awake()
        {
            _label = GetComponent<TMP_Text>();
        }
        void OnEnable()
        {
            LocalisationManager.Instance.OnLanguageChanged += OnLanguageChanged;
        }
        void OnDisable()
        {
            LocalisationManager.Instance.OnLanguageChanged -= OnLanguageChanged;
        }
        void OnLanguageChanged(string lang) => Refresh();
        public void Refresh()
        {
            _label.text = LocalisationManager.Instance.GetText(Key);
        }
    }
}


