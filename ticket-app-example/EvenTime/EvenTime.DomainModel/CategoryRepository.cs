using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using System.Text.RegularExpressions;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace EvenTime.DomainModel
{
    public class CategoryRepository
    {
        private static CategoryRepository _instance = null;

        private IList<Category> _listCategories;

        private static ISessionFactory sessionFactory = EvenTime.DomainModel.NHibernateService.CreateSessionFactory();

        public static CategoryRepository getInstance()
        {
            if (_instance == null)
                _instance = new CategoryRepository();

            return _instance;
        }

        private void LoadCategoriesFromDatabase()
        {
            try
            {
                using (var session2 = sessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Category");
                    session2.Flush();
                    _listCategories = upit.List<Category>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int Count()
        {
            LoadCategoriesFromDatabase();

            return _listCategories.Count;
        }

        public void addCategory(Category inCategory)
        {
            LoadCategoriesFromDatabase();

            foreach (Category cat in _listCategories)
                if (cat.CategoryName == inCategory.CategoryName)
                    throw new CategoryAlreadyExists();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inCategory);
                    transaction.Commit();
                }
            }

            LoadCategoriesFromDatabase();
        }

        public Category getCategoryByName(string inCategoryName)
        {
            LoadCategoriesFromDatabase();

            foreach (Category cat in _listCategories)
                if (cat.CategoryName == inCategoryName)
                    return cat;

            throw new CategoryDoesntExist();
        }

        public Category getCategoryByIndex(int Index)
        {
            LoadCategoriesFromDatabase();

            if (0 <= Index && Index < Count())
                return _listCategories[Index];
            else
                throw new CategoryDoesntExist();
        }

        public bool categoryExists(string inCategoryName)
        {
            LoadCategoriesFromDatabase();

            return getCategoryByName(inCategoryName) != null;
        }
    }
}
