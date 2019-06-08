using System.Runtime.Serialization;

[DataContract]
public class HighScore
{
    [DataMember]
    public float Time;
    [DataMember]
    public string Initials;

    public HighScore(string initials, float time)
    {
        Time = time;
        Initials = initials;
    }
}
