using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homeworld_ColorPicker.Objects
{
    using Controls;
    public class BoxActions
    {
        public Action<ColourBox>? ColourLeftClick { get; }
        public Action<ColourBox>? ColourRightClick { get; }
        public Action<ColourBox>? ColourMiddleClick { get; }

        public Action<BadgeBox>? BadgeLeftClick { get; }
        public Action<BadgeBox>? BadgeRightClick { get; }
        public Action<BadgeBox>? BadgeMiddleClick { get; }

        public BoxActions()
        {
            ;
        }

        public BoxActions(Action<ColourBox>? colourLeftClick, Action<ColourBox>? colourRightClick, Action<ColourBox>? colourMiddleClick,
                          Action<BadgeBox>? badgeLeftClick, Action<BadgeBox>? badgeRightClick, Action<BadgeBox>? badgeMiddleClick)
        {
            this.ColourLeftClick = colourLeftClick;
            this.ColourRightClick = colourRightClick;
            this.ColourMiddleClick = colourMiddleClick;

            this.BadgeLeftClick = badgeLeftClick;
            this.BadgeRightClick = badgeRightClick;
            this.BadgeMiddleClick = badgeMiddleClick;
        }
    }
}