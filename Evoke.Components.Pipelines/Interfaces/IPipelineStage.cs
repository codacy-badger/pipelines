using System;

namespace Evoke.Components.Pipelines.Interfaces
{
    public interface IPipelineStage<TPayload>
    {
        TPayload Process(TPayload payload);
    }
}
