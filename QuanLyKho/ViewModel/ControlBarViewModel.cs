using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCommand { get; set; }
        #endregion

        public ControlBarViewModel() 
        {
            CloseWindowCommand = new RelayCommand<UserControl>((p) => { return p==null ? false : true ; },(p) => { GetWindownParent(p); });
        }

        void GetWindownParent(UserControl p)
        {
            FrameworkElement t = p.Parent as FrameworkElement;
        }
    }
}
