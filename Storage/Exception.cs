namespace webapp.Storage;

[System.Serializable]
public class IncorrectPlaceException : System.Exception
{
    public IncorrectPlaceException() { }
    public IncorrectPlaceException(string message) : base(message) { }
    public IncorrectPlaceException(string message, System.Exception inner) : base(message, inner) { }
    protected IncorrectPlaceException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}