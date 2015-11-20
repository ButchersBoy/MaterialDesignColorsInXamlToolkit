﻿using System;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Input;

namespace MahMaterialDragablzMashUp
{
    public class PaletteSelectorViewModel : PaletteHelper
    {
        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;  
            ApplyPrimaryCommand = new DelegateCommand<Swatch>(ApplyPrimary, new Predicate<Swatch>(s => s != Primary));
            ApplyAccentCommand = new DelegateCommand<Swatch>(ApplyAccent, new Predicate<Swatch>(s => s != Accent));
            ToggleBaseCommand = new DelegateCommand<bool?>(SetLightDark);
        }

        public ICommand ToggleBaseCommand { get; }
        public IEnumerable<Swatch> Swatches { get; }

        public DelegateCommand<Swatch> ApplyPrimaryCommand { get; }

        private void ApplyPrimary(Swatch swatch)
        {
            ReplacePrimaryColor(swatch);
            ApplyPrimaryCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand<Swatch> ApplyAccentCommand { get; }

        private void ApplyAccent(Swatch swatch)
        {
            ReplaceAccentColor(swatch);
            ApplyAccentCommand.RaiseCanExecuteChanged();
        }
    }
}
