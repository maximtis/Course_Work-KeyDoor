using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_Kristina
{
    class PassLock
    {
        public PassLock()
        { 
            password = "1234"; // Пароль для открытия 
            control_pass = "1111"; //  Пароль для "КОНТРОЛЯ"
            IsOpen = false; 
            EnableChange += EnableChanges_Run;
            FinalChangePass += FinalChangePass_Run;
        }
        
        public event EventHandler CorrectPassword; // Событие, если введен правильный пароль
        public event EventHandler Error;// СОбытие, если Ошибка.

  
        // Пароль
        public bool IsControlEnable { get; set; }  // Состояние кнопи "КОНТОЛЬ"

        public event EventHandler AccesControl; // Если введен КонтрПароль верно

        private string possibleControlPassword;
        public string PossibleControlPassword // Свойство, позволяющее проверять Корректность ввода КОнтрПароля
        {
            get
            {
                return possibleControlPassword;
            }
            set
            {
                possibleControlPassword = value;
                if (possibleControlPassword.Length == control_pass.Length)
                {
                    if (possibleControlPassword == control_pass)
                    {
                        if (AccesControl != null) // Вызываем событие
                            AccesControl(this, new EventArgs());
                    }else
                    if (Error != null) // Вызываем событие
                        Error(this, new EventArgs());
                }
                
            }
        }

        public bool IsAccestToChange { get; set; }  // возможность замены Пароля

        public event EventHandler EnableChange; // Событие - Если пароль изменен

        private string possiblePasswordChange; // Ввод, нового пароля
        public string PossiblePasswordChange
        {
            get
            {
                return possiblePasswordChange;
            }
            set
            {
                possiblePasswordChange = value;
                if (possiblePasswordChange.Length == password.Length)
                {
                    if (EnableChange != null) // Вызываем событие
                        EnableChange(this, new EventArgs());
                }
            }
        }
        public void EnableChanges_Run(object sender, EventArgs e) // Метод, который заменяет Пароль
        {
            IsControlEnable = false; // Если пароль изменен, то "КОНТРОЛЬ" (как-бы) Можно отпустить
            password = possiblePasswordChange;
        }


        //-----------------------------------------------------------------------------------------------

        //Контр Пароль 
        public bool IsControlPassChangeEnable { get; set; } // Состояние смены Контр-Пароля

        public event EventHandler AccesToChange; // Событие, если пароль можно изменить

        private string possibleControlChangePassword; 
        public string PossibleControlChangePassword  // Проверка на корректность ввода, сущесвующего Контр-Пароля
        {
            get
            {
                return possibleControlChangePassword;
            }
            set
            {
                possibleControlChangePassword = value;
                if (possibleControlChangePassword.Length == control_pass.Length)
                {
                    if (possibleControlChangePassword == control_pass)
                    {
                        if (AccesToChange != null) // Вызываем событие
                            AccesToChange(this, new EventArgs());
                    }
                    else
                        if (Error != null) // Вызываем событие
                            Error(this, new EventArgs());
                }

            }
        }

        public bool IsAccestToChangeControlPass { get; set; } // возможность замены Контр-Пароля

        public event EventHandler FinalChangePass;  // событие замены

        private string newControlPass;  //  новый пароль
        public string NewControlPass
        {
            get
            {
                return newControlPass;
            }
            set
            {
                newControlPass = value;
                if (newControlPass.Length == control_pass.Length)
                {
                    if (FinalChangePass != null) // Вызываем событие
                        FinalChangePass(this, new EventArgs());
                }
            }
        }

        public void FinalChangePass_Run(object sender, EventArgs e)
        {
            
            IsControlPassChangeEnable = false; // Кнопка Вызов "отжимается"
            control_pass = NewControlPass;
            
            //if (EnableChange != null) // Вызываем событие
                      // EnableChange(this, new EventArgs());
        }

        private string password; // Существующий Пароль
  
        public event EventHandler PassChanged; // Событие - Если Пароль был изменен
        public bool IsOpen { get; set; } // Если дверь открыта
        public string Password  // Переменная хранящаяю существующий пароль
        {
            get
            {
                return password;
            }
            set
            {
                password=value;
                if (PassChanged != null) // Вызываем событие
                    PassChanged(this, new EventArgs());
            }

        }

        public event EventHandler ControlPassChanged; // Событие если Контр-Пароль Изменен

        private string control_pass; // Переменная Хранящая Контр-Пароль
        public string Control_Pass
        {
            get
            {
                return control_pass;
            }
            set
            {
                control_pass = value;
                if (ControlPassChanged != null) // Вызываем событие
                    ControlPassChanged(this, new EventArgs());
            }
        }
   

        public void Open_Button() // Событие, позволяет открыть дверь из нутри
        {
            if (CorrectPassword != null) // Вызываем событие
                CorrectPassword(this, new EventArgs());
        }
        public void Error_Button() // Позволяет вызвать ошибку
        {
            if (Error != null) // Вызываем событие
                Error(this, new EventArgs());
        }


        public void IsCorrectPass(object sender, EventArgs e)  // Обработчки состояни ввода пароля- Открывающий дверь
        {
            if (((NumberBoard)sender).Enter_Panel == password)
            {
                if (CorrectPassword != null) // Вызываем событие
                    CorrectPassword(this, new EventArgs());
            }
            else
            {
                if (Error != null) // Вызываем событие
                    Error(this, new EventArgs());
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

        internal Inner Inner
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
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
