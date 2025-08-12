using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace Digital_info.Models
{
	public class Tools
	{
		public static bool VerifSiret(string siret)
		{
		   string	RegexSiret = "^[0-9]{14}$";
			if (Regex.IsMatch(siret, RegexSiret) == true)
			{
				return true;

			}
			else
			{
				return false;

			}
		}
		public static string Getvalcheckbox(string oryes)
		{
			string Lower = oryes == "true" ? "oui" : "non";
			if (!string.IsNullOrEmpty(oryes))
			{
				if (oryes == "OUIS" || oryes == "NON")
				{
					Lower = oryes.ToLower();
				}
			
			}
			return Lower;
		}
		
		public static bool verifTel(string tel)
		{
			bool ok = true;
			string rg_fr = "^[1-689]\\d{8}$";
			if (Regex.IsMatch(tel,rg_fr)) { return ok; }
			if (tel.Length < 8 || tel.Length > 20) { ok = false; } 
			else
			{
				ok = true;	
			} 
			return ok;	
			
		}
		public static bool VerifDate(string Date)
		{
			string rg_date = "((2000|2400|2800|(19|2[0-9])(0[48]|[2468][048]|[13579][26]))(\\/|-|\\.)02(\\/|-|\\.)28)|^(((19|2[0-9])[0-9]{2})-02-(0[1-9]|1[0-9]|2[0-8]))|^(((19|2[0-9])[0-9]{2})(\\/|-|\\.)(0[13578]|10|12)(\\/|-|\\.)(0[1-9]|[12][0-9]|3[01]))|^(((19|2[0-9])[0-9]{2})(\\/|-|\\.)(0[469]|11)(\\/|-|\\.)(0[1-9]|[12][0-9]|30))" ;
			if (Regex.IsMatch(Date, rg_date) == true)
			{
				return true;
			}
			else
			{
				
				return false;
			}

		
		
		}

	

		public static bool IsValiEamil(string email)
		{
			try
			{
				var add = new System.Net.Mail.MailAddress(email);
				return add.Address.Equals(email);
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool IsInt(string stringNumber)
		{

			Int64 numericValue;


			bool isNumber = Int64.TryParse(stringNumber, out numericValue);
			if (isNumber == false || stringNumber == "0" || stringNumber == null)
			{

				isNumber = false;

			}
		
			

			return isNumber;
		}
		public static bool IsString(string stringNumber)
		{

			Int64 numericValue;


			bool isNumber = !Int64.TryParse(stringNumber, out numericValue);

			if (isNumber == false || stringNumber == null || stringNumber.Length > 255)
			{

				isNumber = false;
			}



			return isNumber;
		}
		public static bool validateMDP(string mdp)
		{
			//Si le mot de passe n'est pas assez sécurisé, retournez false
			//avec  caractère spécial
			string RegPassword = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
			if (Regex.IsMatch(mdp, RegPassword) == true)
			{
				return true;
			}
			else
			{

				return false;
			}

		
		}
		public static bool IsValidTimeFormat(string OutputDate)
		{
			TimeSpan dummyOutput;

			return TimeSpan.TryParse(OutputDate, out dummyOutput);
		}
		public static bool IsValidTime(string Time)
		{
			//si l'heure n'est pas bon, retournez false
			//heure sur 24 h 
			string RegTime = "^((0[0-9]|1[0-9]|2[0-3]):){1}(([0-5][0-9]){1})$";
			if (Regex.IsMatch(Time, RegTime) == true)
			{
				return true;
			}
			else
			{

				return false;
			}


		}
		
	}
}
