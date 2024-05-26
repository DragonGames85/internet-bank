using CreditService.Model.DTO;

namespace CreditService.Logger
{
    public interface IExceptionService
    {
        Task GetException();
    }
    public class ExceptionService: IExceptionService
    {
        public async Task GetException()
        {
            Random rand = new Random();
            if (DateTime.Now.Minute % 2 == 0)
            {
                var count = rand.Next(0, 100);
                if (count < 90) throw new Exception("Something went wrong");
            }
            else
            {
                var count = rand.Next(0, 2);
                if (count == 0) throw new Exception("Something went wrong");
            }
        }
    }
    public static class Retry
    {
        public static void GetException()
        {
            Random rand = new Random();
            if (DateTime.Now.Minute % 2 == 0)
            {
                var count = rand.Next(0, 100);
                if (count < 90) throw new Exception("Something went wrong");
            }
            else
            {
                var count = rand.Next(0, 2);
                if (count == 0) throw new Exception("Something went wrong");
            }
        }

        public static void Do(
            Action action,
            TimeSpan retryInterval,
            int maxAttemptCount = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, maxAttemptCount);
        }

        public static T Do<T>(
            Func<T> action,
            TimeSpan retryInterval,
            int maxAttemptCount = 10)
        {
            var exceptions = new List<Exception>();

            for (int attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    Random rand = new Random();
                    if (DateTime.Now.Minute % 2 == 0)
                    {
                        int count = rand.Next(0, 100);
                        if (count < 90) throw new Exception("Something went wrong");
                    }
                    else
                    {
                        int count = rand.Next(0, 2);
                        if (count == 0) throw new Exception("Something went wrong");
                    }

                    if (attempted > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }
                    return action();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Retry times " + attempted.ToString());
                    Thread.Sleep(retryInterval);
                    exceptions.Add(ex);
                }
            }
            throw new AggregateException(exceptions);
        }
    }

}
