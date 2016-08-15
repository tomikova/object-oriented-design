using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrugaDomacaZadaca_Burza
{
    public static class Factory
    {
        public static IStockExchange CreateStockExchange()
        {
            return new StockExchange();
        }
    }

    public class StockExchange : IStockExchange
    {
        public Dictionary<string, Stock> listStocks = null;
        public Dictionary<string, long> listAvailibleStocks = null;
        public Dictionary<string, Index> listIndices = null;
        public Dictionary<string, Portfolio> listPortfolios = null;

        public StockExchange()
        {
            this.listStocks = new Dictionary<string, Stock>();
            this.listAvailibleStocks = new Dictionary<string, long>();
            this.listIndices = new Dictionary<string, Index>();
            this.listPortfolios = new Dictionary<string, Portfolio>();
        }

        public void ListStock(string inStockName, long inNumberOfShares, decimal inInitialPrice, DateTime inTimeStamp)
        {
            if(StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom već postoji!");
            }
            if (inNumberOfShares <= 0)
            {
                throw new StockExchangeException("Broj dionica ne smije biti manji ili jednak 0!");
            }
            if (inInitialPrice <= 0)
            {
                throw new StockExchangeException("Cijena dionice ne smije biti manja ili jednaka 0!");
            }
            Stock newStock = new Stock(inStockName.ToLower(), inNumberOfShares, inInitialPrice, inTimeStamp);
            listStocks.Add(inStockName.ToLower(), newStock);
            listAvailibleStocks.Add(inStockName.ToLower(), inNumberOfShares);
        }

        public void DelistStock(string inStockName)
        {
            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }

            List<string> indicesToDel = new List<string>();
            List<string> portfoliosToDel = new List<string>();
 
            foreach (KeyValuePair<string, Index> entry in listIndices)
            {
                if (IsStockPartOfIndex(entry.Key, inStockName))
                {
                    listIndices[entry.Key].listIndexStocks.Remove(inStockName.ToLower());
                }
            }
            foreach (KeyValuePair<string, Portfolio> entry in listPortfolios)
            {
                if (IsStockPartOfPortfolio(entry.Key, inStockName))
                {
                    listPortfolios[entry.Key].listPortfolioStocks.Remove(inStockName.ToLower());
                    listPortfolios[entry.Key].listAvailiblePortfolioStocks.Remove(inStockName.ToLower());
                }
            }

            listStocks.Remove(inStockName.ToLower());
            listAvailibleStocks.Remove(inStockName.ToLower());
        }

        public bool StockExists(string inStockName)
        {
            return listStocks.ContainsKey(inStockName.ToLower());
        }

        public int NumberOfStocks()
        {
            return listStocks.Count;
        }

        public void SetStockPrice(string inStockName, DateTime inIimeStamp, decimal inStockValue)
        {
            inStockName = inStockName.ToLower();

            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            if (listStocks[inStockName].listPrices.ContainsKey(inIimeStamp))
            {
                throw new StockExchangeException("Za upisano vrijeme već je definirana cijena dionice");
            }
            if (inStockValue <= 0)
            {
                throw new StockExchangeException("Cijena dionice ne smije biti manja ili jednaka 0!");
            }
            listStocks[inStockName].AddToListPrices(inIimeStamp, inStockValue);
            if (DateTime.Compare(inIimeStamp, listStocks[inStockName].inFirstTimeStamp) < 0)
            {
                listStocks[inStockName].inFirstTimeStamp = inIimeStamp;
                listStocks[inStockName].inInitialPrice = inStockValue;
            }
        }

        public decimal GetStockPrice(string inStockName, DateTime inTimeStamp)
        {
            inStockName = inStockName.ToLower();

            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            if (DateTime.Compare(inTimeStamp,listStocks[inStockName].inFirstTimeStamp) < 0)
            {
                throw new StockExchangeException("Dionica ne postoji za traženo vrijeme");
            }
            DateTime tmp = listStocks[inStockName].inFirstTimeStamp;
            foreach (KeyValuePair<DateTime, decimal> entry in listStocks[inStockName].listPrices)
            {
                if (DateTime.Compare(tmp, entry.Key) <= 0 && DateTime.Compare(entry.Key, inTimeStamp) <= 0)
                {
                    tmp = entry.Key;
                }
            }
            return listStocks[inStockName].listPrices[tmp];
        }

        public decimal GetInitialStockPrice(string inStockName)
        {
            inStockName = inStockName.ToLower();

            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            return listStocks[inStockName].inInitialPrice;
        }

        public decimal GetLastStockPrice(string inStockName)
        {
            inStockName = inStockName.ToLower();

            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            DateTime tmp = listStocks[inStockName].inFirstTimeStamp;
            foreach (KeyValuePair<DateTime, decimal> entry in listStocks[inStockName].listPrices)
            {
                if (DateTime.Compare(tmp, entry.Key) <= 0)
                {
                    tmp = entry.Key;
                }
            }
            return listStocks[inStockName].listPrices[tmp];
        }


        public void CreateIndex(string inIndexName, IndexTypes inIndexType)
        {
            if (IndexExists(inIndexName))
            {
                throw new StockExchangeException("Index s tim imenom već postoji!");
            }
            if (inIndexType != IndexTypes.AVERAGE && inIndexType != IndexTypes.WEIGHTED)
            {
                throw new StockExchangeException("Nedopušteni tip indeksa!");
            }
            Index newIndex = new Index(inIndexName, inIndexType);
            listIndices.Add(inIndexName.ToLower(), newIndex);
        }

        public void AddStockToIndex(string inIndexName, string inStockName)
        {
            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            if (!IndexExists(inIndexName))
            {
                throw new StockExchangeException("Index s tim imenom ne postoji!");
            }
            if(listIndices[inIndexName.ToLower()].listIndexStocks.ContainsKey(inStockName.ToLower()))
            {
                throw new StockExchangeException("Dionica već postoji u indeksu!");
            }
            Stock toAdd = listStocks[inStockName.ToLower()];
            listIndices[inIndexName.ToLower()].listIndexStocks.Add(inStockName.ToLower(), toAdd);
        }

        public void RemoveStockFromIndex(string inIndexName, string inStockName)
        {
            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            if (!IndexExists(inIndexName))
            {
                throw new StockExchangeException("Index s tim imenom ne postoji!");
            }
            if (!IsStockPartOfIndex(inIndexName,inStockName))
            {
                throw new StockExchangeException("Dionica ne postoji u indeksu!");
            }
            listIndices[inIndexName.ToLower()].listIndexStocks.Remove(inStockName.ToLower());
        }

        public bool IsStockPartOfIndex(string inIndexName, string inStockName)
        {
            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            if (!IndexExists(inIndexName))
            {
                throw new StockExchangeException("Index s tim imenom ne postoji!");
            }
            return listIndices[inIndexName.ToLower()].listIndexStocks.ContainsKey(inStockName.ToLower());
        }

        public decimal GetIndexValue(string inIndexName, DateTime inTimeStamp)
        {
            if (!IndexExists(inIndexName))
            {
                throw new StockExchangeException("Index s tim imenom ne postoji!");
            }

            if (listIndices[inIndexName.ToLower()].type == IndexTypes.AVERAGE)
            {
                decimal total = 0;
                long count = 0;
                foreach (KeyValuePair<string, Stock> entry in listIndices[inIndexName.ToLower()].listIndexStocks)
                {
                    total += GetStockPrice(entry.Key, inTimeStamp) * entry.Value.inNumberOfShares;
                    count += entry.Value.inNumberOfShares;
                }
                if (count != 0)
                {
                    return Math.Round(total / count, 3);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                decimal total = 0;
                decimal result = 0;
                foreach (KeyValuePair<string, Stock> entry in listIndices[inIndexName.ToLower()].listIndexStocks)
                {
                    total += GetStockPrice(entry.Key, inTimeStamp) * entry.Value.inNumberOfShares;
                }

                if (total != 0)
                {
                    foreach (KeyValuePair<string, Stock> entry in listIndices[inIndexName.ToLower()].listIndexStocks)
                    {
                        decimal factor = GetStockPrice(entry.Key, inTimeStamp) * entry.Value.inNumberOfShares / total;
                        result += GetStockPrice(entry.Key, inTimeStamp) * factor;
                    }
                    return Math.Round(result, 3);
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool IndexExists(string inIndexName)
        {
            return listIndices.ContainsKey(inIndexName.ToLower());
        }

        public int NumberOfIndices()
        {
            return listIndices.Count;
        }

        public int NumberOfStocksInIndex(string inIndexName)
        {
            if (!IndexExists(inIndexName))
            {
                throw new StockExchangeException("Index s tim imenom ne postoji!");
            }
            return listIndices[inIndexName.ToLower()].listIndexStocks.Count;
        }

        public void CreatePortfolio(string inPortfolioID)
        {
            if (PortfolioExists(inPortfolioID))
            {
                throw new StockExchangeException("Portfolio s tim imenom već postoji!");
            }
            Portfolio newPortfolio = new Portfolio(inPortfolioID);
            listPortfolios.Add(inPortfolioID, newPortfolio);
        }

        public void AddStockToPortfolio(string inPortfolioID, string inStockName, int numberOfShares)
        {
            if (numberOfShares < 0)
            {
                throw new StockExchangeException("Broj dionica ne može biti negativan!");
            }
            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            if (!PortfolioExists(inPortfolioID))
            {
                throw new StockExchangeException("Portfolio s tim imenom ne postoji!");
            }
            if (listAvailibleStocks[inStockName.ToLower()] > 0 && numberOfShares > 0)
            {
                long toAdd = 0;

                if ( listAvailibleStocks[inStockName.ToLower()] < numberOfShares )
                {
                    toAdd = listAvailibleStocks[inStockName.ToLower()];
                }
                else
                {
                    toAdd = numberOfShares;
                }

                if (IsStockPartOfPortfolio(inPortfolioID,inStockName))
                {
                    listPortfolios[inPortfolioID].listAvailiblePortfolioStocks[inStockName.ToLower()] = listPortfolios[inPortfolioID].listAvailiblePortfolioStocks[inStockName.ToLower()] + toAdd;
                    listAvailibleStocks[inStockName.ToLower()] = listAvailibleStocks[inStockName.ToLower()] - toAdd;
                }
                else
                {
                    listPortfolios[inPortfolioID].listPortfolioStocks.Add(inStockName.ToLower(), listStocks[inStockName.ToLower()]);
                    listPortfolios[inPortfolioID].listAvailiblePortfolioStocks.Add(inStockName.ToLower(), toAdd);
                    listAvailibleStocks[inStockName.ToLower()] = listAvailibleStocks[inStockName.ToLower()] - toAdd;
                }
            }
        }

        public void RemoveStockFromPortfolio(string inPortfolioID, string inStockName, int numberOfShares)
        {
            if (numberOfShares < 0)
            {
                throw new StockExchangeException("Broj dionica ne može biti negativan!");
            }
            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            if (!PortfolioExists(inPortfolioID))
            {
                throw new StockExchangeException("Portfolio s tim imenom ne postoji!");
            }
            if (!IsStockPartOfPortfolio(inPortfolioID, inStockName))
            {
                throw new StockExchangeException("Dionica ne postoji u portfelju!");
            }
            if (listPortfolios[inPortfolioID].listAvailiblePortfolioStocks[inStockName.ToLower()] <= numberOfShares)
            {
                listAvailibleStocks[inStockName.ToLower()] = listAvailibleStocks[inStockName.ToLower()] + listPortfolios[inPortfolioID].listAvailiblePortfolioStocks[inStockName.ToLower()];
                listPortfolios[inPortfolioID].listPortfolioStocks.Remove(inStockName.ToLower());
                listPortfolios[inPortfolioID].listAvailiblePortfolioStocks.Remove(inStockName.ToLower());
            }
            else
            {
                listAvailibleStocks[inStockName.ToLower()] = listAvailibleStocks[inStockName.ToLower()] + numberOfShares;
                listPortfolios[inPortfolioID].listAvailiblePortfolioStocks[inStockName.ToLower()] = listPortfolios[inPortfolioID].listAvailiblePortfolioStocks[inStockName.ToLower()] - numberOfShares;
            }
        }

        public void RemoveStockFromPortfolio(string inPortfolioID, string inStockName)
        {
            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            if (!PortfolioExists(inPortfolioID))
            {
                throw new StockExchangeException("Portfolio s tim imenom ne postoji!");
            }
            if (!IsStockPartOfPortfolio(inPortfolioID, inStockName))
            {
                throw new StockExchangeException("Dionica ne postoji u portfelju!");
            }
            listAvailibleStocks[inStockName.ToLower()] = listAvailibleStocks[inStockName.ToLower()] + listPortfolios[inPortfolioID].listAvailiblePortfolioStocks[inStockName.ToLower()];
            listPortfolios[inPortfolioID].listPortfolioStocks.Remove(inStockName.ToLower());
            listPortfolios[inPortfolioID].listAvailiblePortfolioStocks.Remove(inStockName.ToLower());
        }

        public int NumberOfPortfolios()
        {
            return listPortfolios.Count;
        }

        public int NumberOfStocksInPortfolio(string inPortfolioID)
        {
            if (!PortfolioExists(inPortfolioID))
            {
                throw new StockExchangeException("Portfolio s tim imenom ne postoji!");
            }
            return listPortfolios[inPortfolioID].listPortfolioStocks.Count;
        }

        public bool PortfolioExists(string inPortfolioID)
        {
            return listPortfolios.ContainsKey(inPortfolioID);
        }

        public bool IsStockPartOfPortfolio(string inPortfolioID, string inStockName)
        {
            if (!PortfolioExists(inPortfolioID))
            {
                throw new StockExchangeException("Portfolio s tim imenom ne postoji!");
            }
            return listPortfolios[inPortfolioID].listAvailiblePortfolioStocks.ContainsKey(inStockName.ToLower());
        }

        public int NumberOfSharesOfStockInPortfolio(string inPortfolioID, string inStockName)
        {
            if (!StockExists(inStockName))
            {
                throw new StockExchangeException("Dionica s tim imenom ne postoji!");
            }
            if (!PortfolioExists(inPortfolioID))
            {
                throw new StockExchangeException("Portfolio s tim imenom ne postoji!");
            }
            if(IsStockPartOfPortfolio(inPortfolioID, inStockName))
            {
                return (int)listPortfolios[inPortfolioID].listAvailiblePortfolioStocks[inStockName.ToLower()];
            }
            else
            {
                return 0;
            }
        }

        public decimal GetPortfolioValue(string inPortfolioID, DateTime timeStamp)
        {
            if (!PortfolioExists(inPortfolioID))
            {
                throw new StockExchangeException("Portfolio s tim imenom ne postoji!");
            }
            decimal result = 0;
            foreach (KeyValuePair<string, long> entry in listPortfolios[inPortfolioID].listAvailiblePortfolioStocks)
            {
                result += GetStockPrice(entry.Key, timeStamp) * entry.Value;
            }

            return result;
        }

        public decimal GetPortfolioPercentChangeInValueForMonth(string inPortfolioID, int Year, int Month)
        {
            try
            {
                DateTime firstDay = new DateTime(Year, Month, 1, 0, 0, 0, 0);
                DateTime lastDay = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month), 23, 59, 59, 999);
                decimal theBeginning = GetPortfolioValue(inPortfolioID, firstDay);
                decimal theEnd = GetPortfolioValue(inPortfolioID, lastDay);
                decimal result = ((theEnd - theBeginning) / theBeginning) * 100;
                return Math.Round(result, 3);
            }
            catch
            {
                throw new StockExchangeException("Pogrešan datum!");
            }
        }
    }

    public class Stock
    {
        public string inStockName;
        public long inNumberOfShares;
        public decimal inInitialPrice;
        public DateTime inFirstTimeStamp;
        public Dictionary<DateTime, decimal> listPrices;

        public Stock(string _inStockName, long _inNumberOfShares, decimal _inInitialPrice, DateTime _inTimeStamp)
        {
            this.inStockName = _inStockName;
            this.inNumberOfShares = _inNumberOfShares;
            this.inInitialPrice = _inInitialPrice;
            this.inFirstTimeStamp = _inTimeStamp;
            this.listPrices = new Dictionary<DateTime, decimal>();
            this.AddToListPrices(_inTimeStamp, _inInitialPrice);
        }

        public void AddToListPrices(DateTime _timeStamp, decimal _price)
        {
            this.listPrices.Add(_timeStamp, _price);
        }
    }

    public class Index
    {
        public string name;
        public IndexTypes type;
        public Dictionary<string, Stock> listIndexStocks;

        public Index(string inIndexName, IndexTypes inIndexType)
        {
            this.type = inIndexType;
            this.name = inIndexName;
            this.listIndexStocks = new Dictionary<string, Stock>();
        }
    }

    public class Portfolio
    {
        public string ID;
        public Dictionary<string, Stock> listPortfolioStocks;
        public Dictionary<string, long> listAvailiblePortfolioStocks;

        public Portfolio(string portfolioID)
        {
            this.ID = portfolioID;
            this.listPortfolioStocks = new Dictionary<string, Stock>();
            this.listAvailiblePortfolioStocks = new Dictionary<string, long>();
        }
    }
}
