using Digital_info.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace Digital_info.Controllers
{
	
	public class ProfessionalController : Controller
	{
		private IWebHostEnvironment Environment;
		public ProfessionalController(IWebHostEnvironment _environment)
		{
			Environment = _environment;
		}
	


			
		

		public IActionResult Index()
		{
			Database.Connect();
	


			ViewData["experience"] = new Experience_DAO().getBestExpVM();
			Database.Connect();
			return View();
		}
		public IActionResult Services(int id,IFormFile images, string submit, string service, string service_details, IFormFile brochure,
			int page = 1,int idtodelete =0)
		{
			ViewData["page"] = 0;
			ViewData["ok"] = 0;
			ViewData["nbrOfPages"] = 0;
			ViewData["pageId"] = 0;
			/*	if (HttpContext.Session.GetInt32("Professional_id") == 0 || HttpContext.Session.GetString("Professional_Email") == null || HttpContext.Session.GetString("firstname") == null || HttpContext.Session.GetString("lastname") == null)
				{
					return RedirectToAction("Index", "Home");


				}*/
			id = 1;
			Database.Connect();
			
			int professional_id = 0;
			if (HttpContext.Session.GetInt32("Professional_id") > 0)
			{
				professional_id = (int)HttpContext.Session.GetInt32("Professional_id");



				if (submit == "service")
				{
					if (service != null && service_details != null && brochure != null && images != null)
					{
						string extension = System.IO.Path.GetExtension(brochure.FileName);
						string newname = Guid.NewGuid().ToString() + extension;
						string filePath = Path.Combine(this.Environment.WebRootPath, "assets/brochures", newname);
						Stream fileStream = new FileStream(filePath, FileMode.Create);
						brochure.CopyToAsync(fileStream);
						string brochures = "brochures/" + newname;

						string extensionIMG = System.IO.Path.GetExtension(images.FileName);
						string newnameIMG = Guid.NewGuid().ToString() + extensionIMG;
						string filePathIMG = Path.Combine(this.Environment.WebRootPath, "assets/images/resource/", newnameIMG);
						Stream fileStreamIMG = new FileStream(filePathIMG, FileMode.Create);
						images.CopyToAsync(fileStreamIMG);
						string image = "images/resource/" + newnameIMG;
						if (Tools.IsString(image) == false || Tools.IsString(brochures) == false)
						{
							ViewData["ok"] = 500;
						}
						else if (Tools.IsString(service) == false)
						{
							ViewData["ok"] = 500;
						}
						else
						{
							Services serv = new Services(professional_id, service, service_details, brochures, image);
							new Services_DAO().save(serv);
							return RedirectToAction("Services", "Professional");
						}
					}

				}
				int nbrElementsByPage = 12;


				//List<ProfessionalViewModel> ads = new Experience_DAO().getBestByNBR(dep);

				int countAll = new Services_DAO().GetCountAll();

				int dep = (page * nbrElementsByPage) - nbrElementsByPage;
				if (dep ==0)
				{
					dep = 12;
					var services = new Services_DAO().getIdProfessionalVM(professional_id, dep);
					ViewData["servicesAll"] = services;
					ViewData["services"] = new Services_DAO().getIdProfessionalVM(professional_id, dep); ;

				}
				else
				{
					var services = new Services_DAO().getIdProfessionalVM(professional_id, dep);
					ViewData["servicesAll"] = services;

				}
				//string safeLimit = dep + "," + nbrElementsByPage;
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

				ViewData["pageId"] = id;
				ViewData["servicespro"] = new Professional_DAO().getById(professional_id);

			}
			ViewData["professionals"] = new Professional_DAO().getAll();
			if (idtodelete > 0)
			{
				new Services_DAO().delete(idtodelete);
				return RedirectToAction("Services", "Professional");

			}
			Database.Connect();	
			return View();
		}
		public IActionResult EditServices(string go, IFormFile images, string submit, string service, string service_details, IFormFile brochure, int id = 0)
		{
			ViewData["ok"] = 0;
			ViewData["update"] = 0;
			Database.Connect();
			if (submit == "updateService")
			{
			
			
				int id_professional = (int)HttpContext.Session.GetInt32("Professional_id");

				var brochures = "";
				if (service != null && service_details != null && images != null)
				{
					if (brochure == null)
					{
						Services sevices = new Services_DAO().getById(id);
						  brochures = sevices.Brochure;
					}
					else
					{
						string extension = System.IO.Path.GetExtension(brochure.FileName);
						string newname = Guid.NewGuid().ToString() + extension;
						string filePath = Path.Combine(this.Environment.WebRootPath, "assets/brochures", newname);
						Stream fileStream = new FileStream(filePath, FileMode.Create);
						brochure.CopyToAsync(fileStream);
						brochures = "brochures/" + newname;
					}
					
					



						string extensionIMG = System.IO.Path.GetExtension(images.FileName);
						string newnameIMG = Guid.NewGuid().ToString() + extensionIMG;
						string filePathIMG = Path.Combine(this.Environment.WebRootPath, "assets/images/resource/", newnameIMG);
						Stream fileStreamIMG = new FileStream(filePathIMG, FileMode.Create);
						images.CopyToAsync(fileStreamIMG);
						string image = "images/resource/" + newnameIMG;
						if (Tools.IsString(image) == false || Tools.IsString(brochures) == false)
						{
							ViewData["ok"] = 500;
						}
						else if (Tools.IsString(service) == false || Tools.IsInt(id.ToString()) == false)
						{
							ViewData["ok"] = 500;
						}
						else
						{
							Services serv = new Services(id, id_professional, service, service_details, brochures, image);
							new Services_DAO().save(serv);
							ViewData["update"] = 1;

							return RedirectToAction("Services", "Professional");
						}
					

				}

			}

			if (id > 0)
			{
				ViewData["service"] = new Services_DAO().getById(id);

			}
			Database.Connect();
			return View();
		}
			public IActionResult Management(string go, IFormFile avatar, int id_professional, int siret, string firstname, string lastname, 
				string project_qualification, string project_datail, string availability, double price, string success, string email,
			 IFormFile images, string submit, string service, string service_details, IFormFile brochure, int id=0
			)
		{
			/*if (HttpContext.Session.GetInt32("Professional_id") == 0 || HttpContext.Session.GetString("Professional_Email") == null || HttpContext.Session.GetString("firstname") == null || HttpContext.Session.GetString("lastname") == null)
			{
				return RedirectToAction("Index", "Home");


			}*/
			ViewData["ok"] = 0;
			ViewData["update"] = 0;
			Database.Connect();

			id_professional = (int)HttpContext.Session.GetInt32("Professional_id");

			if (go == "updateProfil" && id_professional >0     && firstname != null && lastname != null && siret != null && 
				project_qualification != null && project_datail != null && email != null)
			{
				if (availability == "Checked")
				{
					availability = "yes";
				}
				string extension = System.IO.Path.GetExtension(avatar.FileName);
				string newname = Guid.NewGuid().ToString() + extension;
				string filePath = Path.Combine(this.Environment.WebRootPath, "assets/images/resource/", newname);
				Stream fileStream = new FileStream(filePath, FileMode.Create);
				avatar.CopyToAsync(fileStream);
				string avatars = "images/resource/" + newname;
				if (Tools.IsString(avatars) == false )
				{
					ViewData["ok"] = 500;
				}else if ( Tools.IsInt(siret.ToString()) == false)
				{
					ViewData["ok"] = 500;

				}

				else if (Tools.IsInt(id_professional.ToString()) == false || Tools.IsString(service) == false || 
					Tools.IsString(firstname) == false || Tools.IsString(lastname) == false  || Tools.IsString(project_qualification) == false)
				{
					ViewData["ok"] = 500;
				}
				else
				{
					Professional professional = new Professional(id_professional, siret, firstname, lastname, project_qualification, 
						project_datail, availability, price, success, email, avatars);

					new Professional_DAO().save(professional);
					ViewData["update"] = 1;
					return RedirectToAction("Services", "Professional");
				}
			}









			var Professional = new Professional_DAO().getById(id_professional);




			ViewData["professional"] = Professional;
			
			Database.Connect();
			return View();
		}
		
	}
}
