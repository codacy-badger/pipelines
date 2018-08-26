using System;
using System.Collections.Generic;

namespace Evoke.Components.Pipelines.Interfaces
{
    public interface IPipelineProcessor<TPayload>
    {
        TPayload Process(TPayload payload, ICollection<IPipelineStage<TPayload>> stages);
    }
}
