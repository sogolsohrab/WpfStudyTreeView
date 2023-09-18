using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfStudyTreeView.Library
{
    public class CustomOrdering
    {

        public void Ordering(ObservableCollection<TreeModel> collection)
        {
            List<TreeModel> listOfCollection = collection.OrderBy(x => PadNumbers(x.Title)).ToList();

            for (int i = 0; i < listOfCollection.Count; i++)
            {
                collection.Move(collection.IndexOf(listOfCollection[i]), i);
            }
        }

        private string PadNumbers(string input)
        {
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }
    }
}
