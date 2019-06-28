using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cos_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void massiv1()
        {
            double h = Convert.ToDouble(textBox1.Text);
            int n = Convert.ToInt32(2 / h) + 1;
            double hsmall = Convert.ToDouble(textBox2.Text);
            int nbig = Convert.ToInt32(2 / hsmall) + 1;
            double[] x = new double[nbig];
            double[] y = new double[nbig];
            double[] a = new double[n];
            double[] b = new double[n];
            double[] c = new double[n];
            double[] d = new double[n];
            double[] u = new double[n];
            double[] v = new double[n];
            double[] m = new double[n];
            double[] xi = new double[n];
            double[] f = new double[n];
            double[] s = new double[nbig+200];

            x[0] = -1;
            for(int i=1;i<nbig;i++)
            {
                x[i] = x[i - 1] + hsmall;
            }
            for(int i=0;i<nbig;i++)
            {
                y[i] = Math.Exp(-x[i] * x[i]);
            }
            xi[0] = -1;
            for(int i=1;i<n;i++)
            {
                xi[i] = xi[i - 1] + h;
            }
            for(int i=0;i<n;i++)
            {
                f[i] = Math.Exp(-xi[i] * xi[i]);
            }
            a[0] = 1;
            a[n - 1] = 1;
            b[0] = 0;
            c[0] = 0;
            c[n - 1] = 0;
            d[0] = -2 * xi[0] * Math.Exp(-xi[0] * xi[0]);
            d[n - 1] = -2 * xi[n-1] * Math.Exp(-xi[n-1] * xi[n-1]);
            for (int i = 1; i < n - 1; i++)//матрица для m 
            {
                a[i] = 4;
                b[i] = 1;
                c[i] = 1;
                d[i] = 3 * (f[i + 1] - f[i - 1]) / h;
            }

            u[0] = b[0] / a[0] * (-1);
            v[0] = d[0] / a[0];
            for (int i = 1; i < n; i++)//вспомогательные переменные для m 
            {
                u[i] = (-1) * b[i] / (c[i] * u[i - 1] + a[i]);
                v[i] = (d[i] - c[i] * v[i - 1]) / (c[i] * u[i - 1] + a[i]);
            }

            m[n - 1] = v[n - 1];
            for (int i = n - 2; i > -1; i--)//вычисление m 
            {
                m[i] = u[i] * m[i + 1] + v[i];
            }
            int k = 0;
            double t;
            for(int i=0;i<n-1;i++)
            {
                for(int j = 0;j < h / hsmall; j++)
                {
                    t = j * hsmall / h;
                    s[k] = f[i] * fi1(t) + f[i + 1] * fi2(t) + m[i] * h * fi3(t) + m[i + 1] * h * fi4(t);
                    k++;
                }
            }
            if(k<nbig+1)
            {
                t = 1;
                s[nbig-1] = f[n-2] * fi1(t) + f[n-1] * fi2(t) + m[n-2] * h * fi3(t) + m[n-1] * h * fi4(t);
            }
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (int i = 0; i < nbig; i++)//вывод первой функции 
            {
                chart1.Series[0].Points.AddXY(x[i], y[i]);
            } 
            for (int i = 0; i < nbig; i++)//вывод второй функции 
            {
                chart1.Series[1].Points.AddXY(x[i], s[i]);
            }
        }

        private double fi1(double t)
        {
            t = (t - 1) * (t - 1) * (2 * t + 1);
            return t;
        }

        private double fi2(double t)
        {
            t = t * t * (3 - 2 * t);
            return t;
        }

        private double fi3(double t)
        {
            t = t * (1 - t) * (1 - t);
            return t;
        }

        private double fi4(double t)
        {
            t = -t * t * (1 - t);
            return t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            massiv1();
        }
    }
}
