using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Models
{
    public class Database
    {
        List<Pattern> patterns { get; set; }
        List<Category> categories { get; set; }
        public Database()
        {
            categories = new List<Category>
            {
                new Category("New"),
                new Category("Dresses"),
                new Category("Skirts"),
                new Category("Blouse"),
            };
            patterns = new List<Pattern>
            {
                new Pattern("Dresses A", 150, "Easy", new List<int>() { 0,1 }),
                new Pattern("Dresses B", 200, "Average", new List<int>() { 1 }),
                new Pattern("Dresses C", 200, "Advanced", new List<int>() { 1 }),
                new Pattern("Skirts A", 100, "Easy", new List<int>() { 0, 2 }),
                new Pattern("Skirts B", 50, "Average", new List<int>() { 2 }),
                new Pattern("Blouse A", 200, "Advanced", new List<int>() { 0, 3 }),
            };
        }

        //Получить название категории по id
        public string getNameCategory(int id_category)
        {
            var c = categories.FirstOrDefault(i => i.Id == id_category);
            if (c == null) return null;
            return c.NameCategory;
        }
        //по id выкройки возвращаем список названий категорий
        public List<string> getCategories(int id_pattern)
        {
            var list_patterns = patterns.FirstOrDefault(i => i.Id == id_pattern);
            if (list_patterns == null) return null;
            List<string> names_category = new List<string>();
            foreach (int id in list_patterns.CategoryIds)
                names_category.Add(getNameCategory(id));
            return names_category;
        }
        //Выводит список всех выкроек
        //Выводится название выкройки, названия категорий, название уровня сложности и цена
        public List<(string, List<string>, string, int)> GetPatterns()
        {
            if (patterns == null) return null;
            var res = patterns.Select(d => (d.Name, getCategories(d.Id), d.NameLevel, d.Price)).ToList();
            return res;
        }

        //Получить id по названию категории
        public int getIdCategory(string name_category)
        {
            var c = categories.FirstOrDefault(i => i.NameCategory == name_category);
            if (c == null) return -1;
            return c.Id;
        }
        public List<(string, string, int)> GetPatternsFromCategory(string name_category)
        {
           /// int id_category = getIdCategory(name_category);
            var list_patterns = patterns.Where(p => p.CategoryIds == p.CategoryIds.Where(i => i == getIdCategory(name_category)));
            if (list_patterns == null) return null;
            return list_patterns.Select(d => (d.Name, d.NameLevel, d.Price)).ToList();

        }

        //Выводит список выкроек по уровню сложности
        //Например, вывести все выкройки по сложности "Новичок"
        //Выводится название выкройки и его цена
        public List<(string, List<string>, int)> GetPatternsFromLevel(string name_level)
        {
            var list_patterns = patterns.Where(p => p.Name == (name_level));
            if (list_patterns == null) return null;
            return list_patterns.Select(d => (d.Name, getCategories(d.Id),d.Price)).ToList();
        }
        public void ChangePrice(int id_pattern, int price)
        {
            var p = patterns.FirstOrDefault(id => id.Id == id_pattern);
            if (p != null) p.Price = price;
        }
        public void DeleteCategory(int id_pattern, string name_category)
        {
            var p = patterns.FirstOrDefault(id => id.Id == id_pattern);
            if (p != null)
                p.CategoryIds.Remove(getIdCategory(name_category));
        }
     /*   public void AddCategoriesInPattern(int id_pattern, int id_category)
        {
            var c = categories.FirstOrDefault(id => id.Id == id_category);
            if (c != null)
                patterns.FirstOrDefault(p => p.Id == id_pattern).CategoryIds.Add(id_category);
        }*/
        public void PostPatten(Pattern p) 
        {
            patterns.Add(p);
        }
    }
}
