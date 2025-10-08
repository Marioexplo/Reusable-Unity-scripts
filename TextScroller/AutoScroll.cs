using UnityEngine;

namespace TextScroller
{
    public class AutoScroll : AutoScrollBase
    {
        [Tooltip("The Text to synchronize with the scrolling")]
        public UnityEngine.UI.Text text;
        protected override string Target { set => text.text = value; }
    }
}