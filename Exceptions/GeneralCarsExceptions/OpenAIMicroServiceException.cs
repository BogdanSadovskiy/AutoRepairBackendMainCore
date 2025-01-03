namespace AutoRepairMainCore.Exceptions.GeneralCarsExceptions
{
    public class OpenAIMicroServiceException : Exception
    {
        public OpenAIMicroServiceException(string message) : base(message) { }
        public OpenAIMicroServiceException(string message, Exception innerException) : 
            base(message, innerException) { }
    }
}
