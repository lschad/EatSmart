﻿using System.Windows;
using System.Windows.Controls;

// ReSharper disable All

namespace SchadLucas.EatSmart.UI.Resources.Styles
{
    /// <summary>
    ///     A control featuring a range of loading indicating animations.
    /// </summary>
    /// <remarks>https://github.com/100GPing100/LoadingIndicators.WPF</remarks>
    [TemplatePart(Name = "Border", Type = typeof(Border))]
    public class LoadingIndicator : Control
    {
        // Variables
        protected Border PART_Border;

        /// <summary>
        ///     Identifies the <see cref="IsActive" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(LoadingIndicator), new PropertyMetadata(true, (o, e) =>
            {
                var li = (LoadingIndicator)o;

                if (li.PART_Border == null)
                {
                    return;
                }

                if ((bool)e.NewValue == false)
                {
                    VisualStateManager.GoToElementState(li.PART_Border, "Inactive", false);
                    li.PART_Border.Visibility = Visibility.Collapsed;
                }
                else
                {
                    VisualStateManager.GoToElementState(li.PART_Border, "Active", false);
                    li.PART_Border.Visibility = Visibility.Visible;

                    foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(li.PART_Border))
                    {
                        if (group.Name == "ActiveStates")
                        {
                            foreach (VisualState state in group.States)
                            {
                                if (state.Name == "Active")
                                {
                                    state.Storyboard.SetSpeedRatio(li.PART_Border, li.SpeedRatio);
                                }
                            }
                        }
                    }
                }
            }));

        /// <summary>
        ///     Identifies the <see cref="SpeedRatio" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(LoadingIndicator), new PropertyMetadata(1d, (o, e) =>
            {
                var li = (LoadingIndicator)o;

                if (li.PART_Border == null || li.IsActive == false)
                {
                    return;
                }

                foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(li.PART_Border))
                {
                    if (group.Name == "ActiveStates")
                    {
                        foreach (VisualState state in group.States)
                        {
                            if (state.Name == "Active")
                            {
                                state.Storyboard.SetSpeedRatio(li.PART_Border, (double)e.NewValue);
                            }
                        }
                    }
                }
            }));

        /// <summary>
        ///     Get/set whether the loading indicator is active.
        /// </summary>
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        /// <summary>
        ///     Get/set the speed ratio of the animation.
        /// </summary>
        public double SpeedRatio
        {
            get => (double)GetValue(SpeedRatioProperty);
            set => SetValue(SpeedRatioProperty, value);
        }

        /// <summary>
        ///     When overridden in a derived class, is invoked whenever application code or internal
        ///     processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Border = (Border)GetTemplateChild("PART_Border");

            if (PART_Border != null)
            {
                VisualStateManager.GoToElementState(PART_Border, IsActive ? "Active" : "Inactive", false);
                foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(PART_Border))
                {
                    if (group.Name == "ActiveStates")
                    {
                        foreach (VisualState state in group.States)
                        {
                            if (state.Name == "Active")
                            {
                                state.Storyboard.SetSpeedRatio(PART_Border, SpeedRatio);
                            }
                        }
                    }
                }

                PART_Border.Visibility = IsActive ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}