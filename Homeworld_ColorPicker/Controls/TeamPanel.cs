using Homeworld_ColorPicker.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Controls
{
    public class TeamPanel : Panel
    {
        private const
        int BOX_SIZE = 100;

        private const
        string TEXT_RESET_BUTTON = "Reset";

        private
        Team team;

        private
        TeamColour teamColours;

        private
        ColourBox baseColourBox,
                  stripeColourBox,
                  trailColourBox;



        public TeamPanel(Team team, TeamColour teamColours)
        {
            this.team = team;
            this.teamColours = teamColours;

            InitComponents();
        }

        private void InitComponents()
        {
            int startX = 240,
                posY = 15;

            this.Padding = new Padding(0, 15, 0, 15);
            this.Width = 985;
            this.Height = 130;
            //this.BorderStyle = BorderStyle.Fixed3D;

            Label teamName = new Label();
            teamName.Text = $"{team.Name}:";
            teamName.Font = GC.CUSTOM_FONT;
            //teamName.BorderStyle = BorderStyle.FixedSingle;
            teamName.Size = Util.GetLabelSize(teamName);
            teamName.Location = new Point(startX - teamName.Width - 6, 50);

            PictureBox badgePicture = new PictureBox();
            SetPictureBoxProperties(ref badgePicture, startX, posY);
            BadgeBox badge = new BadgeBox(badgePicture, teamColours.GetBadgePath());
            //badge.SetLeftClickAction();

            startX += 150;

            PictureBox baseColour = new PictureBox();
            SetPictureBoxProperties(ref baseColour, startX, posY);
            baseColourBox = new ColourBox(baseColour, teamColours.GetBaseColor());

            startX += 150;

            PictureBox stripeColour = new PictureBox();
            SetPictureBoxProperties(ref stripeColour, startX, posY);
            stripeColourBox = new ColourBox(stripeColour, teamColours.GetStripeColor());

            startX += 150;

            PictureBox trailColour = new PictureBox();
            SetPictureBoxProperties(ref trailColour, startX, posY);
            trailColourBox = new ColourBox(trailColour, teamColours.GetTrailColor());

            startX += 150;

            Button resetButton = new Button();
            resetButton.Location = new Point(startX, posY);
            resetButton.Size = new Size(BOX_SIZE, BOX_SIZE);
            resetButton.Text = TEXT_RESET_BUTTON;
            resetButton.BackColor = Color.Transparent;
            resetButton.Font = GC.CUSTOM_FONT;

            this.Controls.Add(teamName);
            this.Controls.Add(badgePicture);
            this.Controls.Add(baseColour);
            this.Controls.Add(stripeColour);
            this.Controls.Add(trailColour);
            this.Controls.Add(resetButton);
        }

        private void SetPictureBoxProperties(ref PictureBox box, int posX, int posY)
        {
            box.Size = new Size(BOX_SIZE, BOX_SIZE);
            box.BorderStyle = BorderStyle.Fixed3D;
            box.Location = new Point(posX, posY);
        }

        public void SetColourBoxLeftClicks(Action<ColourBox> action)
        {
            baseColourBox.SetLeftClickAction(action);
            stripeColourBox.SetLeftClickAction(action);
            trailColourBox.SetLeftClickAction(action);
        }
    }
}
