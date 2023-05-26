using learncodefirstapproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Diagnostics;

namespace learncodefirstapproach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GoalContext _context;
        public HomeController(GoalContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<GoalItem> goalItems = _context.GoalItem.ToList();
            return View(goalItems);
        }

        public IActionResult Create(int id,string name,string desc)
        {
                var goal = new GoalItem {Name = name, IsComplete = false, Description = desc };
                _context.GoalItem.Add(goal);   
            
            _context.SaveChanges();
            return Ok();
        }
        public IActionResult Read()
        {
            var goalItems = _context.GoalItem.ToJson()  ; // Replace 'GoalItems' with the name of your goal items DbSet in the data context

            return Json(goalItems);
        }
    /*    public IQueryable<GoalItem> FilterName(IQueryable<GoalItem> Goals , string filterOperator_1 = "", string filterValue_1 = "", string filterOperator_2="", string filterValue_2="")
        {
            if (filterOperator_1.ToString() != "")
            {
                if (filterOperator_1 == "contains")
                {
                    Goals = Goals.Where(p => p.Name.Contains(filterValue_1));
                }
                if (filterOperator_1 == "endswith")
                {
                    Goals = Goals.Where(p => p.Name.EndsWith(filterValue_1));
                }
                if (filterOperator_1 == "startswith")
                {
                    Goals = Goals.Where(p => p.Name.StartsWith(filterValue_1));
                }
                if (filterOperator_1 == "eq")
                {
                    Goals = Goals.Where(p => p.IsComplete == Convert.ToBoolean(filterValue_1));
                }

            }
            if (filterOperator_2.ToString() != "")
            {
                if (filterOperator_2.ToString() == "contains")
                {
                    Goals = Goals.Where(p => p.Name.Contains(filterValue_2));
                }
                if (filterOperator_2.ToString() == "endswith")
                {
                    Goals = Goals.Where(p => p.Name.EndsWith(filterValue_2));
                }
                if (filterOperator_2.ToString() == "startswith")
                {
                    Goals = Goals.Where(p => p.Name.StartsWith(filterValue_2));
                }
                if (filterOperator_2.ToString() == "eq")
                {
                    Goals = Goals.Where(p => p.IsComplete == Convert.ToBoolean(filterValue_2));
                }
            }
            return Goals;
        }
     public IQueryable<GoalItem> FilterDesc(IQueryable<GoalItem> Goals,string filterOperator_1 = "", string filterValue_1 = "",  string filterOperator_2 = "", string filterValue_2 = "")
        {
            if (filterOperator_1.ToString() != "")
            {
                if (filterOperator_1 == "contains")
                {
                    Goals = Goals.Where(p => p.Description.Contains(filterValue_1));
                }
                if (filterOperator_1 == "endswith")
                {
                    Goals = Goals.Where(p => p.Description.EndsWith(filterValue_1));
                }
                if (filterOperator_1 == "startswith")
                {
                    Goals = Goals.Where(p => p.Description.StartsWith(filterValue_1));
                }
                if (filterOperator_1 == "eq")
                {
                    Goals = Goals.Where(p => p.IsComplete == Convert.ToBoolean(filterValue_1));
                }

            }
            if (filterOperator_2.ToString() != "")
            {
                if (filterOperator_2 == "contains")
                {
                    Goals = Goals.Where(p => p.Description.Contains(filterValue_2));
                }
                if (filterOperator_2 == "endswith")
                {
                    Goals = Goals.Where(p => p.Description.EndsWith(filterValue_2));
                }
                if (filterOperator_2 == "startswith")
                {
                    Goals = Goals.Where(p => p.Description.StartsWith(filterValue_2));
                }
                if (filterOperator_2 == "eq")
                {
                    Goals = Goals.Where(p => p.IsComplete == Convert.ToBoolean(filterValue_2));
                }
            }
            return Goals;
        }
   */

            public IActionResult List()
        {
            var Goals = _context.GoalItem;
            return View(Goals);
        }
        public IActionResult Edit(long Id) {

            ViewBag.id=Id;
            var Goal = from Goals in _context.GoalItem where Goals.Id == Id select Goals;
            string name="";
            string description="";
            bool isComplete=false;
            foreach(var i in Goal)
            {
                name = i.Name;
                description= i.Description;
                isComplete = i.IsComplete;
            }
            ViewBag.name = name;
            ViewBag.description = description;
            ViewBag.isComplete = isComplete;
            return View();
        }
        [HttpPost]
        public IActionResult Edit([Bind(Prefix = "models")] IEnumerable<GoalItem> Goal)
        {
            foreach (var item in Goal)
            { 
                _context.GoalItem.Update(item);
                _context.SaveChanges();
            }    
            return Ok(Goal);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var Goal = _context.GoalItem.Where(s => s.Id == Id).FirstOrDefault();
          
                _context.GoalItem.Remove(Goal);
                _context.SaveChanges();
            
            var Goals = _context.GoalItem;
            return Ok(Goal);
        }
        [HttpPost]
        public  string Search(string input)
        {
            if (input != null)
            {
                var Goals = _context.GoalItem
                    .Where(s => s.Id.ToString().Contains(input) ||
                    s.Name.Contains(input) ||
                    s.Description.Contains(input))
                    .Select(s => new { s.Id, s.Name, s.Description,s.IsComplete })
                    .ToJson();
                Console.WriteLine(Goals);
                return Goals;
            }
            else
            {
                var Goalquery = from t in _context.GoalItem select t;
                var Goals = Goalquery.ToJson();
                Console.WriteLine(Goals);
                return Goals;
            }
            
        }
        public IActionResult Privacy()
        {
            var Goal = new GoalItem { Name="kp",IsComplete=false,Description="good guy"};
            _context.GoalItem.Add(Goal);
            _context.SaveChanges();
            
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}