namespace TextScroller
{
    public class ScrollActions : UnityEngine.MonoBehaviour
    {
        public TextScroll scroller;
        private bool once;
        public UnityEngine.Events.UnityEvent onScrollCompleted;

        private void Update()
        {
            if (scroller.HasFinished)
            {
                if (!once)
                {
                    onScrollCompleted?.Invoke();
                    once = true;
                }
            }
            else if (once) once = false;
        }
    }
}