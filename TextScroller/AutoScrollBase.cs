namespace TextScroller
{
    public abstract class AutoScrollBase : UnityEngine.MonoBehaviour
    {
        protected abstract string Target {set;}
        public TextScroll scroller;

        private void Update() => Target = scroller.GetScrolled;

        /// <summary>Call <see cref="TextScroll.Restart"/> and set text of <see cref="text"/> empty</summary>
        public void Restart()
        {
            scroller.Restart();
            Target = string.Empty;
        }
    }
}
