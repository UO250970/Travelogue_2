﻿using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Travelogue_2.Main.Views.Journey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JourneyTemplateView : AbsoluteLayout
    {
        private double _journeyNameFrameTop;

        public JourneyTemplateView()
        {
            InitializeComponent();

            JourneyName.SizeChanged += OnJourneyNameSizeChanged;
            MainScroll.PropertyChanged += OnScrollViewPropertyChanged;
        }

        protected void OnAppearing()
        {
            JourneyName.TranslationY = _journeyNameFrameTop * 0.1;
            JourneyNameBoxView.TranslationY = _journeyNameFrameTop * 0.1;
            JourneyStateBoxView.TranslationY = _journeyNameFrameTop * 0.1;
            JourneyState.TranslationY = _journeyNameFrameTop * 0.1;

            JourneyNameBoxView.FadeTo(0, 0);
        }

        private void OnJourneyNameSizeChanged(object sender, System.EventArgs e)
        {
            if (JourneyName.Height > 0)
            {
                JourneyName.SizeChanged -= OnJourneyNameSizeChanged;

                //As soon as the news header has been repositioned, we can grab the actual screen top position
                _journeyNameFrameTop = JourneyName.Y;
            }
        }

        private void OnScrollViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(ScrollView.ScrollYProperty.PropertyName))
            {
                var scrolled = ((ScrollView)sender).ScrollY;

                if (scrolled < _journeyNameFrameTop)
                {
                    JourneyName.TranslationY = (0 - scrolled);
                    JourneyNameBoxView.TranslationY = (0 - scrolled);
                    JourneyStateBoxView.TranslationY = (0 - scrolled);
                    JourneyState.TranslationY = (0 - scrolled);

                    JourneyNameBoxView.FadeTo(0, 100);
                }
                else
                {
                    JourneyName.TranslationY = 0 - _journeyNameFrameTop;
                    JourneyNameBoxView.TranslationY = 0 - _journeyNameFrameTop;
                    JourneyStateBoxView.TranslationY = 0 - _journeyNameFrameTop;
                    JourneyState.TranslationY = 0 - _journeyNameFrameTop;

                    JourneyNameBoxView.FadeTo(1, 100);
                }

                if (1.5 * scrolled < _journeyNameFrameTop)
                {
                    JourneyNameBoxView.FadeTo(0, 100);
                }
                else
                {
                    JourneyNameBoxView.FadeTo(1, 100);
                }
            }
        }

    }
}