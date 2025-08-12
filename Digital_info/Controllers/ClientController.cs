using Digital_info.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Crypto.Tls;
using Stripe;
using static System.Net.Mime.MediaTypeNames;

namespace Digital_info.Controllers
{

	public class ClientController : Controller
	{
		public IActionResult Index()
		{
			Database.Connect();
			ViewData["services"] = new Services_DAO().getAll();


			Database.Close();
			return View();
		}
	
		public IActionResult Service_details(int id, int page = 1)//affichage la liste de detailles 
		{
			ViewData["ok"] = 0;


			Database.Connect();
			if (Tools.IsInt(id.ToString()) == false || Tools.IsInt(page.ToString()) == false)//verification des valleurs demandés
			{
				ViewData["ok"] = 502;

			}
			else
			{

				int nbrElementsByPage = 12;
				int dep = (page * nbrElementsByPage) - nbrElementsByPage;
				//string safeLimit = dep + "," + nbrElementsByPage;
				List<ProfessionalViewModel> ads = new Experience_DAO().getBestByNBR(dep);

				int countAll = new Client_DAO().GetCountAll();

				int nbrOfPages = (countAll / nbrElementsByPage) + 1;
				int prev = page - 1;
				int next = page + 1;
				if (page == 1)
				{
					prev = 1;
				}
				if (page == nbrOfPages)
				{
					next = page;
				}
				ViewData["nbrOfPages"] = nbrOfPages;
				ViewData["next"] = next;
				ViewData["prev"] = prev;
				ViewData["countAll"] = countAll;
				ViewData["page"] = page;

				var professionalViewModel = new Professional_DAO().getAllIdServicesVM(id);
				Professional professional = new Professional();
				ViewData["servicesAll"] = professionalViewModel;
				ViewData["pageId"] = id;
				Services services = new Services_DAO().getById(id);
				foreach (ProfessionalViewModel Pro in professionalViewModel)
				{
					professional = new Professional_DAO().getById(Pro.Professional.Id_professional);
				}

				ViewData["servicespro"] = professional;
			}
			ViewData["services"] = new Services_DAO().getAll();

			Database.Close();
			return View();
		}

		public IActionResult Profil(int idtodelete = 0, int idtodeleteproject = 0)  //affichage du profil 

		{
			Database.Connect();

			if (idtodelete > 0)//verification si l'id est supperieur a 0
			{
				new Interest_DAO().delete(idtodelete);
				return RedirectToAction("Profil", "Client");
			}
			ViewData["update"] = 0;
			ViewData["ok"] = 0;

			int client_id = 0;
			if (HttpContext.Session.GetInt32("Clients_id")>0)// verification des informations de l'id client est valable 
			{
				client_id = (int)HttpContext.Session.GetInt32("Clients_id");
				if (Tools.IsInt(client_id.ToString()) == false) // verification des informations 
				{
					ViewData["ok"] = 502;
				}
				else
				{
				 
					ViewData["Appointement"] = new Client_DAO().getByIdClientVM(client_id); //affichage des rendez-vous (appointement) par l'idclient

					ViewData["project"] = new Project_DAO().getByClientId(client_id); //affichage des projet des clients  
					if (idtodeleteproject > 0)
					{
						var subscri = new Appointment_DAO().getByProjectId(idtodeleteproject); 
						if (subscri == null) // veriffication si j'ai la subcription 
						{
							ViewData["ok"] = 400;
							
						}
						else if (subscri.Project_id != idtodeleteproject) 
						{
							new Project_DAO().delete(idtodeleteproject);


							return RedirectToAction("Profil", "Client");//retourne dans la page profil
						}
					
					}
					Database.Close();
				}
			
			}
			Database.Connect();
			string email = HttpContext.Session.GetString("Client_Email");
			ViewData["subscription"] = new Subscription_DAO().getSubcriptionByEmailClient(email);// subscription par l'email client affichage
			Database.Close();

			Database.Connect();
			ViewData["interestEmail"] = new Client_DAO().getByEmailClient(email);// interest par l'email client  affichage 

			Database.Close();

			return View();
		}
		public IActionResult Management(string? go, string? firstname, string? lastname, 
			string? address, string? city, string? phone, int age, string? email) // Management profil  pour modifier les informations personnel
		{	
			ViewData["update"] = 0;
			ViewData["ok"] = 0;

			Database.Connect();
			int client_id = 0;
			if (HttpContext.Session.GetInt32("Clients_id") > 0)//verifiaction si l'id client existe
			{
				client_id = (int)HttpContext.Session.GetInt32("Clients_id");
				
					ViewData["client"] = new Client_DAO().getById(client_id);

				if (go == "update" && client_id != null && firstname != null && lastname != null  //verifiaction si go a la  valeur update
					&& address != null && city != null && phone != null && age != null && email != null)
				{

					if (Tools.IsInt(client_id.ToString()) == false || Tools.IsInt(age.ToString()) == false)//verifiaction si les valleurs sont correcte 
					{
						ViewData["ok"] = 502;
					}
					else if (Tools.IsValiEamil(email) == false)//verifiaction si les valleurs sont correcte 
					{
						ViewData["ok"] = 500;

					}
					else
					{
						//sauvegarde des valleurs 
						Client client = new Client(client_id, firstname, lastname, address, city, phone, age, email);

						new Client_DAO().save(client);
						ViewData["update"] = 1;
						return RedirectToAction("Management", "Client");


					}
				}
			}
		


			Database.Close();

			return View();
		}
		
	}
}