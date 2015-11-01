using System;
using System.Threading.Tasks;

namespace LCL.Threading
{
    /// <summary>
    /// 异步操作
    /// Task是.NET 4并行编程最为核心的一个类
    /// </summary>
    public static class Async
    {
        public static Task AsyncRun(Action task)
        {
            return AsyncRun(task, TaskCreationOptions.None);
        }
        public static Task AsyncRun(Action task, TaskCreationOptions taskOption)
        {
            return AsyncRun(task, taskOption, null);
        }
        public static Task AsyncRun(Action task, Action<Exception> exceptionHandler)
        {
            return AsyncRun(task, TaskCreationOptions.None, exceptionHandler);
        }
        public static Task AsyncRun(Action task, TaskCreationOptions taskOption, Action<Exception> exceptionHandler)
        {
            return Task.Factory.StartNew(task, taskOption).ContinueWith(t =>
            {
                if (exceptionHandler != null)
                    exceptionHandler(t.Exception);
                else
                {
                    for (var i = 0; i < t.Exception.InnerExceptions.Count; i++)
                    {
                        Console.WriteLine(t.Exception.InnerExceptions[i].Message);
                    }
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public static Task AsyncRun(Action<object> task, object state)
        {
            return AsyncRun(task, state, TaskCreationOptions.None);
        }
        public static Task AsyncRun(Action<object> task, object state, TaskCreationOptions taskOption)
        {
            return AsyncRun(task, state, taskOption, null);
        }
        public static Task AsyncRun(Action<object> task, object state, Action<Exception> exceptionHandler)
        {
            return AsyncRun(task, state, TaskCreationOptions.None, exceptionHandler);
        }
        public static Task AsyncRun(Action<object> task, object state, TaskCreationOptions taskOption, Action<Exception> exceptionHandler)
        {
            return Task.Factory.StartNew(task, state, taskOption).ContinueWith(t =>
            {
                if (exceptionHandler != null)
                    exceptionHandler(t.Exception);
                else
                {
                    for (var i = 0; i < t.Exception.InnerExceptions.Count; i++)
                    {
                        Console.WriteLine(t.Exception.InnerExceptions[i].Message);
                    }
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
