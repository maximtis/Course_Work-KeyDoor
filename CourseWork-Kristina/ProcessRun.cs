using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_Kristina
{
        class ProcessRun
        {
        
        public System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        public event EventHandler Ticks; // Событие секунды
        public event EventHandler TimeUp; // Событие сброса состояний
        public event EventHandler CallTimeUp; // Событие сброса вызова, по времени
        public event EventHandler OpenTimeUp; // Событие закрытия двери, по окончанию времени
        public event EventHandler TalkTimeUp; // Событие окончания разговора
        public event EventHandler ChangeControlPassTimeUp; // Ожидание ввода нового пароля, если нет, то сброс состояния
        public bool IsCalled { get; set; } // Если совершается звонок
        public bool IsDeal { get; set; } // Если совершается разговор
        public bool IsControlChangeEnable { get; set; } // Если смена КОнтр-Пароля возможна

        public int WaitTime;
        public int CallWaitTime;
        public int OpenWaitTime;
        public int TalkWaitTime;
        public int ChangeCPassTime;
        public ProcessRun()
        {
            WaitTime=0;
            CallWaitTime=0;
            OpenWaitTime=0;
        }
        public void Start()
        {
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            Timer.Tick += Timer_Ticks;
        }

        private void Timer_Ticks(object sender, EventArgs e)
        {
            if (Ticks != null)
                Ticks(this, new EventArgs());
        }
        public void ChangeControlPassRun()
        {
            if (ChangeControlPassTimeUp != null)
                ChangeControlPassTimeUp(this, new EventArgs());
        }
        public void ChangeControlPassWait()
        {
            if (ChangeCPassTime == 10)
            {
                if (ChangeControlPassTimeUp != null)
                    ChangeControlPassTimeUp(this, new EventArgs());
                ChangeCPassTime = 0;
            }
            else
                ChangeCPassTime++;
        }
        public void Open_Wait()
        {
            if (OpenWaitTime == 6)
            {
                if (OpenTimeUp != null)
                    OpenTimeUp(this, new EventArgs());
                OpenWaitTime = 0;
            }
            else
                OpenWaitTime++;
        }
        public void Call_Wait_Next()
        {
            if (CallWaitTime == 7)
            {
                if (CallTimeUp != null)
                    CallTimeUp(this, new EventArgs());
                CallWaitTime = 0;
            }
            else
                CallWaitTime++;
        }
        public void WaitTime_Next()
        {
            if (WaitTime == 3)
            {
                if (TimeUp != null)
                    TimeUp(this, new EventArgs());
                WaitTime = 0;
            }
            else
                WaitTime++;
        }

        public void TalkWaitTime_Next()
        {
            if (TalkWaitTime == 6)
            {
                if (TalkTimeUp != null)
                    TalkTimeUp(this, new EventArgs());
                TalkWaitTime = 0;
            }
            else
                TalkWaitTime++;
        }

        public void WaitTime_Reset()
        {
            WaitTime = 0;
        }
        public void OpenWaitTime_Reset()
        {
            OpenWaitTime = 0;
        }
        public void CallWaitTime_Reset()
        {
            CallWaitTime = 0;
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
    }
}
