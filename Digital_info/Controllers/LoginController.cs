using Digital_info.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Crypto.Generators;
using Stripe;

namespace Digital_info.Controllers
{
	public class LoginController : Controller
	{
		private IWebHostEnvironment Environment;
		public LoginController(IWebHostEnvironment _environment)
		{
			Environment = _environment;
		}


		public IActionResult ClientLog(string? go,  string? firstname, string? lastname, string? address, string? city, 
			string? phone, int age, string? email, string? password, string? passwordComf) //methode de connexion pour le client
		{
			ViewData["ok"] = 0;
			ViewData["email"] = 0;
		
			ViewData["connexion"] = 0;
			ViewData["inscription"] = 0;
			Database.Connect();

			if (go == "clientLog")//verification si go a la valleur clientLog 
			{

				if (email != null  && password != null)//verification que le mot de passe et eimail eexiste
				{
					// verification si email est bon
					if (Tools.IsValiEamil(email) == false) { ViewData["email"] = 300; }
					else
					{
						Client c = new Client_DAO().login(email, password);///verification que les informations sont bon et existe


						if (c == null)//verification si c'est null les informations envoyes 
						{

							ViewData["connexion"] = 1;
						}
						else
						{

							//vsauvegarde dans le saission les informations 

							HttpContext.Session.SetInt32("Clients_id", c.Id_client);
							HttpContext.Session.SetString("Client_Email", c.Email);
							HttpContext.Session.SetString("firstname", c.Firstname);
							HttpContext.Session.SetString("lastname", c.Lastname);
							HttpContext.Session.SetString("Clients_Password", c.Password);

							return RedirectToAction("Profil", "Client");
						}
					}
				}
				}


			if (go == "clientSign")//verification si go à la valleur clientSing
			{



                if (firstname != null && lastname != null && phone != null && email != null && password != null && passwordComf != null)//veriffication si les valeurs sont reçu  
				{
					string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);//Achage du mots de passe  
				  //verification si l'email est bon 

					if (Tools.IsValiEamil(email) == false) { ViewData["email"] = 300; }
					else if (password != passwordComf ) //verification si le mot de passe sont les mêmes 
					{
						ViewData["inscription"] = 2;

					}
					else
					{
                        Database.Close();

                        Database.Connect();

                        //sauvegarde des informations 
                        Client c = new Client(firstname, lastname, address, city, phone, age, email, passwordHash);
						new Client_DAO().save(c);
						ViewData["inscription"] = 1;
						HttpContext.Session.SetInt32("Clients_id", c.Id_client);
						HttpContext.Session.SetString("firstname", c.Firstname);

						return RedirectToAction("ClientLog", "Login");
                        Database.Close();
                    }



                }

            }




            return View();
		}
		public IActionResult ProfLog(string? go, string? firstname, string? lastname, string? email, string? password, string? passwordComf,
			int siret, string? project_qualification, string? project_datail, string? availability, double price, string? success
			)
		{
			Database.Connect();

			ViewData["inscription"] = 0;
			ViewData["email"] = 0;
			ViewData["siret"] = 0;
			ViewData["connexion"] = 0;
			if (go == "professionalLogin")
			{
				//verification si c'est bien un email

				 if (siret != null && email != null  && password != null)//
				{
					// verification si les int sont bien des chiffres 
					if (Tools.IsInt(siret.ToString())  == false) { ViewData["siret"] = 201; }
					else if (Tools.IsValiEamil(email) == false) { ViewData["email"] = 301; }                    // verification si l'email est bien un email
					else
					{
						
						Professional c = new Professional_DAO().login(siret, email, password);
						if (c == null)// verification si les valleur existe et si c'est bon 
						{
							ViewData["connexion"] = 2;
						}
						else
						{
							//sa
							//envois de données 

							HttpContext.Session.SetInt32("Professional_id", c.Id_professional);
							HttpContext.Session.SetString("Professional_Email", c.Email);
							HttpContext.Session.SetString("firstname", c.Firstname);
							HttpContext.Session.SetString("lastname", c.Lastname);
							HttpContext.Session.SetString("Professional_Password", c.Password);


							return RedirectToAction("Services", "Professional");
						}
					}

		
				}
			}

			if ( go == "professionalSign")//verification si il y a professionalSign comme valleur dans go 
			{
				

				if ( siret != null && firstname != null && lastname != null && email != null && password != null && passwordComf != null)//verrification d'information si existe
					{
					if (  Tools.IsInt(siret.ToString())==false) { ViewData["siret"] = 202; } 

					else if (Tools.IsValiEamil(email) == false) { ViewData["email"] = 301; }

					else if (password != passwordComf )
						{

						ViewData["inscription"] = 2;

					}
					else
						{
						string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

						Professional c = new Professional(siret, firstname, lastname, project_qualification, project_datail, availability, price,
								success, email, passwordHash);
						new Professional_DAO().save(c);
						ViewData["inscription"] = 1;
						HttpContext.Session.SetInt32("Professional_id", c.Id_professional);
						HttpContext.Session.SetString("firstname", c.Firstname);

						return RedirectToAction("ProfLog", "Login");

						}
					}

				}
				
			


			ViewData["client"] = new Professional_DAO().getAll();
			Database.Close();
			return View();
		}
		public IActionResult Logout()
		{
		
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult ManagementPassword(string go, string passwordbefore, string? password, string passwordComf , int siret)
		{
			ViewData["ok"] = 0;
			ViewData["update"] = 0;
			ViewData["siret"] = 0;
		   Database.Connect();


			if (go == "updateCli")
			{
				string email = HttpContext.Session.GetString("Client_Email");
				//if (IsValiEamil(email) == false) { ViewData["email"] = 1;IsValiEamil(email) == true && }
				if (email != null && password != null && passwordComf != null && passwordbefore != null)//verification si les valleurs sont reçu
				{
					Client c = new Client_DAO().login(email, passwordbefore);

					if (c == null || passwordComf == passwordbefore || password != passwordComf)//verification du mot de passe 
					{
						ViewData["ok"] = 502;
					}
				
					else
					{
						string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
						int client_id = (int)HttpContext.Session.GetInt32("Clients_id");

						Client client = new Client(client_id, passwordHash);

						new Client_DAO().savePassword(client);



						ViewData["update"] = 1;

						return RedirectToAction("Profil", "Client");
					}
				}

				Database.Close();
			} 
			if (go == "updatePro")
			{
					

					Database.Connect();


				
				string email = HttpContext.Session.GetString("Professional_Email");
				//if (IsValiEamil(email) == false) { ViewData["email"] = 1;IsValiEamil(email) == true && }
				if (email != null && password != null && passwordComf != null && passwordbefore != null)
				{
					Professional c = new Professional_DAO().login(siret, email, passwordbefore);

					if (c == null || passwordComf == passwordbefore || password != passwordComf)
					{
						ViewData["ok"] = 502;
					}
		
					else
					{
						string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
						int id_professional = (int)HttpContext.Session.GetInt32("Professional_id");

						Professional Professional = new Professional(id_professional, passwordHash);

						new Professional_DAO().savePassword(Professional);



						ViewData["update"] = 1;

						return RedirectToAction("Services"  , "Professional");
					}
				}



				Database.Close();

			}




			return View();
		}

	}
}
