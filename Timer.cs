public struct Timer
{
    private float timer;

    /// <summary>Adds <see cref="UnityEngine.Time.deltaTime"/></summary>
    public static Timer operator ++(Timer field)
    {
        field.timer += UnityEngine.Time.deltaTime;
        return field;
    }

    /// <summary>Restart</summary>
    public static Timer operator --(Timer field)
    {
        field.timer = 0;
        return field;
    }

    public static implicit operator float(Timer field) => field.timer;
}
