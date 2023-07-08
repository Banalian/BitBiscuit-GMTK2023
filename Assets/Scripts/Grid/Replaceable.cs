using UnityEngine;

namespace Grid
{
    public class Replaceable : MonoBehaviour
    {
        private void OnMouseUpAsButton()
        {
            ElementManager.Instance.ReplaceElement(this.gameObject);
        }
    }
}
