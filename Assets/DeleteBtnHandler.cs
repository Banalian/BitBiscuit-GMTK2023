using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class DeleteBtnHandler : MonoBehaviour
{
    public void ResetSelectedElement()
    {
        ElementManager.Instance.SetActionType(ElementManager.ActionType.Replace);
        ElementManager.Instance.ResetSelected();
    }
}
