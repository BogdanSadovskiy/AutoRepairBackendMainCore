namespace AutoRepairMainCore.Exceptions.AutoServiceExceptions
{
    public class AutoServiceAlreadyExistException : Exception
    {
        public AutoServiceAlreadyExistException(string message) : base(message) { }

        public AutoServiceAlreadyExistException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
