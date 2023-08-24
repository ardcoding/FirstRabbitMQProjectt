
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

        factory.Uri = new Uri("amqps://udcnxpyu:9YDgfobQNo9V8Ld1AogZ3iiVCyzpJXR1@gull.rmq.cloudamqp.com/udcnxpyu");

        using var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare("msg-queue", true, false, false);

        var mesaj = "İlk Mesajım";
        var body = Encoding.UTF8.GetBytes(mesaj);

        channel.BasicPublish(String.Empty, "msg-queue", null, body);

        Console.WriteLine("Mesaj Gönderildi");
        Console.ReadLine();
    }
}