# Notifications

This assemly provides the way to combine all notifications into the one library.

Messaging of any level baised on the special code and could be shown with any GUI library.

## Messages

- Info
- Success
- Warning


## Exceptions

## Different UI wrappers

## Logs

Actually I had an idea to us an attribute as decorators, but as I realised c# doesn't implement [Aspect-Oriented Programming](https://en.wikipedia.org/wiki/Aspect-oriented_programming) directly only via side libraries e.g. [PostSharp] (https://www.postsharp.net/). 

This means that usage of OnEntry() or OnExit() requires modification of IL code, but attributes provides functional for keeping metadata which can be accessed via Reflection. This is not what I want and know I don't know how to avoid usage such construction 

~~~csharp
try 
{
    _logger.Info("smth started");
    // ...
    _logger.Info("smt completed");
}
catch (Exception ex)
{
    _logger.Error(ex.ToString);
}
~~~

many times.

> **Perhaps I have to rethinking the logging model. What if I will log only some entry/exit points, but automatically write logs in case of exceptions?**

Ok, in case of exception I can do my own exeption handler and put into logger, but what about info, warning levels?

In [AspNetCore](https://github.com/dotnet/aspnetcore) they use such scheme:

- In the [separate file](https://github.com/dotnet/aspnetcore/blob/49c01eefecf1dbd75e5536aa803d689390c6770a/src/SignalR/clients/csharp/Client.Core/src/HubConnection.Log.cs#L252) Class.Log.cs describe any possible log cases:

~~~csharp
public partial class HubConnection
{
    private static class Log
    {
        private static readonly Action<ILogger, string, int, Exception> _preparingNonBlockingInvocation = LoggerMessage.Define<string, int>(LogLevel.Trace, new EventId(1, "PreparingNonBlockingInvocation"), "Preparing non-blocking invocation of '{Target}', with {ArgumentCount} argument(s).");
        
        // ...
            
        public static void PreparingNonBlockingInvocation(ILogger logger, string target, int count)
        {
            _preparingNonBlockingInvocation(logger, target, count, null);
        }
        
        // ...
    }
}
~~~

- [use it](https://github.com/dotnet/aspnetcore/blob/49c01eefecf1dbd75e5536aa803d689390c6770a/src/SignalR/clients/csharp/Client.Core/src/HubConnection.cs#L847) like simple static function in the code:


~~~csharp

  private async Task SendCoreAsyncCore(string methodName, object[] args, CancellationToken cancellationToken)
        {
           // ...
            try
            {
                // ...
                Log.PreparingNonBlockingInvocation(_logger, methodName, args.Length);
                // ...
            }
            finally
            {
                // ... 
            }
        }
~~~

Don't sure that I like such scheme...