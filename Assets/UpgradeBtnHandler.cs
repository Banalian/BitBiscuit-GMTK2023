using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class UpgradeBtnHandler : MonoBehaviour
{
    public void SelectUpgradeMode()
    {
        ElementManager.Instance.SetActionType(ElementManager.ActionType.Upgrade);
    }
}
