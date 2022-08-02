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
        BadgeBox badge;

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

            badge = new BadgeBox(teamColours.GetBadgePath());
            badge.Size = new Size(BOX_SIZE, BOX_SIZE);
            badge.BorderStyle = BorderStyle.Fixed3D;
            badge.Location = new Point(startX, posY);
            //badge.SetLeftClickAction();

            startX += 150;

            baseColourBox = new ColourBox(teamColours.GetBaseColor());
            SetPictureBoxProperties(ref baseColourBox, startX, posY);

            startX += 150;

            stripeColourBox = new ColourBox(teamColours.GetStripeColor());
            SetPictureBoxProperties(ref stripeColourBox, startX, posY);

            startX += 150;

            trailColourBox = new ColourBox(teamColours.GetTrailColor());
            SetPictureBoxProperties(ref trailColourBox, startX, posY);

            startX += 150;

            Button resetButton = new Button();
            resetButton.Location = new Point(startX, posY);
            resetButton.Size = new Size(BOX_SIZE, BOX_SIZE);
            resetButton.Text = TEXT_RESET_BUTTON;
            resetButton.BackColor = Color.Transparent;
            resetButton.Font = GC.CUSTOM_FONT;

            this.Controls.Add(teamName);
            this.Controls.Add(badge);
            this.Controls.Add(baseColourBox);
            this.Controls.Add(stripeColourBox);
            this.Controls.Add(trailColourBox);
            this.Controls.Add(resetButton);
        }

        private void SetPictureBoxProperties(ref ColourBox box, int posX, int posY)
        {
            box.Size = new Size(BOX_SIZE, BOX_SIZE);
            box.BorderStyle = BorderStyle.Fixed3D;
            box.Location = new Point(posX, posY);
        }

        public void SetColourBoxActions(Action<ColourBox>? leftClick, Action<ColourBox>? rightClick, Action<ColourBox>? middleClick)
        {
            baseColourBox.SetLeftClickAction(leftClick);
            baseColourBox.SetRightClickAction(rightClick);
            baseColourBox.SetMiddleClickAction(middleClick);

            stripeColourBox.SetLeftClickAction(leftClick);
            stripeColourBox.SetRightClickAction(rightClick);
            stripeColourBox.SetMiddleClickAction(middleClick);

            trailColourBox.SetLeftClickAction(leftClick);
            trailColourBox.SetRightClickAction(rightClick);
            trailColourBox.SetMiddleClickAction(middleClick);
        }

        public void SetBadgeBoxActions(Action<BadgeBox>? leftClick, Action<BadgeBox>? rightClick, Action<BadgeBox>? middleClick)
        {
            badge.SetLeftClickAction(leftClick);
            badge.SetRightClickAction(rightClick);
            badge.SetMiddleClickAction(middleClick);
        }
    }
}
