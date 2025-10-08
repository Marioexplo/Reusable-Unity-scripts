namespace TextScroller
{
    public class AutoScrollTMPro : AutoScrollBase
    {
        public TMPro.TextMeshProUGUI text;
        protected override string Target { set => text.text = value; }
    }
}