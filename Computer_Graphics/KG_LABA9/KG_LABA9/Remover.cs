using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KG_LABA9
{
    class Remover
    {
        // проверка параллельности векторов через поиск угла
        private static bool CheckParal(Vector a, Vector b)
        {
            double aa = Math.Sqrt((double)(a.x * a.x + a.y * a.y + a.z * a.z));
            double bb = Math.Sqrt((double)(b.x * b.x + b.y * b.y + b.z * b.z));
            double sk = DNO.ScalarMultiplication(a, b);
            double angle = sk / aa / bb;
            return Math.Abs(angle) < 0.05;
        }

        // проверка лежат ли отрезки на одной прямой и пересекаются
        private static bool IsSegmentsOnLineAndHaveIntersection(Line s1, Line s2)
        {
            Vector v1 = new Vector(s1);
            Vector v2 = new Vector(s2);
            Vector h = new Vector(v1.y, -v1.x);
            Vector v3;
            if (CheckParal(h, v2)) // проверка параллельности
            {
               
                if (!(s1.p1 == s2.p1))
                    v3 = new Vector(s1.p1, s2.p1);
                else if (!(s1.p2 == s2.p1))
                    v3 = new Vector(s1.p2, s2.p1);
                else if (!(s1.p2 == s2.p2))
                    v3 = new Vector(s1.p2, s2.p2);
                else
                    v3 = new Vector(s1.p1, s2.p2);

                if (CheckParal(h, v3))
                {   // проверка принадлежности одной прямой
                    // проверка наличия пересечений
                    if (s1.p1.X == s1.p2.X)
                    { 
                        //vertical segments
                        if (s1.p1.Y <= s2.p2.Y && s1.p1.Y >= s2.p1.Y)
                            return true;
                        if (s1.p2.Y <= s2.p2.Y && s1.p2.Y >= s2.p1.Y)
                            return true;
                    }
                    else
                    {
                        if (s1.p1.X <= s2.p2.X && s1.p1.X >= s2.p1.X)
                            return true;
                        if (s1.p2.X <= s2.p2.X && s1.p2.X >= s2.p1.X)
                            return true;
                    }
                }
            }
            return false;
        }

        // удаления из списка отрезков всех перекрывающихся
        public static void FindOverlappingSegments(ref List<Line> segments)
        {
            for (int i = 0; i < segments.Count(); i++)
            {
                if (!segments[i].empty)
                {
                    for (int j = 0; j < segments.Count(); j++)
                    {
                        if (segments[i].empty)
                            break;
                        if (segments[j].empty || i == j)
                            continue;
                        if (IsSegmentsOnLineAndHaveIntersection(segments[i], segments[j]))
                        {
                            Line s1 = segments[i];
                            Line s2 = segments[j];
                            AddToListSegmentsResultsOfIntersection(ref segments,ref s1, ref s2);
                            segments[i] = s1;
                            segments[j] = s2;
                            

                        }
                    }
                }
            }
            DeleteDoubleAndNullSegments(ref segments);
        }

        // удаление одинаковых отрезков и отрезков, которые отмечены как удаленные
        private static void DeleteDoubleAndNullSegments( ref List<Line> segments)
        {
            List<Line> resSegment = new List<Line>();
            for (int i = 0; i < segments.Count(); i++)
            {
                bool flag = true;
                if (!segments[i].empty)
                {
                    for (int j = 0; j < resSegment.Count() && flag; j++)
                    {
                        if (segments[i].isEqual(resSegment[j]))
                            flag = false;
                    }
                    if (flag)
                        resSegment.Add(segments[i]);
                }
            }
            segments = DNO.CloneList(resSegment);
        }

        //разбиение пересекающихся отрезков на части, добавление "уникальных" отрезков в список, удаление исходных
        private static void AddToListSegmentsResultsOfIntersection(ref List<Line> segments, ref Line s1, ref Line s2)
        {
          

           // if (s1.p1.X == s2.p2.X || s2.p1.X == s1.p2.X)
            //    return;
            Point t = new Point();
            Point end = new Point();
            s1.empty = true;
            s2.empty = true;
            if (s1.isEqual(s2)) // магия
                return;

            if (s1.p1.X == s1.p2.X)
            { 
                //для вертикальных отрезков отдельно
                if (s2.p1.Y < s1.p1.Y)
                {
                    segments.Add(new Line(s2.p1, s1.p1));
                }
                else
                {
                    segments.Add(new Line(s1.p1, s2.p1));
                }
                

                if (s2.p2.Y == s1.p2.Y)
                    return;

                if (s2.p2.Y < s1.p2.Y)
                {
                    t = s2.p2;
                    end = s1.p2;
                }
                else
                {
                    t = s1.p2;
                    end = s2.p2;
                }
                segments.Add(new Line(t, end));
                return;
            }



            if (s2.p1.X < s1.p1.X)
            {
                segments.Add(new Line(s2.p1, s1.p1));
            }
            else if (s2.p1.X > s1.p1.X)
            {
                segments.Add(new Line(s1.p1, s2.p1));
            }

            if (s2.p2.X == s1.p2.X)
                return;
            if (s2.p2.X < s1.p2.X)
            {
                t = s2.p2;
                end = s1.p2;
            }
            else
            {
                t = s1.p2;
                end = s2.p2;
            }
            segments.Add(new Line(t, end));
        }

    }
}
