using System;

namespace Evoke.Components.Pipelines.Interfaces
{
    public interface IPipeline<TPayload>
    {
        IPipeline<TPayload> Pipe(IPipelineStage<TPayload> stage);

        TPayload Process(TPayload payload); 
    }
}
