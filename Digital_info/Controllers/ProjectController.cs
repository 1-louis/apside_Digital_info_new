using BCrypt.Net;
using Digital_info.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Crypto.Generators;
using Renci.SshNet.Messages;
using Stripe;
using System;
using System.Runtime.ConstrainedExecution;
using Subscription = Digital_info.Models.Subscription;

namespace Digital_info.Controllers
{


	
	
	public class ProjectController : Controller
	{
		private  DateOnly ToDateOnly( DateTime dateTime)
		{
			return DateOnly.FromDateTime(dateTime);
		}
		public bool IsValidTimeFormat(string OutputDate)
		{
			TimeSpan dummyOutput;

			return TimeSpan.TryParse(OutputDate, out dummyOutput);
		}
	
		private int NombreDeMois(DateTime DateDebut, DateTime DateFin)
		{
			int mois = 0;// init à 0 car on va compter
			if (DateDebut.Year != DateFin.Year)
			{
				// traitement première année
				for (int compteur = DateDebut.Month; compteur <= 12; ++compteur)
					mois++;
				// traitement des années pleines
				mois += (DateFin.Year - (DateDebut.Year + 1)) * 12;
				// traitement dernière année
				for (int compteur = 1; compteur <= DateFin.Month; ++compteur)
					mois++;
			}
			else
				for (int compteur = DateDebut.Month; compteur <= DateFin.Month; ++compteur)
					mois++;
			return mois;
		}
		public IActionResult Index(string? go, string? number, string? type, string? name, string? estimates, int nbr_peron, string siret, string? description, int id = 0)
		{

			ViewData["ok"] = 0;
			ViewData["update"] = 0;
			ViewData["project"] = null;
			if (HttpContext.Session.GetInt32("Clients_id") == 0)
			{
				ViewData["ok"] = 500;
			}
			else
			{
				int client_id = (int)HttpContext.Session.GetInt32("Clients_id");

			
			Database.Connect();
			var pro = new Project_DAO().getAll();

			if (go == "project" && type != null && name != null && estimates != null && description != null)
			{
				if (HttpContext.Session.GetInt32("Clients_id") > 0)
				{


					/*
									if (Tools.IsInt(client_id.ToString()) == true || Tools.IsInt(number.ToString()) == true || Tools.IsInt(nbr_peron.ToString()) == true || Tools.IsInt(siret.ToString()) == true)
									{
										ViewData["ok"] = 500;

									}*/



					Project p = new Project(client_id, number, type, name, estimates, nbr_peron, siret, description);
					new Project_DAO().save(p);
					HttpContext.Session.SetInt32("Professional_id", client_id);


					foreach (Project proj in pro)
					{
						Project project = new Project_DAO().getById(proj.Id_project);
						Console.WriteLine(project.Type, project.Id_project);
						if (project != null && type == project.Type && project.Id_project > 0)
						{
							HttpContext.Session.SetInt32("idProject", project.Id_project);

							HttpContext.Session.SetString("type", project.Type);
						}
					}

					ViewData["ok"] = 1;
					return RedirectToAction("Index", "Appointment");

				}
				else
				{
					View();

				}
			}
			Database.Close();

			Database.Connect();

			if (id > 0)
			{
				ViewData["update"] = 1;
				if (go == "update")
				{
					//&& type != null && name != null && estimates != null && description != null
					Project p = new Project(id, client_id, number, type, name, estimates, nbr_peron, siret, description);
					new Project_DAO().save(p);
					HttpContext.Session.SetInt32("Professional_id", client_id);
					foreach (Project proj in pro)
					{
						Project project = new Project_DAO().getById(proj.Id_project);
						Console.WriteLine(project.Type, project.Id_project);
						if (project != null && type == project.Type && project.Id_project > 0)
						{
							HttpContext.Session.SetInt32("idProject", project.Id_project);

							HttpContext.Session.SetString("type", project.Type);
						}
					}

					ViewData["ok"] = 1;
					return RedirectToAction("Index", "Appointment");

				}

			} 
			}

			ViewData["project"] = new Project_DAO().getById(id);
            Database.Close();

            Database.Connect();

            ViewData["services"] =	new Services_DAO().getAll();

			Database.Close();

			return View();
		}


			public IActionResult Subscription( string? go, string pay_mode, string? end_date, string? price)
		{
			ViewData["ok"] = 0;
			if (Tools.IsInt(price) == false )
			{
				ViewData["ok"] = 500;
				
			}
			int client_id = 0;
			Database.Connect();	

			
				Subscription s = new Subscription_DAO().getSubcriptionByEmailClient(HttpContext.Session.GetString("Client_Email"));

				
			 if(HttpContext.Session.GetString("Client_Email" ) == null)
			{
				return View();
			}
			else if(HttpContext.Session.GetString("Client_Email") == s.Name_client)
				{
				HttpContext.Session.SetInt32("Clients_id", s.Id_client);
				ViewData["ok"] = 1;
				return RedirectToAction("Index", "Project");

			}




			double autreprice = 0;
				DateTime dateTimeOffset = DateTime.Now;
				DateOnly Todateonly = ToDateOnly(dateTimeOffset);
				
				if (go == "expertise" || go == "check" && client_id != null && pay_mode != null && end_date != null && pay_mode != null)
				{
				client_id = (int)HttpContext.Session.GetInt32("Clients_id");

				if (Tools.IsInt(client_id.ToString()) == false)
				{
					ViewData["ok"] = 500;

				}
				string name_client = new Client_DAO().getById(client_id).Email;
				
			
				
				int mois = NombreDeMois(dateTimeOffset, DateTime.Parse(end_date));
					autreprice = 99.05 * mois;

					Console.WriteLine(mois);

					if (Todateonly >= DateOnly.Parse(end_date) || client_id == null || name_client == null)
					{
						ViewData["ok"] = 500;

					}
					else if (Int64.Parse(pay_mode) < 1 && Tools.IsInt(pay_mode) == false)
					{
						ViewData["ok"] = 500;

					}

					else if (autreprice < 49.10)
					{
						ViewData["ok"] = 200;

					}
					else if (mois > 0)
					{
						ViewData["ok"] = 1;
						ViewData["enddate"] = end_date;
						ViewData["paymode"] = pay_mode;
						if (Int64.Parse(pay_mode) > 1)
						{
							autreprice = (99.05 * mois) / Int64.Parse(pay_mode);
							autreprice = Math.Round(autreprice, 2);
							ViewData["pay"] = autreprice;
							ViewData["modePay"] = "/" + pay_mode;
						}
						else
						{
							autreprice = 99.05 * mois;
							autreprice = Math.Round(autreprice, 2);
							ViewData["pay"] = autreprice;

						}
						Console.WriteLine(autreprice);

						if (go == "expertise")
						{

							if (Int64.Parse(pay_mode) > 1)
							{
								autreprice = (99.05 * mois) / Int64.Parse(pay_mode);
								autreprice = Math.Round(autreprice, 2);
								ViewData["pay"] = autreprice + " / " + pay_mode;
							}
							else
							{
								autreprice = 99.05 * mois;
								autreprice = Math.Round(autreprice, 2);
								ViewData["pay"] = autreprice;

							}
							Console.WriteLine(autreprice);

			
						//ToDateOnly(dateTimeOffset);
						///dateTimeOffset = DateTime.Parse(Todateonly);
						Subscription Sub = new Models.Subscription(client_id, pay_mode, name_client, dateTimeOffset, DateTime.Parse(end_date), autreprice);

							new Subscription_DAO().save(Sub);
						HttpContext.Session.SetInt32("Professional_id", client_id);

						return RedirectToAction("Index", "Project");


					}

					Database.Close();

				}
					else
					{
						//ViewData["ok"] = 500;
					}



				}
				else
				{
					if (end_date == null && pay_mode == null)
					{
						ViewData["ok"] = 2;

					}
					//client_id = (int)HttpContext.Session.GetInt32("Clients_id");
					//ViewData["client"] = new Client_DAO().getById(client_id);*/
					return View();
				}

			


			return View();
		}
	
	}
}
