public struct Direction
{
    private int value;

    public Direction(bool positive) => value = positive ? 1 : -1;

    public static Direction Random => new(UnityEngine.Random.Range(0, 2) == 0);

    public static implicit operator int(Direction direction) => direction.value;

    public void Invert() => value *= -1;
}
