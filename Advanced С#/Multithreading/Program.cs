using System.Threading;

//for (int i = 0; i < 10; i++)
//{
//    Thread.Sleep(500);      // задержка выполнения на 500 миллисекунд
//    Console.WriteLine(i);
//}

//// создаем новый поток
//Thread myThread1 = new Thread(Print); 
//Thread myThread2 = new Thread(new ThreadStart(Print));
//Thread myThread3 = new Thread(()=>Console.WriteLine("Hello Threads"));

//myThread1.Start();  // запускаем поток myThread1
//myThread2.Start();  // запускаем поток myThread2
//myThread3.Start();  // запускаем поток myThread3

//void Print() => Console.WriteLine("Hello Threads");


// создаем новый поток
//Thread myThread = new Thread(Print);
//// запускаем поток myThread
//myThread.Start();

//// действия, выполняемые в главном потоке
//for (int i = 0; i < 5; i++)
//{
//    Console.WriteLine($"Главный поток: {i}");
//    Thread.Sleep(300);
//}

//// действия, выполняемые во втором потокке
//void Print()
//{
//    for (int i = 0; i < 5; i++)
//    {
//        Console.WriteLine($"Второй поток: {i}");
//        Thread.Sleep(400);
//    }
//}

//создаем новые потоки
//Thread myThread1 = new Thread(new ParameterizedThreadStart(Print));
//Thread myThread2 = new Thread(Print);
//Thread myThread3 = new Thread(message => Console.WriteLine(message));
//запускаем потоки
//myThread1.Start("Hello");
//myThread2.Start("Привет");
//myThread3.Start("Salut");
//void Print(object? message) => Console.WriteLine(message);

//int x = 0;

//// запускаем пять потоков
//for (int i = 1; i < 6; i++)
//{
//    Thread myThread = new(Print);
//    myThread.Name = $"Поток {i}";   // устанавливаем имя для каждого потока
//    myThread.Start();
//}

//void Print()
//{
//    x = 1;
//    for (int i = 1; i < 6; i++)
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
//        x++;
//        Thread.Sleep(100);
//    }
//}
CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
CancellationToken token = cancelTokenSource.Token;

Task task = new Task(() =>
{
    for (int i = 1; i < 10; i++)
    {
        if (token.IsCancellationRequested)
            token.ThrowIfCancellationRequested(); // генерируем исключение

        Console.WriteLine($"Квадрат числа {i} равен {i * i}");
        Thread.Sleep(200);
    }
}, token);
try
{
    task.Start();
    Thread.Sleep(1000);
    // после задержки по времени отменяем выполнение задачи
    cancelTokenSource.Cancel();

    // ожидаем завершения задачи
    Thread.Sleep(1000);
}
catch (AggregateException ae)
{
    foreach (Exception e in ae.InnerExceptions)
    {
        if (e is TaskCanceledException)
            Console.WriteLine("Операция прервана");
        else
            Console.WriteLine(e.Message);
    }
}
finally
{
    cancelTokenSource.Dispose();
}

//  проверяем статус задачи
Console.WriteLine($"Task Status: {task.Status}");