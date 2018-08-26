using System;
using System.Collections.Generic;
using Moq;
using Evoke.Components.Pipelines.Interfaces;
using Evoke.Components.Pipelines.Processors;
using Xunit;

namespace Evoke.Components.Pipelines.Tests.Processors
{
    public class InterruptibleProcessorTest
    {
        private static bool UninterruptingCheck(int param) {
            return false;
        }

        private static bool InterruptingCheck(int param) {
            return true;
        }

        [Fact]
        public void ItShouldCallAllStagesIfNotInterrupted()
        {
            // Given we have a list of stages
            List<IPipelineStage<int>> stages = new List<IPipelineStage<int>>();

            Mock<IPipelineStage<int>> stageMockOne = new Mock<IPipelineStage<int>>();
            stageMockOne.Setup(stage => stage.Process(1)).Returns(2);
            stages.Add(stageMockOne.Object);

            Mock<IPipelineStage<int>> stageMockTwo = new Mock<IPipelineStage<int>>();
            stageMockTwo.Setup(stage => stage.Process(2)).Returns(3);
            stages.Add(stageMockTwo.Object);

            // And we run the processor with given stages
            InterruptibleProcessor<int> processor = new InterruptibleProcessor<int>(UninterruptingCheck);
            int result = processor.Process(1, stages);

            // both stages should have been called
            stageMockOne.Verify(stage => stage.Process(1));
            stageMockTwo.Verify(stage => stage.Process(2));

            // And the result should be the output of the last stage
            Assert.True(3 == result);
        }

        [Fact]
        public void ItShouldStopCallingStagesIfInterrupted()
        {
            // Given we have a list of stages
            List<IPipelineStage<int>> stages = new List<IPipelineStage<int>>();

            Mock<IPipelineStage<int>> stageMockOne = new Mock<IPipelineStage<int>>();
            stageMockOne.Setup(stage => stage.Process(1)).Returns(2);
            stages.Add(stageMockOne.Object);

            Mock<IPipelineStage<int>> stageMockTwo = new Mock<IPipelineStage<int>>();
            stageMockTwo.Setup(stage => stage.Process(2)).Returns(3);
            stages.Add(stageMockTwo.Object);

            // And we run the processor with given stages
            InterruptibleProcessor<int> processor = new InterruptibleProcessor<int>(InterruptingCheck);
            int result = processor.Process(1, stages);

            // then the first stage should have been called
            stageMockOne.Verify(stage => stage.Process(1));

            // And the second stage should have never been called
            stageMockTwo.Verify(stage => stage.Process(It.IsAny<int>()), Times.Never());

            // And the result should be the output of the first stage
            // since the pipeline was interrupted after that stage
            Assert.True(2 == result);
        }
    }
}
