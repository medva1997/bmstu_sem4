using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
namespace KG_LABA9
{
    class Algo
    {
        List<Point> pl1;
        List<Point> pl2;
        Color col;
        Graphics g;


        public Algo(List<Point> pl1, List<Point> pl2, Color col, Graphics g)
        {
            this.col = col;
            this.g = g;
            this.pl1 = pl1;
            this.pl2 = pl2;
            this.pl1.Add(pl1[0]);
            this.pl2.Add(pl2[0]);
        }

        private Point P(double t, Point p1, Point p2)
        {
            Point tmp = new Point();
            tmp.X = p1.X + (int)(Math.Round((p2.X - p1.X) * t));
            tmp.Y = p1.Y + (int)(Math.Round((p2.Y - p1.Y) * t));
            return tmp;
        }
        
        bool VisibleVertex(Point vertex, Point p1, Vector norm)
        {
            Vector v1=new Vector(vertex, p1);
            int mult = DNO.ScalarMultiplication(v1, norm);
            return mult > 0;
        }

        void CutSegment(List<Point> Cut, List<Vector> normVect, List<Point> polynom, ref List<Point> resPolynom)
        {
            int n = Cut.Count() - 1;
            Point F=new Point();
            Point S=new Point();
            Point I=new Point();

            double Dsk, Wsk, t;
           
            for(int i = 0; i < n; i++) 
            {
                int m = polynom.Count();
                for(int j = 0; j < m; j++) 
                {
                    if(j == 0)
                        F = polynom[j];
                    else 
                    {
                        Vector D=new Vector(polynom[j], S); //Вектор нашего отрезка
                        Dsk = DNO.ScalarMultiplication(D, normVect[i]); //показывает угол и с какой стороны угол

                        if(Dsk != 0) 
                        {
                            Vector W=new Vector(S, Cut[i]); //вектор соединяющий  начало отрезка и вершину многоугольника
                            Wsk =DNO.ScalarMultiplication(W, normVect[i]); //видимость для паралельных
                            t = -Wsk / Dsk;
                            if(t >= 0 && t <= 1) {
                                I = P(t, S, polynom[j]);//точка пересечения
                                resPolynom.Add(I);
                            }
                        }
                    }
                    S = polynom[j];

                    if(VisibleVertex(S, Cut[i], normVect[i]))
                        resPolynom.Add(S);
                }

                ///if(scene->stepFlag)
                //    PrintResPolynom(scene, polynom, Qt::white);
                polynom = DNO.CloneList(resPolynom);
                polynom.Add(polynom[0]);
                //DebugPrintPolynom(polynom);
                /*if(scene->stepFlag) {
                    scene->repaintScene();
                    PrintResPolynom(scene, resPolynom, Qt::green);
                    scene->sleepFeature(1000);
                }*/
                resPolynom.Clear();
            }
            //polynom.Add(polynom[0]);
            resPolynom =DNO.CloneList( polynom);
        }

        void PrintResPolynom( List<Line> lines)
        {
            Pen pen = new Pen(col, 2);
            for(int i = 0; i < lines.Count; i++) {
                g.DrawLine(pen,lines[i].p1, lines[i].p2);
                 Pen myPen = new Pen(Color.Red, 3);
                    Font drawFont = new Font("Arial",7);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    //g.DrawString(i.ToString()+" "+lines[i].p1.ToString() + " " + lines[i].p2.ToString(), drawFont, drawBrush, (lines[i].p1.X + lines[i].p2.X) / 2, (lines[i].p1.Y + lines[i].p2.Y) / 2);
                g.Flush();
                System.Threading.Thread.Sleep(10);
            }
        }

       
       

        

            

        public void Start()
        {
            int direction = DNO.IsConvexPolygon(pl1);
            if (direction == 0)
            {
                System.Windows.Forms.MessageBox.Show("Похоже отсекатель не выпуклый");
                return;
            }

            //поизк нормалей
            List<Vector> normVect = new List<Vector>();
            DNO.FindNormVectors(pl1, direction, ref normVect);

            List<Point> resPolynom = new List<Point>();
            CutSegment(pl1, normVect, pl2, ref resPolynom);

            List<Line> lines = new List<Line>(); ;
            GetSegmentsFromVertex(lines, resPolynom);
            Remover.FindOverlappingSegments(ref lines);

            PrintResPolynom(lines);

        }

        // Конвертирование циклического списка вершин в список ребер с упорядоченными
        // отрезками: tSegment сам упорядочит по возрастанию х (и при == х по у)
        void GetSegmentsFromVertex(List<Line> segments, List<Point> resPolynom)
        {
            for(int i = 0; i < resPolynom.Count() - 1; i++) {
                if(!(resPolynom[i]==resPolynom[i+1]))
                    segments.Add(new Line(resPolynom[i],resPolynom[i+1]));
            }
        }
    }
}
