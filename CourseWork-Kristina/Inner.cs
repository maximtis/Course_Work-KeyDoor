using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_Kristina
{
    class Inner : ProcessRun
    {
        private string enter_Panel; //Хранит текст Окна внутренней панели
        public event EventHandler PanelChanged; //Событие - Если текст окна внут.панели был изменен 


        public string Enter_Panel
        {
            get
            {
                return enter_Panel;
            }
            set
            {
                enter_Panel = value;
                if (PanelChanged != null) // Вызываем - Если текст окна внут.панели был изменен 
                    PanelChanged(this, new EventArgs());
            }
        }

        internal NumberBoard NumberBoard
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
