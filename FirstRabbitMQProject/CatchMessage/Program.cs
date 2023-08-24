// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://udcnxpyu:9YDgfobQNo9V8Ld1AogZ3iiVCyzpJXR1@gull.rmq.cloudamqp.com/udcnxpyu");


using var connection =  factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare("msg-queue", true, false, false);

var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume("msg-queue", true, consumer);

consumer.Received += Consumer_Received;

Console.ReadLine();

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    Console.WriteLine("Message: "+Encoding.UTF8.GetString(e.Body.ToArray()));
}