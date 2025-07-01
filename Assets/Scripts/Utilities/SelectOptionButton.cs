using UnityEngine;
using UnityEngine.UI;

namespace AdrianMunteanTest
{
    public class SelectOptionButton : Button
    {
        public Image SelectedImageRef;
        public void SetSelected(bool value)
        {
            SelectedImageRef.enabled = value;
        }
    }
}
