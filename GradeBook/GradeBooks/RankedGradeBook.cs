using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires a minimum of 5 students");
            }

            List<double> averageGrades = GetAverageGrades();
            double percentageRanking = GetValuePercentageRanking(averageGrade, averageGrades);

            return percentageRanking switch
            {
                <= 20 => 'A',
                <= 40 => 'B',
                <= 60 => 'C',
                <= 80 => 'D',
                _ => 'F'
            };
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires a minimum of 5 students");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires a minimum of 5 students");
                return;
            }

            base.CalculateStudentStatistics(name);
        }

        private List<double> GetAverageGrades() => Students.Select(student => student.AverageGrade).ToList();

        private static double GetValuePercentageRanking<T>(T value, List<T> values) where T : IComparable<T>
        {
            List<T> orderedValues = values.OrderBy(el => el).ToList();

            double GetPercentageFromIndex(int index) => (double)(values.Count - index) / values.Count * 100;

            for (int i = 0; i < orderedValues.Count - 1; i++)
            {
                if (orderedValues[i + 1].CompareTo(value) > 0)
                {
                    return GetPercentageFromIndex(i);
                }
            }

            return 0;
        }
    }
}
