using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Go
{
    class GUI_Presenter
    {
        private readonly IView View;
        public GUI_Presenter (IView View)
        {
            this.View = View;
        }
    }
}
