namespace AutoRepairMainCore.Exceptions.GeneralCarsExceptions
{
    public class CarAlreadyExistException : Exception
    {
        public CarAlreadyExistException(string message) : base(message) { }
        public CarAlreadyExistException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }
}
