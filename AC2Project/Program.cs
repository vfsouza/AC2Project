using System.Collections;
using System.Text;

namespace AC2Project {
	class Program {


		public static Dictionary<string, int> mnemonicos = new Dictionary<string, int>();
		public static string fileName = "";

		public static void Main(String[] args) {
			try {
				IniciarMnemonicos();

				Console.WriteLine("Raiz do arquivo .ula: ");
				fileName = Console.ReadLine();
				string[] str = fileName.Split("\\");
				var inputFile = new StreamReader(new FileStream(fileName, FileMode.Open));

				ArrayList listExpressions = LerArquivo(inputFile);

				String outputFileName = fileName.Substring(0, fileName.LastIndexOf('.') + 1) + "hex";
				CriarArquivo(listExpressions, outputFileName);
				
				inputFile.Close();
			} catch (Exception e) {
				Console.WriteLine(e.StackTrace);
			}
		}

		public static ArrayList LerArquivo(StreamReader file) {
			ArrayList hex = new ArrayList();
			int a = 0;
			int b = 0;
			String line;

			while (file.Peek() != -1) {
				line = file.ReadLine();
				line = line.Replace("\n", "").Replace(" ", "");
				if (!(line.Equals("inicio:") || line.Equals("fim."))) {
					char aux = line.ElementAt(0);
					if (aux == 'X') {
						char c = line.ElementAt(2);
						a = c >= 48 && c <= 57 ? c - 48 : c - 55;
					} else if (aux == 'Y') {
						char c = line.ElementAt(2);
						b = c >= 48 && c <= 57 ? c - 48 : c - 55;
					} else {
						string mnemonico = line.Substring(2, line.Length - 3);
						string newString = new StringBuilder().Append(ParaHexa(a)).Append(ParaHexa(b)).Append(ParaHexa(mnemonicos[mnemonico])).ToString();
						hex.Add(newString);
					}
					} else if (line.Equals("fim.")) {
					break;
				}
			}

			return hex;
		}

		public static void IniciarMnemonicos() {
			mnemonicos.Add("An", 0);
			mnemonicos.Add("nAoB", 1);
			mnemonicos.Add("AnB", 2);
			mnemonicos.Add("zeroL", 3);
			mnemonicos.Add("nAeB", 4);
			mnemonicos.Add("Bn", 5);
			mnemonicos.Add("AxB", 6);
			mnemonicos.Add("ABn", 7);
			mnemonicos.Add("AnoB", 8);
			mnemonicos.Add("nAxB", 9);
			mnemonicos.Add("copiaB", 10);
			mnemonicos.Add("AB", 11);
			mnemonicos.Add("umL", 12);
			mnemonicos.Add("AoBn", 13);
			mnemonicos.Add("AoB", 14);
			mnemonicos.Add("copiaA", 15);
		}

		public static char ParaHexa(int value) {
			return (value < 10) ? value.ToString().ElementAt(0) : ((char)(value + 55));
		}

		public static void CriarArquivo(ArrayList result, String fileName) {
			StreamWriter sw;
			try {
				sw = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate));
			} catch (Exception e) {
				Console.WriteLine(e.StackTrace);
				return;
			}

			try {
				foreach (var expression in result) {
					try {
						sw.Write(expression + "\n");
					} catch (Exception e) {
						Console.WriteLine(e.StackTrace);
					}
				}
				sw.Write("fim ");
			} catch (Exception e) {
				Console.WriteLine(e.StackTrace);
			}

			sw.Close();
		}
	} 
}
