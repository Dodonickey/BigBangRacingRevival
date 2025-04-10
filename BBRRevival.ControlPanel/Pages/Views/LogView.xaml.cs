﻿using BBRRevival.ControlPanel.Pages.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BBRRevival.ControlPanel.Pages.Views
{
    /// <summary>
    /// Interaction logic for LogView.xaml
    /// </summary>
    public partial class LogView : Page
    {
        LogViewModel ViewModel { get; }

        public LogView()
        {
            InitializeComponent();

            if (ViewModel is null)
            {
                ViewModel = new();
            }

            this.DataContext = ViewModel;
        }
    }
}
