﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class StepButtonBar : Control
    {
        public static readonly DependencyProperty BackProperty = DependencyProperty.Register(
                nameof(Back), typeof(object), typeof(StepButtonBar), new PropertyMetadata(null));

        public object Back
        {
            get
            {
                return GetValue(BackProperty);
            }

            set
            {
                SetValue(BackProperty, value);
            }
        }

        public static readonly DependencyProperty CancelProperty = DependencyProperty.Register(
                nameof(Cancel), typeof(object), typeof(StepButtonBar), new PropertyMetadata(null));

        public object Cancel
        {
            get
            {
                return GetValue(CancelProperty);
            }

            set
            {
                SetValue(CancelProperty, value);
            }
        }

        public static readonly DependencyProperty ContinueProperty = DependencyProperty.Register(
                nameof(Continue), typeof(object), typeof(StepButtonBar), new PropertyMetadata(null));

        public object Continue
        {
            get
            {
                return GetValue(ContinueProperty);
            }

            set
            {
                SetValue(ContinueProperty, value);
            }
        }

        internal static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
                nameof(Mode), typeof(StepperOrientation), typeof(StepButtonBar), new PropertyMetadata(StepperOrientation.Horizontal));

        internal StepperOrientation Mode
        {
            get
            {
                return (StepperOrientation)GetValue(ModeProperty);
            }

            set
            {
                SetValue(ModeProperty, value);
            }
        }

        static StepButtonBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepButtonBar), new FrameworkPropertyMetadata(typeof(StepButtonBar)));
        }

        public StepButtonBar() : base() { }

        public override void OnApplyTemplate()
        {
            // read the Orientation of the Stepper and set as the Mode
            //     - changing the Orientation throws the UI of the Stepper and builds a new one
            //     - therefore this method will be called for a new instance and the changes of Orientation will be applied to Mode
            Stepper stepper = FindStepper();

            if (stepper != null)
            {
                Mode = stepper.Orientation;
            }

            base.OnApplyTemplate();
        }

        private Stepper FindStepper()
        {
            DependencyObject element = VisualTreeHelper.GetParent(this);

            while (element != null && !(element is Stepper))
            {
                element = VisualTreeHelper.GetParent(element);
            }

            return element as Stepper;
        }
    }
}
