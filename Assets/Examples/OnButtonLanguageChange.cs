using UnityEngine;

public class OnButtonLanguageChange : MonoBehaviour
{
    [SerializeField] private string language;
   public void OnClick()
    {
        LocalisationToolset.LocalisationManager.Instance.ChangeLanguage(language);
    }
}
