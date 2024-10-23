using System;
using System.Linq;
using calculadora.Data;
using calculadora.Models;
using Microsoft.Maui.Controls;

namespace calculadora
{
    {
    public partial class HistorialPage : ContentPage
        CalculadoraContext db;

        public HistorialPage()
        {
            InitializeComponent();
            db = new CalculadoraContext();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var calculos = db.Calculos.OrderByDescending(c => c.FechaHora).ToList();
            listViewCalculos.ItemsSource = calculos;
        }
    }
}
