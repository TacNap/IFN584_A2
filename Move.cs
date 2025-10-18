public struct Move
{
    public Disc Disc { get; set; }
    public int Lane { get; set; }

    public Move(Disc disc, int lane)
    {
        Disc = disc;
        Lane = lane;
    }
}