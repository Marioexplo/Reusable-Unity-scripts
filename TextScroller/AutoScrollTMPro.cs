using UnityEngine;

namespace TextScroller
{
    public class AutoScrollTMPro : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI text;
        public TextScroll scroller;

        private void Update()
        {
            if (text != null) text.text = scroller.GetScrolled();
        }

        /// <summary>Call <see cref="TextScroll.Restart"/> and set text of <see cref="text"/> empty</summary>
        public void Restart()
        {
            scroller.Restart();
            text.text = string.Empty;
        }
    }
}