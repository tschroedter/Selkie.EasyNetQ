# Selkie.EasyNetQ

The Selkie.EasyNetQ project creates a NuGet package. The package is an extension to EasyNetQ and provides some small extensions and installers for EasyNetQ. The package contains an extension to subscribe to messages async, a logger, classes to support simple usage of the IConsume interface.  

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
```CS
    public void SubscribeHandlerCallsBusTest()
    {
        IBus bus = Substitute.For <IBus>();
        ILogger logger = Substitute.For <ILogger>();

        bus.SubscribeHandlerAsync <TestMessage>(logger,
                                                "TestId",
                                                TestHandler);

        bus.Received()
           .SubscribeAsync("TestId",
                           Arg.Any <Func <TestMessage, Task>>());
    }
```

# Selkie
Selkie.EasyNetQ is part of the Selkie project which is based on Castle Windsor and EasyNetQ. The main goal of the Selkie project is to calculate and displays the shortest path for a boat travelling along survey lines from point A to B. The algorithm takes into account the minimum required turn circle of a vessel required to navigate from one line to another.

The project started as a little ant colony optimization application. Over time the application grew and was split up into different services which communicate via RabbitMQ. The whole project is used to try out TDD, BDD, DRY and SOLID.

### Selkie Projects

* [Selkie](https://github.com/tschroedter/Selkie)
* [Selkie.Aco](https://github.com/tschroedter/Selkie.Aco)
* [Selkie Common](https://github.com/tschroedter/Selkie.Common)
* [Selkie EasyNetQ](https://github.com/tschroedter/Selkie.EasyNetQ)
* [Selkie Geometry](https://github.com/tschroedter/Selkie.Geometry)
* [Selkie NUnit Extensions](https://github.com/tschroedter/Selkie.NUnit.Extensions)
* [Selkie Racetrack](https://github.com/tschroedter/Selkie.Racetrack)
* [Selkie Services ACO](https://github.com/tschroedter/Selkie.Services.Aco)
* [Selkie Services ACO Common](https://github.com/tschroedter/Selkie.Services.Aco.Common)
* [Selkie Services Common](https://github.com/tschroedter/Selkie.Services.Common)
* [Selkie Services Lines](https://github.com/tschroedter/Selkie.Services.Lines)
* [Selkie Services Lines Common](https://github.com/tschroedter/Selkie.Services.Lines.Common)
* [Selkie Services Monitor](https://github.com/tschroedter/Selkie.Services.Monitor)
* [Selkie Services Racetracks](https://github.com/tschroedter/Selkie.Services.Racetracks)
* [Selkie Services Racetracks Common](https://github.com/tschroedter/Selkie.Services.Racetracks.Common)
* [Selkie.Scripts (Bamboo)](https://github.com/tschroedter/Selkie.Scripts)
* [Selkie.Tools (Bamboo)](https://github.com/tschroedter/Selkie.Tools)
* Selkie Web
* [Selkie Windsor](https://github.com/tschroedter/Selkie.Windsor)
* [Selkie WPF](https://github.com/tschroedter/Selkie.WPF)
* [Selkie XUnit Extensions](https://github.com/tschroedter/Selkie.XUnit.Extensions)