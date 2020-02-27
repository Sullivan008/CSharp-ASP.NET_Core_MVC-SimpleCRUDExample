using System;

namespace Core.ApplicationCore.BackEndExceptionHandler
{
    public interface IBackEndExceptionHandler
    {
        void ExceptionOperations<TException>(string additionalMessage, TException ex) where TException : Exception;
    }
}
