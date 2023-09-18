using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using WpfStudyTreeView.Library;

namespace WpfStudyTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public TreeViewModel MyViewModel
        {
            get { return DataContext as TreeViewModel; }
            set { DataContext = value; }
        }
        public TreeModel Wells { get; set; }
        public TreeModel Rocks { get; set; }
        public TreeModel Polygones { get; set; }
        public TreeModel WellStrategies { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            StateChanged += MainWindowStateChangeRaised;
            LoadTree();
        }

        #region Private Methods
        private void LoadTree()
        {
            MyViewModel = new TreeViewModel();

            // Wells
            Wells = new TreeModel("Wells", @"/Resources/Wells.jpg");
            MyViewModel.Items.Add(Wells);
            Wells.Items.Add(new TreeModel("R1_W012", @"/Resources/WellsM.jpg"));
            Wells.Items.Add(new TreeModel("R2_W1", @"/Resources/WellsM.jpg"));
            Wells.Items.Add(new TreeModel("R1_W1", @"/Resources/WellsM.jpg"));
            Wells.Items.Add(new TreeModel("R1_W7", @"/Resources/WellsM.jpg"));
            Wells.Items.Add(new TreeModel("R2_W11", @"/Resources/WellsM.jpg"));
            Wells.Items.Add(new TreeModel("R2_W03", @"/Resources/WellsM.jpg"));
            Wells.Items.Add(new TreeModel("R2_W123456789123456789", @"/Resources/WellsM.jpg"));

            // Rocks
            Rocks = new TreeModel("Rocks", @"/Resources/Rocks.jpg");
            MyViewModel.Items.Add(Rocks);
            Rocks.Items.Add(new TreeModel("R4", @"/Resources/RocksM.jpg"));
            Rocks.Items.Add(new TreeModel("R2", @"/Resources/RocksM.jpg"));
            Rocks.Items.Add(new TreeModel("R1", @"/Resources/RocksM.jpg"));
            Rocks.Items.Add(new TreeModel("R3", @"/Resources/RocksM.jpg"));
            Rocks.Items.Add(new TreeModel("R5", @"/Resources/RocksM.jpg"));
            Rocks.Items.Add(new TreeModel("R6", @"/Resources/RocksM.jpg"));
            Rocks.Items.Add(new TreeModel("R7", @"/Resources/RocksM.jpg"));

            // Polygones
            Polygones = new TreeModel("Polygones", @"/Resources/Polygones.jpg");
            MyViewModel.Items.Add(Polygones);

            // WellStrategies
            WellStrategies = new TreeModel("WellStrategies", @"/Resources/WellStrategies.jpg");
            MyViewModel.Items.Add(WellStrategies);
            WellStrategies.Items.Add(new TreeModel("WS1", @"/Resources/WellStrategiesM.jpg"));
            WellStrategies.Items.Add(new TreeModel("WS2", @"/Resources/WellStrategiesM.jpg"));
            WellStrategies.Items.Add(new TreeModel("WS3", @"/Resources/WellStrategiesM.jpg"));
            WellStrategies.Items.Add(new TreeModel("WS4", @"/Resources/WellStrategiesM.jpg"));
            WellStrategies.Items.Add(new TreeModel("WS5", @"/Resources/WellStrategiesM.jpg"));
            WellStrategies.Items.Add(new TreeModel("WS6", @"/Resources/WellStrategiesM.jpg"));

            // Ordering
            SortTree();
        }
        
        private void SortTree()
        {
            CustomOrdering customOrdering = new CustomOrdering();
            customOrdering.Ordering(Wells.Items);
            customOrdering.Ordering(Rocks.Items);
            customOrdering.Ordering(WellStrategies.Items);
            customOrdering.Ordering(MyViewModel.Items);
        }

        private void ControlPanelsVisibilty(Border selectedBorder)
        {
            Border visibleBorder;
            List<Border> bordersList = new List<Border>() { HomePanel, RenamePanel, DeletePanel };
            foreach (var border in bordersList)
            {
                border.Visibility = Visibility.Collapsed;
            }


            if (selectedBorder == RenamePanel)
            {
                visibleBorder = RenamePanel;
            }
            else if (selectedBorder == DeletePanel)
            {
                visibleBorder = DeletePanel;
            }
            else
            {
                visibleBorder = HomePanel;
            }

            visibleBorder.Visibility = Visibility.Visible;
        }
        #endregion Private Methods


        #region Events

        #region SelectedItemChanged
        private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MyViewModel.SelectedItem = e.NewValue as TreeModel;
        }
        #endregion SelectedItemChanged

        #region Delete Events
        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ControlPanelsVisibilty(DeletePanel);
        }

        private void btnAbortDelete_Click(object sender, RoutedEventArgs e)
        {
            ControlPanelsVisibilty(HomePanel);
        }

        private void btnProceesDelete_Click(object sender, RoutedEventArgs e)
        {

            if (
                MyViewModel.SelectedItem.DisplayedImagePath == @"/Resources/Wells.jpg" ||
                MyViewModel.SelectedItem.DisplayedImagePath == @"/Resources/WellStrategies.jpg" ||
                MyViewModel.SelectedItem.DisplayedImagePath == @"/Resources/Polygones.jpg" ||
                MyViewModel.SelectedItem.DisplayedImagePath == @"/Resources/Rocks.jpg")
            {
                // This is because of duplicate renaming error
                if (MyViewModel.SelectedItem.DisplayedImagePath == @"/Resources/Wells.jpg")
                {
                    Wells.Items.Clear();
                }
                else if (MyViewModel.SelectedItem.DisplayedImagePath == @"/Resources/WellStrategies.jpg")
                {
                    WellStrategies.Items.Clear();
                }
                else if (MyViewModel.SelectedItem.DisplayedImagePath == @"/Resources/Rocks.jpg")
                {
                    Rocks.Items.Clear();
                }
                MyViewModel.Items.Remove(MyViewModel.SelectedItem);
            }
            else
            {
                Wells.Items.Remove(MyViewModel.SelectedItem);
                Rocks.Items.Remove(MyViewModel.SelectedItem);
                WellStrategies.Items.Remove(MyViewModel.SelectedItem);
                Polygones.Items.Remove(MyViewModel.SelectedItem);
            }

            ControlPanelsVisibilty(HomePanel);

        }
        #endregion Delete Events

        #region Rename Events
        private void renameMenuItem_Click(object sender, RoutedEventArgs e)
        {

            ControlPanelsVisibilty(RenamePanel);

            if (MyViewModel.SelectedItem.Title != null)
            {
                txtRename.Text = MyViewModel.SelectedItem.Title;
            }
            else
            {
                txtRename.Text = "";
            }

        }

        private void btnAbortRename_Click(object sender, RoutedEventArgs e)
        {
            lblError.Content = string.Empty;
            ControlPanelsVisibilty(HomePanel);
        }

        private void btnProceesRename_Click(object sender, RoutedEventArgs e)
        {

            if (
                (txtRename.Text.Length > 1) &&
                (!MyViewModel.Items.Any(x => x.Title == txtRename.Text)) &&
                (!Wells.Items.Any(x => x.Title == txtRename.Text)) &&
                (!Rocks.Items.Any(x => x.Title == txtRename.Text)) &&
                (!WellStrategies.Items.Any(x => x.Title == txtRename.Text))
             )
            {
                MyViewModel.SelectedItem.Title = txtRename.Text;
                ControlPanelsVisibilty(HomePanel);

                SortTree();

                lblError.Content = string.Empty;
            }
            else if (txtRename.Text.Length < 2)
            {
                lblError.Content = "**Please choose a name with more than 2 characters!";
                ControlPanelsVisibilty(RenamePanel);
            }
            else
            {
                lblError.Content = "**duplicated name Please choose a new one!";
                ControlPanelsVisibilty(RenamePanel);
            }
            txtRename.Clear();
        }
        #endregion Rename Events

        #endregion Events


        #region Window style Events
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Minimize
        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        // Maximize
        private void CommandBinding_Executed_Maximize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        // Restore
        private void CommandBinding_Executed_Restore(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        // Close
        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        // State change
        private void MainWindowStateChangeRaised(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                MainWindowBorder.BorderThickness = new Thickness(8);
                RestoreButton.Visibility = Visibility.Visible;
                MaximizeButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MainWindowBorder.BorderThickness = new Thickness(0);
                RestoreButton.Visibility = Visibility.Collapsed;
                MaximizeButton.Visibility = Visibility.Visible;
            }
        }
        #endregion Window style Events

    }
}
