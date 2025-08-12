using Digital_info.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Digital_info.Controllers
{
    public class Admin3060 : Controller
    {
        private IWebHostEnvironment Environment;
        public Admin3060(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult Index(string? go, string? email, string? password)
        {
            Database.Connect();
            ViewData["ok"] = 0;

			ViewData["connexion"] = 0;
            if (go == "connexion")
            {
               if (email != null && password != null)
                {



					if(Tools.IsValiEamil(email) == false)
                    {
						ViewData["ok"] = 502;

					}
					else if (Tools.validateMDP(password) == false)//verrification des valeurs du mot de passe 
					{
						ViewData["ok"] = 500;

                    }
                    else
                    {
						Admin c = new Admin_DAO().login(email, password);

						if (c == null)
						{
							ViewData["connexion"] = 1;


							return RedirectToAction("Index", "Home");
						}
						else
						{

							HttpContext.Session.SetInt32("Admin_Id", c.Id_admin);
							HttpContext.Session.SetString("Admin_Email", c.Email);


							return RedirectToAction("Dashboard", "Admin3060");
						}
					}
				
                }
            }
            ViewData["inscription"] = 0;

			ViewData["Admins"] = new Admin_DAO().getAll();
            Database.Close();
            return View();

        }


        public IActionResult Logout()
        {

            HttpContext.Session.Clear();



            return RedirectToAction("Index", "Home");
        }

		public IActionResult Dashboard()
		{
			return View();
		}
        public IActionResult Client(string insert, int id=0,int idtodelete = 0)
        {
            ViewData["listClient"] = 1;
            Database.Connect();

            if (idtodelete > 0)
            {
                new Client_DAO().delete(idtodelete);
            }

            ViewData["clients"] = new Client_DAO().getAll();

            ViewData["insert"] = 1;
            if (insert == "go")
            {
                return View();
            }

            Database.Close();
            return View();
        }
        public IActionResult ClientEdit(string go, string firstname, string lastname, string address,
			string city, string phone, int age, string email, int id =0)
        {
			ViewData["ok"] = 0;
			ViewData["email"] = 0;
			ViewData["connexion"] = 0;
			ViewData["inscription"] = 0;
            Database.Connect();
            if (id==0)
            {
                ViewData["ok"] = 500;
            }
            else if(go == "client")
            {
                if (Tools.IsInt(id.ToString()) == true && Tools.IsString(firstname) == true && Tools.IsString(lastname) == true &&
                    Tools.IsString(city) == true && Tools.verifTel(phone) == true && Tools.IsInt(age.ToString()) == true &&  Tools.IsValiEamil(email) == true)
                {
                    Client cli = new Client( id,  firstname,  lastname,  address, city,  phone,  age,  email);
                    new Client_DAO().save(cli);
					ViewData["ok"] = 1;

				}
				else
                {
                    ViewData["ok"] = 500;
                }

			}
			ViewData["update"] = new Client_DAO().getById(id);

			Database.Close();
            return View();
        }
        public IActionResult ClientAdd(string go, string firstname, string lastname, string address,
         string city, string phone, int age, string email,string password, string? passwordComf)
        {
            ViewData["ok"] = 0;
            ViewData["email"] = 0;
            ViewData["connexion"] = 0;
            ViewData["inscription"] = 0;
            Database.Connect();
             if (go == "client")
            {
                if ( Tools.IsString(firstname) == true && Tools.IsString(lastname) == true &&
                    Tools.IsString(city) == true && Tools.verifTel(phone) == true && Tools.IsInt(age.ToString()) == true && Tools.IsValiEamil(email) == true)
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                    Client c = new Client(firstname, lastname, address, city, phone, age, email, passwordHash);
                    new Client_DAO().save(c);
                    ViewData["ok"] = 1;

                }
                else if (firstname == null && lastname == null && email == null && password == null)
                {
                    ViewData["ok"] = 500;
                }
                else if ( password != passwordComf )
                {
					ViewData["inscription"] = 300;

				}

			} 

            Database.Close();
            return View();
        }
    }
}
