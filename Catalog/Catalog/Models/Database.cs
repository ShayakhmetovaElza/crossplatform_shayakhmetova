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
        List<Level> levels { get; set; }
        public Database(List<Pattern> patterns_1, List<Category> categories_1, List<Level> levels_1)
        {
            this.patterns = patterns_1;
            this.categories = categories_1;
            this.levels = levels_1;
        }

        //Выводит по Id название выкройки
        public string GetNamePattern(int id)
        {
            var list_patterns = patterns.FirstOrDefault(p => p.Id == id);
            if (list_patterns == null) return null;
            return list_patterns.Name;
        }

        //Выводит список выкроек по уровню сложности
        //Например, вывести все выкройки по сложности "Новичок"
        //Выводится название выкройки и его цена
        public List<(string, int)> GetPatternsFromLevel(string name_level)
        {
            var list_patterns = patterns.Where(p => p.IdLevel == GetIdLevel(name_level));
            if (list_patterns == null) return null;
            return list_patterns.Select(d => (d.Name, d.Price)).ToList();
        }

        //По названию уровня сложности выводим его Id
        public int GetIdLevel(string name_level)
        {
            var level = levels.FirstOrDefault(p => p.NameLevel == name_level);
            if (level == null) return -1;
            return level.Id;
        }

        //По Id уровня сложности выкройки выводим его название
        public string GetLevel(int id)
        {
            var level = levels.FirstOrDefault(p => p.Id == id);
            if (level == null) return null;
            return level.NameLevel;
        }

        //Выводит список всех выкроек
        //Выводится название выкройки, его категория и цена
        public List<(string, string, string, int)> GetPatterns()
        {
            if (patterns == null) return null;
            var res = patterns.Select(d => (d.Name, GetCategory(d.IdCategory),GetLevel(d.IdLevel), d.Price)).ToList();
            return res;
        }

        //Выводит список выкроек по категории
        //Например, вывести все выкройки по категории "Платья"
        //Выводится название выкройки, уровень сложности и цена
        public List<(string, string, int)> GetPatternsFromCategory(string name_category)
        {
            var list_patterns = patterns.Where(p => p.IdCategory == GetIdCategory(name_category));
            if (list_patterns == null) return null;
            return list_patterns.Select(d => (d.Name, GetLevel(d.IdLevel), d.Price)).ToList();
        }

        //Выводит список выкроек по категории
        //Например, вывести все выкройки по категории "Платья"
        //Выводится название выкройки, уровень сложности и цена
        public int GetIdCategory(string name_category)
        {
            var category = categories.FirstOrDefault(p => p.NameCategory == name_category);
            if (category == null) return -1;
            return category.Id;
        }

        //Выводит название категории по Id
        public string GetCategory(int id)
        {
            var category = categories.FirstOrDefault(p => p.Id == id);
            if (category == null) return null;
            return category.NameCategory;
        }


        //Меняет категорию выкройки.
        //Например, у нас в категории "Новинки" должны появится новые выкройки
        //а старые мы должны перенести в другие категории.
        //Но делает это для одной выкройки.
        public void ChangeCategoryPattern(int id_pattern, string name_category)
        {
            var pattern = patterns.FirstOrDefault(p => p.Id == id_pattern);

            if (pattern != null) 
                patterns.FirstOrDefault(p => p.Id == id_pattern).IdCategory = GetIdCategory(name_category); 
        }

        //Меняет категорию у нескольких выкроек.
        //Например, нам нужно удалить категорию и расширить список категорий.
        //Поэтому перенесем пока во временную категорию "Temp"
        public void ChangeCategoryPatterns(string name_category)
        {
            var pattern = patterns.Where(p => p.IdCategory == GetIdCategory(name_category));
            var id_p = pattern.Select(p => p.Id);
            foreach(int id_pp in id_p)
            {
                ChangeCategoryPattern(id_pp, "Temp");
            }
        }

        //Перед тем, как удалить категорию
        //переносим выкройки из данной категории во временную "Temp"
        public void DeletCategory(string name_category)
        {
            ChangeCategoryPatterns(name_category);
            var c = categories.FirstOrDefault(p => p.NameCategory == name_category);
            categories.Remove(c);
        }

        //Выводит все категории
        public List<(int,string)> GetCategory()
        {
            return categories.Select(p => (p.Id, p.NameCategory)).ToList(); 
        }
        //Добавляет новую категорию 
        public void AddCategory(string s)
        {
            categories.Add(new Category(s));
        }
    }
}




