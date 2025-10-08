using UnityEngine;

public class ImageFiller : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image image;
    [Tooltip("Amount to add in pixel/s")]
    public float adder;
    [Tooltip("Amount to subtract in pixel/s")]
    public float subtracter;
    private float target = -1;
    private float FillAmount
    {
        get => image.fillAmount;
        set => image.fillAmount = value;
    }

    void Awake() => enabled = false;

    public void UpdateAmount(float value)
    {
        target = Mathf.Clamp01(value);
        enabled = true;
    }

    void Update()
    {
        if (target >= FillAmount)
        {
            FillAmount += adder * Time.deltaTime;
            if (target < FillAmount)
            {
                FillAmount = target;
                enabled = true;
            }
        }
        else
        {
            FillAmount -= subtracter * Time.deltaTime;
            if (target > FillAmount)
            {
                FillAmount = target;
                enabled = true;
            }
        }
    }
}
