using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleSlider : MonoBehaviour, IDragHandler
{
    [SerializeField] private UnityEngine.UI.Image fill;
    [SerializeField, Tooltip("Text is optional")] private UnityEngine.UI.Text indicator;
    private RectTransform trans;
    private RectTransform parent;
    private float ox;
    private float width;
    [Tooltip("Gets the current fill amount")]
    public UnityEngine.Events.UnityEvent<float> onDrag;
    /// <summary>Normalized between 0 and 1</summary>
    public float Value
    {
        get => fill.fillAmount;
        set
        {
            fill.fillAmount = value;
            trans.anchoredPosition = new(ox + Value * width, trans.anchoredPosition.y);
            if (indicator != null) indicator.text = ((int)(Value * 100)).ToString() + "%";
        }
    }

    void Awake()
    {
        ReloadFillRect();
        trans = (RectTransform)transform;
        parent = (RectTransform)transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, Input.mousePosition, null, out Vector2 point);
        Value = (point.x - ox) / width; // value : 1 = pointer : fillRight
        onDrag.Invoke(Value);
    }

    public void ReloadFillRect()
    {
        RectTransform fillTrans = fill.rectTransform;
        width = fillTrans.rect.width;
        ox = fillTrans.anchoredPosition.x - width / 2;
    }
}
