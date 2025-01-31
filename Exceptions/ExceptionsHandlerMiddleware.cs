namespace AutoRepairMainCore.Exceptions
{
    public class ExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionsHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (AggregateException aggEx)
            {
                foreach (var ex in aggEx.InnerExceptions)
                {
                    await ExceptionResponseHelper.WriteErrorResponseAsync(context, ex);
                }
            }
            catch (Exception ex)
            {
                await ExceptionResponseHelper.WriteErrorResponseAsync(context, ex);
            }
        }
    }
}
