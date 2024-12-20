namespace AutoRepairMainCore.Exceptions.AutoServiceExceptions
{
    public class AutoServiceNotFoundException : Exception
    {
        public AutoServiceNotFoundException(string message) : base(message) { }

        public AutoServiceNotFoundException(string message, Exception innerException) : 
            base(message, innerException) { }
    }
}
