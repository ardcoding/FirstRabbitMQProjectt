
// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;
using System.Timers;
using System;

class FirstRabbitMQProject
{
    static void Main(string[] args)
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        timer.Elapsed += TimerElapsed;
        timer.Interval = 1000;
        timer.Enabled = true;

        Console.WriteLine("Program Başladı");
        Console.ReadLine();
    }
    static void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        SendMessage();
    }

    static void SendMessage()
    {
        var factory = new ConnectionFactory();
        var msgqueue = "msg-queue";

        factory.Uri = new Uri("cloudmqp urlsi");

        using var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(msgqueue, true, false, false);//memory hariç tutulması(true), başka yerden ulaşılabilmesi (false), auto delete (false)


        var mesaj = "İlk Mesajım";
        var body = Encoding.UTF8.GetBytes(mesaj);

        channel.BasicPublish(String.Empty, msgqueue, null, body);//exchange => String.Empty(exchange göndermedik), routingKey (msgqueue ddeğişkeni), basic properties, body

        Console.WriteLine("Mesaj Gönderildi");
        Console.ReadLine();
    }
}