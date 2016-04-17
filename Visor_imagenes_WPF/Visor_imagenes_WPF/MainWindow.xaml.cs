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
using Microsoft.Win32; //libreria obligatoria OpenFileDialog
using System.Windows.Interop;
using System.Diagnostics;

namespace Visor_imagenes_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog dlg_archivo;
        private List<String> ruta_archivos;
        private int ident;
       /* enum monitor_estado
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }*/
        public MainWindow()
        {
            InitializeComponent();
            this.dlg_archivo = new OpenFileDialog();
            this.dlg_archivo.Multiselect = true;
            this.dlg_archivo.Filter = "Imagenes JPG (*.jpg)|*jpg|Imagenes PNG (*.png)|*png|Imagenes BMP (*.bmp)|*bmp";

            this.ruta_archivos = null;
            this.ident = 0;

            //apagar pantalla
           // IntPtr windowHandle = Process.GetCurrentProcess().MainWindowHandle;
           // Fondo_Class.SetMonitorState((int)monitor_estado.OFF, windowHandle);
        }
        private void cargar_imagen(string path) {
            
            BitmapImage bitmapImage = new BitmapImage();//crear un objeto bitmapImage
            bitmapImage.BeginInit();//inicializar bitmapImage
            bitmapImage.UriSource = new Uri(path);//cargar primera imagen
            bitmapImage.EndInit();//Fin inicializacion bitmapImage
            this.contenedor_imagen.Source = bitmapImage; //mostrar imagen
        }
        private void btn_cargar_Click(object sender, RoutedEventArgs e)
        {
            if (this.dlg_archivo.ShowDialog() == true) {//si se selecciono una imagen
                //convertir el arreglo resultante en un lista
                this.ruta_archivos = this.dlg_archivo.FileNames.OfType<String>().ToList();
                cargar_imagen(this.ruta_archivos[0]);
               // Console.WriteLine(ruta_archivos[0].Replace(@"\",@"\\"));//cambiar \ por \\ del path
               /*BitmapImage bitmapImage = new BitmapImage();//crear un objeto bitmapImage
               bitmapImage.BeginInit();//inicializar bitmapImage
               bitmapImage.UriSource = new Uri(ruta_archivos[0]);//cargar primera imagen
               bitmapImage.EndInit();//Fin inicializacion bitmapImage
               this.contenedor_imagen.Source = bitmapImage; //mostrar imagen*/
               // this.contenedor_imagen.Source = new BitmapImage(new Uri(@"C:\Users\rock_\OneDrive\Imágenes\per.jpg", UriKind.Relative));
            }
        }

        private void btn_borrar_Click(object sender, RoutedEventArgs e)
        {
            if (this.ruta_archivos != null)//validacion
            {
                this.contenedor_imagen.Source = null;//resetea contenedor de imagen
                this.ruta_archivos.RemoveAt(ident);//remueve objeto seleccionado
                this.ruta_archivos = this.ruta_archivos.Count > 0 ? this.ruta_archivos : null;

                if (this.ruta_archivos != null)//validacion
                {
                    this.ident = this.ident == 0 ? this.ruta_archivos.Count - 1 : this.ident - 1;
                    cargar_imagen(this.ruta_archivos[ident]);
                }
            }
        }

        private void btn_copiar_Click(object sender, RoutedEventArgs e)
        {
            try
            {//validacion
                Clipboard.SetImage((BitmapImage)this.contenedor_imagen.Source);//copia al portapapeles

            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("No hay elementos","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btn_fondo_Click(object sender, RoutedEventArgs e)
        {
            if (this.ruta_archivos != null)
                Fondo_Class.SetDesktopWallpaper(this.ruta_archivos[ident]);
            else
                MessageBox.Show("Cargue una imagen antes.","Advertencia",MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        int con = 0;
        private void chk_ajuste_Checked(object sender, RoutedEventArgs e)
        {
            this.contenedor_imagen.Stretch = Stretch.Fill;
        }

        private void chk_ajuste_Unchecked(object sender, RoutedEventArgs e)
        {
            this.contenedor_imagen.Stretch = Stretch.Uniform;
        }

        private void btn_atras_Click(object sender, RoutedEventArgs e)
        {
            if (this.ruta_archivos != null)//validacion
            {
                this.ident = this.ident == 0 ? this.ruta_archivos.Count - 1 : this.ident - 1;
                cargar_imagen(this.ruta_archivos[ident]);
            }
        }

        private void btn_siguente_Click(object sender, RoutedEventArgs e)
        {
            if (this.ruta_archivos != null)//validacion
            {
                this.ident = this.ident == this.ruta_archivos.Count - 1 ? 0 : this.ident + 1;
                cargar_imagen(this.ruta_archivos[ident]);
            }
        }
    }
}
