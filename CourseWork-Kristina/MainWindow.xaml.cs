using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Reflection;

namespace CourseWork_Kristina
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NumberBoard MyOutPanel = new NumberBoard( string.Empty);
        PassLock MyLock = new PassLock();
        ProcessRun myRunSystem = new ProcessRun();
        Inner MyInPanel = new Inner();
        
        
        public MainWindow()
        {
            //myRunSystem.ChangeControlPassTimeUp += CorrectControlPassword_Run;
            MyLock.ControlPassChanged += PassChanged_Run;
            MyLock.FinalChangePass += PassChanged_Run;
            MyLock.EnableChange += PassChanged_Run;
            MyLock.AccesToChange += AccesControlToChangeCheckPassword_Run;
            MyLock.AccesControl += AccesControl_Run;
            myRunSystem.TimeUp += TimeUp_Run;
            myRunSystem.OpenTimeUp += OpenUp_Run;
            myRunSystem.CallTimeUp += CallUp_Run;
            myRunSystem.TalkTimeUp += TalkUp_Run;
            myRunSystem.Start();
            MyOutPanel.Possible += MyLock.IsCorrectPass;
            MyLock.Error += Error_Run;
            MyLock.CorrectPassword += CorrectPassword_Run;
            InitializeComponent();
            
            String path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "dver.jpg");
            BackGrid_EnterPanel.Background = new ImageBrush(new BitmapImage(new Uri(path)));
           
            path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "panel.jpg");
            KeyLockPanel.Background = new ImageBrush(new BitmapImage(new Uri(path)));
            
            path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "inwall.jpg");
            BackGrid_ProtectedLock.Background = new ImageBrush(new BitmapImage(new Uri(path)));
            
            path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "in.jpg");
            InPanel.Background = new ImageBrush(new BitmapImage(new Uri(path)));

            path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "room.jpg");
            Room_Panel.Background = new ImageBrush(new BitmapImage(new Uri(path)));
            MyOutPanel.PanelChanged += MainPanel_Changed;
            MyInPanel.PanelChanged += InPanel_Changed;
        }

        private void AccesControl_Run(object sender, EventArgs e)
        {
                MyLock.IsAccestToChange = true;
                MyOutPanel.Enter_Panel = "N.Pass";
                MyOutPanel.RunTone();
        }
        private void AccesControlToChangeCheckPassword_Run(object sender, EventArgs e)
        {
            MyLock.IsAccestToChangeControlPass = true;
            MyOutPanel.Enter_Panel = "N.Pass";
            MyOutPanel.RunTone();
            MyOutPanel.RunTone();
            //MyOutPanel.Enter_Panel = "Ok.";
            //myRunSystem.Ticks -= Timer_Ticks;
            MyLock.IsControlEnable = false;
            MyLock.IsAccestToChange = false;
            MyLock.PossiblePasswordChange = string.Empty;
            MyOutPanel.RunTone();
            // Смена пароля (таймер)
        }

        private void CorrectPassword_Run(object sender, EventArgs e)
        {
            myRunSystem.Ticks += Timer_Ticks_Open;
            //MyLock.CorrectPassword -= CorrectControlPassword_Run;
            MyOutPanel.Enter_Panel = "Open.";
            myRunSystem.Ticks -= Timer_Ticks;
            MyOutPanel.RunOpen();
            Room_Panel.Visibility = Visibility.Visible;
            //todo: Добавить звуковой сигнал, в метод, в Lock;
        }

        private void PassChanged_Run(object sender, EventArgs e)
        {
            MyOutPanel.RunTone();
            MyOutPanel.Enter_Panel = "Ok.";
            //myRunSystem.Ticks -= Timer_Ticks;
            MyLock.NewControlPass = string.Empty;
            MyLock.IsControlEnable = false;
            MyLock.IsAccestToChange = false;
            MyLock.IsControlPassChangeEnable = false;
            MyLock.IsAccestToChangeControlPass = false;
            MyLock.PossiblePasswordChange = string.Empty;
            MyLock.PossibleControlPassword = string.Empty;
            MyLock.PossibleControlChangePassword = string.Empty;
            MyOutPanel.RunTone();
            // Смена пароля (таймер)
        }

        private void Error_Run(object sender, EventArgs e)
        {
            MyLock.IsControlEnable = false;
            MyLock.IsAccestToChange = false;
            MyLock.PossibleControlPassword = string.Empty;
            MyOutPanel.Enter_Panel="Error.";
            MyOutPanel.RunError();
                 //todo: Добавить звуковой сигнал, в метод, в Lock;
        }
        private void TimeUp_Run(object sender, EventArgs e)
        {
            MyLock.IsAccestToChange = false;
            myRunSystem.IsCalled=false;
            MyOutPanel.Enter_Panel = string.Empty;
            MyInPanel.Enter_Panel = string.Empty;
            MyLock.PossiblePasswordChange = string.Empty;
            MyLock.PossibleControlPassword = string.Empty;
            MyLock.PossibleControlChangePassword = string.Empty;
            MyLock.NewControlPass = string.Empty;
            MyLock.IsControlEnable = false;
            MyLock.IsAccestToChange = false;
            myRunSystem.Ticks -= Timer_Ticks;
            myRunSystem.WaitTime_Reset();
        }
        private void OpenUp_Run(object sender, EventArgs e)
        {
            MyOutPanel.Enter_Panel = string.Empty;
            MyInPanel.Enter_Panel = string.Empty;
            MyOutPanel.CloseTone();
            myRunSystem.Ticks += Timer_Ticks;
            myRunSystem.Ticks -= Timer_Ticks_Open;
            Room_Panel.Visibility = Visibility.Hidden;
            MyLock.IsOpen = false;
            myRunSystem.IsCalled = false;
            myRunSystem.WaitTime_Reset();
        }
        private void CallUp_Run(object sender, EventArgs e)
        {
            MyOutPanel.Enter_Panel = string.Empty;
            myRunSystem.Ticks-=Timer_Ticks_Call;
            MyOutPanel.EndCallTone();
            //todo: Звук сброса.
            myRunSystem.CallWaitTime_Reset();
        }

        private void TalkUp_Run(object sender, EventArgs e)
        {
            myRunSystem.Ticks -= Timer_Ticks_Talk;
            MyOutPanel.Enter_Panel = string.Empty;
            MyInPanel.Enter_Panel = string.Empty;
            myRunSystem.Ticks += Timer_Ticks;
            //todo: Звук сброса.
            myRunSystem.CallWaitTime_Reset();
        }

        private void Timer_Ticks_CorrectControlPassword(object sender, EventArgs e)
        {
            myRunSystem.ChangeControlPassWait();
        }


        private void Timer_Ticks(object sender, EventArgs e)
        {
            myRunSystem.WaitTime_Next();
        }

        private void Timer_Ticks_Open(object sender, EventArgs e)
        {
            myRunSystem.Ticks -= Timer_Ticks;
            MyLock.IsOpen = true;
            MyInPanel.Enter_Panel = "Open.";
            myRunSystem.Open_Wait();
            
        }

        private void Timer_Ticks_Talk(object sender, EventArgs e)
        {
            MyOutPanel.Enter_Panel = "Deal.";
            MyInPanel.Enter_Panel = "Recive.";
            myRunSystem.IsDeal = true;
            MyOutPanel.EndCallTone();
            myRunSystem.TalkWaitTime_Next();
            
            //todo: звук вызова
        }

        private void Timer_Ticks_Call(object sender, EventArgs e)
        {
            myRunSystem.Ticks -= Timer_Ticks;
            MyOutPanel.Enter_Panel = "Call.";
            TextBox_InPanel.Text = "Call.";
            MyOutPanel.CallTone();
            myRunSystem.Call_Wait_Next();

            //todo: звук вызова
        }

       




        



        private void InPanel_Changed(object sender, EventArgs e)
        {
            TextBox_InPanel.Text = MyInPanel.Enter_Panel;
        }

        
        private void MainPanel_Changed(object sender, EventArgs e)
        {
            TextBlock_Enter_Panel.Text = MyOutPanel.Enter_Panel;
        }
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if(MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass+= 1.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 1.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                if (MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange += 1.ToString();
                }
                else
                    MyLock.PossibleControlPassword += 1.ToString();
            }
            else
            {
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 1.ToString();
                MyOutPanel.RunTone();
                myRunSystem.WaitTime_Reset();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if (MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass += 2.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 2.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                
                if(MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange+= 2.ToString();
                }
                else
                MyLock.PossibleControlPassword += 2.ToString();
            }
            else
            {
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 2.ToString();
                MyOutPanel.RunTone();
                myRunSystem.WaitTime_Reset();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }
        }

        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if (MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass += 3.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 3.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                
                if (MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange += 3.ToString();
                }
                else
                    MyLock.PossibleControlPassword += 3.ToString();
            }
            else
            {
                myRunSystem.WaitTime_Reset();
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 3.ToString();
                MyOutPanel.RunTone();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }
        }

        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if (MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass += 4.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 4.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                
                if (MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange += 4.ToString();
                }
                else
                    MyLock.PossibleControlPassword += 4.ToString();
            }
            else
            {
                myRunSystem.WaitTime_Reset();
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 4.ToString();
                MyOutPanel.RunTone();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }

        }

        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if (MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass += 5.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 5.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                
                if (MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange += 5.ToString();
                }
                else
                    MyLock.PossibleControlPassword += 5.ToString();
            }
            else
            {
                myRunSystem.WaitTime_Reset();
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 5.ToString();
                MyOutPanel.RunTone();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }

        }

        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if (MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass += 6.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 6.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                
                if (MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange += 6.ToString();
                }
                else
                    MyLock.PossibleControlPassword += 6.ToString();
            }
            else
            {
                myRunSystem.WaitTime_Reset();
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 6.ToString();
                MyOutPanel.RunTone();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }

        }

        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if (MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass += 7.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 7.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                
                if (MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange += 7.ToString();
                }
                else
                    MyLock.PossibleControlPassword += 7.ToString();
            }
            else
            {
                myRunSystem.WaitTime_Reset();
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 7.ToString();
                MyOutPanel.RunTone();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }

        }

        private void Button_8_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if (MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass += 8.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 8.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                
                if (MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange += 8.ToString();
                }
                else
                    MyLock.PossibleControlPassword += 8.ToString();
            }
            else
            {
                myRunSystem.WaitTime_Reset();
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 8.ToString();
                MyOutPanel.RunTone();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }
        }

        private void Button_9_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if (MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass += 9.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 9.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                
                if (MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange += 9.ToString();
                }
                else
                    MyLock.PossibleControlPassword += 9.ToString();
            }
            else
            {
                myRunSystem.WaitTime_Reset();
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 9.ToString();
                MyOutPanel.RunTone();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }
        }

        private void Button_0_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsControlPassChangeEnable)
            {
                if (MyLock.IsAccestToChangeControlPass)
                {
                    MyLock.NewControlPass += 1.ToString();
                }
                else
                    MyLock.PossibleControlChangePassword += 1.ToString();
                
            }
            else if (MyLock.IsControlEnable)
            {
                
                if (MyLock.IsAccestToChange)
                {
                    MyLock.PossiblePasswordChange += 0.ToString();
                }
                else
                    MyLock.PossibleControlPassword += 0.ToString();
            }
            else
            {
                myRunSystem.WaitTime_Reset();
                MyOutPanel.IsError();
                MyOutPanel.Enter_Panel += 0.ToString();
                MyOutPanel.RunTone();
                myRunSystem.Ticks -= Timer_Ticks;
                myRunSystem.Ticks += Timer_Ticks;
            }
        }

        private void Button_Call_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsOpen)
            {
                MyLock.IsControlPassChangeEnable = true;
                myRunSystem.IsCalled = true;
                MyOutPanel.RunTone();
                MyOutPanel.Enter_Panel = "C.Pass.";
                MyLock.IsControlPassChangeEnable=true;

            }
            else
            {
                myRunSystem.IsCalled = true;
                myRunSystem.Ticks -= Timer_Ticks_Call;
                myRunSystem.Ticks += Timer_Ticks_Call;
                MyOutPanel.RunTone();
            }
        }

        private void Button_Control_Click(object sender, RoutedEventArgs e)
        {
            if (MyLock.IsOpen)
            {
                if (myRunSystem.IsCalled)
                {
                    myRunSystem.Ticks -= Timer_Ticks_CorrectControlPassword;
                    myRunSystem.Ticks += Timer_Ticks_CorrectControlPassword;
                    MyOutPanel.Enter_Panel = "C.Pass.";
                    MyLock.IsControlPassChangeEnable = true;
                }
                else
                {
                    myRunSystem.Ticks -= Timer_Ticks_CorrectControlPassword;
                    myRunSystem.Ticks += Timer_Ticks_CorrectControlPassword;
                    MyLock.IsControlEnable = true;
                    MyOutPanel.Enter_Panel = "C.Pass.";
                }
            }
            
            else
            {
                MyOutPanel.RunTone();
                MyLock.Error_Button();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BackGrid_ProtectedLock.Visibility = Visibility.Hidden;
            InPanel.Visibility = Visibility.Hidden;
            Recive_Call.Visibility = Visibility.Hidden;
            Open_Lock.Visibility = Visibility.Hidden;
        }

        private void ToIn_Click(object sender, RoutedEventArgs e)
        {
            BackGrid_ProtectedLock.Visibility = Visibility.Visible;
            InPanel.Visibility = Visibility.Visible;
            Recive_Call.Visibility = Visibility.Visible;
            Open_Lock.Visibility = Visibility.Visible;

        }

        private void Open_Lock_Click(object sender, RoutedEventArgs e)
        {
            if (!MyLock.IsOpen)
                MyLock.Open_Button();
            else
            {
                TextBox_InPanel.Text = "Error.";
                MyOutPanel.RunError();
            }
        }

        private void Recive_Call_Click(object sender, RoutedEventArgs e)
        {

            if (myRunSystem.IsCalled)
            {
                myRunSystem.Ticks -= Timer_Ticks_Talk;
                myRunSystem.Ticks += Timer_Ticks_Talk;
                myRunSystem.Ticks -= Timer_Ticks_Call;

                
            }
            else
            {
                TextBox_InPanel.Text = "Error.";
                MyOutPanel.RunTone();
            }
        }
    }
}
