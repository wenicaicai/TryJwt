using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TryTController : ControllerBase
    {
        [HttpGet]
        public ActionResult RunT()
        {
            StringBuilder strList = new StringBuilder();
            SortedList<Person> list = new SortedList<Person>();
            string[] names = new string[]
            {
                "Franscoise",
                "Bill",
                "Li",
                "Sandra",
                "Gunnar",
                "Alok",
                "Hiroyuki",
                "Maria",
                "Alessandro",
                "Raul"
            };
            int[] ages = new int[] { 45, 19, 28, 23, 18, 9, 108, 72, 30, 35 };
            for (int x = 0; x < 10; x++)
            {
                list.AddHead(new Person(names[x], ages[x]));
            }
            foreach (Person p in list)
            {
                strList.Append(p.ToString()+"\n");
            }
            strList.Append("Done with unsorted list");

            list.BubbleSort();

            foreach (Person p in list)
            {
                strList.Append(p.ToString() + "\n");
            }
            strList.Append("Done with sorted list");

            return StatusCode(StatusCodes.Status200OK, strList.ToString());
        }
    }
}