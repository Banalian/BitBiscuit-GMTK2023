using UnityEngine;

namespace Grid
{
    public class Replaceable : Highlightable
    {
        private void OnMouseUpAsButton()
        {
            ElementManager.Instance.ReplaceElement(this.gameObject);
        }
    }
}
