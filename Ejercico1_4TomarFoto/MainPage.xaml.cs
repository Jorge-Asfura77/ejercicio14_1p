using Ejercico1_4TomarFoto.Models;
using Ejercico1_4TomarFoto.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ejercico1_4TomarFoto
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        Plugin.Media.Abstractions.MediaFile Filefoto = null;
        private Byte[] ConvertImageToByteArray()
        {
            if (Filefoto != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = Filefoto.GetStream();
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }

            }
            return null;

        }

        private async void tomarFoto_Clicked(object sender, EventArgs e)
        {

            //var
            Filefoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MisFotos",
                Name = "test.jpg",
                SaveToAlbum = true,
            });

            await DisplayAlert("path directorio", Filefoto.Path, "ok");

            if (Filefoto != null)
            {
                foto.Source = ImageSource.FromStream(() =>
                {
                    return Filefoto.GetStream();
                });
            }

        }
        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {

            if (Filefoto == null)
            {
                await DisplayAlert("Operación finalizada", "No se tomó ninguna fotografía", "Hecho");
                return;
            }

            var foto = new Foto
            {
                id = 0,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                foto = ConvertImageToByteArray(),
            };

            var result = await App.DBase.SavePhoto(foto);

            if (result > 0)
            {
                await DisplayAlert( "Operación finalizada", "La fotografía se guardó correctamente", "Hecho");
            }
            else
            {
                await DisplayAlert("Operación finalizada", "Error al guardar la fotografía", "Hecho");
            }

        }

        private async void btnListar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListFoto());
        }
    }
}
