#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator;

/// <summary>
/// Represents a request that returns a response of type <typeparamref name="TResponse"/>.
/// </summary>
public interface IRequest<out TResponse>
{
    /// <summary>
    /// Marker property to ensure <typeparamref name="TResponse"/> is referenced.
    /// </summary>
    //static abstract TResponse? ResponseType { get; }
}
#nullable restore
