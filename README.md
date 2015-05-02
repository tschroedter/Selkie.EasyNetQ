# Selkie.Windsor

The Selkie.EasyNetQ project creates a NuGet package. The package is an extension to EasyNetQ and provides Some small extensions and installers for EasyNetQ. The package contains an extension to subscribe to messages async, a logger, classes to support simple usage of the IConsume interface.  

Please, check the provided examples for more details.

# Examples:

SelkieMessageConsumer
```CS
    public class MessageAConsumer : SelkieMessageConsumer <MessageA>
    {
        public override void Handle(MessageA message)
        {
            Console.WriteLine("==> Consumed message {0}...",
                              message.GetType()
                                     .Name);
        }
    }
```

SelkieMessageConsumerAsync
```CS
    public class MessageBConsumerAsync : SelkieMessageConsumerAsync <MessageB>
    {
        public override void Handle(MessageB message)
        {
            Console.WriteLine("==> Consumed message {0}...",
                              message.GetType()
                                     .Name);
        }
    }
```

BusExtensions

    todo

# Selkie
Selkie.EasyNetQ is part of the Selkie project which is based on Castle Windsor and EasyNetQ. The main goal of the Selkie project is to calculate and displays the shortest path for a boat travelling along survey lines from point A to B. The algorithm takes into account the minimum required turn circle of a vessel required to navigate from one line to another.

The project started as a little ant colony optimization application. Over time the application grew and was split up into different services which communicate via RabbitMQ. The whole project is used to try out TDD, BDD, DRY and SOLID.

# Selkie projects:

* Selkie ACO
* Selkie Common
* [Selkie EasyNetQ](https://github.com/tschroedter/Selkie.EasyNetQ)
* Selkie Geometry
* Selkie NUnit Extensions
* Selkie Racetrack
* Selkie Services ACO
* Selkie Service Common
* Selkie Services Lines
* Selkie Services Monitor
* Selkie Services Racetracks
* Selkie Web
* [Selkie Windsor](https://github.com/tschroedter/Selkie.Windsor)
* Selkie WPF
* Selkie XUnit Extensions
 

