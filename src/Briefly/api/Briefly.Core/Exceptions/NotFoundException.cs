using System.Collections.ObjectModel;
using System.Net;

namespace Briefly.Core.Exceptions;

public class NotFoundException : BrieflyException
{
    public NotFoundException(string message)
        : base(message, new Collection<string>(), HttpStatusCode.NotFound)
    {
    }
}