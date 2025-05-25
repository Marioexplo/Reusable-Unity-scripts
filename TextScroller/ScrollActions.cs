namespace TextScroller
{
    public class ScrollActions : UnityEngine.MonoBehaviour
    {
        public TextScroll scroller;
        private bool once;
        public UnityEngine.Events.UnityEvent OnScrollCompleted;

        private void Update()
        {
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