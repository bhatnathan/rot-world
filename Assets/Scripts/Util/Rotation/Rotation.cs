[System.Serializable]
public struct Rotation
{
    public Axis axis;
    public Direction direction;    

    public Rotation(Axis axis, Direction direction)
    {
        this.axis = axis;
        this.direction = direction;
    }
}
