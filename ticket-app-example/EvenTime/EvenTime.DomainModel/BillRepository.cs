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
    public class BillRepository
    {
        private static BillRepository _instance = null;

        private IList<Bill> _listBills;

        private static ISessionFactory sessionFactory = EvenTime.DomainModel.NHibernateService.CreateSessionFactory();

        public static BillRepository getInstance()
        {
            if (_instance == null)
                _instance = new BillRepository();

            return _instance;
        }

        private void LoadBillsFromDatabase()
        {
            try
            {
                using (var session2 = sessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Bill");
                    session2.Flush();
                    _listBills = upit.List<Bill>();
                }

            }
            catch (Exception e)
            {
               throw new Exception(e.Message);
            }
         }
        

        public int Count()
        {
            LoadBillsFromDatabase();

            return _listBills.Count;
        }

        public void addBill(Bill inBill)
        {
            LoadBillsFromDatabase();

            foreach (Bill bill in _listBills)
                if (bill.Code == inBill.Code)
                    throw new BillAlreadyExists();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inBill);
                    transaction.Commit();
                }
            }

            LoadBillsFromDatabase();
        }

        public Bill getBillByCode(string inBillCode)
        {
            LoadBillsFromDatabase();

            foreach (Bill bill in _listBills)
                if (bill.Code == inBillCode)
                    return bill;

            throw new BillDoesntExist();
        }

        public Bill getBillByIndex(int Index)
        {
            LoadBillsFromDatabase();

            if (0 <= Index && Index < Count())
                return _listBills[Index];
            else
                throw new BillDoesntExist();
        }

        public bool billExists(string inBillCode)
        {
            LoadBillsFromDatabase();

            return getBillByCode(inBillCode) != null;
        }
    }
}