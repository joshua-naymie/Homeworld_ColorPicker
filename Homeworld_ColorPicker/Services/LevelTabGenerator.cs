using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Services
{
    using Homeworld_ColorPicker.Controls;
    using Objects;
    public class LevelTabGenerator
    {
        private
        Action<ColourBox>? colourLeftClick,
                           colourRightClick,
                           colourMiddleClick;

        private
        Action<BadgeBox>? badgeLeftClick,
                          badgeRightClick,
                          badgeMiddleClick;

        public void SetColourActions(Action<ColourBox>? leftClickAction, Action<ColourBox>? rightClickAction, Action<ColourBox>? middleClickAction)
        {
            colourLeftClick = leftClickAction;
            colourRightClick = rightClickAction;
            colourMiddleClick = middleClickAction;
        }

        public void SetBadgeActions(Action<BadgeBox>? leftClickAction, Action<BadgeBox>? rightClickAction, Action<BadgeBox>? middleClickAction)
        {
            badgeLeftClick = leftClickAction;
            badgeRightClick = rightClickAction;
            badgeMiddleClick = middleClickAction;
        }

        public TabPage GenerateTabPage(TeamColour[] teams, int levelNum)
        {
            TabPage page = new TabPage();
            page.BackColor = Color.White;
            page.Text = $"Level {levelNum+1}";
            page.Controls.Add(GenerateContentPanel(teams, levelNum));
            page.Controls.Add(GenerateHeader(GC.CUSTOM_FONT));

            return page;
        }

        private Panel GenerateContentPanel(TeamColour[] level, int levelNum)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = false;
            panel.HorizontalScroll.Enabled = false;
            panel.HorizontalScroll.Visible = false;
            panel.HorizontalScroll.Maximum = 0;
            panel.AutoScroll = true;

            int teamNum = 0,
                teamOffset = 0;

            foreach (TeamColour team in level)
            {
                TeamPanel teamPanel = new TeamPanel(GC.DICT_HW2_LEVEL_TEAM_NAMES[new Tuple<int, int>(levelNum, teamNum++)], team);
                teamPanel.Location = new Point(0, teamOffset);
                teamOffset += teamPanel.Height;
                //teamPanel.BackColor = Color.Red;
                teamPanel.SetColourBoxActions(colourLeftClick, colourRightClick, colourMiddleClick);
                panel.Controls.Add(teamPanel);
            }

            return panel;

            //Panel panel = new Panel();
            //panel.Dock = DockStyle.Fill;

            //int startX = 240,
            //    startY = 20,
            //    teamNameOffsetX = -6,
            //    teamNameOffsetY = 35,
            //    boxHeight = 100;

            ////foreach(TeamColour team in level.GetTeams())
            //for(int i=0; i<1; i++)
            //{
            //    Label teamName = new Label();
            //    teamName.Text = "Team Name: ";
            //    teamName.Font = font;
            //    teamName.Size = Util.GetLabelSize(teamName);
            //    int h = startX + teamNameOffsetX - Util.GetLabelSize(teamName).Width;
            //    int j = startY + 50 - (Util.GetLabelSize(teamName).Height / 2);
            //    teamName.Location = new Point(h, j);

            //    PictureBox badgePicture = new PictureBox();
            //    badgePicture.Size = new Size(100, 100);
            //    badgePicture.BorderStyle = BorderStyle.Fixed3D;
            //    badgePicture.Location = new Point(startX, startY);

            //    startX += 150;

            //    PictureBox basePicture = new PictureBox();
            //    basePicture.Size = new Size(100, 100);
            //    basePicture.BorderStyle = BorderStyle.Fixed3D;
            //    basePicture.Location = new Point(startX, startY);

            //    startX += 150;

            //    PictureBox trailPicture = new PictureBox();
            //    trailPicture.Size = new Size(100, 100);
            //    trailPicture.BorderStyle = BorderStyle.Fixed3D;
            //    trailPicture.Location = new Point(startX, startY);

            //    startX += 150;

            //    PictureBox stripePicture = new PictureBox();
            //    stripePicture.Size = new Size(100, 100);
            //    stripePicture.BorderStyle = BorderStyle.Fixed3D;
            //    stripePicture.Location = new Point(startX, startY);

            //    panel.Controls.Add(teamName);
            //    panel.Controls.Add(badgePicture);
            //    panel.Controls.Add(basePicture);
            //    panel.Controls.Add(trailPicture);
            //    panel.Controls.Add(stripePicture);
            //}

            //return panel;
        }

        private Panel GenerateHeader(Font font)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Height = 60;

            Button resetButton = new Button();

            resetButton.Font = font;// new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            resetButton.Location = new System.Drawing.Point(6, 6);
            resetButton.Size = new System.Drawing.Size(170, 48);
            //resetButton.TabIndex = 28;
            resetButton.Text = "Reset All";
            resetButton.UseVisualStyleBackColor = true;

            Label badgeLabel = new Label();
            badgeLabel.Font = font;
            badgeLabel.Location = new System.Drawing.Point(240, 14);
            badgeLabel.Text = "Badge";
            badgeLabel.Size = Util.GetLabelSize(badgeLabel);
            badgeLabel.BorderStyle = BorderStyle.FixedSingle;

            Label baseLabel = new Label();
            baseLabel.Font = font;
            baseLabel.Location = new System.Drawing.Point(405, 14);
            baseLabel.Text = "Base";
            baseLabel.Size = Util.GetLabelSize(baseLabel);
            baseLabel.BorderStyle = BorderStyle.FixedSingle;

            Label stripeLabel = new Label();
            stripeLabel.Font = font;
            stripeLabel.Location = new System.Drawing.Point(540, 10);
            stripeLabel.Text = "Stripe";
            stripeLabel.Size = Util.GetLabelSize(stripeLabel);
            stripeLabel.BorderStyle = BorderStyle.FixedSingle;

            Label trailLabel = new Label();
            trailLabel.Font = font;
            trailLabel.Location = new System.Drawing.Point(710, 10);
            trailLabel.Text = "Trail";
            trailLabel.Size = Util.GetLabelSize(trailLabel);
            trailLabel.BorderStyle = BorderStyle.FixedSingle;

            //System.Diagnostics.Debug.WriteLine("WIDTH: " + badge.Size.Width);

            panel.Controls.Add(resetButton);
            panel.Controls.Add(badgeLabel);
            panel.Controls.Add(baseLabel);
            panel.Controls.Add(stripeLabel);
            panel.Controls.Add(trailLabel);

            return panel;
        }
    }
}
