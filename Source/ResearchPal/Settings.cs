using System;

using UnityEngine;
using Verse;

namespace ResearchPal
{
    public class Settings : ModSettings
    {
        #region tuning parameters

        public const int     LineMaxLengthNodes = 20;
        public const int     MinTrunkSize       = 2;

        public static bool   showNotification   = true;
        public static bool   shouldPause        = false;
        public static bool   shouldReset        = false;
        public static bool   showFilteredLinks  = false;
        public static bool   debugResearch      = false;

        public enum grpStrategyType
        {
            DEFAULT,
            TABS_STRICT,
            TABS_RELAXED,
            PREREQUISITES
        }
        public static grpStrategyType GroupingStrategy = grpStrategyType.DEFAULT;

        #endregion tuning parameters

        #region UI elements

        public const float   HubSize            = 16f;
        public const int     TipID              = 24 * 1271;

        public static readonly Vector2 IconSize = new Vector2 (18f, 18f);
        public static readonly Vector2 NodeMargins = new Vector2 (50f, 10f);
        public static readonly Vector2 NodeSize = new Vector2 (200f, 50f);

        public static float FilterNonMatchAlpha = 0.2f;
        #endregion UI elements

        public static void DoSettingsWindowContents (Rect rect)
        {
            Listing_Standard list = new Listing_Standard (GameFont.Small);
            list.ColumnWidth = rect.width;
            list.Begin (rect);

            list.CheckboxLabeled (ResourceBank.String.ShowNotificationPopup, ref showNotification,
                                  ResourceBank.String.ShowNotificationPopupTip);
            list.CheckboxLabeled (ResourceBank.String.ShouldPauseOnOpen, ref shouldPause,
                                  ResourceBank.String.ShouldPauseOnOpenTip);
            list.CheckboxLabeled (ResourceBank.String.DebugResearch, ref debugResearch,
                                  ResourceBank.String.DebugResearchTip);

            list.CheckboxLabeled (ResourceBank.String.ShouldResetOnOpen, ref shouldReset,
                                  ResourceBank.String.ShouldResetOnOpenTip);

            list.CheckboxLabeled (ResourceBank.String.ShowFilteredLinks, ref showFilteredLinks,
                                  ResourceBank.String.ShowFilteredLinksTip);

            list.GapLine();
            list.Label("Grouping Strategy");
            if (list.RadioButton(ResourceBank.String.GroupingDefault, GroupingStrategy == grpStrategyType.DEFAULT))
                GroupingStrategy = grpStrategyType.DEFAULT;

            if (list.RadioButton(ResourceBank.String.GroupingTabStrict, GroupingStrategy == grpStrategyType.TABS_STRICT))
                GroupingStrategy = grpStrategyType.TABS_STRICT;

            if (list.RadioButton(ResourceBank.String.GroupingTabRelaxed, GroupingStrategy == grpStrategyType.TABS_RELAXED))
                GroupingStrategy = grpStrategyType.TABS_RELAXED;

            if (list.RadioButton(ResourceBank.String.GroupingPrereq, GroupingStrategy == grpStrategyType.PREREQUISITES))
                GroupingStrategy = grpStrategyType.PREREQUISITES;

            list.GapLine();
            list.Label(ResourceBank.String.FilterOpacityDesc(0.2f, (float)Math.Round(FilterNonMatchAlpha, 2)));
            FilterNonMatchAlpha = (float)Math.Round(list.Slider(FilterNonMatchAlpha, 0, 1), 2);

            list.End();
        }

        public override void ExposeData ()
        {
            base.ExposeData ();
            Scribe_Values.Look (ref showNotification, "ShowNotificationPopup", true);
            Scribe_Values.Look (ref shouldPause, "ShouldPauseOnOpen", false);
            Scribe_Values.Look (ref shouldReset, "ShouldResetOnOpen", false);
            Scribe_Values.Look (ref showFilteredLinks, "ShowFilteredLinks", false);
            Scribe_Values.Look (ref debugResearch, "DebugResearch", false);
            Scribe_Values.Look (ref FilterNonMatchAlpha, "FilterNonMatchAlpha", 0.2f, false);
            Scribe_Values.Look<grpStrategyType> (ref GroupingStrategy, "GroupingStrategy", grpStrategyType.DEFAULT, true);
        }

    }
}