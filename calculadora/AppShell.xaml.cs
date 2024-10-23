using Calculadora.Data;
using Calculadora.Models;

namespace Calculadora;

public partial class MainPage : ContentPage
{
    double resultado = 0;
    string operacion = "";
    bool operacionPresionada = false;
    CalculadoraContext db;

    public MainPage()
    {
        InitializeComponent();
        db = new CalculadoraContext();
    }

    void OnNumberClicked(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        if ((txtDisplay.Text == "0") || operacionPresionada)
        {
            txtDisplay.Text = boton.Text;
            operacionPresionada = false;
        }
        else
        {
            txtDisplay.Text += boton.Text;
        }
    }

    void OnOperatorClicked(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        operacion = boton.Text;
        resultado = double.Parse(txtDisplay.Text);
        operacionPresionada = true;
    }

    void OnEqualClicked(object sender, EventArgs e)
    {
        double segundoNumero = double.Parse(txtDisplay.Text);
        double finalResult = 0;

        switch (operacion)
        {
            case "+":
                finalResult = resultado + segundoNumero;
                break;
            case "−":
                finalResult = resultado - segundoNumero;
                break;
            case "×":
                finalResult = resultado * segundoNumero;
                break;
            case "÷":
                if (segundoNumero != 0)
                {
                    finalResult = resultado / segundoNumero;
                }
                else
                {
                    DisplayAlert("Error", "No se puede dividir entre cero", "OK");
                    return;
                }
                break;
            default:
                return;
        }

        txtDisplay.Text = finalResult.ToString();
        operacionPresionada = false;

        // Guardar el cálculo en la base de datos
        GuardarCalculo($"{resultado} {operacion} {segundoNumero} = {finalResult}");
    }

    void OnClearClicked(object sender, EventArgs e)
    {
        txtDisplay.Text = "0";
        resultado = 0;
        operacion = "";
        operacionPresionada = false;
    }

    void OnNegateClicked(object sender, EventArgs e)
    {
        double numero = double.Parse(txtDisplay.Text);
        numero = -numero;
        txtDisplay.Text = numero.ToString();
    }

    void OnDecimalClicked(object sender, EventArgs e)
    {
        if (!txtDisplay.Text.Contains("."))
        {
            txtDisplay.Text += ".";
        }
    }

    void OnPercentageClicked(object sender, EventArgs e)
    {
        double numero = double.Parse(txtDisplay.Text);
        numero = numero / 100;
        txtDisplay.Text = numero.ToString();
    }

    void GuardarCalculo(string operacionRealizada)
    {
        var calculo = new Calculo
        {
            Operacion = operacionRealizada,
            Resultado = txtDisplay.Text,
            FechaHora = DateTime.Now
        };
        db.Calculos.Add(calculo);
        db.SaveChanges();
    }

    async void OnMostrarCalculosClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HistorialPage());
    }
}
