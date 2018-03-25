using System;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace MouseHeatmap.Collector.Tests
{
    internal class MockKeyboardMouseEvents : IKeyboardMouseEvents
    {
        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPress;
        public event KeyEventHandler KeyUp;
        public event MouseEventHandler MouseMove;
        public event EventHandler<MouseEventExtArgs> MouseMoveExt;
        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseDown;
        public event EventHandler<MouseEventExtArgs> MouseDownExt;
        public event MouseEventHandler MouseUp;
        public event EventHandler<MouseEventExtArgs> MouseUpExt;
        public event MouseEventHandler MouseWheel;
        public event EventHandler<MouseEventExtArgs> MouseWheelExt;
        public event MouseEventHandler MouseDoubleClick;
        public event MouseEventHandler MouseDragStarted;
        public event EventHandler<MouseEventExtArgs> MouseDragStartedExt;
        public event MouseEventHandler MouseDragFinished;
        public event EventHandler<MouseEventExtArgs> MouseDragFinishedExt;

        public void FireMouseMoved(int x,int y)
        {

            MouseMove?.Invoke(this, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}