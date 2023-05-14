using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    using Models;
    public class Account : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View("login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAccount(UserAccount userdata)
        {
            try
            {
                using (var dbC = new DBc())
                {
                    if (userdata.roleid == null)
                    {   
                        List<string> getdata = dbC.RoleData.Where(x => x.isDef == 1).Select(x => x.roleid.ToString()).ToList();
                        userdata.roleid = String.Join(",",getdata) ;
                    }
                    if (userdata.kantor == null)
                    {
                        var getdata = dbC.Kantor.FirstOrDefault(x => x.isDef == 1);
                        userdata.kantor = getdata.kantorid.ToString();
                    }
                    if (userdata.layarlist == null)
                    {
                        var getdata = dbC.Layar.FirstOrDefault(x => x.isDef == 1);
                        userdata.layarlist = getdata.layarid.ToString();
                    }
                    dbC.Users.Add(userdata);
                    dbC.SaveChanges();
                    return RedirectToAction("Login","Account");
                }


            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAccount(UserAccount userData)
        {
            //InfoAccount Split 
            using(var dbCont = new DBc())
            {
                var userdata = dbCont.Users.FirstOrDefault(x => x.username == userData.username);
                if(userdata == null)
                {
                    ViewBag.Msg = "Account Not Found";
                    return View("Login");
                }
                else
                {
                    if(userdata.pswd == userData.pswd)
                    {
                        HttpContext.Session.SetString("userid", userdata.id.ToString());
                        return Redirect("HomePage");
                    }
                }
            }
            return RedirectToAction("Login","Account");
        }

        public IActionResult HomePage()
        {
            Dictionary<string,string[]> datalayar = new Dictionary<string, string[]>();
            using (var dbCont = new DBc())
            {
                String userid = HttpContext.Session.GetString("userid");
                var userdata = dbCont.Users.First(x => x.id.ToString() == userid);
                List<string> data = new List<string>();
                foreach(string item in userdata.roleid.Split(','))
                {
                    foreach(Layar lyr in dbCont.Layar.Where(x => x.roleNeed.ToString() == item).ToList<Layar>())
                    {
                        if (lyr == null || lyr.isDef == 1)
                        {
                            continue;
                        }
                        datalayar.Add(lyr.namalayar, lyr.pathcode.Split("/"));
                    }

                }
            }
            ViewBag.DataLayar = datalayar;
             return View("Home");
        }


        public ActionResult AddLayar()
        {
            List<RoleData> rd;
            using (DBc dbcon = new DBc())
            {
                rd = dbcon.RoleData.ToList();
            }
            ViewBag.RD = rd;
            return View("Layar/AddLayar");
        }

        public ActionResult DetailLayar()
        {
            using(DBc dbcon = new DBc())
            {
                ViewBag.layar = dbcon.Layar.ToList();
            }
            return View("Layar/DetailLayar");
        }

        public IActionResult DeleteLayar(int id)
        {
            using(var dbCon = new DBc())
            {
                Layar dtlyr = dbCon.Layar.First<Layar>(x => x.layarid == id);
                dbCon.Layar.Remove(dtlyr);
                dbCon.SaveChanges();
            }
            return RedirectToAction("DetailLayar");
        }

        public ActionResult EditLayar(int id)
        {
            Layar lyr;
            List<RoleData> rd;
            using(DBc dbcon = new DBc())
            {
                lyr = dbcon.Layar.First<Layar>(x => x.layarid == id);
                rd = dbcon.RoleData.ToList();
            }
            if(lyr.isDef == 0)
            {
                lyr.boolisview = false;
            }
            else
            {
                lyr.boolisview = true;
            }
            ViewBag.DataLayar =  lyr;
            ViewBag.RD = rd;
            return View("Layar/EditLayar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessAddLayar(Layar layar)
        {
            if (layar.boolisview == false)
            {
                layar.isDef = 0;    
            }
            else
            {
                layar.isDef = 1;
            }
            using (var dbCont = new DBc())
            {
                dbCont.Layar.Add(layar);
                dbCont.SaveChanges();
                return RedirectToAction("DetailLayar","Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessEditLayar(Layar layar,int id)
        {
            if (layar.boolisview == false)
            {
                layar.isDef = 0;
            }
            else
            {
                layar.isDef = 1;
            }
            using (var dbCont = new DBc())
            {
                var dataedited = dbCont.Layar.First(x => x.layarid.ToString() == id.ToString());
                dataedited.isDef = layar.isDef;
                dataedited.pathcode = layar.pathcode;
                dataedited.namalayar = layar.namalayar;
                dbCont.SaveChanges();
                return RedirectToAction("DetailLayar", "Account");
            }
        }

    }


}
