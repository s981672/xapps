﻿using Xamarin.Forms;

namespace xapps
{
    public partial class MovieListPage : ContentPage
    {
        MovieListPageViewModel viewModel;

        public MovieListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MovieListPageViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as MovieData;
            if (item == null)
                return;

            await Navigation.PushAsync(new MovieDetailPage(item));

            // Manually deselect item
            listView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}