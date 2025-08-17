using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPREAGLE.Extensions
{
    public static class ControlExtensions
    {
        private static readonly Type[] ContainerTypes =
    {
        typeof(Panel),
        typeof(GroupBox),
        typeof(TabPage),
        typeof(DevExpress.XtraEditors.PanelControl),
        typeof(DevExpress.XtraEditors.GroupControl)
    };

        public static bool IsContainer(this Control control) =>
            ContainerTypes.Any(t => t.IsInstanceOfType(control));
    }
}
