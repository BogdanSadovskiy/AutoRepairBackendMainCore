namespace AutoRepairMainCore.Exceptions.AutoServiceExceptions
{
    public class PasswordValidateException : Exception
    {
        public PasswordValidateException(string message) : base(message) { }

        public PasswordValidateException(string message, Exception innerException) : 
            base(message, innerException) { }
    }
}
