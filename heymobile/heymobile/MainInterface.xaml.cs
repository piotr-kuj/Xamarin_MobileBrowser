using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Linq;

namespace heymobile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainInterface : ContentPage
    {
        ///string apiVersion = Android.OS.Build.VERSION.Sdk;

        public List<string> pageText = new List<string>();
        public List<string> pageSource = new List<string>();

        public List<string> currentHistory = new List<string>();
        public List<string> currentBookmarks = new List<string>();

        public ObservableCollection<string> historyCollection = new ObservableCollection<string>();
        public ObservableCollection<string> bookmarkCollection = new ObservableCollection<string>();
        public ObservableCollection<string> mostvisitedCollection = new ObservableCollection<string>();

        public interface IGetCurrentBookmarks { string GetB(); }
        public interface IBaseUrl { string Get(); }

        public enum TouchActionType
        {
            Entered,
            Pressed,
            Moved,
            Released,
            Exited,
            Cancelled
        }



        private HtmlWebViewSource source = new HtmlWebViewSource();

        private bool isActiveWindow;
        private bool openCloseMenu = false;


        public MainInterface()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            Browser.Source = "http://www.google.pl"; /// defualt card page should first starting

            Browser.Navigated += OnNavigatedHandler;

        }

        public MainInterface(string selectedItem, ObservableCollection<string> his, ObservableCollection<string> book)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            Browser.Source = selectedItem;

            historyCollection = his;
            bookmarkCollection = book;

            Browser.Navigated += OnNavigatedHandler;
        }

        // next main counstructor with 4 args 1 string 3 ob collect
        public MainInterface(string selectedItem, ObservableCollection<string> his, ObservableCollection<string> book, ObservableCollection<string> visit)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            Browser.Source = selectedItem;

            historyCollection = his;
            bookmarkCollection = book;
            mostvisitedCollection = visit;


            Browser.Navigated += OnNavigatedHandler;
            //Browser.Navigated += CountingToAddMostView;
            Browser.Focused += ViewWithOptionToOpenNewCard;
        }



        /// FIX ELSE 
        private void backClicked(object sender, EventArgs e)
        {
            // Check to see if there is anywhere to go back to
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
            else
            { // If not, go to default fastclickView ( google, yahoo, facebook etc  )

                //Navigation.PushAsync(new DefaultCardPage());
            }
        }

        private void forwardClicked(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
            {
                Browser.GoForward();
            }
        }

        /*
        protected override void OnAppearing()
        {
            base.OnAppearing();
            isActiveWindow = true;
            Device.StartTimer(TimeSpan.FromSeconds(0.1), TimerCallback);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            isActiveWindow = false;
        }
        */


        bool TimerCallback()
        {
            ProgressBarNavigating.Progress += 0.01;

            return isActiveWindow || ProgressBarNavigating.Progress == 1;
        }

        private async void webOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            currentHistory.Add(e.Url); /// AKTUALNA HISTORIA 
            historyCollection.Add(e.Url);

            ProgressBarNavigating.IsVisible = true;
            await Task.Run(async () =>
            {
                while (ProgressBarNavigating.IsVisible == true)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isActiveWindow = true;
                        Device.StartTimer(TimeSpan.FromSeconds(0.1), TimerCallback);
                    });
                    await Task.Delay(1000);
                }
            });
        }

        private void webOnEndNavigating(object sender, WebNavigatedEventArgs e)
        {
            isActiveWindow = false;
            ProgressBarNavigating.IsVisible = false;
            TimerCallback();
            ProgressBarNavigating.Progress = 0;
        }

        public void searchBarClicked(object sender, EventArgs e)
        {
            string helpUrl = SearchBar.Text;

            {
                /*
                if (!SearchBar.Text.Contains("http://www.") && (!SearchBar.Text.Contains(".com") || !SearchBar.Text.Contains(".pl")))
                {
                    // google url + searchbar text 
                }
                else if ((!SearchBar.Text.Contains("http://www.")) && (SearchBar.Text.Contains(".com") || SearchBar.Text.Contains(".pl")))
                {
                    SearchBar.Text = "http://www" + helpUrl;
                }
                else if (SearchBar.Text.Contains("www.") || !SearchBar.Text.Contains("http://") && (SearchBar.Text.Contains(".com") || SearchBar.Text.Contains(".pl")))
                {
                    SearchBar.Text = "http://" + helpUrl;
                }
                else if (!SearchBar.Text.Contains("http://www"))
                {
                    ///SearchBar.Text = "http://" + helpUrl;

                }
                else
                {

                }

                */
            }

            if (SearchBar.Text.Contains("www.") && (SearchBar.Text.Contains(".pl") || SearchBar.Text.Contains(".com")))
            {
                SearchBar.Text = "http://" + helpUrl;
            }
            else if (SearchBar.Text.Contains(".pl") || SearchBar.Text.Contains(".com"))
            {
                SearchBar.Text = "http://www." + helpUrl;
            }
            else if (!SearchBar.Text.Contains("www.") && !SearchBar.Text.Contains(".pl") || !SearchBar.Text.Contains(".com"))
            {
                SearchBar.Text = "http://www.google.pl/search?q=" + helpUrl;
            }
            (Browser.Source as UrlWebViewSource).Url = SearchBar.Text;
        }

        /*
        SearchBar searchBar = new SearchBar
        {
            SearchCommand = new 
        };
        */


        protected void searchBar_Clicked(object sender, EventArgs e)
        {
            (Browser.Source as UrlWebViewSource).Url = SearchBar.Text;
        }
        /*
        protected override void searchBar_Clicked(object sender, EventArgs e)
        {
            (Browser.Source as UrlWebViewSource).Url = SearchBar.Text;
        }*/

        public void OnNavigatedHandler(object sender, WebNavigatedEventArgs args)
        {
            SearchBar.Text = args.Url;
        }


        // OTWIERANIA NOWYCH STRON

        private void openMenu(object sender, EventArgs e)
        {
            MenuSlideFromBottom.HeightRequest = 100;
            if (openCloseMenu == true)
            {
                var animate = new Animation(d => MenuSlideFromBottom.HeightRequest = d, 100, -20);
                animate.Commit(MenuSlideFromBottom, "Hey", 8, 500);
                openCloseMenu = false;
            }
            else // OPEN
            {
                var animate = new Animation(d => MenuSlideFromBottom.MinimumHeightRequest = d, -50, 100);
                animate.Commit(MenuSlideFromBottom, "Hey", 8, 500);
                openCloseMenu = true;
            }

        }

        private void openMenuSettings(object sender, EventArgs e)
        {
            var animate = new Animation(d => MenuSlideFromBottom.HeightRequest = d, 100, -20);
            animate.Commit(MenuSlideFromBottom, "Hey", 8, 800);
            openCloseMenu = false;

            Navigation.PushModalAsync(new NavigationPage(new Settings()));

        }

        private void openMenuLists(object sender, EventArgs e)
        {
            var animate = new Animation(d => MenuSlideFromBottom.HeightRequest = d, 100, -20);
            animate.Commit(MenuSlideFromBottom, "Hey", 8, 800);
            openCloseMenu = false;

            //Navigation.PushModalAsync(new HistoryBookmarksTabbedPage());  // CONSTRUCTOR WITHOUT ARGS
            //Navigation.PushModalAsync(new NavigationPage(new HistoryBookmarksTabbedPage(historyCollection, bookmarkCollection)));

            Navigation.PushModalAsync(new NavigationPage(new HistoryBookmarksTabbedPage(historyCollection, bookmarkCollection, mostvisitedCollection)));
        }



        private void openNewCard(object sender, EventArgs e)
        {
            /// Navigation.PushAsync(new DefaultCardPage());
        }

        private void OpenCardsMenu(object sender, EventArgs e)
        {

        }
            // CARDS 

        private void ViewWithOptionToOpenNewCard(object sender, FocusEventArgs e)
        {
            var x = DateTime.Now;
            x.AddSeconds(2);
            if (Browser.IsFocused && DateTime.Now >= x)
            {
                TapCards.IsVisible = true;

                Tap_OpenNewCard.Clicked += delegate { TapCards.IsVisible = false; };
                Tap_OpenNCardInBG.Clicked += delegate { TapCards.IsVisible = false; };

                Tap_Refresh.Clicked += delegate { TapCards.IsVisible = false; };

                Tap_Cancel.Clicked += delegate { TapCards.IsVisible = false; };
            }
        }
        
        private bool TapCardsVisiblity()
        {
            if(TapCards.IsVisible == true)
            {
                return TapCards.IsVisible = false;
            }
            else
            {
                return TapCards.IsVisible = true;
            }
        }

        private void Tap_OpenNewCard_Clicked(object sender, EventArgs e)
        {

            TapCards.IsVisible = false;
        }

        private void Tap_OpenNCardInBG_Clicked(object sender, EventArgs e)
        {

            TapCards.IsVisible = false;
        }


        private void Tap_Refresh_Clicked(object sender, EventArgs e)
        {
            Browser.Source = (Browser.Source as UrlWebViewSource).Url;
            TapCards.IsVisible = false;
        }

        private void Tap_Cancel_Clicked(object sender, EventArgs e)
        {
            TapCards.IsVisible = false;
        }



        // FUNKCJONALNOSCI ZWIAZANIE Z HISTORIA / ZAKLADKAMI / NAJCZESCIEJ ODWIEDZANE  

        public async void AddToBookmark(object sender, WebNavigatedEventArgs e)
        {
            var answer = await DisplayAlert("Question?", "Would you like to add a webside to bookmarks?", "Yes", "No");
            
            if(answer == true)
            {
                currentBookmarks.Add(e.Url);
                bookmarkCollection.Add(e.Url);
                //HistoryBookmarksTabbedPage.IGetCurrentBookmarks

                var animate = new Animation(d => MenuSlideFromBottom.HeightRequest = d, 100, -20);
                animate.Commit(MenuSlideFromBottom, "Hey", 8, 800);
                openCloseMenu = false;
            }
            else
            {
                await DisplayAlert("Notification.", "the page has not been added to the bookmarks", "Exit");
            }
        }

        private void CountingToAddMostView(object sender, EventArgs e)
        {
            int[] counter = new int [historyCollection.Count];
            int arrayCounter = 0;
            int arrayCounter2 = 0;
            foreach(var item in historyCollection)
            {
                
                foreach(var item2 in historyCollection)
                {
                    if (arrayCounter != arrayCounter2)
                    {
                        if (item == item2)
                        {
                            counter[arrayCounter]++;
                        }
                    }
                    arrayCounter2++;
                }
                arrayCounter++;
            }

            /*
            int ac = 0;
            int[] help = new int[bookmarkCollection.Count];
            for(int i=0; i<arrayCounter; i++)
            {
                if(counter[i]>10)
                {
                    help[ac]++; 
                }
                ac++;
            }
            */

            int arcount = 0;
            foreach (var item in historyCollection)
            {
                if(counter[arcount]>2)
                {
                    if(item.ToString() != mostvisitedCollection.Contains(item).ToString())
                    {
                    mostvisitedCollection.Add(item);
                    }
                }
                arcount++;
            }

        }

        private void DeleteDuplicates()
        {
            //if (mostvisitedCollection.RemoveAt)'
            // if(mostvisitined find same row in next step
        }

    }

}



/*
  private string myVal;

        public string MyVal
        {
            get => myVal;
            set => myVal = value;
        }

 
 */
