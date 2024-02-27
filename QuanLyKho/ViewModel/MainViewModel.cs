using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set {  _TonKhoList = value; OnPropertyChanged(); } }

        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand {  get; set; }
        public ICommand ObjectCommand {  get; set; }
        public ICommand SuplierCommand {  get; set; }
        // mọi thứ xử lý sẽ nằm trong này
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLoaded = true;

                if (p == null) return;

                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                //p.Show();

                var loginVM = loginWindow.DataContext as LoginViewModel;

                if(loginVM.IsLogin) {
                    p.Show();
                    LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
            });

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UnitWindow wd = new UnitWindow(); wd.ShowDialog(); });
            ObjectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectWindow wd = new ObjectWindow(); wd.ShowDialog(); });
            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SuplierWindow wd = new SuplierWindow(); wd.ShowDialog(); });


        }

        void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<TonKho>();
            var obbjectList = DataProvider.Ins.DB.Objects;
            int i = 0;
            foreach(var item  in obbjectList)
            {
                var inputList = DataProvider.Ins.DB.InputInfoes.Where(p=>p.IdObject == item.Id);
                var outputList = DataProvider.Ins.DB.OutputInfoes.Where(p=>p.IdObject == item.Id);
                var sumInput = 0;
                var sumOutput = 0;

                if(inputList != null)
                {
                    sumInput = (int)inputList.Sum(p => p.Count);
                }
                if(outputList != null)
                {
                    sumOutput = (int)outputList.Sum(p => p.Count);
                }
                TonKho tonKho = new TonKho();
                tonKho.STT = i;
                tonKho.Count = sumInput-sumOutput;
                tonKho.Object = item;
                TonKhoList.Add(tonKho);
                i++;
            }
            
        }
    }
}
