using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog.Models;

namespace Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatternsController : ControllerBase
    {
        private readonly TodoContext _context;

        public PatternsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Patterns
        //По GET: api/Patterns должен вывести вы выкройки
        [HttpGet]
        public string Get()
        {
            string s;

            s = "api/Patterns/All - выводит все выкройки \n\n";

            s += "api/Patterns/GetPatternsL/name_lavel - выводит выкройки с уровнем сложности name_lavel\n";
            s += "Например, уровень сложности 'Easy'\n  api/Patterns/GetPatternsL/Easy \n\n";

            s += "api/Patterns/GetPatternsС/name_category - выводит выкройки с категорией name_category\n";
            s += "Например, категория 'Dresses'\n  api/Patterns/GetPatternsC/Dresses \n\n";

            s += "api/Patterns/ChangeCategory/id_pattern/name_category - меняет категорию у выкройки\n";
            s += "Например, нужно поменять выркойку с id = 1 и категорией 'New' на категорию 'Dresses'\n  api/Patterns/ChangeCategory/1/Dresses \n\n";

            s += "api/Patterns/DeleteCategory/name_category - удаляет категорию по названию, но при этом у выкроек меняется категория на 'Temp'\n";
            s += "Например, удалить категорию 'Skirts'\n  api/Patterns/DeleteCategory/Skirts \n\n";

            s += "api/Patterns - выводит информацию\n";

            return s;
        }

        //Выводит все выкройки
        [HttpGet("All")]
      /*  [HttpGet]
        [Route("All")]*/
        public string GetAll()
        {
            var res = Startup.database.GetPatterns();
            if (res == null) return null;
            string s="";
            foreach(var result in res)
            {
                s=s+"Pattern:"+result.Item1 + ",  Category:" + result.Item2 + ",  Level:"+result.Item3+",  Price:"+result.Item4+"\n";
            }
            return s;
        }

        // GET: api/Patterns/GetPatternsL/Easy
        // По GET: api/Patterns/GetPatternsL/Easy должен вывести все выкройки с уровнем сложности "Easy"
        [HttpGet("GetPatternsL/{name_lavel}")]
        public string GetPatternsL(string name_lavel)
        {
            var res = Startup.database.GetPatternsFromLevel(name_lavel);
            if (res == null) return null;
            string s = "";
            var p = GetAll();
            foreach (var result in res)
            {
                s = s + "Pattern:" + result.Item1 + ",  Price:" + result.Item2 + "\n";
            }
            return p+"\n\n"+"NameLavel: "+name_lavel+"\n"+s;
        }

        // GET: api/Patterns/GetPatternsC/Dresses
        // По команде GET: api/Patterns/GetPatternsC/Dresses должен вывести выкройки с категорией "Dresses"
        [HttpGet("GetPatternsC/{name_category}")]
        public string GetPatternsC(string name_category)
        {
            var res = Startup.database.GetPatternsFromCategory(name_category);
            if (res == null) return null;
            string s = "";
            var p = GetAll();
            foreach (var result in res)
            {
                s = s + "Pattern:" + result.Item1 + ",  Level:" + result.Item2 + ",  Price:" + result.Item3 + "\n";
            }
            return p + "\n\n" + "NameCategory: " + name_category + "\n" + s;
        }

        // По команде GET: api/Patterns/ChangeCategory/id_pattern/name_category 
        //должен поменять у выкройки категорию
        [HttpGet("ChangeCategory/{id_pattern}/{name_category}")]
        public string ChangeCategory(int id_pattern, string name_category)
        {
            var p = GetAll();
            Startup.database.ChangeCategoryPattern(id_pattern, name_category);
            var s = GetAll();
            string pattern = Startup.database.GetNamePattern(id_pattern);
            return p + "\n\n" + "Id: " + id_pattern + ",  Pattern: " + pattern + "\n" + s;
        }

        // По команде GET: api/Patterns/DeleteCategory/name_category должен сначала перенести все выкройки
        //по данной категории во времененную папку, затем удалить категорию
        [HttpGet("DeleteCategory/{name_category}")]
        public string DeleteCategory(string name_category)
        {
            var p = GetAll();
            Startup.database.DeletCategory(name_category);
            var s = GetAll();
            return p + "\n\n" + "Category: " + name_category + "\n" + s;
        }

        // PUT: api/Patterns/id_pattern/name_category
        [HttpPut("{id_pattern}/{name_category}")]
        public string ChangeCategory1(int id_pattern, string name_category)
        {
            var p = GetAll();
            Startup.database.ChangeCategoryPattern(id_pattern, name_category);
            var s = GetAll();
            string pattern = Startup.database.GetNamePattern(id_pattern);
            return p + "\n\n" + "Id: " + id_pattern + ",  Pattern: " + pattern + "\n" + s;
        }

        // POST: api/Patterns
        //Добавляет новую категорию и выводит список категорий с их Id
        [HttpPost("{new_category}")]
       public string AddCategory(string new_category)
        {
            Startup.database.AddCategory(new_category);
            var res = Startup.database.GetCategory();
            string s = "";
            foreach (var result in res)
            {
                s = s + "Id:" + result.Item1 + ",  Category:" + result.Item2 + "\n";
            }
            return s;
        } 

        // DELETE: api/Patterns/name_category
        // По DELETE: api/Patterns/name_category должен сначала перенести все выкройки
        //по данной категории во времененную папку, затем удалить категорию
        [HttpDelete("{name_category}")]
        public string DeleteCategory1(string name_category)
        {
            var p = GetAll();
            Startup.database.DeletCategory(name_category);
            var s = GetAll();
            return p + "\n\n" + "Category: " + name_category + "\n" + s;
        }

        private bool PatternsExists(long id)
        {
            return _context.pattern1.Any(e => e.Id == id);
        }
    }
}
