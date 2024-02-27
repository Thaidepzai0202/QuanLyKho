using QuanLyKho.Model;
using System;
using System.Collections.ObjectModel;

namespace QuanLyKho.ViewModel
{
    public class ObservableCollectionExtensions<T>
    {
        public static implicit operator ObservableCollectionExtensions<T>(ObservableCollection<Unit> v)
        {
            throw new NotImplementedException();
        }
    }
}