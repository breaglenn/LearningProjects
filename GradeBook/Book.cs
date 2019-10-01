using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{

    public delegate void GradeAddedDelegate(object sender,EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }
        public string Name
        {
            get;
            set;
        }

    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public class DiskBook : IBook
    {
        private List<double> grades;

        public string Name => throw new NotImplementedException();

        public DiskBook(string name) : base(name)
        {
            grades = new List<double>();
            this.Name = name;

        }

        public event GradeAddedDelegate GradeAdded;

        public void AddGrade(double grade)
        {
            var writer = File.AppendText($"{Name}.txt");
            writer.WriteLine(grade);
        }

        public Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
        public string AddGrade(char letter, int x)
        {
            return "";
        }

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                default:
                    AddGrade(0);
                    break;

            }
        }


        public abstract class Book : NamedObject, IBook
        {
            public Book(string name) : base(name)
            {
               
            }

            public abstract event GradeAddedDelegate GradeAdded;

            public abstract void AddGrade(double grade)
            {
                if (grade <= 100 && grade >= 0)
                {
                    grades.Add(grade);
                    GradeAdded?.Invoke(this, new EventArgs());
                }
                else
                {
                    throw new ArgumentException($"Invalid {nameof(grade)}");
                    //Console.WriteLine("Invalid value");
                }
            }

            public abstract Statistics GetStatistics();


                                 
            
        }

            public class InMemoryBook : Book, IBook
            {

                public InMemoryBook(string name) : base(name)
                {
                    grades = new List<double>();
                    this.Name = name;

                }
                public string AddGrade(char letter, int x)
                {
                    return "";
                }

                public void AddGrade(char letter)
                {
                    switch (letter)
                    {
                        case 'A':
                            AddGrade(90);
                            break;
                        case 'B':
                            AddGrade(80);
                            break;
                        case 'C':
                            AddGrade(70);
                            break;
                        case 'D':
                            AddGrade(60);
                            break;
                        default:
                            AddGrade(0);
                            break;

                    }
                }


                public override void AddGrade(double grade)
                {
                    if (grade <= 100 && grade >= 0)
                    {
                        grades.Add(grade);
                        if (GradeAdded != null)
                        {
                            GradeAdded(this, new EventArgs());
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid {nameof(grade)}");
                        //Console.WriteLine("Invalid value");
                    }

                }

                public override event GradeAddedDelegate GradeAdded;

                public override Statistics GetStatistics()
                {
                    var result = new Statistics();
                   
                    using (var reader = File.OpenText($"{Name}.txt"))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var number = double.Parse(line);
                        result.Add(number);
                        line = reader.ReadLine;
                    }
                }
                    return result;                                       

                }

                public void ShowStatistics()
                {
                    var result = 0.0;
                    var highGrade = double.MinValue;
                    var lowGrade = double.MaxValue;

                    foreach (var number in grades)
                    {
                        lowGrade = Math.Min(number, lowGrade);
                        highGrade = Math.Max(number, highGrade);
                        result += number;
                    }

                    result /= grades.Count;
                }

                private List<double> grades;

                public string Name
                {
                    get;
                    private set;

                }
            }

        const string category = "Science";

        

     

    }
}
    
