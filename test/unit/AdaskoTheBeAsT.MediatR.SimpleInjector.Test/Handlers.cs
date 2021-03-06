using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AdaskoTheBeAsT.MediatR.SimpleInjector.Test
{
#pragma warning disable CA1812
#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1202 // Elements should be ordered by access
#pragma warning disable MA0048 // File name must match type name
    public class Ping : IRequest<Pong>
    {
        public string? Message { get; set; }

        public Action<Ping>? ThrowAction { get; set; }
    }

    public sealed class DerivedPing : Ping
    {
    }

    public class Pong
    {
        public string? Message { get; set; }
    }

    public sealed class Zing : IRequest<Zong>
    {
        public string? Message { get; set; }
    }

    public sealed class Zong
    {
        public string? Message { get; set; }
    }

    public sealed class Ding : IRequest
    {
        public string? Message { get; set; }
    }

    public sealed class Pinged : INotification
    {
    }

    internal sealed class InternalPing : IRequest
    {
    }

    public class GenericHandler : INotificationHandler<INotification>
    {
        public Task Handle(INotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class DingAsyncHandler : IRequestHandler<Ding>
    {
        public Task<Unit> Handle(Ding request, CancellationToken cancellationToken) => Unit.Task;
    }

    public class PingedHandler : INotificationHandler<Pinged>
    {
        public Task Handle(Pinged notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class PingedAlsoHandler : INotificationHandler<Pinged>
    {
        public Task Handle(Pinged notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class Logger
    {
        public IList<string> Messages { get; } = new List<string>();
    }

    public class PingHandler : IRequestHandler<Ping, Pong>
    {
        private readonly Logger _logger;

        public PingHandler(Logger logger)
        {
            _logger = logger;
        }

        public Task<Pong> Handle(Ping request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.Messages.Add("Handler");

            request.ThrowAction?.Invoke(request);

            return Task.FromResult(new Pong { Message = request.Message + " Pong" });
        }
    }

    public class DerivedPingHandler : IRequestHandler<DerivedPing, Pong>
    {
        private readonly Logger _logger;

        public DerivedPingHandler(Logger logger)
        {
            _logger = logger;
        }

        public Task<Pong> Handle(DerivedPing request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _logger.Messages.Add("Handler");
            return Task.FromResult(new Pong { Message = $"Derived{request.Message} Pong" });
        }
    }

    public class ZingHandler : IRequestHandler<Zing, Zong>
    {
        private readonly Logger _output;

        public ZingHandler(Logger output)
        {
            _output = output;
        }

        public Task<Zong> Handle(Zing request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _output.Messages.Add("Handler");
            return Task.FromResult(new Zong { Message = request.Message + " Zong" });
        }
    }

    public class DuplicateTest : IRequest<string>
    {
    }

    public class DuplicateHandler1 : IRequestHandler<DuplicateTest, string>
    {
        public Task<string> Handle(DuplicateTest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(nameof(DuplicateHandler1));
        }
    }

#pragma warning disable S125 // Sections of code should not be commented out
    /*
     * SimpleInjector do not allow to register duplicates of given implementation
     * it can be registered by using container.Collection.Register but then it can be only
     * retrieved as IEnumerable
        public class DuplicateHandler2 : IRequestHandler<DuplicateTest, string>
        {
            public Task<string> Handle(DuplicateTest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(nameof(DuplicateHandler2));
            }
        }
        */
#pragma warning restore S125 // Sections of code should not be commented out

    internal sealed class InternalPingHandler : IRequestHandler<InternalPing>
    {
        public Task<Unit> Handle(InternalPing request, CancellationToken cancellationToken) => Unit.Task;
    }

    internal sealed class MyCustomMediator : IMediator
    {
        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            throw new NotSupportedException();
        }
    }
#pragma warning restore MA0048 // File name must match type name
#pragma warning restore SA1202 // Elements should be ordered by access
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore CA1812
}
