using GradeBook.Enums;
using System;
using System.Collections.Generic;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool weighted) : base(name, weighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
          

            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            var averages = new List<double>();

            foreach (var student in Students)
            {
                averages.Add(student.AverageGrade);
            }

            averages.Sort();
            var twentyPercentBucketSize = averages.Count * 20 / 100;
            var rankIndex = 0;

            for (int i = 0; i < averages.Count; i++)
            {
                if (averageGrade <= averages[i])
                {
                    rankIndex = i;
                    break;
                }
            }

            if (rankIndex == 0)
                return 'F';
            if (rankIndex <= twentyPercentBucketSize)
                return 'D';
            if (rankIndex <= twentyPercentBucketSize * 2)
                return 'C';
            if (rankIndex <= twentyPercentBucketSize * 3)
                return 'B';
            if (rankIndex <= twentyPercentBucketSize * 4)
                return 'A';

            return 'F';
        }


        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }


        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }

    }


}
