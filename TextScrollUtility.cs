using UnityEngine;

namespace TextScroller
{
    public class TextScrollUtility : MonoBehaviour
    {
        [Tooltip("The Text to synchronize with the scrolling")]
        public UnityEngine.UI.Text text;
        public TextScroll scroller;
        private bool once;
        public UnityEngine.Events.UnityEvent OnScrollCompleted;

        private void Update()
        {
            if (text != null) text.text = scroller.GetScrolled();
            if (OnScrollCompleted != null)
            {
                if (scroller.HasFinished && !once)
                {
                    OnScrollCompleted.Invoke();
                    once = true;
                }
                if (once && !scroller.HasFinished) once = false;
            }
        }
    }
}