using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TasksLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            // 1 способ
            Task task = new Task(() => 
            {
                Thread.Sleep(3000);
                Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) Задча завершена");
            });
            //некоторое кол строк кода спустся
            task.Start();
            */

            /*
            // 2 способ
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) Задча завершена");
                // опционально возвращение результата
                return 0;
            });
            */

            /*
            // 3 способ
            var task = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) Задча завершена");
                // опционально возвращение результата
                return 0;
            });

            // только для получение результата
            var result = task.Result;
            */

            // для отмены задачи
            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            var cancellationTask = Task.Run(() =>
            {
                Thread.Sleep(10);
                Console.WriteLine($"({Thread.CurrentThread.ManagedThreadId}) Задча завершена");
                // опционально возвращение результата
                return 0;
            }, cancellationToken);

            cancellationSource.Cancel();

            Console.WriteLine("Главные поток завершил работу");
            Console.ReadLine();
        }

        private static Task DoSomething()
        {
            return Task.CompletedTask; // полезно async-await
        }

        private static Task<bool> DoSomethingWithBool()
        {
            try
            {
                // для отмены - Task.FromCanceled
                return Task.FromResult(true); // передача результата можно и вне блока try
            }
            catch(Exception exception)
            {
                return Task.FromException<bool>(exception); // для исключения
            }
        }
    }
}
