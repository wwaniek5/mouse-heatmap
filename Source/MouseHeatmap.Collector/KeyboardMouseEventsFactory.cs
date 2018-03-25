using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace MouseHeatmap.Collector
{

    public interface IKeyboardMouseEventsFactory {
        IKeyboardMouseEvents Create();
    }

    public class KeyboardMouseEventsFactory: IKeyboardMouseEventsFactory
    {
        public IKeyboardMouseEvents Create()
        {
            return Hook.GlobalEvents();

        }

       
    }
}