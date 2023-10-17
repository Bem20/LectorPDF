using iText.Kernel.Pdf;
using System.Windows.Forms;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Diagnostics;

namespace LectorPDF
{
    public partial class Form1 : Form
    {
        string file = @"C:\Users\Bem\Downloads\mala.pdf";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //Lectura de pdf's
        private void button1_Click(object sender, EventArgs e)
        {
            PdfReader pdfRead = new PdfReader(file);
            PdfDocument pdfDocu = new PdfDocument(pdfRead);

            string data = "";
            string strSource = "";

            for (int page = 1; page <= 1; page++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                data = PdfTextExtractor.GetTextFromPage(pdfDocu.GetPage(page), strategy);
                strSource = data;
                string textoExtraido = ExtraerTextoEspecifico(strSource);
                Debug.WriteLine(data);
                Debug.WriteLine("Texto Extraído: " + textoExtraido);
            }

            
        }
        private string ExtraerTextoEspecifico(string texto)
        {
            // Puedes utilizar técnicas de manipulación de cadenas para extraer el texto específico que necesitas.
            // Por ejemplo, si deseas extraer el texto entre "INICIO" y "FIN":
            int indiceInicio = texto.IndexOf("MUESTRA DE ");
            int indiceFin = texto.IndexOf(":", indiceInicio);

            if (indiceInicio >= 0 && indiceFin >= 0)
            {
                return texto.Substring(indiceInicio + "INICIO".Length, indiceFin - indiceInicio - "INICIO".Length);
            }
            else
            {
                return "Texto no encontrado.";
            }
        }

        //Mover de directorio los pdf
        private void button2_Click(object sender, EventArgs e)
        {
            string srcDir = @"C:\Biopsias";
            string destDir = @"C:\Biopsias\No tocar";


            try
            {
                if (Directory.Exists(srcDir) && Directory.Exists(destDir))
                {
                    DirectoryInfo dir = new DirectoryInfo(srcDir);
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        //Los convierte en bytes a medida que recorre los pdf de la carpeta biopsia
                        byte[] bytes = File.ReadAllBytes(@"C:\Biopsias\" + file.Name);

                        string destFile = Path.Combine(destDir, file.Name);
                        if (!File.Exists(destFile))
                        {
                            file.MoveTo(destFile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                throw;
            }
        }
    }
}