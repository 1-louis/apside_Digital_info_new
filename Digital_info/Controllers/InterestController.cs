using Digital_info.Models;
using Microsoft.AspNetCore.Mvc;

namespace Digital_info.Controllers
{
	public class InterestController : Controller
	{
		public IActionResult Index()
		{
			Database.Connect();

			ViewData["ExperienceTop8"] = new Experience_DAO().getBestByNBR(8);//affichage des 8 meilleurs  experices 

			Database.Close();
			return View();
		}
		public IActionResult Consultancy( string go,  string type_project, int nbr_person, string description, double price, int clients_id = 0, int id = 0)//methode interest client  
		{
			ViewData["ok"] = 0;
			ViewData["update"] = 0;

			Database.Connect();
			clients_id = (int)HttpContext.Session.GetInt32("Clients_id");
			if (go == "interest" && clients_id != null && type_project != null && nbr_person != null && description != null && price != null)//condition si il y a tous les informations
			{

				if (Tools.IsInt(clients_id.ToString()) == false || Tools.IsInt(price.ToString()) == false || Tools.IsInt(nbr_person.ToString()) == false) //verification des chiffres  
				{
					ViewData["ok"] = 502;

				}
				else if (Tools.IsString(type_project) == false)// verification de chain de caracters
				{
					ViewData["ok"] = 500;

				}
				else
				{
					///validation des informations

					Interest interest = new Interest(clients_id, type_project, nbr_person, description, price);
					new Interest_DAO().save(interest);
					ViewData["ok"] = 1;
					return RedirectToAction("Profil", "Client");


				}
			}

			Database.Close();
			Database.Connect();
			if (id > 0 )// condition si j'ai l'id
				{
					ViewData["update"] = 1;
					if (go == "update" && clients_id != null && type_project != null && nbr_person != null && description != null && price != null) //veriffication si il y a les valeurs
					{

						if (Tools.IsInt(clients_id.ToString()) == false || Tools.IsInt(price.ToString()) == false || Tools.IsInt(nbr_person.ToString()) == false) // verification des valleur int
						{
							ViewData["ok"] = 502;

						}
						else if (Tools.IsString(type_project) == false)// verification des chaine de caracters
						{
							ViewData["ok"] = 500;

						}
						else
						{

						//validation des valeurs 
							Interest i = new Interest(id,clients_id, type_project, nbr_person, description, price);
							new Interest_DAO().save(i);
							ViewData["ok"] = 1;
							return RedirectToAction("Profil", "Client");


						}
						/*Interest interest = new Interest(id, clients_id, type_project, nbr_person, description, price);
						new Interest_DAO().save(interest);
						ViewData["ok"] = 1;
						return RedirectToAction("Profil", "Client");*/
					}
				}

			

			ViewData["services"] = new Services_DAO().getAll();///Affichage des services  
			ViewData["interest"] = new Interest_DAO().getById(id);/// /Affichage de interst du client  

			Database.Close();
			return View();
		}
		
	}
}
