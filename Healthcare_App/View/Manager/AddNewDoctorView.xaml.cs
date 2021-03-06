﻿using Healthcare_App.ViewModel.Manager;
using HealthcareData.Models;
using System.Windows;

namespace Healthcare_App.View.Manager
{
    /// <summary>
    /// Interaction logic for AddNewDoctorView.xaml
    /// </summary>
    public partial class AddNewDoctorView : Window
    {
        public AddNewDoctorView(int managerId)
        {
            InitializeComponent();
            this.DataContext = new AddNewDoctorViewModel(this, managerId);
        }
    }
}
