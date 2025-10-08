public readonly struct Direction
{
    public readonly sbyte value;

    public Direction(bool positive) => value = (sbyte)(positive ? 1 : -1);

    public static Direction Random => new(UnityEngine.Random.Range(0, 2) == 0);

    public static implicit operator int(Direction direction) => direction.value;

    public readonly bool Positive => value.Equals(1);
}
