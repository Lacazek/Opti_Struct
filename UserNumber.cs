using System.Collections.Generic;

namespace DOSIMETRY_PDF
{
	internal static class UserNumber
	{
		private static readonly Dictionary<string, string> _userNumber;
		public static string UserNumb(string key)
		{
			_userNumber.TryGetValue(key, out string value);
			return value;
		}

		static UserNumber()
		{
			_userNumber = new Dictionary<string, string>()
			{
				{"19917","FARCY JACQUET Marie Pierre"  },
				{"33383","LANG Philippe" },
				{"53391","BONS Françoise" },
				{"78712","DEBRIGODE Charles" },
				{"78847","AZRIA David" },
				{"109623","KOTZKI Léa"},
				{"127895","ZAIDI Fabien" },
				{"104357","MEGER Lionel" },
				{"104540","BIANCARDIE Marie" },
				{"506328","CASAS Melanie" },
				{"508899","ALRIC Karl" },
				{"508902","PIRON Bérengère" },
				{"515442","DUBOIS Karine" },
				{"519636","MOLINIER Gilles" },
				{"520602","VERSTRAET Rodolfe" },
				{"614512","MICHAUD Maxime" },
				{"129391","LACAZE Killian" },
				{"130255","TAMSAMANI Karima" },
				{"303568","RICARD Sandrine" },
				{"306078","REDONDO Emilie" },
				{"502382","COMBE Isabelle" },
				{"503013","DEPAULE Rose Marie" },
				{"504184","DUBOSCQ Isabelle" },
				{"506667","PAUL Marion" },
				{"507874","VINAS Raymond" },
				{"508447","BRIOUDES Magali" },
				{"508684","NICOLAS Helene" },
				{"513618","BARTOLETTI Virginie" },
				{"525034","DEMOULINGER Stephanie" },
				{"525045","BARATHIEU Laetitia" },
				{"527745","BENMOUFFOK Leah" },
				{"669763","HUGUES Florence" },
				{"704660","CLAVEL priscilla" },
                {"723687"," nan Camille" },
                {"909437","HUGUES Arnauld" },
				{"909845","HIPPOLITE Virginie" },
				{"913015","MAURIN Stephanie" },
				{"916396","ISAIA Emilie" }
			};
		}
	}
}
