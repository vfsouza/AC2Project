using System.Collections;
using System.Text;

namespace AC2Project {
	class Program {


		public static Dictionary<string, int> mnemonicos = new Dictionary<string, int>();

		public static void Main(String[] args) {
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

			Console.WriteLine("Raiz do arquivo .ula: ");
			string fileName = Console.ReadLine();
			using (var inputFile = new StreamReader(new FileStream(fileName, FileMode.Open))) {
				ArrayList hex = LerArquivo(inputFile);

				String fileNameOut = fileName.Substring(0, fileName.LastIndexOf('.') + 1) + "hex";
				CriarHex(hex, fileNameOut);
			}
		}

		public static ArrayList LerArquivo(StreamReader file) {
			ArrayList hex = new ArrayList();
			int a = 0, b = 0;

			while (true) {
				string line = file.ReadLine();
				line = line.Replace("\n", "").Replace(" ", "");
				if (line.Equals("fim.")) {
					break;
				}
				if (!line.Equals("inicio:")) { 
					char aux = line.ElementAt(0);
					if (aux == 'X') {
						char c = line.ElementAt(2);
						a = c >= 48 && c <= 57 ? c - 48 : c - 55;
					} else if (aux == 'Y') {
						char c = line.ElementAt(2);
						b = c >= 48 && c <= 57 ? c - 48 : c - 55;
					} else {
						string mnemonico = line.Substring(2, line.Length - 3);
						string newString = "" + ParaHex(a) + ParaHex(b) + ParaHex(mnemonicos[mnemonico]).ToString();
						hex.Add(newString);
					}
				}
			}

			return hex;
		}

		public static char ParaHex(int value) {
			return (value < 10) ? value.ToString().ElementAt(0) : ((char)(value + 55));
		}

		public static void CriarHex(ArrayList list, String fileName) {
			using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate))) {
				foreach (var elem in list) {
					sw.Write(elem + "\n");
				}
				sw.Write("fim ");
			}
		}
	}
}
