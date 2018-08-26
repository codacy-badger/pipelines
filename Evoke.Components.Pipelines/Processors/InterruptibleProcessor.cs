using System;

using System.Collections.Generic;
using Evoke.Components.Pipelines.Interfaces;

namespace Evoke.Components.Pipelines.Processors
{
    /// <summary>
    /// Pipeline processor that checks the payload after every stage against
    /// a check function. If said check function returns true the processor will
    /// interrupt the pipeline and return the payload.
    /// </summary>
    public class InterruptibleProcessor<TPayload> : IPipelineProcessor<TPayload>
    {
        private readonly Func<TPayload, bool> _check;

        /// <summary>
        /// Initializes a new instance of the Interruptible Processor.
        /// Takes a check Func that determines if the pipeline needs to be
        /// interrupted. The payload is checked after every stage against
        /// this given check Func.
        /// </summary>
        public InterruptibleProcessor(Func<TPayload, bool> check)
        {
            _check = check ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Process the payload through all the given stages in provided order
        /// and interrupt if the check Func might return true after a stage.
        /// </summary>
        public TPayload Process(TPayload payload, ICollection<IPipelineStage<TPayload>> stages)
        {
            foreach (IPipelineStage<TPayload> stage in stages)
            {
                payload = stage.Process(payload);

                if (true == _check.Invoke(payload))
                {
                    return payload;
                }
            }

            return payload;
        }
    }
}
