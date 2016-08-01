using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BaiChuaIMovie
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomList CL=new CustomList();
            string chon;
            int maxID=0;

            do
            {
                Console.Clear();
                Console.WriteLine("PLEASE AN OPTION:");
                Console.WriteLine("1. Insert new Movie");
                Console.WriteLine("2. View list of Movie");
                Console.WriteLine("3. Sort Movie by Average List");
                Console.WriteLine("4. Delete a movie");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Enter a number:");
                chon = Console.ReadLine();
                switch (chon)
                {
                    case "1":
                        Movie mv = new Movie();
                        Console.Write("Name=");
                        mv.Name = Console.ReadLine();
                        Console.Write("PublishDate=");
                        DateTime tam;
                        while (!DateTime.TryParse(Console.ReadLine(), out tam))
                        {
                            Console.Write("Reenter PublishDate=");
                        }
                        mv.PublishDate = tam;
                        Console.Write("Director=");
                        mv.Director = Console.ReadLine();
                        Console.Write("Language=");
                        mv.Subtitle = Console.ReadLine();
                        for (int i = 0; i < 3; i++)
                        {
                            Console.Write("Rate[{0}]=", i);
                            mv[i] = Double.Parse(Console.ReadLine());
                        }
                        mv.ID = ++maxID;
                        CL.Add(mv);
                        Console.WriteLine("Done!");
                        break;
                    case "2":
                        foreach (IMovie m in CL)
                        {
                            ((Movie)m).Calculate();
                            m.Display();
                        }
                        break;
                    case "3":
                        CL.Sort(new CustomSort());
                        foreach (IMovie m in CL)
                            m.Display();
                        break;
                    case "4":
                        Console.Write("Enter Movie'name to delete:");
                        string nameDel = Console.ReadLine();
                        foreach (IMovie m in CL)
                            if (m.Name == nameDel)
                            {
                                CL.Remove(m);
                                daxoa=true;
                                break;
                            }
                        
                        if (daxoa)
                            Console.WriteLine("Done!");
                        else
                            Console.WriteLine("Not found!");
                        break;
                    case "5":
                        Console.WriteLine("Bye bye!");
                        break;
                    default:
                        Console.WriteLine("invalid option!");
                        break;
                }
                Console.ReadLine();
            } while (chon != "5");
        }
    }

    interface IMovie
    {
        int ID { set; get; }
        string Name { set; get; }
        DateTime PublishDate { set; get; }
        string Director { set; get; }
        string Subtitle { set; get; }
        float AverageRate { get; }

        void Display();
    }

    class Movie : IMovie
    {
        #region IMovie Members
        int _ID;
        string _Name;
        DateTime _PublishDate;
        string _Director;
        string _Subtitle;
        float _AverageRate;
        double[] RateList = new double[3];
        public double this[int i]
        {
            get { return RateList[i]; }
            set { RateList[i] = value; }
        }

        public void Calculate()
        {
            _AverageRate = (float)RateList.Average();
        }

        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public DateTime PublishDate
        {
            get
            {
                return _PublishDate;
            }
            set
            {
                _PublishDate = value;
            }
        }

        public string Director
        {
            get
            {
                return _Director;
            }
            set
            {
                _Director = value;
            }
        }

        public string Subtitle
        {
            get
            {
                return _Subtitle;
            }
            set
            {
                _Subtitle = value;
            }
        }

        public float AverageRate
        {
            get { return _AverageRate; }
        }

        public void Display()
        {
            Console.WriteLine("\nName={0}, PublishDate={1}, Director={2}, Language={3}, AverageRate={4}", Name, PublishDate, Director, Subtitle, AverageRate);
        }

        #endregion
    }

    class CustomList : IEnumerable
    {
        ArrayList MovieList = new ArrayList();

        public void Add(IMovie movie)
        {
            MovieList.Add(movie);
        }

        public void Remove(IMovie movie)
        {
            MovieList.Remove(movie);
        }

        public void Sort(IComparer comp)
        {
            MovieList.Sort(comp);
        }


        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            foreach (IMovie m in MovieList)
                yield return m;
        }

        #endregion

    }

    class CustomSort : IComparer
    {
        #region IComparer Members

        public int Compare(object x, object y)
        {
            IMovie A = x as IMovie;
            IMovie B = y as IMovie;
            return A.AverageRate.CompareTo(B.AverageRate);
        }

        #endregion
    }


}

