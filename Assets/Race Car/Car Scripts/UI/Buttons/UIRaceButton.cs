using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
{
    [SerializeField] private RaceInfo raceInfo;
    [SerializeField] private Image icon;
    [SerializeField] private Image Lock;
    [SerializeField] private Text title;

    private void Start()
    {
        ApplyProperty(raceInfo);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (raceInfo == null) return;

        SceneManager.LoadScene(raceInfo.SceneName);
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;

        if (property is RaceInfo == false) return;
        raceInfo = property as RaceInfo;

        icon.sprite = raceInfo.Icon;
        title.text = raceInfo.Title;
    }
}
