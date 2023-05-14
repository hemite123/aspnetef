using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    using Models;
    public class Role : Controller
    {
        
        public ActionResult AddRole()
        {
            return View("~/Views/Account/Role/AddRole.cshtml");
        }

        public ActionResult ProcessAddRole(RoleData rd)
        {
            if (rd.isDefe)
            {
                rd.isDef = 1;
            }
            else
            {
                rd.isDef = 0;
            }
            using (var dbc = new DBc())
            {
                dbc.RoleData.Add(rd);
                dbc.SaveChanges();
            }
            return RedirectToAction("HomePage","Account");
        }

        public ActionResult EditRole(int id)
        {
            RoleData rd;
            using (DBc dbcon = new DBc())
            {
                rd = dbcon.RoleData.First<RoleData>(x => x.roleid == id);
            }
            ViewBag.DR = rd;
            return View("~/Views/Account/Role/EditRole.cshtml");
        }

        public ActionResult DetailRole()
        {
            using (DBc dbcon = new DBc())
            {
                ViewBag.RD = dbcon.RoleData.ToList();
            }
            return View("~/Views/Account/Role/DetailRole.cshtml");
        }
        public ActionResult ProcessDeleteRole(int id)
        {
            using (var dbCon = new DBc())
            {
                //get user data with this role first 
                List<UserAccount> uac = dbCon.Users.ToList();
                foreach(UserAccount user in uac)
                {
                    List<string> split = user.roleid.Split(",").ToList();
                    if(split.Count > 1)
                    {
                        split.Remove(id.ToString());
                        user.roleid = String.Join(",", split);
                    }
                    else
                    {
                        List<string> getdata = dbCon.RoleData.Where(x => x.isDef == 1).Select(x => x.roleid.ToString()).ToList();
                        user.roleid = String.Join(",", getdata);
                    }
                    
                }
                
                RoleData dtlyr = dbCon.RoleData.First<RoleData>(x => x.roleid == id);
                dbCon.RoleData.Remove(dtlyr);
                dbCon.SaveChanges();
            }
            return RedirectToAction("DetailRole","Role");
        }

        public ActionResult ProcessEditRole(RoleData rd,int id)
        {
            using (var dbCon = new DBc())
            { 
                RoleData dtlyr = dbCon.RoleData.First<RoleData>(x => x.roleid == id);
                if (rd.isDefe)
                {
                    dtlyr.isDef = 1;
                }
                else
                {
                    dtlyr.isDef = 0;
                }
                dtlyr.namaRole = rd.namaRole;
                dbCon.SaveChanges();
            }
            return RedirectToAction("DetailRole", "Role");
        }

    }
}
