namespace ShiftLogger.Models;

public class ShiftItem
{
    public long Id { get; set; }
    public string WorkerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

}
