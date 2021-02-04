using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsefulApp.Model.Enumeration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsefulApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Points : ContentPage
    {
        #region Fields

        int _team1Points = 0;
        int _team2Points = 0;

        bool _winner = false;
        bool _firstLoad = true;

        #endregion

        #region Constructor

        public Points()
        {
            InitializeComponent();

            Team1NameEntry.Focus();
        }

        #endregion

        #region Events

        private void AddPointsBtn_Clicked(object sender, EventArgs e)
        {
            AddPoints();
        }

        private void Team1NameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (!String.IsNullOrEmpty(Team1NameEntry.Text))
            {
                Team1NameLbl.Text = Team1NameEntry.Text;
                Akuza1Btn.Text = $"{Team1NameEntry.Text} (+3)";

                Team1Stack.IsVisible = true;
            }
            else
            {
                Akuza1Btn.IsVisible = false;
                Akuza1Bodova4Btn.IsVisible = false;
                AkuzaLbl.IsVisible = false;
                Team1Stack.IsVisible = false;
                AddPointsBtn.IsVisible = false;
            }

            if (!String.IsNullOrEmpty(Team2NameEntry.Text) && !String.IsNullOrEmpty(Team1NameEntry.Text))
            {
                AkuzaLbl.IsVisible = true;
                Akuza1Btn.IsVisible = true;
                Akuza1Bodova4Btn.IsVisible = true;
                Akuza2Bodova4Btn.IsVisible = true;
                Akuza2Btn.IsVisible = true;
                AddPointsBtn.IsVisible = true;
            }
        }

        private void Team2NameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (!String.IsNullOrEmpty(Team2NameEntry.Text))
            {
                Team2NameLbl.Text = Team2NameEntry.Text;
                Akuza2Btn.Text = $"{Team2NameEntry.Text} (+3)";

                Team2Stack.IsVisible = true;
            }
            else
            {
                Akuza2Btn.IsVisible = false;
                Akuza2Bodova4Btn.IsVisible = false;
                AkuzaLbl.IsVisible = false;
                Team2Stack.IsVisible = false;
                AddPointsBtn.IsVisible = false;
            }

            if (!String.IsNullOrEmpty(Team2NameEntry.Text) && !String.IsNullOrEmpty(Team1NameEntry.Text))
            {
                AkuzaLbl.IsVisible = true;
                Akuza2Bodova4Btn.IsVisible = true;
                Akuza1Bodova4Btn.IsVisible = true;
                Akuza1Btn.IsVisible = true;
                Akuza2Btn.IsVisible = true;
                AddPointsBtn.IsVisible = true;
            }
        }

        private void Akuza1Btn_Clicked(object sender, EventArgs e)
        {
            AutoAddPoints(Ekipa.Team1, 3);
        }

        private void Akuza2Btn_Clicked(object sender, EventArgs e)
        {
            AutoAddPoints(Ekipa.Team2, 3);
        }

        private void Team1ScoreEntry_Completed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Team2ScoreEntry.Text))
                Team2ScoreEntry.Focus();
            else
            {
                AddPoints();
                Team1ScoreEntry.Focus();
            }
        }

        private void Team2ScoreEntry_Completed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Team2ScoreEntry.Text))
                AddPoints();

            Team1ScoreEntry.Focus();
        }

        private void Akuza1Bodova4Btn_Clicked(object sender, EventArgs e)
        {
            AutoAddPoints(Ekipa.Team1, 4);
        }

        private void Akuza2Bodova4Btn_Clicked(object sender, EventArgs e)
        {
            AutoAddPoints(Ekipa.Team2, 4);
        }

        private void Team1NameEntry_Completed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Team2NameEntry.Text))
                Team2NameEntry.Focus();
            else
                Team1ScoreEntry.Focus();
        }

        private void Team2NameEntry_Completed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Team1NameEntry.Text))
                Team1NameEntry.Focus();
            else
                Team1ScoreEntry.Focus();
        }

        #endregion

        #region Methods

        private void AddPoints()
        {
            var errorsMessage = Validate();
            if (!string.IsNullOrEmpty(errorsMessage))
            {
                DisplayAlert("Upozorenje", errorsMessage, "OK");
                return;
            }

            CreatePointLabels();

            _team1Points += int.Parse(Team1ScoreEntry.Text);
            _team2Points += int.Parse(Team2ScoreEntry.Text);

            Team1ScoreLbl.Text = _team1Points.ToString();
            Team2ScoreLbl.Text = _team2Points.ToString();

            Team1ScoreEntry.Text = string.Empty;
            Team2ScoreEntry.Text = string.Empty;

            ValidateWinner();

            if (_winner)
            {
                RestartCounting();
                _winner = false;
            }
        }

        private void ValidateWinner()
        {
            if (_team1Points >= int.Parse(GoalPointsEntry.Text) || (_team1Points >= int.Parse(GoalPointsEntry.Text) && _team1Points > _team2Points))
            {
                DisplayAlert("Bravissimo", $"Ekipa {Team1NameEntry.Text} je pobjednik!", "OK");
                _winner = true;
            }
            else if (_team2Points >= int.Parse(GoalPointsEntry.Text) || (_team2Points >= int.Parse(GoalPointsEntry.Text) && _team2Points > _team1Points))
            {
                DisplayAlert("Bravissimo", $"Ekipa {Team2NameEntry.Text} je pobjednik!", "OK");
                _winner = true;
            }
        }

        private void RestartCounting()
        {
            _team1Points = 0;
            _team2Points = 0;
            Team1ScoresStack.Children.Clear();
            Team2ScoresStack.Children.Clear();
        }

        private void CreatePointLabels(string team1 = "0", string team2 = "0")
        {
            Label score1Lbl = new Label();
            if (team1 == "0")
                score1Lbl.Text = Team1ScoreEntry.Text;
            else
                score1Lbl.Text = team1;
            score1Lbl.Margin = new Thickness(1.5, 0);

            Label score2Lbl = new Label();
            if (team2 == "0")
                score2Lbl.Text = Team2ScoreEntry.Text;
            else
                score1Lbl.Text = team2;
            score2Lbl.Margin = new Thickness(1.5, 0);

            Team1ScoresStack.Children.Add(score1Lbl);
            Team2ScoresStack.Children.Add(score2Lbl);
        }

        private void AutoAddPoints(Ekipa team, int points)
        {
            switch (team)
            {
                case Ekipa.Team1:
                    _team1Points += points;
                    Team1ScoreLbl.Text = _team1Points.ToString();
                    CreatePointLabels(points.ToString(), "0");
                    break;
                case Ekipa.Team2:
                    _team2Points += points;
                    Team2ScoreLbl.Text = _team2Points.ToString();
                    CreatePointLabels("0", points.ToString());
                    break;
            }
        }

        private string Validate()
        {
            string validationMessage = "";

            if (String.IsNullOrEmpty(Team1ScoreEntry.Text) || String.IsNullOrEmpty(Team2ScoreEntry.Text))
            {
                validationMessage += "Nisu uneseni bodovi ekipa!" + Environment.NewLine;
            }
            if (String.IsNullOrEmpty(GoalPointsEntry.Text))
            {
                validationMessage += "Nisu uneseni bodovi do koliko se igra!" + Environment.NewLine;
            }

            return validationMessage;
        }

        #endregion

        #region Overrides

        protected override void OnAppearing()
        {
            if (_firstLoad)
            {
                _firstLoad = false;
            }
        }

        #endregion
                
    }
}