using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class SuplierViewModel : BaseViewModel
    {
        private ObservableCollection<Suplier> _List;
        public ObservableCollection<Suplier> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Suplier _SelectedItem;
        public Suplier SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    Address = SelectedItem.Address;
                    MoreInfo = SelectedItem.MoreInfo;
                    ContractDate = SelectedItem.ContracDate;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; OnPropertyChanged(); }
        }
        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; OnPropertyChanged(); }
        }
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; OnPropertyChanged(); }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }
        private string _MoreInfo;
        public string MoreInfo
        {
            get { return _MoreInfo; }
            set { _MoreInfo = value; OnPropertyChanged(); }
        }
        private DateTime? _ContractDate;
        public DateTime? ContractDate
        {
            get { return _ContractDate; }
            set { _ContractDate = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public SuplierViewModel()
        {
            List = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers.ToList());
            AddCommand = new RelayCommand<object>((p) =>
            {
                /*if (string.IsNullOrEmpty(DisplayName))
                    return false;
                var displayList = DataProvider.Ins.DB.Supliers.Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0) return false;*/
                return true;

            }, (p) =>
            {
                var Suplier = new Suplier() { DisplayName = DisplayName , Address = Address,Phone = Phone,Email = Email,ContracDate = ContractDate,MoreInfo = MoreInfo };
                DataProvider.Ins.DB.Supliers.Add(Suplier);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Suplier);
            });
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem==null)
                    return false;
                var displayList = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id);
                if (displayList == null || displayList.Count() != 0) return true;
                return false;

            }, (p) =>
            {
                var Suplier = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Suplier.DisplayName = DisplayName;
                Suplier.Address = Address;
                Suplier.Phone = Phone; 
                Suplier.Email = Email;
                Suplier.ContracDate = ContractDate;
                Suplier.MoreInfo = MoreInfo;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;



            });
        }

    }
}