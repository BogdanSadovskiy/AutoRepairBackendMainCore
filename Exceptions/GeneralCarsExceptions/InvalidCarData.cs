namespace AutoRepairMainCore.Exceptions.GeneralCarsExceptions
{
    public class InvalidCarDataException : Exception
    {
        public InvalidCarDataException(string message) : base(message) { }
        public InvalidCarDataException(string message, Exception inerException) :
            base(message, inerException)
        { }
    }
}
