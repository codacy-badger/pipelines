using System;

namespace Evoke.Components.Pipelines.Interfaces
{
    public interface IPipeline<TPayload>
    {
        TPayload Process(TPayload payload); 
    }
}
