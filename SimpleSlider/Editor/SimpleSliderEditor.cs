using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Reflection;

[CustomEditor(typeof(SimpleSlider))]
public class SimpleSliderEditor : Editor
{
    static readonly FieldInfo fillFieldInfo = typeof(SimpleSlider).GetField("fill", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo textFieldInfo = typeof(SimpleSlider).GetField("indicator", BindingFlags.NonPublic | BindingFlags.Instance);

    [MenuItem("GameObject/UI/Simple slider")]
    static void CreateSimpleSlider(MenuCommand command)
    {
        System.Type[] image = new System.Type[] { typeof(RectTransform), typeof(CanvasRenderer), typeof(Image) };
        GameObject obj = new("Slider", image);
        {
            GameObject parent = command.context as GameObject;
            for (Transform canvas = parent.transform; true; canvas = canvas.parent)
            {
                if (canvas.TryGetComponent(out Canvas _)) break;
                if (canvas.parent == null)
                {
                    parent = new("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                    parent.transform.SetParent(canvas);
                    break;
                }
            }
            GameObjectUtility.SetParentAndAlign(obj, parent);
        }
        Image fill;
        {
            GameObject filler = new("Fill", image);
            GameObjectUtility.SetParentAndAlign(filler, obj);
            fill = filler.GetComponent<Image>();
        }
        fill.type = Image.Type.Filled;
        fill.fillMethod = Image.FillMethod.Horizontal;
        Text text;
        {
            GameObject indicator = new("Indicator", image[0]);
            GameObjectUtility.SetParentAndAlign(indicator, obj);
            text = indicator.AddComponent<Text>();
        }
        GameObject thumb = new("Thumb", image);
        GameObjectUtility.SetParentAndAlign(thumb, obj);
        SimpleSlider slider = thumb.AddComponent<SimpleSlider>();
        fillFieldInfo.SetValue(slider, fill);
        textFieldInfo.SetValue(slider, text);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SimpleSlider slider = (SimpleSlider)target;
        RectTransform transform = (RectTransform)slider.transform;
        Image fill = (Image)fillFieldInfo.GetValue(slider);
        Text text = (Text)textFieldInfo.GetValue(slider);
        if (fill != null && text != null && fill.type == Image.Type.Filled)
        {
            fill.fillAmount = EditorGUILayout.Slider(fill.fillAmount, 0, 1);
            float width = fill.rectTransform.rect.width;
            transform.anchoredPosition = new(fill.rectTransform.anchoredPosition.x - width / 2 + fill.fillAmount * width, transform.anchoredPosition.y);
            text.text = ((int)(fill.fillAmount * 100)).ToString() + "%";
        }
    }
}
