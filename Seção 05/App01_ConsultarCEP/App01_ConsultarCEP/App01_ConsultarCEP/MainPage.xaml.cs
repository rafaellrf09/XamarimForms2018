using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            botaoBuscar.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = textoCEP.Text.Trim();
           
            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    if (end != null)
                    {
                        labelCEP.Text = string.Format("Endereço: {0}, {1} {2}", end.localidade, end.uf, end.logradouro);
                    }
                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrado para o CEP: " + cep + " informado.", "OK");
                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("Erro Crítico", e.Message, "OK");
                }

                
            }
            

          
        }

        private bool isValidCEP(string cep)
        {
            Boolean valid = true;
            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP Inválido! O CEP deve conter 8 caracteres.", "OK");
                valid = false;
            }
            int novocep = 0;
            if (!int.TryParse(cep, out novocep))
            {
                DisplayAlert("Erro", "CEP Inválido! O CEP deve ser apenas de números.", "OK");
                valid = false;
            }
            return valid;
        }
	}
}
