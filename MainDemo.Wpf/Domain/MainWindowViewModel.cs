using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;

namespace MaterialDesignDemo.Domain
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            _allItems = GenerateDemoItems(snackbarMessageQueue);
            FilterItems(null);

            MovePrevCommand = new AnotherCommandImplementation(
                _ =>
                {
                    if (!string.IsNullOrWhiteSpace(SearchKeyword))
                        SearchKeyword = string.Empty;

                    SelectedIndex--;
                },
                _ => SelectedIndex > 0);

            MoveNextCommand = new AnotherCommandImplementation(
               _ =>
               {
                   if (!string.IsNullOrWhiteSpace(SearchKeyword))
                       SearchKeyword = string.Empty;

                   SelectedIndex++;
               },
               _ => SelectedIndex < _allItems.Count - 1);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly ObservableCollection<DemoItem> _allItems;
        private ObservableCollection<DemoItem>? _demoItems;
        private DemoItem? _selectedItem;
        private int _selectedIndex;
        private string? _searchKeyword;

        public string? SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchKeyword)));
                FilterItems(_searchKeyword);
            }
        }

        public ObservableCollection<DemoItem>? DemoItems
        {
            get => _demoItems;
            set
            {
                _demoItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DemoItems)));
            }
        }

        public DemoItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value == null || value.Equals(_selectedItem)) return;

                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
            }
        }

        public AnotherCommandImplementation MovePrevCommand { get; }
        public AnotherCommandImplementation MoveNextCommand { get; }

        private ObservableCollection<DemoItem> GenerateDemoItems(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue is null)
                throw new ArgumentNullException(nameof(snackbarMessageQueue));

            return new ObservableCollection<DemoItem>
            {
                new DemoItem("Home", new Home(),
                    new []
                    {
                        new DocumentationLink(DocumentationLinkType.Wiki, $"{ConfigurationManager.AppSettings["GitHub"]}/wiki", "WIKI"),
                        DocumentationLink.DemoPageLink<Home>()
                    }
                ),
                new DemoItem("Palette", new PaletteSelector { DataContext = new PaletteSelectorViewModel() },
                    new []
                    {
                        DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                        DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                        DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                        DocumentationLink.DemoPageLink<PaletteSelector>("Demo View"),
                        DocumentationLink.DemoPageLink<PaletteSelectorViewModel>("Demo View Model"),
                        DocumentationLink.ApiLink<PaletteHelper>()
                    }),
                new DemoItem("Color Tool", new ColorTool { DataContext = new ColorToolViewModel() },
                    new []
                    {
                        DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                        DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                        DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                        DocumentationLink.DemoPageLink<ColorTool>("Demo View"),
                        DocumentationLink.DemoPageLink<ColorToolViewModel>("Demo View Model"),
                        DocumentationLink.ApiLink<PaletteHelper>()
                    }),
                new DemoItem("Theme Editor", new ThemeEditor(),
                    new []
                    {
                        DocumentationLink.ApiLink<ThemeEditor>()
                    }),
                new DemoItem("Buttons", new Buttons { DataContext = new ButtonsViewModel() } ,
                    new []
                    {
                        DocumentationLink.WikiLink("Button-Styles", "Buttons"),
                        DocumentationLink.DemoPageLink<Buttons>("Demo View"),
                        DocumentationLink.DemoPageLink<ButtonsViewModel>("Demo View Model"),
                        DocumentationLink.StyleLink("Button"),
                        DocumentationLink.StyleLink("PopupBox"),
                        DocumentationLink.ApiLink<PopupBox>()
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new DemoItem("Toggles", new Toggles(), 
                    new []
                    {
                        DocumentationLink.DemoPageLink<Toggles>(),
                        DocumentationLink.StyleLink("ToggleButton"),
                        DocumentationLink.StyleLink("CheckBox"),
                        DocumentationLink.ApiLink<Toggles>()
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new DemoItem("Rating Bar", new RatingBar(), new []
                {
                    DocumentationLink.DemoPageLink<RatingBar>(),
                    DocumentationLink.StyleLink("RatingBar"),
                    DocumentationLink.ApiLink<RatingBar>()
                }),
                new DemoItem("Fields", new Fields(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Fields>(),
                        DocumentationLink.StyleLink("TextBox")
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new DemoItem("Fields line up", new FieldsLineUp(), new []
                {
                    DocumentationLink.DemoPageLink<FieldsLineUp>()
                }),
                new DemoItem("ComboBoxes", new ComboBoxes(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<ComboBoxes>(),
                        DocumentationLink.StyleLink("ComboBox")
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new DemoItem("Pickers", new Pickers { DataContext = new PickersViewModel()},
                    new []
                    {
                        DocumentationLink.DemoPageLink<Pickers>(),
                        DocumentationLink.StyleLink("Clock"),
                        DocumentationLink.StyleLink("DatePicker"),
                        DocumentationLink.ApiLink<TimePicker>()
                    }),
                new DemoItem("Sliders", new Sliders(), new []
                    {
                        DocumentationLink.DemoPageLink<Sliders>(),
                        DocumentationLink.StyleLink("Slider")
                    }),
                new DemoItem("Chips", new Chips(), new []
                    {
                        DocumentationLink.DemoPageLink<Chips>(),
                        DocumentationLink.StyleLink("Chip"),
                        DocumentationLink.ApiLink<Chip>()
                    }),
                new DemoItem("Typography", new Typography(), new []
                    {
                        DocumentationLink.DemoPageLink<Typography>(),
                        DocumentationLink.StyleLink("TextBlock")
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto,
                        HorizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },
                new DemoItem("Cards", new Cards(), new []
                    {
                        DocumentationLink.DemoPageLink<Cards>(),
                        DocumentationLink.StyleLink("Card"),
                        DocumentationLink.ApiLink<Card>()
                    })
                {
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
                new DemoItem("Icon Pack", new IconPack { DataContext = new IconPackViewModel(snackbarMessageQueue) },
                    new []
                    {
                        DocumentationLink.DemoPageLink<IconPack>("Demo View"),
                        DocumentationLink.DemoPageLink<IconPackViewModel>("Demo View Model"),
                        DocumentationLink.ApiLink<PackIcon>()
                    }),
                new DemoItem("Colour Zones", new ColorZones(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<ColorZones>(),
                        DocumentationLink.ApiLink<ColorZone>()
                    }),
                new DemoItem("Lists", new Lists { DataContext = new ListsAndGridsViewModel()},
                    new []
                    {
                        DocumentationLink.DemoPageLink<Lists>("Demo View"),
                        DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                        DocumentationLink.StyleLink("ListBox"),
                        DocumentationLink.StyleLink("ListView")
                    })
                {
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
                new DemoItem("Trees", new Trees { DataContext = new TreesViewModel() },
                    new []
                    {
                        DocumentationLink.DemoPageLink<Trees>("Demo View"),
                        DocumentationLink.DemoPageLink<TreesViewModel>("Demo View Model"),
                        DocumentationLink.StyleLink("TreeView")
                    }),
                new DemoItem("Data Grids", new DataGrids { DataContext = new ListsAndGridsViewModel()},
                    new []
                    {
                        DocumentationLink.DemoPageLink<DataGrids>("Demo View"),
                        DocumentationLink.DemoPageLink<ListsAndGridsViewModel>("Demo View Model", "Domain"),
                        DocumentationLink.StyleLink("DataGrid")
                    }),
                new DemoItem("Expander", new Expander(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Expander>(),
                        DocumentationLink.StyleLink("Expander")
                    }),
                new DemoItem("Group Boxes", new GroupBoxes(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<GroupBoxes>(),
                        DocumentationLink.StyleLink("GroupBox")
                    }),
                new DemoItem("Menus & Tool Bars", new MenusAndToolBars(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<MenusAndToolBars>(),
                        DocumentationLink.StyleLink("Menu"),
                        DocumentationLink.StyleLink("ToolBar")
                    }),
                new DemoItem("Progress Indicators", new Progress(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Progress>(),
                        DocumentationLink.StyleLink("ProgressBar")
                    }),
                new DemoItem("Navigation Rail", new NavigationRail { DataContext = null},
                    new []
                    {
                        DocumentationLink.DemoPageLink<NavigationRail>("Demo View"),
                        DocumentationLink.StyleLink("TabControl"),
                    })
                {
                    VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                },
                new DemoItem("Dialogs", new Dialogs { DataContext = new DialogsViewModel()},
                    new []
                    {
                        DocumentationLink.WikiLink("Dialogs", "Dialogs"),
                        DocumentationLink.DemoPageLink<Dialogs>("Demo View"),
                        DocumentationLink.DemoPageLink<DialogsViewModel>("Demo View Model", "Domain"),
                        DocumentationLink.ApiLink<DialogHost>()
                    }),
                new DemoItem("Drawer", new Drawers(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Drawers>("Demo View"),
                        DocumentationLink.ApiLink<DrawerHost>()
                    }),
                new DemoItem("Snackbar", new Snackbars(),
                    new []
                    {
                        DocumentationLink.WikiLink("Snackbar", "Snackbar"),
                        DocumentationLink.DemoPageLink<Snackbars>(),
                        DocumentationLink.StyleLink("Snackbar"),
                        DocumentationLink.ApiLink<Snackbar>(),
                        DocumentationLink.ApiLink<ISnackbarMessageQueue>()
                    }),
                new DemoItem("Transitions", new Transitions(),
                    new []
                    {
                        DocumentationLink.WikiLink("Transitions", "Transitions"),
                        DocumentationLink.DemoPageLink<Transitions>(),
                        DocumentationLink.ApiLink<Transitioner>("Transitions"),
                        DocumentationLink.ApiLink<TransitionerSlide>("Transitions"),
                        DocumentationLink.ApiLink<TransitioningContent>("Transitions"),
                    }),
                new DemoItem("Shadows", new Shadows(),
                    new []
                    {
                        DocumentationLink.DemoPageLink<Shadows>(),
                    })
            };
        }

        private void FilterItems(string? keyword)
        {
            var filteredItems =
                string.IsNullOrWhiteSpace(keyword) ?
                _allItems :
                _allItems.Where(i => i.Name.ToLower().Contains(keyword!.ToLower()));

            DemoItems = new ObservableCollection<DemoItem>(filteredItems);
        }
    }
}