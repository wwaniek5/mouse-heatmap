using Gma.System.MouseKeyHook;

namespace MouseHeatmap.Collector.Tests
{
    internal class MockKeyboardMouseEventsFactory : IKeyboardMouseEventsFactory
    {
        private MockKeyboardMouseEvents mockEvents;

        public MockKeyboardMouseEventsFactory(MockKeyboardMouseEvents mockEvents)
        {
            this.mockEvents = mockEvents;
        }

        public IKeyboardMouseEvents Create()
        {
            return mockEvents;
        }
    }
}