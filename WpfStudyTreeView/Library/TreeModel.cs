using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfStudyTreeView.Properties;

namespace WpfStudyTreeView.Library
{
    public class TreeModel : PropertyChangedBase
    {
        private string title;
        public string Title
        {
            get => this.title;
            set
            {
                this.title = value;
                OnPropertyChanged("Title");
            }
        }

        public string DisplayedImagePath { get; set; }

        public ObservableCollection<TreeModel> Items { get; set; }

        public CollectionView ItemsView { get; set; }

        public TreeModel(string value, string displayedImagePath)
        {
            Items = new ObservableCollection<TreeModel>();
            ItemsView = new ListCollectionView(Items);
            Title = value;
            DisplayedImagePath = displayedImagePath;
        }
    }
}
