using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace heymobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryBookmarksTabbedPage : TabbedPage
    {

        public ObservableCollection<string> hisCollection = new ObservableCollection<string>();
        public ObservableCollection<string> bookCollection = new ObservableCollection<string>();
        public ObservableCollection<string> visitedCollection = new ObservableCollection<string>();


        public HistoryBookmarksTabbedPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            CreateBookmarkList();
            CreateHistoryList();
            //var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
            //deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
        }

        public HistoryBookmarksTabbedPage(string val)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            CreateBookmarkList();
            BookmarkListView.ItemsSource += val;
        }
        
        // CORRECT ONE 
        public HistoryBookmarksTabbedPage(ObservableCollection<string> his, ObservableCollection<string> book)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            HistoryListView.ItemsSource = his;
            BookmarkListView.ItemsSource = book;

            hisCollection = his;
            bookCollection = book;

            HistoryListView.ItemSelected += OnItemSelected;
            BookmarkListView.ItemSelected += OnItemSelected;

        }

        public HistoryBookmarksTabbedPage(ObservableCollection<string> his, ObservableCollection<string> book, ObservableCollection<string> visit)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            HistoryListView.ItemsSource = his;
            BookmarkListView.ItemsSource = book;
            MostVisitedListView.ItemsSource = visit;

            hisCollection = his;
            bookCollection = book;
            visitedCollection = visit;

            HistoryListView.ItemSelected += OnItemSelected;
            BookmarkListView.ItemSelected += OnItemSelected;
            MostVisitedListView.ItemSelected += OnItemSelected;
        }


        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ///var selectedPage = new MainInterface(HistoryListView.SelectedItem.ToString(), hisCollection, bookCollection);

            if (BookmarkListView.SelectedItem != null)
            {
                string helperBm;
                helperBm = BookmarkListView.SelectedItem.ToString();
                Application.Current.MainPage = new MainInterface(helperBm, hisCollection, bookCollection, visitedCollection);
            }

            if (HistoryListView.SelectedItem != null)
            {
                string helperHis;
                helperHis = HistoryListView.SelectedItem.ToString();
                Application.Current.MainPage = new MainInterface(helperHis, hisCollection, bookCollection, visitedCollection);
            }

            if(MostVisitedListView.SelectedItem != null)
            {
                string helperVis;
                helperVis= MostVisitedListView.SelectedItem.ToString();
                Application.Current.MainPage = new MainInterface(helperVis, hisCollection, bookCollection, visitedCollection);
            }

                {
                    //await Navigation.PopModalAsync();
                    //await Navigation.NavigationStack.Count() - 2;
                    //await Navigation.PushModalAsync(selectedPage);
                    //await Navigation.PopModalAsync();
                    //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count]);
                    //if (Navigation.NavigationStack.Count >= 2)
                    //{
                        //  App.Current.MainPage.Navigation.RemovePage(Navigation.NavigationStack.First());
                    //}
                    //await Navigation.PopModalAsync();

                    //await Navigation.PopToRootAsync();
                    //await Navigation.PushModalAsync(new NavigationPage(selectedPage));
                    //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count-1]);
                }

        }

        private void DelegateToWebViewFromBookmarkTPage(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string helperBm, helperHis, helperVis;

            helperBm = BookmarkListView.SelectedItem.ToString();
            helperHis = HistoryListView.SelectedItem.ToString();
            helperVis = MostVisitedListView.SelectedItem.ToString();
        }


        private void CreateBookmarkFromCollection(ObservableCollection<string> Collect)
        {
            foreach(string item in Collect)
            {
                //BookmarkListView.ItemsSource += item;
                BookmarkListView.ItemsSource = new string[] { item };
            }
        }

        private void CreateHistoryFromCollection(ObservableCollection<string> Collect)
        {
            foreach (string item in Collect)
            {
                HistoryListView.ItemsSource = new string[] { item };
            }
        }

        private void CreateBookmarkList()
        {
            BookmarkListView.ItemsSource = new string[]
            {
                "Google",
                "googlea",
            };

        }

        private void CreateHistoryList()
        {
            HistoryListView.ItemsSource = new string[]
            {
                "Google",
            };
        }

        private void CreateFavoriteList()
        {
            //FavoriteFlexLayout.Children.Add();

            {

            };
        }

    }
}