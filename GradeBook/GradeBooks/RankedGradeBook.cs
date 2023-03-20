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

            List<double> gradesRank = GetAverageGradesSorted();

            int gradeRank = 0;
            for (int i = 1; i < gradesRank.Count; i++)
            {
                if (gradesRank[i] > averageGrade)
                {
                    gradeRank = gradesRank.Count - i + 1;
                    break;
                }
            }

            double percentageIndex = (double)gradeRank / gradesRank.Count * 100;

            char letterGrade = percentageIndex switch
            {
                <= 20 => 'A',
                <= 40 => 'B',
                <= 60 => 'C',
                <= 80 => 'D',
                _ => 'F'
            };

            return letterGrade;
        }

        private List<double> GetAverageGradesSorted()
        {
            List<double> averageGrades = Students.Select(student => student.AverageGrade).ToList();
            return averageGrades.OrderBy(el => el).ToList();
        }
    }
}
