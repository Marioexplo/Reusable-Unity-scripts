using UnityEngine;

namespace TextScroller
{
    public class AutoScroll : MonoBehaviour
    {
        [Tooltip("The Text to synchronize with the scrolling")]
        public UnityEngine.UI.Text text;
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