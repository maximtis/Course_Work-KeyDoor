using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Media;

namespace CourseWork_Kristina
{
    class NumberBoard
    {
        public WMPLib.WindowsMediaPlayer ButtonTone = new WMPLib.WindowsMediaPlayer(); // Sounds
        public WMPLib.WindowsMediaPlayer Error = new WMPLib.WindowsMediaPlayer();
        public WMPLib.WindowsMediaPlayer Open = new WMPLib.WindowsMediaPlayer();
        public WMPLib.WindowsMediaPlayer Close = new WMPLib.WindowsMediaPlayer();
        public WMPLib.WindowsMediaPlayer Call = new WMPLib.WindowsMediaPlayer();

        private List<string> check =new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
        public NumberBoard(string Panel)
        {

            
            Enter_Panel = Panel;
            ButtonTone.URL="ButtonTone.mp3";
            ButtonTone.settings.volume = 100;
            ButtonTone.controls.stop();
            Error.URL = "Error.mp3";
            Error.settings.volume = 100;
            Error.controls.stop();
            Open.URL = "Open.mp3";
            Open.settings.volume = 100;
            Open.controls.stop();
            Call.URL = "CallVoice.mp3";
            Call.settings.volume = 100;
            Call.controls.stop();
            Close.URL = "Close.mp3";
            Close.settings.volume = 100;
            Close.controls.stop();

        }
        public event EventHandler PanelChanged; //  событие -- текст главной панели изменен.
        public event EventHandler Possible;  //  Событие -Дверь открыта, можно изменять пароль

        public void CloseTone() //Звук закрытия дверей
        {
            Close.controls.play();
        }
        public void CallTone() // Звук вызова
        {
            Call.controls.play();
        }
        public void EndCallTone() //  Выключить звук вызова
        {
            Call.controls.stop();
        }

        public void RunTone() //  Звук клавиши
        {
            ButtonTone.controls.play();
        }
        public void RunError() // Звук ошибки
        {
            Error.controls.play();
        }
        public void RunOpen() // Звук открытия двери
        {
            Open.controls.play();
        }
        public bool IsError() // Проверка, если горит ошибка, то можно сразу заново набирать пароль
        {
            if (enter_Panel == "Error.")
            {
                enter_Panel = string.Empty;
                return true;
            }
            else
                return false;

        }
        private string enter_Panel; //  Переменная, хранящая Состояние экрана главной панели
        public string Enter_Panel // Окно Состояни экрана внешней панели.
        {
            get
            {
                return enter_Panel;
            }
            set
            {
                if (enter_Panel != "Open.")
                {

                    enter_Panel = value;
                    if (enter_Panel.Length == 4)
                    {
                        if (Possible != null) // Вызываем событие
                            Possible(this, new EventArgs());
                    }
                    if (PanelChanged != null) // Вызываем событие
                        PanelChanged(this, new EventArgs());

                }
                else
                {
                    if(!check.Contains(value))
                    enter_Panel = value;
                    if (PanelChanged != null) // Вызываем событие
                        PanelChanged(this, new EventArgs());
                }
            }
        }

        public MainWindow MainWindow
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        internal ProcessRun ProcessRun
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
