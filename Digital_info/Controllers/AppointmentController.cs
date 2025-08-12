using Digital_info.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Runtime.ConstrainedExecution;

namespace Digital_info.Controllers
{
	public class AppointmentController : Controller
	{
		public IActionResult Index(int idProfessional , string? go, string? date, string time, string? appointment_titre, string? type, int id=0)
		{
			/*if (HttpContext.Session.GetInt32("Professional_id") == 0 || HttpContext.Session.GetString("Professional_Email") == null || HttpContext.Session.GetString("firstname") == null || HttpContext.Session.GetString("lastname") == null)
			{
				return RedirectToAction("Index", "Home");


			}*/
			Database.Connect();
			ViewData["ok"] = 0;
			ViewData["update"] = 0;
			List<ProfessionalViewModel> ser = new List<ProfessionalViewModel>();
			type = HttpContext.Session.GetString("type");
			int idproject = 0;
			if (id !=null)//condition pour la suppression des informations si id envoyer est nulle 
			{
				new Appointment_DAO().delete(id);
				return RedirectToAction("Profil", "Client");
			}
			if (type != null && HttpContext.Session.GetInt32("idProject") > 0 ) // condition pour savoir j'ai un type de projet er si j'ai un projet 
			{
				ser = new Professional_DAO().getServiceByType(type);
				idproject=(int)HttpContext.Session.GetInt32("idProject");

				if (go == "appointment" && date != null && appointment_titre != null)//condition pour savoir si j'ai go à la valeur appointment et si j'ai une date et un titre de rendez-vous
				{
					if (Tools.VerifDate(date)==false || Tools.IsValidTime(time) ==false)//verification des valleurs si correcte 
					{
						ViewData["ok"] = 500;
					}
					else if (Tools.IsInt(idProfessional.ToString()) == false || Tools.IsInt(idproject.ToString())== false) //verification des valleurs si correcte 
					{
						ViewData["ok"] = 502;

					}
					else
					{
						//sauvegarde des valeurs 
						DateTime dateandtime = DateTime.Parse( date +" "+ time);	
						Appointment appointment = new Appointment(idProfessional, idproject, dateandtime, appointment_titre);
						new Appointment_DAO().save(appointment);
						return RedirectToAction("Profil", "Client");

					}

				}

			}
			ViewData["servicetype"] = ser;


			Database.Close();

			return View();


		}
		public IActionResult EditAppointment( string? go, string? date, string time, string? appointment_titre, string? type, int id = 0, int idProfessional =0)
		{
			Database.Connect();
			ViewData["ok"] = 0;
			ViewData["update"] = 0;
			ViewData["type_project"] = "";
			List<ProfessionalViewModel> ser = new List<ProfessionalViewModel>();
			int idproject = 0;
			
				//codition pour l'afchage des vendez-vous selon l'id '
									

			if (id > 0)
			{

				var subscri = new Appointment_DAO().getById(id);
				ser = new Professional_DAO().getServiceByType(type); //traitement pour trouver le type du qui le nom du service proposer par un professionnel   '

				idproject = subscri.Project_id;      //traitement pour trouver l'id du projet   '

				ViewData["type_project"]= new Project_DAO().getById(idproject);
				

				

				if (go == "update")//sauvegarde les informations si go a la valeur update 
				{
					if (Tools.VerifDate(date) == false || Tools.IsValidTime(time) == false) //verification que les informations soit ceux demandé 
					{
						ViewData["ok"] = 500;
					}
					else if (Tools.IsInt(idProfessional.ToString()) == false || Tools.IsInt(idproject.ToString()) == false) //verification que les informations soit ceux demandé 
					{
						ViewData["ok"] = 502;

					}
					else
					{
						// sauvegarde les informations 
						DateTime dateandtime = DateTime.Parse(date + " " + time);
						Appointment appointment = new Appointment(id, idProfessional, idproject, dateandtime, appointment_titre);
						new Appointment_DAO().save(appointment);
						return RedirectToAction("Profil", "Client");

					}
				}


				ViewData["service"] = subscri;



			}
			ViewData["services"] = new Services_DAO().getAll();
			ViewData["servicetype"] = ser;

			Database.Close();

			return View();
		}
	}
}
