using System.Runtime.Serialization;

[DataContract]
public class HighScore
{
    [DataMember]
    public float Time;
    [DataMember]
    public string Initials;
}
