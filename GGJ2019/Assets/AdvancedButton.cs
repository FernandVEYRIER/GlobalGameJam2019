using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdvancedButton : Button
{
    public override void Select()
    {
        if ((UnityEngine.Object)EventSystem.current == (UnityEngine.Object)null || EventSystem.current.alreadySelecting)
            return;
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        UpdateSelectionState(new BaseEventData(EventSystem.current));
    }
}
